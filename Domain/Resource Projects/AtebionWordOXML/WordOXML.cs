using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Xml;
using System.Drawing;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;



namespace Atebion.Word.OpenXML
{
    public class WordOXML
    {
        /// <summary>
        /// Remove hidden text from a word document (docx)
        /// </summary>
        /// <param name="docName">MS Word file (*.docx)</param>
        public void DeleteHiddenText(string docName)
        {
            // Given a document name, delete all the hidden text.
            const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(docName, true))
            {
                // Manage namespaces to perform XPath queries.
                NameTable nt = new NameTable();
                XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
                nsManager.AddNamespace("w", wordmlNamespace);

                // Get the document part from the package.
                // Load the XML in the document part into an XmlDocument instance.
                XmlDocument xdoc = new XmlDocument(nt);
                xdoc.Load(wdDoc.MainDocumentPart.GetStream());
                XmlNodeList hiddenNodes = xdoc.SelectNodes("//w:vanish", nsManager);
                foreach (System.Xml.XmlNode hiddenNode in hiddenNodes)
                {
                    XmlNode topNode = hiddenNode.ParentNode.ParentNode;
                    XmlNode topParentNode = topNode.ParentNode;
                    topParentNode.RemoveChild(topNode);
                    if (!(topParentNode.HasChildNodes))
                    {
                        topParentNode.ParentNode.RemoveChild(topParentNode);
                    }
                }

                // Save the document XML back to its document part.
                xdoc.Save(wdDoc.MainDocumentPart.GetStream(FileMode.Create, FileAccess.Write));
            }
        }

        /// <summary>
        ///  Filter with a RegEx should work to remove invalid characters
        ///  Reference: http://www.west-wind.com/weblog/posts/2012/Jan/02/XmlWriter-and-lower-ASCII-characters
        /// </summary>
        /// <param name="DirtyString"></param>
        /// <returns></returns>
        private string RemoveInvalidCharacters(string DirtyString)
        {
            string cleanedString = Regex.Replace(DirtyString, @"[\u0000-\u0008]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000B]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000B]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000C]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000E-\u001F]", "");

            return cleanedString;
        }

        
        /// <summary>
        /// Used for generating a DOCX report
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="headerCaptions"></param>
        /// <param name="pathFile"></param>
        /// <param name="DocType"></param>
        /// <param name="PropName"></param>
        public void writeDocx(DataTable dt, string headerCaptions, string pathFile, string DocType, string PropName)
        {
             using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
          {

            string[] Captions = headerCaptions.Split('|');
            int headerCount = Captions.Length;

            // Add a new main document part. 
            MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
            //Create DOM tree for simple document. 
            mainPart.Document = new Document();
            Body body = new Body();

            // >> Addded 09.21.2013
            SectionProperties sectionProperties = new SectionProperties();

            PageSize pageSize = new PageSize()

            {

                Width = (UInt32Value)15840U,

                Height = (UInt32Value)12240U,

                Orient = PageOrientationValues.Landscape

            };

            PageMargin pageMargin = new PageMargin()

            {

                Top = 1440,

                Right = (UInt32Value)1440U,

                Bottom = 1440,

                Left = (UInt32Value)1440U,

                Header = (UInt32Value)720U,

                Footer = (UInt32Value)720U,

                Gutter = (UInt32Value)0U

            };

            Columns columns = new Columns() { Space = "720" };

            DocGrid docGrid = new DocGrid() { LinePitch = 360 };

            sectionProperties.Append(pageSize, pageMargin, columns, docGrid);

            body.Append(sectionProperties);
            //<< End Added 09.21.2013


            Table table = new Table();
            TableProperties tblPr = new TableProperties();
            TableBorders tblBorders = new TableBorders();
            tblBorders.TopBorder = new TopBorder();
            tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
            tblBorders.BottomBorder = new BottomBorder();
            tblBorders.BottomBorder.Val =new EnumValue<BorderValues>(  BorderValues.Single);
            tblBorders.LeftBorder = new LeftBorder();
            tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
            tblBorders.RightBorder = new RightBorder();
            tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
            tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
            tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
            tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
            tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
            tblPr.Append(tblBorders);
            table.Append(tblPr);
            TableRow tr;
            TableCell tc;
            //first row - title
            tr = new TableRow();
            tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", PropName, "    Document Name: ", DocType)))));
            TableCellProperties tcp=new TableCellProperties();
            GridSpan gridSpan=new GridSpan();
            gridSpan.Val=headerCount;
            tcp.Append(gridSpan);
            tc.Append(tcp);
            tr.Append(tc);
            table.Append(tr);
            //second row 
            tr = new TableRow();
            // Generate Header
            for (int i = 0; i < Captions.Length; i++)
            {
                string s = Captions[i];
                tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text(s))));
                tr.Append(tc);
            }
            table.Append(tr);

            // Insert Data
            foreach (DataRow row in dt.Rows)
            {
                tr = new TableRow();
                for (int field = 0; field < dt.Columns.Count; field++)
                {
                    tc = new TableCell();
                    //tc.Append(new Paragraph(new Run(new Text(row[field].ToString())))); // Removed 09.25.2013
                    string cleanValue = RemoveInvalidCharacters(row[field].ToString()); // Added 09.25.2013
                    tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                    tr.Append(tc);
                }

                table.Append(tr);

            }
            
            //appending table to body
            body.Append(table);
            // and body to the document
            mainPart.Document.Append(body);
            // Save changes to the main document part. 
            mainPart.Document.Save();
          }
      }


        /// <summary>
        /// Used for generating a DOCX report
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="headerCaptions"></param>
        /// <param name="pathFile"></param>
        /// <param name="DocType"></param>
        /// <param name="PropName"></param>
        public void writeDocx2(DataTable dt, string headerCaptions, string pathFile, string DocType, string PropName, string pathHTML, string columnWidth)
        {
            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
            {

                string[] Captions = headerCaptions.Split('|');
                string[] ColumnWidth = columnWidth.Split('|'); // Added 4/23/2016
                int headerCount = Captions.Length;

                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create DOM tree for simple document. 
                mainPart.Document = new Document();
                Body body = new Body();

                // >> Addded 09.21.2013
                SectionProperties sectionProperties = new SectionProperties();

                PageSize pageSize = new PageSize()

                {

                    Width = (UInt32Value)15840U,

                    Height = (UInt32Value)12240U,

                    Orient = PageOrientationValues.Landscape

                };

                PageMargin pageMargin = new PageMargin()

                {

                    Top = 1440,

                    Right = (UInt32Value)1440U,

                    Bottom = 1440,

                    Left = (UInt32Value)1440U,

                    Header = (UInt32Value)720U,

                    Footer = (UInt32Value)720U,

                    Gutter = (UInt32Value)0U

                };

                Columns columns = new Columns() { Space = "720" };

                DocGrid docGrid = new DocGrid() { LinePitch = 360 };

                sectionProperties.Append(pageSize, pageMargin, columns, docGrid);

                body.Append(sectionProperties);
                //<< End Added 09.21.2013


                Table table = new Table();
                TableProperties tblPr = new TableProperties();
                TableBorders tblBorders = new TableBorders();
                tblBorders.TopBorder = new TopBorder();
                tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.BottomBorder = new BottomBorder();
                tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.LeftBorder = new LeftBorder();
                tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.RightBorder = new RightBorder();
                tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                tblPr.Append(tblBorders);

                table.Append(tblPr);

                TableRow tr;
                TableCell tc;
                //first row - title
                tr = new TableRow();
                tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", PropName, "    Document Name: ", DocType)))));
                TableCellProperties tcp = new TableCellProperties();
                //GridSpan gridSpan = new GridSpan();
                //gridSpan.Val = headerCount;
                //tcp.Append(gridSpan);
                //tc.Append(tcp);
                //tr.Append(tc);
                //table.Append(tr);
                ////second row 
                //tr = new TableRow();

                int yWidth = 0;
                // Generate Header
                for (int i = 1; i < Captions.Length; i++)
                {

                    if (i < ColumnWidth.Length)
                    {
                        yWidth = Convert.ToInt32(ColumnWidth[i]) * 1000; // Added 04/23/2016
                    }

                    //if(Captions[i] == "Text")
                    //{
                    //    yWidth = 100;
                    //}
                    tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                    tcp = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = yWidth.ToString() }); // Added 04/23/2016
                    tc.Append(tcp); // Added 04/23/2016

                    string s = Captions[i];
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text(s))));
                    tr.Append(tc);
                }
                table.Append(tr);

                // Insert Data
                foreach (DataRow row in dt.Rows)
                {
                    tr = new TableRow();
                    for (int field = 0; field < dt.Columns.Count; field++)
                    {
                        tc = new TableCell();
                     //   tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                        if (dt.Columns[field].ColumnName != "Text")
                        {
                            if (dt.Columns[field].ColumnName != "UID") // UID is required to know which HTML file to insert
                            {
                                //tc.Append(new Paragraph(new Run(new Text(row[field].ToString())))); // Removed 0.9.25.2013
                                string cleanValue = RemoveInvalidCharacters(row[field].ToString()); // Added 09.25.2013
                                tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                                tr.Append(tc);
                            }
                        }
                        else
                        {
                            string altChunkId = string.Concat("seg", row["UID"].ToString());

                            // Create alternative format import part.
                            AlternativeFormatImportPart formatImportPart =
                               mainPart.AddAlternativeFormatImportPart(
                                  AlternativeFormatImportPartType.Html, altChunkId);

                            string pathHTMLFile = string.Concat(pathHTML, @"\", row["UID"].ToString(), ".html");

                            if (File.Exists(pathHTMLFile))
                            {

                                using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                                {
                                    formatImportPart.FeedData(fileStream);
                                }

                                // formatImportPart.FeedData(ms);
                                AltChunk altChunk = new AltChunk();
                                altChunk.Id = altChunkId;

                                Paragraph paragraph = new Paragraph();
                                Run run_paragraph = new Run();

                                run_paragraph.Append(altChunk);
                                paragraph.Append(run_paragraph);
                                tc.RemoveAllChildren<Paragraph>(); // Added 09.17.2016
                                tc.Append(paragraph);
                                tr.Append(tc);


                            }
                            else
                            {
                                tc.Append(new Paragraph(new Run(new Text("File Not Found: " + pathHTMLFile)))); 
                                tr.Append(tc);
                            }


                        }
                    }

                    table.Append(tr);

                }

                

                //appending table to body
                body.Append(table);
                // and body to the document
                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();
            }
        }


        public void writeDocx3(DataTable dt, string headerCaptions, string pathFile, string DocType, string PropName, string pathHTML, string segPathHTML, bool shadedBkgrdSec)
        {
            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
            {

                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;

                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create DOM tree for simple document. 
                mainPart.Document = new Document();
                Body body = new Body();

                // >> Addded 09.21.2013
                SectionProperties sectionProperties = new SectionProperties();

                PageSize pageSize = new PageSize()

                {

                    Width = (UInt32Value)15840U,

                    Height = (UInt32Value)12240U,

                    Orient = PageOrientationValues.Landscape

                };

                PageMargin pageMargin = new PageMargin()

                {

                    Top = 1440,

                    Right = (UInt32Value)1440U,

                    Bottom = 1440,

                    Left = (UInt32Value)1440U,

                    Header = (UInt32Value)720U,

                    Footer = (UInt32Value)720U,

                    Gutter = (UInt32Value)0U

                };

                Columns columns = new Columns() { Space = "720" };

                DocGrid docGrid = new DocGrid() { LinePitch = 360 };

                sectionProperties.Append(pageSize, pageMargin, columns, docGrid);

                body.Append(sectionProperties);
                //<< End Added 09.21.2013


                Table table = new Table();
                TableProperties tblPr = new TableProperties();
                TableBorders tblBorders = new TableBorders();
                tblBorders.TopBorder = new TopBorder();
                tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.BottomBorder = new BottomBorder();
                tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.LeftBorder = new LeftBorder();
                tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.RightBorder = new RightBorder();
                tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                tblPr.Append(tblBorders);
                table.Append(tblPr);
                TableRow tr;
                TableCell tc;
                //first row - title
                tr = new TableRow();
                tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", PropName, "    Document Name: ", DocType)))));
                TableCellProperties tcp = new TableCellProperties();
                GridSpan gridSpan = new GridSpan();
                gridSpan.Val = headerCount;
                tcp.Append(gridSpan);
                tc.Append(tcp);
                tr.Append(tc);
                table.Append(tr);
                //second row 
                tr = new TableRow();
                // Generate Header
                for (int i = 0; i < Captions.Length; i++)
                {
                    string s = Captions[i];
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text(s))));
                    tr.Append(tc);
                }
                table.Append(tr);

                int x = 0;
                // Insert Data
                foreach (DataRow row in dt.Rows)
                {
                    tr = new TableRow();
                    for (int field = 0; field < dt.Columns.Count; field++)
                    {
                        tc = new TableCell();
                        if (dt.Columns[field].ColumnName != "Text")
                        {
                            if (dt.Columns[field].ColumnName != "xUID") // UID is required to know which HTML file to insert
                            {
                                string sUID = row["xUID"].ToString(); // Added 5.10.2015
                               
                                string cleanValue = RemoveInvalidCharacters(row[field].ToString()); 
                                tc.Append(new Paragraph(new Run(new Text(cleanValue))));
                                if (shadedBkgrdSec) // Added 5.10.2015
                                {
                                    if (sUID.IndexOf('_') == -1) // Added 5.10.2015
                                        tc.Append(new TableCellProperties(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "E4E8EE" })); // Added 5.10.2015
                                }

                                tr.Append(tc);
                            }
                        }
                        else
                        {
                            x++;
                            string sUID = row["xUID"].ToString();
                            string altChunkId = string.Empty;
                            //if (sUID.IndexOf('_') > -1)
                            //    altChunkId = string.Concat("sent", row["xUID"].ToString());
                            //else
                            //    altChunkId = string.Concat("seg", row["xUID"].ToString());

                            altChunkId = string.Concat ("txt", x.ToString());

                            // Create alternative format import part.
                            AlternativeFormatImportPart formatImportPart =
                               mainPart.AddAlternativeFormatImportPart(
                                  AlternativeFormatImportPartType.Html, altChunkId);

                            string pathHTMLFile = string.Empty;
                           // string sUID = row["xUID"].ToString();
                            if (sUID.IndexOf('_') > -1)
                                pathHTMLFile = string.Concat(pathHTML, @"\", row["xUID"].ToString(), ".html"); // Sentence HTML
                            else
                                pathHTMLFile = string.Concat(segPathHTML, @"\", row["xUID"].ToString(), ".html"); // Segment HTML 

                            if (File.Exists(pathHTMLFile))
                            {

                                using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                                {
                                    formatImportPart.FeedData(fileStream);
                                }
                         

                                // formatImportPart.FeedData(ms);
                                AltChunk altChunk = new AltChunk();
                                altChunk.Id = altChunkId;

                                Paragraph paragraph = new Paragraph();
                                Run run_paragraph = new Run();                 

                                run_paragraph.Append(altChunk);
                                paragraph.Append(run_paragraph);

                                if (shadedBkgrdSec) // Added 5.10.2015
                                {
                                    if (sUID.IndexOf('_') == -1) // Segment
                                        paragraph.Append(new ParagraphProperties(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "E4E8EE" })); // Added 5.10.2015
                                }

                                tc.Append(paragraph);
                                if (shadedBkgrdSec) // Added 5.10.2015
                                {
                                    if (sUID.IndexOf('_') == -1) // Segment
                                    {
                                      //  tc.Append(new ParagraphProperties(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "E4E8EE" })); // Added 5.10.2015
                                        tc.Append(new TableCellProperties(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "E4E8EE" })); // Added 5.10.2015 
                                        

                                    }
                                }

                                
                                tr.Append(tc);

                                                 


                            }
                            else
                            {
                                tc.Append(new Paragraph(new Run(new Text("File Not Found: " + pathHTMLFile))));
                                tr.Append(tc);
                            }

                            
                        }
                    }

                    table.Append(tr);

                }

                //appending table to body
                body.Append(table);
                // and body to the document
                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();
            }
        }


        private System.Drawing.Color setQualityBackColor(Double dblQuality) // Added 05.31.2014
        {
            System.Drawing.Color color = new System.Drawing.Color();
            color = System.Drawing.Color.Red;

            if (dblQuality < 30)
                color = System.Drawing.Color.Red;
            else if (dblQuality < 40 && dblQuality >= 29.99)
                color = System.Drawing.Color.Salmon;
            else if (dblQuality < 50 && dblQuality >= 39.99)
                color = System.Drawing.Color.Gold;
            else if (dblQuality < 60 && dblQuality >= 49.99)
                color = System.Drawing.Color.Yellow;
            else if (dblQuality < 70 && dblQuality >= 59.99)
                color = System.Drawing.Color.GreenYellow;
            else if (dblQuality < 80 && dblQuality >= 69.99)
                color = System.Drawing.Color.LightGreen;
            else if (dblQuality < 90 && dblQuality >= 79.99)
                color = System.Drawing.Color.Green;
            else if (dblQuality >= 89.99)
                color = System.Drawing.Color.DarkGreen;

            return color;
        }

        private bool IsDouble(string value)
        {
            try
            {
                double x = Convert.ToDouble(value);
            }
            catch
            {
                return false;
            }

            return true;

        }

        public void writeQCDocx(DataTable dt, string headerCaptions, string pathFile, string DocName, string ProjName, string pathHTML, string pathNotesHTML, string columnWidth, string chartsPicsPath, string RankAvg, string LSColor, string CColor, string PVColor, string AColor, string DTColor, string RAvg, string LSTotal, string CWTotal, string PVTotal, string ATotal, string DTTotal)
        {
            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
            {
                string[] Captions = headerCaptions.Split('|');
                string[] ColumnWidth = columnWidth.Split('|'); // Added 4/23/2016
                int headerCount = Captions.Length;

                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create DOM tree for simple document. 
                mainPart.Document = new Document();
                Body body = new Body();

                // >> Addded 09.21.2013
                SectionProperties sectionProperties = new SectionProperties();

                PageSize pageSize = new PageSize()

                {

                    Width = (UInt32Value)15840U,

                    Height = (UInt32Value)12240U,

                    Orient = PageOrientationValues.Landscape

                };

                PageMargin pageMargin = new PageMargin()

                {

                    Top = 1440,

                    Right = (UInt32Value)1440U,

                    Bottom = 1440,

                    Left = (UInt32Value)1440U,

                    Header = (UInt32Value)720U,

                    Footer = (UInt32Value)720U,

                    Gutter = (UInt32Value)0U

                };

                Columns columns = new Columns() { Space = "720" };

                DocGrid docGrid = new DocGrid() { LinePitch = 360 };

                sectionProperties.Append(pageSize, pageMargin, columns, docGrid);

                body.Append(sectionProperties);
                //<< End Added 09.21.2013

                Paragraph paragraph;
                Run run_paragraph;

                paragraph = new Paragraph();
                run_paragraph = new Run();

                paragraph = body.AppendChild(new Paragraph());
                run_paragraph = paragraph.AppendChild(new Run());

                RunProperties runProperties = run_paragraph.AppendChild(new RunProperties());
                FontSize fontSize = new FontSize();
                fontSize.Val = "30";
                runProperties.AppendChild(fontSize);

                string title = string.Concat("QC Report - ", DocName);
                run_paragraph.AppendChild(new Text(title));



                Table table = new Table();
                TableProperties tblPr = new TableProperties();
                TableBorders tblBorders = new TableBorders();
                tblBorders.TopBorder = new TopBorder();
                tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.None);
                tblBorders.BottomBorder = new BottomBorder();
                tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.None);
                tblBorders.LeftBorder = new LeftBorder();
                tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.None);
                tblBorders.RightBorder = new RightBorder();
                tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.None);
                tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                tblBorders.InsideHorizontalBorder.Val = BorderValues.None;
                tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                tblBorders.InsideVerticalBorder.Val = BorderValues.None;
                tblPr.Append(tblBorders);

                TableCellSpacing tblCellSpacing = new TableCellSpacing() { Width = "114" };
                tblPr.Append(tblCellSpacing);

                TableCellWidth tblCellWidth = new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" };

                tblPr.Append(tblCellWidth);

                table.Append(tblPr);

                TableRow tr;
                TableCell tc;
                //first row - title
                tr = new TableRow();

                // Rank


                tc = new TableCell();

                TableCellProperties tcpShaded = GetCellShaddedProperty("Black");

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Rank Avg. ", Environment.NewLine, Environment.NewLine, RankAvg)))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Readability Rectangle
                string Gradelevel = string.Empty;
                string readColor = "Pink";

                double dRAvg = 0;
                if (RAvg.Trim() != string.Empty)
                {
                    dRAvg = Convert.ToDouble(RAvg);
                }

                GetReadablilityLevel(dRAvg, out Gradelevel, out readColor);

                tcpShaded = GetCellShaddedProperty(readColor);

                tc = new TableCell();

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Readability ", Environment.NewLine, Environment.NewLine, RAvg.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Long Sentences
                tc = new TableCell();

                tcpShaded = GetCellShaddedProperty(LSColor);

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Long Sentences ", Environment.NewLine, Environment.NewLine, LSTotal.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Complex Words
                tc = new TableCell();

                tcpShaded = GetCellShaddedProperty(CColor);

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Complex Words ", Environment.NewLine, Environment.NewLine, CWTotal.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Passive Voice
                tc = new TableCell();

                tcpShaded = GetCellShaddedProperty(PVColor);

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Passive Voice ", Environment.NewLine, Environment.NewLine, PVTotal.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Adverbs
                tc = new TableCell();

                tcpShaded = GetCellShaddedProperty(AColor);

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Adverbs ", Environment.NewLine, Environment.NewLine, ATotal.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                // Dictionary Terms
                tc = new TableCell();

                tcpShaded = GetCellShaddedProperty(DTColor);

                tc.Append(tcpShaded);

                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(string.Concat("Dictionary Terms ", Environment.NewLine, Environment.NewLine, DTTotal.ToString())))));

                tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                tr.Append(tc);

                table.Append(tr);

                body.Append(table);


                //tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", ProjName, "    Document Name: ", DocName)))));
                //TableCellProperties tcp = new TableCellProperties();
                //GridSpan gridSpan = new GridSpan();
                //gridSpan.Val = headerCount;
                //tcp.Append(gridSpan);
                //tc.Append(tcp);
                //tr.Append(tc);
                //table.Append(tr);
                //second row 
                //     tr = new TableRow();


                // insert Chart pics -- chartsPicsPath  myDoc
                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
                    // Readability Chart image
                string imageFile = "R.png";
                string imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                if (File.Exists(imagePathFile))
                {
                    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                    {
                        imagePart.FeedData(stream);
                    }

                    AddImageToBody(body, "Readability", mainPart.GetIdOfPart(imagePart));
                }

                //    // Long Sentences image
                //imageFile = "LS.png";
                //imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                //if (File.Exists(imagePathFile))
                //{
                //    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                //    {
                //        imagePart.FeedData(stream);
                //    }

                //    AddImageToBody(body, "LongSentences", mainPart.GetIdOfPart(imagePart));
                //}

                //// Complex Words image
                //imageFile = "CW.png";
                //imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                //if (File.Exists(imagePathFile))
                //{
                //    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                //    {
                //        imagePart.FeedData(stream);
                //    }

                //    AddImageToBody(body, "ComplexWords", mainPart.GetIdOfPart(imagePart));
                //}

                //// Passive Voice image
                //imageFile = "PV.png";
                //imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                //if (File.Exists(imagePathFile))
                //{
                //    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                //    {
                //        imagePart.FeedData(stream);
                //    }

                //    AddImageToBody(body, "PassiveVoice", mainPart.GetIdOfPart(imagePart));
                //}

                //// Adverbs image
                //imageFile = "A.png";
                //imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                //if (File.Exists(imagePathFile))
                //{
                //    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                //    {
                //        imagePart.FeedData(stream);
                //    }

                //    AddImageToBody(body, "Adverbs", mainPart.GetIdOfPart(imagePart));
                //}

                //// Dictionary Terms image
                //imageFile = "DT.png";
                //imagePathFile = Path.Combine(chartsPicsPath, imageFile);
                //if (File.Exists(imagePathFile))
                //{
                //    using (FileStream stream = new FileStream(imagePathFile, FileMode.Open))
                //    {
                //        imagePart.FeedData(stream);
                //    }

                //    AddImageToBody(body, "DictionaryTerms", mainPart.GetIdOfPart(imagePart));

                //}



                //Table table = new Table();
                //TableProperties tblPr = new TableProperties();
                //TableBorders tblBorders = new TableBorders();
                //tblBorders.TopBorder = new TopBorder();
                //tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                //tblBorders.BottomBorder = new BottomBorder();
                //tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                //tblBorders.LeftBorder = new LeftBorder();
                //tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                //tblBorders.RightBorder = new RightBorder();
                //tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                //tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                //tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                //tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                //tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                //tblPr.Append(tblBorders);
                //table.Append(tblPr);

                //TableRow tr;
                //TableCell tc;
                //first row - title
                //tr = new TableRow();
                //tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", ProjName, "    Document Name: ", DocName)))));
                //TableCellProperties tcp = new TableCellProperties();
                //GridSpan gridSpan = new GridSpan();
                //gridSpan.Val = headerCount;
                //tcp.Append(gridSpan);
                //tc.Append(tcp);
                //tr.Append(tc);
                //table.Append(tr);
                //second row 
           //     tr = new TableRow();

                TableCellProperties tcp;

                int yWidth = 0;
                // Generate Header
                //for (int i = 0; i < Captions.Length; i++)
                //{
 
                //    tc = new TableCell();

                //    //    yWidth = Convert.ToInt32(ColumnWidth[i]) * 1000; // Added 04/23/2016
                //    tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                //    //tcp = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = yWidth.ToString() }); // Added 04/23/2016
                //    //tc.Append(tcp); // Added 04/23/2016

                //    string s = Captions[i];
                //    tc = new TableCell();
                //    tc.Append(new Paragraph(new Run(new Text(s))));
                //    tr.Append(tc);
                //}
                //table.Append(tr);

               // body.Append(table);

                string uid = string.Empty;

                // Insert Data
                foreach (DataRow row in dt.Rows)
                {

                    table = new Table();
                    tblPr = new TableProperties();
                    tblBorders = new TableBorders();
                    tblBorders.TopBorder = new TopBorder();
                    tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    tblBorders.BottomBorder = new BottomBorder();
                    tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    tblBorders.LeftBorder = new LeftBorder();
                    tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    tblBorders.RightBorder = new RightBorder();
                    tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                    tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                    tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                    tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                    tblPr.Append(tblBorders);

                    GridSpan gridSpan = new GridSpan();
                    gridSpan.Val = headerCount;
                    tblPr.Append(gridSpan);

                    table.Append(tblPr);

                    tr = new TableRow();

                    for (int i = 0; i < Captions.Length; i++)
                    {

                        tc = new TableCell();

                        yWidth = Convert.ToInt32(ColumnWidth[i]) * 1000; // Added 04/23/2016
                           
                        //tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                        tcp = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = yWidth.ToString() }); // Added 04/23/2016
                        tc.Append(tcp); // Added 04/23/2016

                        string s = Captions[i];
                        tc = new TableCell();
                     //   tc.Append(new Paragraph(new Run(new Text(s))));
                        if (s == "Rank")
                        {
                            tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(s))));
                            tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "500" }));
                        }
                        else if (s == "Number")
                        {
                            tc.Append(new Paragraph(new Run(new Text(s))));
                            tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2000" }));
                        }
                        else if (s == "Capton")
                        {
                            tc.Append(new Paragraph(new Run(new Text(s))));
                            tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3000" }));
                        }
                        else
                        {
                            tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(s))));
                            tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                        }


                        tr.Append(tc);
                    }

                    table.Append(tr);

                    tr = new TableRow();

                    for (int field = 0; field < dt.Columns.Count; field++)
                    {
                        string cleanValue = RemoveInvalidCharacters(row[field].ToString()); // Added 09.25.2013

                        tc = new TableCell();
                        //   tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                        //if (dt.Columns[field].ColumnName != "Text")
                        //{
                        //    if (dt.Columns[field].ColumnName != "UID") // UID is required to know which HTML file to insert
                        //    {
                        //        if (dt.Columns[field].ColumnName != "Readability")
                        //        {
                        //            //tc.Append(new Paragraph(new Run(new Text(row[field].ToString())))); // Removed 0.9.25.2013
                        //            tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                        //            tr.Append(tc);
                        //        }


                        //        //tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                        //        //tr.Append(tc);                  

                        //    }
                        //}
                        if (dt.Columns[field].ColumnName == "Rank")
                        {
                            if (cleanValue != string.Empty)
                            {              
                                Text txt = new Text(cleanValue);

                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                tr.Append(tc);
                                
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "Readability")
                        {

                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    Double qc = Convert.ToDouble(cleanValue);

                                    System.Drawing.Color color = setQualityBackColor(qc);
                                    // Create the TableCellProperties object
                                    tcpShaded = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto, });
                                 //   TableCellProperties tcpShaded = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto, }, new Justification { Val = JustificationValues.Center });


                                    // Create the Shading object
                                    DocumentFormat.OpenXml.Wordprocessing.Shading shading =
                                        new DocumentFormat.OpenXml.Wordprocessing.Shading()
                                        {
                                            Color = "auto",
                                            Fill = System.Drawing.ColorTranslator.ToHtml(color),
                                            Val = ShadingPatternValues.Clear
                                        };

                                    // Add the Shading object to the TableCellProperties object
                                    tcpShaded.Append(shading);
                                    tc.Append(tcpShaded);


                                    // Add Center alignment
                                    //DocumentFormat.OpenXml.Wordprocessing.Justification justCenter = new Justification()
                                    //{
                                    //    Val = JustificationValues.Center
                                    //};

                                    //tcpShaded.Append(justCenter);


                                    

                               //     tc.Append(new Paragraph(new Run(new Text(cleanValue))));

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }),new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "LongSentences")
                        {
                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    tcpShaded = GetCellShaddedProperty(LSColor);
                                    tc.Append(tcpShaded);

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "ComplexWords")
                        {
                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    tcpShaded = GetCellShaddedProperty(CColor);
                                    tc.Append(tcpShaded);

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "PassiveVoice")
                        {
                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    tcpShaded = GetCellShaddedProperty(PVColor);
                                    tc.Append(tcpShaded);

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "Adverbs")
                        {
                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    tcpShaded = GetCellShaddedProperty(AColor);
                                    tc.Append(tcpShaded);

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }
                        else if (dt.Columns[field].ColumnName == "DictionaryTerms")
                        {
                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    tcpShaded = GetCellShaddedProperty(DTColor);
                                    tc.Append(tcpShaded);

                                    tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(cleanValue))));

                                    tr.Append(tc);
                                }
                            }
                            else
                            {
                                tc.Append(new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), new Run(new Text(" "))));

                                tr.Append(tc);
                            }
                        }


                        //else if (dt.Columns[field].ColumnName == "Text")
                        //{
                        //    string altChunkId = string.Concat("seg", row["UID"].ToString());

                        //    // Create alternative format import part.
                        //    AlternativeFormatImportPart formatImportPart =
                        //        mainPart.AddAlternativeFormatImportPart(
                        //            AlternativeFormatImportPartType.Html, altChunkId);

                        //    string pathHTMLFile = string.Concat(pathHTML, @"\", row["UID"].ToString(), ".html");

                        //    if (File.Exists(pathHTMLFile))
                        //    {

                        //        using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                        //        {
                        //            formatImportPart.FeedData(fileStream);
                        //        }

                        //        // formatImportPart.FeedData(ms);
                        //        AltChunk altChunk = new AltChunk();
                        //        altChunk.Id = altChunkId;

                        //        paragraph = new Paragraph();
                        //        run_paragraph = new Run();

                        //        run_paragraph.Append(altChunk);
                        //        paragraph.Append(run_paragraph);
                        //        tc.Append(paragraph);
                        //        tr.Append(tc);


                        //    }
                        //    else
                        //    {
                        //        tc.Append(new Paragraph(new Run(new Text("File Not Found: " + pathHTMLFile))));
                        //        tr.Append(tc);
                        //    }

                        //}
                        else if (dt.Columns[field].ColumnName != "Text" && dt.Columns[field].ColumnName != "UID")
                        {
                            tc.Append(new Paragraph(new Run(new Text(cleanValue))));
                            tr.Append(tc);
                        }
                        else if (dt.Columns[field].ColumnName == "UID")
                        {
                            uid = cleanValue;
                        }
                        
                        
                    }

                    table.Append(tr);
                    body.Append(table);

                    //paragraph = new Paragraph();
                    //run_paragraph = new Run();

                    //paragraph = body.AppendChild(new Paragraph());
                    //run_paragraph = paragraph.AppendChild(new Run());
                    //run_paragraph.AppendChild(new Text(" "));

                    //table = new Table();
                    //tblPr = new TableProperties();
                    //tblBorders = new TableBorders();
                    //tblBorders.TopBorder = new TopBorder();
                    //tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    //tblBorders.BottomBorder = new BottomBorder();
                    //tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    //tblBorders.LeftBorder = new LeftBorder();
                    //tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    //tblBorders.RightBorder = new RightBorder();
                    //tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                    //tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                    //tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                    //tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                    //tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                    //tblPr.Append(tblBorders);
                    //table.Append(tblPr);

                    //// Add Text
                    //tr = new TableRow();
                    //tc = new TableCell();

                    string altChunkId = string.Concat("seg", uid);
                    string altNoteChunkId = string.Concat("note", uid);

                    // Create alternative format import part.
                    AlternativeFormatImportPart formatImportPart =
                        mainPart.AddAlternativeFormatImportPart(
                            AlternativeFormatImportPartType.Html, altChunkId);

                    string pathHTMLFile = string.Concat(pathHTML, @"\", uid, ".html");

                    if (File.Exists(pathHTMLFile))
                    {

                        using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                        {
                            formatImportPart.FeedData(fileStream);
                        }

                        // formatImportPart.FeedData(ms);
                        AltChunk altChunk = new AltChunk();
                        altChunk.Id = altChunkId;

                        //paragraph = new Paragraph();
                        //run_paragraph = new Run();

                        //run_paragraph.Append(altChunk);
                        //paragraph.Append(run_paragraph);
                        //tc.Append(paragraph);
                        //tr.Append(tc);


                        paragraph = body.AppendChild(new Paragraph());
                        run_paragraph = new Run();

                        paragraph = body.AppendChild(new Paragraph());
                        run_paragraph = paragraph.AppendChild(new Run());
                        run_paragraph.AppendChild(altChunk);

                        
                    }

                    // Notes
                    pathHTMLFile = string.Concat(pathNotesHTML, @"\", uid, ".html");

                    if (File.Exists(pathHTMLFile))
                    {
                        // Notes caption
                        //paragraph = new Paragraph();
                        //run_paragraph = new Run();

                        // Create alternative format import part.
                        AlternativeFormatImportPart formatImportPart2 =
                            mainPart.AddAlternativeFormatImportPart(
                                AlternativeFormatImportPartType.Html, altNoteChunkId);

                        paragraph = body.AppendChild(new Paragraph());
                        run_paragraph = paragraph.AppendChild(new Run());

                        runProperties = run_paragraph.AppendChild(new RunProperties());
                        fontSize = new FontSize();
                        fontSize.Val = "30";
                        runProperties.AppendChild(fontSize);

                        title = "Notes:";
                        run_paragraph.AppendChild(new Text(title));


                        //paragraph = new Paragraph();
                        //run_paragraph = new Run();

                        // Insert Notes HTML 
                        using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                        {
                            formatImportPart2.FeedData(fileStream);
                        }

                        AltChunk altChunk2 = new AltChunk();
                        altChunk2.Id = altNoteChunkId;

                        paragraph = body.AppendChild(new Paragraph());
                        run_paragraph = new Run();

                        paragraph = body.AppendChild(new Paragraph());
                        run_paragraph = paragraph.AppendChild(new Run());
                        run_paragraph.AppendChild(altChunk2);

                       // mainPart.Document.Append(body);
                    }


                    //else
                    //{
                    //    tc.Append(new Paragraph(new Run(new Text("File Not Found: " + pathHTMLFile))));
                    //    tr.Append(tc);
                    //}

                    //table.Append(tr);
                    //body.Append(table);

                    //paragraph = new Paragraph();
                    //run_paragraph = new Run();

                    //paragraph = body.AppendChild(new Paragraph());
                    //run_paragraph = paragraph.AppendChild(new Run());
                    //run_paragraph.AppendChild(new Text(" "));
                }

                //appending table to body
              //  body.Append(table);
                // and body to the document
                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();

            }

        }

        private TableCellProperties GetCellShaddedProperty(string backGroundColor )
        {
            System.Drawing.Color color = System.Drawing.Color.FromName(backGroundColor);
            TableCellProperties tcpShaded = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto });

            // Create the Shading object
            DocumentFormat.OpenXml.Wordprocessing.Shading shading =
                new DocumentFormat.OpenXml.Wordprocessing.Shading()
                {
                    Color = "auto",
                    Fill = System.Drawing.ColorTranslator.ToHtml(color),
                    Val = ShadingPatternValues.Clear
                };

            // Add the Shading object to the TableCellProperties object
            tcpShaded.Append(shading);

            return tcpShaded;
        }

        private void AddImageToBody(DocumentFormat.OpenXml.Wordprocessing.Body body, string imageName, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 3780000L, Cy = 2000000L }, //{ Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = imageName //"Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = imageName // Example "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                       "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 3780000L, Cy = 2000000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         ) { Preset = A.ShapeTypeValues.Rectangle }))
                             ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            body.AppendChild(new Paragraph(new Run(element)));
        }

        private TableCellProperties GetReadabilityBackgroundColorCellProperty(string ReadabilityValue)
        {
            double readability;

            try
            {
                readability = Convert.ToDouble(ReadabilityValue);
            }
            catch
            {
                return null;
            }

            string gradeLevel = string.Empty;
            string color = "Pink";

            GetReadablilityLevel(readability, out gradeLevel, out color);

            // Create the TableCellProperties object
            TableCellProperties tcp = new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Auto, }
            );
            // Create the Shading object
            DocumentFormat.OpenXml.Wordprocessing.Shading shading =
                new DocumentFormat.OpenXml.Wordprocessing.Shading()
                {
                    Color = color,
                    Fill = ReadabilityValue,
                    Val = ShadingPatternValues.Clear
                };

            return tcp;
        }

        private string GetReadablilityLevel(double readability, out string Gradelevel, out string color)
        {
            color = string.Empty;
            Gradelevel = string.Empty;

            if (readability < 30)
            {
                color = "Red";
                Gradelevel = "College Graduate";
                return "Very Confusing";
            }
            else if (readability < 50 && readability >= 30)
            {
                color = "Salmon";
                Gradelevel = "College";
                return "Difficult";
            }
            else if (readability < 60 && readability >= 50)
            {
                color = "Yellow"; //Gold
                Gradelevel = "10th to 12th Grade";
                return "Fairly Difficult";
            }
            else if (readability < 70 && readability >= 60)
            {
                color = "GreenYellow";
                Gradelevel = "8th & 9th Grade";
                return "Standard";
            }
            else if (readability < 80 && readability >= 70)
            {
                color = "LightGreen";
                Gradelevel = "7th Grade";
                return "Fairly Easy";
            }
            else if (readability < 90 && readability >= 79.99)
            {
                color = "Green";
                Gradelevel = "6th Grade";
                return "Easy";
            }
            else if (readability < 90 && readability >= 100)
            {
                color = "DarkGreen";
                Gradelevel = "5th Grade";
                return "Very Easy";
            }
            else
            {
                color = "DarkGreen";
                Gradelevel = "5th Grade";
                return "Very Easy";
            }

            return string.Empty;

        }

        public void writeDocx4(DataTable dt, string headerCaptions, string pathFile, string DocType, string PropName, string pathHTML, string columnWidth)
        {
            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
            {

                string[] Captions = headerCaptions.Split('|');
                string[] ColumnWidth = columnWidth.Split('|'); // Added 4/23/2016
                int headerCount = Captions.Length;

                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create DOM tree for simple document. 
                mainPart.Document = new Document();
                Body body = new Body();

                // >> Addded 09.21.2013
                SectionProperties sectionProperties = new SectionProperties();

                PageSize pageSize = new PageSize()

                {

                    Width = (UInt32Value)15840U,

                    Height = (UInt32Value)12240U,

                    Orient = PageOrientationValues.Landscape

                };

                PageMargin pageMargin = new PageMargin()

                {

                    Top = 1440,

                    Right = (UInt32Value)1440U,

                    Bottom = 1440,

                    Left = (UInt32Value)1440U,

                    Header = (UInt32Value)720U,

                    Footer = (UInt32Value)720U,

                    Gutter = (UInt32Value)0U

                };

                Columns columns = new Columns() { Space = "720" };

                DocGrid docGrid = new DocGrid() { LinePitch = 360 };

                sectionProperties.Append(pageSize, pageMargin, columns, docGrid);

                body.Append(sectionProperties);
                //<< End Added 09.21.2013


                Table table = new Table();
                TableProperties tblPr = new TableProperties();
                TableBorders tblBorders = new TableBorders();
                tblBorders.TopBorder = new TopBorder();
                tblBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.BottomBorder = new BottomBorder();
                tblBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.LeftBorder = new LeftBorder();
                tblBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.RightBorder = new RightBorder();
                tblBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Single);
                tblBorders.InsideHorizontalBorder = new InsideHorizontalBorder();
                tblBorders.InsideHorizontalBorder.Val = BorderValues.Single;
                tblBorders.InsideVerticalBorder = new InsideVerticalBorder();
                tblBorders.InsideVerticalBorder.Val = BorderValues.Single;
                tblPr.Append(tblBorders);
                table.Append(tblPr);

                TableRow tr;
                TableCell tc;
                //first row - title
                tr = new TableRow();
                tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("Project: ", PropName, "    Document Name: ", DocType)))));
                TableCellProperties tcp = new TableCellProperties();
                //GridSpan gridSpan = new GridSpan();
                //gridSpan.Val = headerCount;
                //tcp.Append(gridSpan);
                //tc.Append(tcp);
                //tr.Append(tc);
                //table.Append(tr);
                ////second row 
                //tr = new TableRow();

                int yWidth = 0;
                // Generate Header
                for (int i = 1; i < Captions.Length; i++)
                {
                    if (i < ColumnWidth.Length)
                    {
                        yWidth = Convert.ToInt32(ColumnWidth[i]) * 1000; // Added 04/23/2016
                    }

                    yWidth = Convert.ToInt32(ColumnWidth[i]) * 1000; // Added 04/23/2016
                    tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                    tcp = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = yWidth.ToString() }); // Added 04/23/2016
                    tc.Append(tcp); // Added 04/23/2016

                    string s = Captions[i];
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text(s))));
                    tr.Append(tc);
                }
                table.Append(tr);

                // Insert Data
                foreach (DataRow row in dt.Rows)
                {
                    tr = new TableRow();
                    for (int field = 0; field < dt.Columns.Count; field++)
                    {
                        string cleanValue = RemoveInvalidCharacters(row[field].ToString()); // Added 09.25.2013

                        tc = new TableCell();
                        //   tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" })); // Added 04/24/2016
                        if (dt.Columns[field].ColumnName != "Text")
                        {
                            if (dt.Columns[field].ColumnName != "UID") // UID is required to know which HTML file to insert
                            {
                                if (dt.Columns[field].ColumnName != "Quality")
                                {
                                    //tc.Append(new Paragraph(new Run(new Text(row[field].ToString())))); // Removed 0.9.25.2013
                                    tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                                    tr.Append(tc);
                                }


                                //tc.Append(new Paragraph(new Run(new Text(cleanValue)))); // Added 09.25.2013
                                //tr.Append(tc);                  

                            }
                        }
                        else if (dt.Columns[field].ColumnName == "Quality")
                        {

                            if (cleanValue != string.Empty)
                            {
                                if (IsDouble(cleanValue))
                                {
                                    Double qc = Convert.ToDouble(cleanValue);

                                    System.Drawing.Color color = setQualityBackColor(qc);
                                    // Create the TableCellProperties object
                                    TableCellProperties tcpShaded = new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto, });

                                    // Create the Shading object
                                    DocumentFormat.OpenXml.Wordprocessing.Shading shading =
                                        new DocumentFormat.OpenXml.Wordprocessing.Shading()
                                        {
                                            Color = System.Drawing.ColorTranslator.ToHtml(color),
                                            Fill = cleanValue,
                                            Val = ShadingPatternValues.Solid
                                        };

                                    // Add the Shading object to the TableCellProperties object
                                    tcpShaded.Append(shading);

                                    tc.Append(tcpShaded);
                                    tr.Append(tc);
                                }
                            }
                            else if (dt.Columns[field].ColumnName == "Text")
                            {
                                string altChunkId = string.Concat("seg", row["UID"].ToString());

                                // Create alternative format import part.
                                AlternativeFormatImportPart formatImportPart =
                                   mainPart.AddAlternativeFormatImportPart(
                                      AlternativeFormatImportPartType.Html, altChunkId);

                                string pathHTMLFile = string.Concat(pathHTML, @"\", row["UID"].ToString(), ".html");

                                if (File.Exists(pathHTMLFile))
                                {

                                    using (FileStream fileStream = File.Open(pathHTMLFile, FileMode.Open))
                                    {
                                        formatImportPart.FeedData(fileStream);
                                    }

                                    // formatImportPart.FeedData(ms);
                                    AltChunk altChunk = new AltChunk();
                                    altChunk.Id = altChunkId;

                                    Paragraph paragraph = new Paragraph();
                                    Run run_paragraph = new Run();

                                    run_paragraph.Append(altChunk);
                                    paragraph.Append(run_paragraph);
                                    tc.Append(paragraph);
                                    tr.Append(tc);


                                }
                                else
                                {
                                    tc.Append(new Paragraph(new Run(new Text("File Not Found: " + pathHTMLFile))));
                                    tr.Append(tc);
                                }


                            }
                            else
                            {
                                tc.Append(new Paragraph(new Run(new Text(cleanValue))));
                                tr.Append(tc);       
                            }
                        }

                        table.Append(tr);

                    }
                }



                //appending table to body
                body.Append(table);
                // and body to the document
                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();
            }
        }


    // ------------------The Following Source Code is for Supporting Storyboard -------------------------------------
        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            
        }
        


        /// <summary>
        /// Regex used to parse MERGEFIELDs in the provided document.
        /// </summary>
        private static readonly Regex instructionRegEx =
            new Regex(
                        @"^[\s]*MERGEFIELD[\s]+(?<name>[#\w]*){1}               # This retrieves the field's name (Named Capture Group -> name)
                            [\s]*(\\\*[\s]+(?<Format>[\w]*){1})?                # Retrieves field's format flag (Named Capture Group -> Format)
                            [\s]*(\\b[\s]+[""]?(?<PreText>[^\\]*){1})?         # Retrieves text to display before field data (Named Capture Group -> PreText)
                                                                                # Retrieves text to display after field data (Named Capture Group -> PostText)
                            [\s]*(\\f[\s]+[""]?(?<PostText>[^\\]*){1})?",
                        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);


        public ArrayList GetFields(string filename)
        {
            _errorMessage = string.Empty;

            ArrayList fields = new ArrayList();

            try
            {

                // first read document in as stream
                byte[] original = File.ReadAllBytes(filename);
                string[] switches = null;

  
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(original, 0, original.Length);

                    // Create a Wordprocessing document object. 
                    using (WordprocessingDocument docx = WordprocessingDocument.Open(stream, true))
                    {
                        //  2010/08/01: addition
                        ConvertFieldCodes(docx.MainDocumentPart.Document);

                        // first: process all tables
                        string fieldName;
                        foreach (var field in docx.MainDocumentPart.Document.Descendants<SimpleField>())
                        {
                            fieldName = GetFieldName(field, out switches);

                            fields.Add(fieldName);

                            // fields.Add(GetFieldName(field, out switches));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
                return null;
            }


            return fields;
        }

        /// <summary>
        /// Fills in a .docx file with the provided data.
        /// </summary>
        /// <param name="filename">Path to the template that must be used.</param>
        /// <param name="dataset">Dataset with the datatables to use to fill the document tables with.  Table names in the dataset should match the table names in the document.</param>
        /// <param name="values">Values to fill the document.  Keys should match the MERGEFIELD names.</param>
        /// <returns>The filled-in document.</returns>
        public byte[] GetWordReport(string filename, DataSet dataset, Dictionary<string, string> values)
        {
            // first read document in as stream
            byte[] original = File.ReadAllBytes(filename);
            string[] switches = null;

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(original, 0, original.Length);

                // Create a Wordprocessing document object. 
                using (WordprocessingDocument docx = WordprocessingDocument.Open(stream, true))
                {
                    //  2010/08/01: addition
                    ConvertFieldCodes(docx.MainDocumentPart.Document);

                    // first: process all tables
                    foreach (var field in docx.MainDocumentPart.Document.Descendants<SimpleField>())
                    {
                        string fieldname = GetFieldName(field, out switches);
                        if (!string.IsNullOrEmpty(fieldname) &&
                            fieldname.StartsWith("TBL_"))
                        {
                            TableRow wrow = GetFirstParent<TableRow>(field);
                            if (wrow == null)
                            {
                                continue;   // can happen: is because table contains multiple fields, and after 1 pass, the initial row is already deleted
                            }

                            Table wtable = GetFirstParent<Table>(wrow);
                            if (wtable == null)
                            {
                                continue;   // can happen: is because table contains multiple fields, and after 1 pass, the initial row is already deleted
                            }

                            string tablename = GetTableNameFromFieldName(fieldname);
                            if (dataset == null ||
                                !dataset.Tables.Contains(tablename) ||
                                dataset.Tables[tablename].Rows.Count == 0)
                            {
                                continue;   // don't remove table here: will be done in next pass
                            }

                            DataTable table = dataset.Tables[tablename];

                            List<TableCellProperties> props = new List<TableCellProperties>();
                            List<string> cellcolumnnames = new List<string>();
                            List<string> paragraphInfo = new List<string>();
                            List<SimpleField> cellfields = new List<SimpleField>();

                            foreach (TableCell cell in wrow.Descendants<TableCell>())
                            {
                                props.Add(cell.GetFirstChild<TableCellProperties>());
                                Paragraph p = cell.GetFirstChild<Paragraph>();
                                if (p != null)
                                {
                                    ParagraphProperties pp = p.GetFirstChild<ParagraphProperties>();
                                    if (pp != null)
                                    {
                                        paragraphInfo.Add(pp.OuterXml);
                                    }
                                    else
                                    {
                                        paragraphInfo.Add(null);
                                    }
                                }
                                else
                                {
                                    paragraphInfo.Add(null);
                                }

                                string colname = string.Empty;
                                SimpleField colfield = null;
                                foreach (SimpleField cellfield in cell.Descendants<SimpleField>())
                                {
                                    colfield = cellfield;
                                    colname = GetColumnNameFromFieldName(GetFieldName(cellfield, out switches));
                                    break;  // supports only 1 cellfield per table
                                }

                                cellcolumnnames.Add(colname);
                                cellfields.Add(colfield);
                            }

                            // keep reference to row properties
                            TableRowProperties rprops = wrow.GetFirstChild<TableRowProperties>();

                            foreach (DataRow row in table.Rows)
                            {
                                TableRow nrow = new TableRow();

                                if (rprops != null)
                                {
                                    nrow.Append(new TableRowProperties(rprops.OuterXml));
                                }

                                for (int i = 0; i < props.Count; i++)
                                {
                                    TableCellProperties cellproperties = new TableCellProperties(props[i].OuterXml);
                                    TableCell cell = new TableCell();
                                    cell.Append(cellproperties);
                                    Paragraph p = new Paragraph(new ParagraphProperties(paragraphInfo[i]));
                                    cell.Append(p);   // cell must contain at minimum a paragraph !

                                    if (!string.IsNullOrEmpty(cellcolumnnames[i]))
                                    {
                                        if (!table.Columns.Contains(cellcolumnnames[i]))
                                        {
                                            throw new Exception(string.Format("Unable to complete template: column name '{0}' is unknown in parameter tables !", cellcolumnnames[i]));
                                        }

                                        if (!row.IsNull(cellcolumnnames[i]))
                                        {
                                            string val = row[cellcolumnnames[i]].ToString();
                                            p.Append(GetRunElementForText(val, cellfields[i]));
                                        }
                                    }

                                    nrow.Append(cell);
                                }

                                wtable.Append(nrow);
                            }

                            // finally : delete template-row (and thus also the mergefields in the table)
                            wrow.Remove();
                        }
                    }

                    // clean empty tables
                    foreach (var field in docx.MainDocumentPart.Document.Descendants<SimpleField>())
                    {
                        string fieldname = GetFieldName(field, out switches);
                        if (!string.IsNullOrEmpty(fieldname) &&
                            fieldname.StartsWith("TBL_"))
                        {
                            TableRow wrow = GetFirstParent<TableRow>(field);
                            if (wrow == null)
                            {
                                continue;   // can happen: is because table contains multiple fields, and after 1 pass, the initial row is already deleted
                            }

                            Table wtable = GetFirstParent<Table>(wrow);
                            if (wtable == null)
                            {
                                continue;   // can happen: is because table contains multiple fields, and after 1 pass, the initial row is already deleted
                            }

                            string tablename = GetTableNameFromFieldName(fieldname);
                            if (dataset == null ||
                                !dataset.Tables.Contains(tablename) ||
                                dataset.Tables[tablename].Rows.Count == 0)
                            {
                                // if there's a 'dt' switch: delete Word-table
                                if (switches.Contains("dt"))
                                {
                                    wtable.Remove();
                                }
                            }
                        }
                    }

                    // next : process all remaining fields in the main document
                    FillWordFieldsInElement(values, docx.MainDocumentPart.Document);

                    docx.MainDocumentPart.Document.Save();  // save main document back in package

                    // process header(s)
                    foreach (HeaderPart hpart in docx.MainDocumentPart.HeaderParts)
                    {
                        //  2010/08/01: addition
                        ConvertFieldCodes(hpart.Header);

                        FillWordFieldsInElement(values, hpart.Header);
                        hpart.Header.Save();    // save header back in package
                    }

                    // process footer(s)
                    foreach (FooterPart fpart in docx.MainDocumentPart.FooterParts)
                    {
                        //  2010/08/01: addition
                        ConvertFieldCodes(fpart.Footer);

                        FillWordFieldsInElement(values, fpart.Footer);
                        fpart.Footer.Save();    // save footer back in package
                    }
                }

                // get package bytes
                stream.Seek(0, SeekOrigin.Begin);
                byte[] data = stream.ToArray();

                return data;
            }
        }

        /// <summary>
        /// Applies any formatting specified to the pre and post text as 
        /// well as to fieldValue.
        /// </summary>
        /// <param name="format">The format flag to apply.</param>
        /// <param name="fieldValue">The data value being inserted.</param>
        /// <param name="preText">The text to appear before fieldValue, if any.</param>
        /// <param name="postText">The text to appear after fieldValue, if any.</param>
        /// <returns>The formatted text; [0] = fieldValue, [1] = preText, [2] = postText.</returns>
        /// <exception cref="">Throw if fieldValue, preText, or postText are null.</exception>
        internal static string[] ApplyFormatting(string format, string fieldValue, string preText, string postText)
        {
            string[] valuesToReturn = new string[3];

            if ("UPPER".Equals(format))
            {
                // Convert everything to uppercase.
                valuesToReturn[0] = fieldValue.ToUpper(CultureInfo.CurrentCulture);
                valuesToReturn[1] = preText.ToUpper(CultureInfo.CurrentCulture);
                valuesToReturn[2] = postText.ToUpper(CultureInfo.CurrentCulture);
            }
            else if ("LOWER".Equals(format))
            {
                // Convert everything to lowercase.
                valuesToReturn[0] = fieldValue.ToLower(CultureInfo.CurrentCulture);
                valuesToReturn[1] = preText.ToLower(CultureInfo.CurrentCulture);
                valuesToReturn[2] = postText.ToLower(CultureInfo.CurrentCulture);
            }
            else if ("FirstCap".Equals(format))
            {
                // Capitalize the first letter, everything else is lowercase.
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    valuesToReturn[0] = fieldValue.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture);
                    if (fieldValue.Length > 1)
                    {
                        valuesToReturn[0] = valuesToReturn[0] + fieldValue.Substring(1).ToLower(CultureInfo.CurrentCulture);
                    }
                }

                if (!string.IsNullOrEmpty(preText))
                {
                    valuesToReturn[1] = preText.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture);
                    if (fieldValue.Length > 1)
                    {
                        valuesToReturn[1] = valuesToReturn[1] + preText.Substring(1).ToLower(CultureInfo.CurrentCulture);
                    }
                }

                if (!string.IsNullOrEmpty(postText))
                {
                    valuesToReturn[2] = postText.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture);
                    if (fieldValue.Length > 1)
                    {
                        valuesToReturn[2] = valuesToReturn[2] + postText.Substring(1).ToLower(CultureInfo.CurrentCulture);
                    }
                }
            }
            else if ("Caps".Equals(format))
            {
                // Title casing: the first letter of every word should be capitalized.
                valuesToReturn[0] = ToTitleCase(fieldValue);
                valuesToReturn[1] = ToTitleCase(preText);
                valuesToReturn[2] = ToTitleCase(postText);
            }
            else
            {
                valuesToReturn[0] = fieldValue;
                valuesToReturn[1] = preText;
                valuesToReturn[2] = postText;
            }

            return valuesToReturn;
        }

        /// <summary>
        /// Executes the field switches on a given element.
        /// The possible switches are:
        /// <list>
        /// <li>dt : delete table</li>
        /// <li>dr : delete row</li>
        /// <li>dp : delete paragraph</li>
        /// </list>
        /// </summary>
        /// <param name="element">The element being operated on.</param>
        /// <param name="switches">The switched to be executed.</param>
        internal static void ExecuteSwitches(OpenXmlElement element, string[] switches)
        {
            if (switches == null || switches.Count() == 0)
            {
                return;
            }

            // check switches (switches are always lowercase)
            if (switches.Contains("dp"))
            {
                Paragraph p = GetFirstParent<Paragraph>(element);
                if (p != null)
                {
                    p.Remove();
                }
            }
            else if (switches.Contains("dr"))
            {
                TableRow row = GetFirstParent<TableRow>(element);
                if (row != null)
                {
                    row.Remove();
                }
            }
            else if (switches.Contains("dt"))
            {
                Table table = GetFirstParent<Table>(element);
                if (table != null)
                {
                    table.Remove();
                }
            }
        }

        /// <summary>
        /// Fills all the <see cref="SimpleFields"/> that are found in a given <see cref="OpenXmlElement"/>.
        /// </summary>
        /// <param name="values">The values to insert; keys should match the placeholder names, values are the data to insert.</param>
        /// <param name="element">The document element taht will contain the new values.</param>
        internal static void FillWordFieldsInElement(Dictionary<string, string> values, OpenXmlElement element)
        {
            string[] switches;
            string[] options;
            string[] formattedText;

            Dictionary<SimpleField, string[]> emptyfields = new Dictionary<SimpleField, string[]>();

            // First pass: fill in data, but do not delete empty fields.  Deletions silently break the loop.
            foreach (var field in element.Descendants<SimpleField>())
            {
                string fieldname = GetFieldNameWithOptions(field, out switches, out options);
                if (!string.IsNullOrEmpty(fieldname))
                {
                    if (values.ContainsKey(fieldname)
                        && !string.IsNullOrEmpty(values[fieldname]))
                    {
                        formattedText = ApplyFormatting(options[0], values[fieldname], options[1], options[2]);

                        // Prepend any text specified to appear before the data in the MergeField
                        if (!string.IsNullOrEmpty(options[1]))
                        {
                            field.Parent.InsertBeforeSelf<Paragraph>(GetPreOrPostParagraphToInsert(formattedText[1], field));
                        }

                        // Append any text specified to appear after the data in the MergeField
                        if (!string.IsNullOrEmpty(options[2]))
                        {
                            field.Parent.InsertAfterSelf<Paragraph>(GetPreOrPostParagraphToInsert(formattedText[2], field));
                        }

                        // replace mergefield with text
                        field.Parent.ReplaceChild<SimpleField>(GetRunElementForText(formattedText[0], field), field);
                    }
                    else
                    {
                        // keep track of unknown or empty fields
                        emptyfields[field] = switches;
                    }
                }
            }

            // second pass : clear empty fields
            foreach (KeyValuePair<SimpleField, string[]> kvp in emptyfields)
            {
                // if field is unknown or empty: execute switches and remove it from document !
                ExecuteSwitches(kvp.Key, kvp.Value);
                kvp.Key.Remove();
            }
        }

        /// <summary>
        /// Returns the columnname from a given fieldname from a Mergefield
        /// The instruction of a table-Mergefield is formatted as TBL_tablename_columnname
        /// </summary>
        /// <param name="fieldname">The field name.</param>
        /// <returns>The column name.</returns>
        /// <exception cref="ArgumentException">Thrown when fieldname is not formatted as TBL_tablename_columname.</exception>
        internal static string GetColumnNameFromFieldName(string fieldname)
        {
            // Column name is after the second underscore.
            int pos1 = fieldname.IndexOf('_');
            if (pos1 <= 0)
            {
                throw new ArgumentException("Error: table-MERGEFIELD should be formatted as follows: TBL_tablename_columnname.");
            }

            int pos2 = fieldname.IndexOf('_', pos1 + 1);
            if (pos2 <= 0)
            {
                throw new ArgumentException("Error: table-MERGEFIELD should be formatted as follows: TBL_tablename_columnname.");
            }

            return fieldname.Substring(pos2 + 1);
        }

        /// <summary>
        /// Returns the fieldname and switches from the given mergefield-instruction
        /// Note: the switches are always returned lowercase !
        /// </summary>
        /// <param name="field">The field being examined.</param>
        /// <param name="switches">An array of switches to apply to the field.</param>
        /// <returns>The name of the field.</returns>
        internal static string GetFieldName(SimpleField field, out string[] switches)
        {
            var a = field.GetAttribute("instr", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            switches = new string[0];
            string fieldname = string.Empty;
            string instruction = a.Value;

            if (!string.IsNullOrEmpty(instruction))
            {
                Match m = instructionRegEx.Match(instruction);
                if (m.Success)
                {
                    fieldname = m.Groups["name"].ToString().Trim();
                    int pos = fieldname.IndexOf('#');
                    if (pos > 0)
                    {
                        // Process the switches, correct the fieldname.
                        switches = fieldname.Substring(pos + 1).ToLower().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        fieldname = fieldname.Substring(0, pos);
                    }
                }
            }

            return fieldname;
        }

        /// <summary>
        /// Returns the fieldname and switches from the given mergefield-instruction
        /// Note: the switches are always returned lowercase !
        /// Note 2: options holds values for formatting and text to insert before and/or after the field value.
        ///         options[0] = Formatting (Upper, Lower, Caps a.k.a. title case, FirstCap)
        ///         options[1] = Text to insert before data
        ///         options[2] = Text to insert after data
        /// </summary>
        /// <param name="field">The field being examined.</param>
        /// <param name="switches">An array of switches to apply to the field.</param>
        /// <param name="options">Formatting options to apply.</param>
        /// <returns>The name of the field.</returns>
        internal static string GetFieldNameWithOptions(SimpleField field, out string[] switches, out string[] options)
        {
            var a = field.GetAttribute("instr", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            switches = new string[0];
            options = new string[3];
            string fieldname = string.Empty;
            string instruction = a.Value;

            if (!string.IsNullOrEmpty(instruction))
            {
                Match m = instructionRegEx.Match(instruction);
                if (m.Success)
                {
                    fieldname = m.Groups["name"].ToString().Trim();
                    options[0] = m.Groups["Format"].Value.Trim();
                    options[1] = m.Groups["PreText"].Value.Trim();
                    options[2] = m.Groups["PostText"].Value.Trim();
                    int pos = fieldname.IndexOf('#');
                    if (pos > 0)
                    {
                        // Process the switches, correct the fieldname.
                        switches = fieldname.Substring(pos + 1).ToLower().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        fieldname = fieldname.Substring(0, pos);
                    }
                }
            }

            return fieldname;
        }

        /// <summary>
        /// Returns the first parent of a given <see cref="OpenXmlElement"/> that corresponds
        /// to the given type.
        /// This methods is different from the Ancestors-method on the OpenXmlElement in the sense that
        /// this method will return only the first-parent in direct line (closest to the given element).
        /// </summary>
        /// <typeparam name="T">The type of element being searched for.</typeparam>
        /// <param name="element">The element being examined.</param>
        /// <returns>The first parent of the element of the specified type.</returns>
        internal static T GetFirstParent<T>(OpenXmlElement element)
            where T : OpenXmlElement
        {
            if (element.Parent == null)
            {
                return null;
            }
            else if (element.Parent.GetType() == typeof(T))
            {
                return element.Parent as T;
            }
            else
            {
                return GetFirstParent<T>(element.Parent);
            }
        }

        /// <summary>
        /// Creates a paragraph to house text that should appear before or after the MergeField.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="fieldToMimic">The MergeField that will have its properties mimiced.</param>
        /// <returns>An OpenXml Paragraph ready to insert.</returns>
        internal static Paragraph GetPreOrPostParagraphToInsert(string text, SimpleField fieldToMimic)
        {
            Run runToInsert = GetRunElementForText(text, fieldToMimic);
            Paragraph paragraphToInsert = new Paragraph();
            paragraphToInsert.Append(runToInsert);

            return paragraphToInsert;
        }

        /// <summary>
        /// Returns a <see cref="Run"/>-openxml element for the given text.
        /// Specific about this run-element is that it can describe multiple-line and tabbed-text.
        /// The <see cref="SimpleField"/> placeholder can be provided too, to allow duplicating the formatting.
        /// </summary>
        /// <param name="text">The text to be inserted.</param>
        /// <param name="placeHolder">The placeholder where the text will be inserted.</param>
        /// <returns>A new <see cref="Run"/>-openxml element containing the specified text.</returns>
        internal static Run GetRunElementForText(string text, SimpleField placeHolder)
        {
            string rpr = null;
            if (placeHolder != null)
            {
                foreach (RunProperties placeholderrpr in placeHolder.Descendants<RunProperties>())
                {
                    rpr = placeholderrpr.OuterXml;
                    break;  // break at first
                }
            }

            Run r = new Run();
            if (!string.IsNullOrEmpty(rpr))
            {
                r.Append(new RunProperties(rpr));
            }

            if (!string.IsNullOrEmpty(text))
            {
                // first process line breaks
                string[] split = text.Split(new string[] { "\n" }, StringSplitOptions.None);
                bool first = true;
                foreach (string s in split)
                {
                    if (!first)
                    {
                        r.Append(new Break());
                    }

                    first = false;

                    // then process tabs
                    bool firsttab = true;
                    string[] tabsplit = s.Split(new string[] { "\t" }, StringSplitOptions.None);
                    foreach (string tabtext in tabsplit)
                    {
                        if (!firsttab)
                        {
                            r.Append(new TabChar());
                        }

                        r.Append(new Text(tabtext));
                        firsttab = false;
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// Returns the table name from a given fieldname from a Mergefield.
        /// The instruction of a table-Mergefield is formatted as TBL_tablename_columnname
        /// </summary>
        /// <param name="fieldname">The field name.</param>
        /// <returns>The table name.</returns>
        /// <exception cref="ArgumentException">Thrown when fieldname is not formatted as TBL_tablename_columname.</exception>
        internal static string GetTableNameFromFieldName(string fieldname)
        {
            int pos1 = fieldname.IndexOf('_');
            if (pos1 <= 0)
            {
                throw new ArgumentException("Error: table-MERGEFIELD should be formatted as follows: TBL_tablename_columnname.");
            }

            int pos2 = fieldname.IndexOf('_', pos1 + 1);
            if (pos2 <= 0)
            {
                throw new ArgumentException("Error: table-MERGEFIELD should be formatted as follows: TBL_tablename_columnname.");
            }

            return fieldname.Substring(pos1 + 1, pos2 - pos1 - 1);
        }

        /// <summary>
        /// Title-cases a string, capitalizing the first letter of every word.
        /// </summary>
        /// <param name="toConvert">The string to convert.</param>
        /// <returns>The string after title-casing.</returns>
        internal static string ToTitleCase(string toConvert)
        {
            return ToTitleCaseHelper(toConvert, string.Empty);
        }

        /// <summary>
        /// Title-cases a string, capitalizing the first letter of every word.
        /// </summary>
        /// <param name="toConvert">The string to convert.</param>
        /// <param name="alreadyConverted">The part of the string already converted.  Seed with an empty string.</param>
        /// <returns>The string after title-casing.</returns>
        internal static string ToTitleCaseHelper(string toConvert, string alreadyConverted)
        {
            /*
             * Tail-recursive title-casing implementation.
             * Edge case: toConvert is empty, null, or just white space.  If so, return alreadyConverted.
             * Else: Capitalize the first letter of the first word in toConvert, append that to alreadyConverted and recur.
             */
            if (string.IsNullOrEmpty(toConvert))
            {
                return alreadyConverted;
            }
            else
            {
                int indexOfFirstSpace = toConvert.IndexOf(' ');
                string firstWord, restOfString;

                // Check to see if we're on the last word or if there are more.
                if (indexOfFirstSpace != -1)
                {
                    firstWord = toConvert.Substring(0, indexOfFirstSpace);
                    restOfString = toConvert.Substring(indexOfFirstSpace).Trim();
                }
                else
                {
                    firstWord = toConvert.Substring(0);
                    restOfString = string.Empty;
                }

                System.Text.StringBuilder sb = new StringBuilder();

                sb.Append(alreadyConverted);
                sb.Append(" ");
                sb.Append(firstWord.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture));

                if (firstWord.Length > 1)
                {
                    sb.Append(firstWord.Substring(1).ToLower(CultureInfo.CurrentCulture));
                }

                return ToTitleCaseHelper(restOfString, sb.ToString());
            }
        }

        /// <summary>
        /// Since MS Word 2010 the SimpleField element is not longer used. It has been replaced by a combination of
        /// Run elements and a FieldCode element. This method will convert the new format to the old SimpleField-compliant 
        /// format.
        /// </summary>
        /// <param name="mainElement"></param>
        internal static void ConvertFieldCodes(OpenXmlElement mainElement)
        {
            //  search for all the Run elements 
            Run[] runs = mainElement.Descendants<Run>().ToArray();
            Dictionary<Run, Run[]> newfields = new Dictionary<Run, Run[]>();
            int cursor = 0;
            if (runs.Length > 0) // Added b/c new documents will cause an error
            {
                do
                {
                    Run run = runs[cursor];
                    if (run.HasChildren && run.Descendants<FieldChar>().Count() > 0
                        && (run.Descendants<FieldChar>().First().FieldCharType & FieldCharValues.Begin) == FieldCharValues.Begin)
                    {
                        List<Run> innerRuns = new List<Run>();
                        innerRuns.Add(run);

                        //  loop until we find the 'end' FieldChar
                        bool found = false;
                        string instruction = null;
                        RunProperties runprop = null;
                        do
                        {
                            cursor++;
                            run = runs[cursor];
                            innerRuns.Add(run);
                            if (run.HasChildren && run.Descendants<FieldCode>().Count() > 0)
                                instruction += run.GetFirstChild<FieldCode>().Text;
                            if (run.HasChildren && run.Descendants<FieldChar>().Count() > 0
                                && (run.Descendants<FieldChar>().First().FieldCharType & FieldCharValues.End) == FieldCharValues.End)
                            {
                                found = true;
                            }
                            if (run.HasChildren && run.Descendants<RunProperties>().Count() > 0)
                                runprop = run.GetFirstChild<RunProperties>();
                        } while (found == false && cursor < runs.Length);

                        //  something went wrong : found Begin but no End. Throw exception
                        if (!found)
                            throw new Exception("Found a Begin FieldChar but no End !");

                        if (!string.IsNullOrEmpty(instruction))
                        {
                            //  build new Run containing a SimpleField
                            Run newrun = new Run();
                            if (runprop != null)
                                newrun.AppendChild(runprop.CloneNode(true));
                            SimpleField simplefield = new SimpleField();
                            simplefield.Instruction = instruction;
                            newrun.AppendChild(simplefield);

                            newfields.Add(newrun, innerRuns.ToArray());
                        }
                    }

                    cursor++;
                } while (cursor < runs.Length);
            }

            //  replace all FieldCodes by old-style SimpleFields
            foreach (KeyValuePair<Run, Run[]> kvp in newfields)
            {
                kvp.Value[0].Parent.ReplaceChild(kvp.Key, kvp.Value[0]);
                for (int i = 1; i < kvp.Value.Length; i++)
                    kvp.Value[i].Remove();
            }
        }
                

    }
  
}
