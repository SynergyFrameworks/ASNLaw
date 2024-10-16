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
using System.Windows.Forms;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AcroParser2
{
    class GenWordRpt
    {

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

        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            var ListData = dt.AsEnumerable().Distinct().ToList();
            var arDistinct = new ArrayList();
            foreach (var item in ListData)
            {
                if (!arDistinct.Contains(item[0].ToString()))
                {
                    arDistinct.Add(item[0].ToString());
                }
            }
            foreach (var item in arDistinct)
            {
                var lstItem = ListData.Where(x => x.ItemArray[0].ToString() == item.ToString()).ToList();
                if (lstItem.Count > 1)
                {
                    var remove = lstItem.Where(x => !string.IsNullOrEmpty(x.ItemArray[0].ToString()) && string.IsNullOrEmpty(x.ItemArray[1].ToString())).FirstOrDefault();
                    if (remove != null)
                    {
                        ListData.RemoveAll(x => x.ItemArray[0] == remove.ItemArray[0] && x.ItemArray[1] == remove.ItemArray[1]);
                    }
                }
            }
            DataTable dt2 = ListData.CopyToDataTable();

            return dt2;
        }


        public bool GenerateRpt(DataTable dt, string headerCaptions, string pathFile, string FileName)
        {
            dt = RemoveDuplicatesRecords(dt);

            bool returnValue = false;

            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(pathFile, WordprocessingDocumentType.Document))
            {

                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;

                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create DOM tree for simple document. 
                mainPart.Document = new Document();
                Body body = new Body();


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
                //  tr = new TableRow();
                //  tc = new TableCell(new Paragraph(new Run(new Text(string.Concat("File Name: ", FileName)))));
                TableCellProperties tcp = new TableCellProperties();
                ////  tcp.Append(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "CFDCED" }); // Added 4.4.2017 -- Color Light Blue
                //  GridSpan gridSpan = new GridSpan();
                //  gridSpan.Val = headerCount;
                //  tcp.Append(gridSpan);
                //  tc.Append(tcp);
                //  tr.Append(tc);
                //  table.Append(tr);

                //second row 
                tr = new TableRow();
                // Generate Header

                for (int i = 0; i < Captions.Length; i++)
                {
                    tcp = new TableCellProperties(); // Added 9.17.2017
                    tcp.Append(new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "CFDCED" }); // Added 9.17.2017 -- Color Light Blue

                    string s = Captions[i];
                    tc = new TableCell();
                    tc.Append(tcp); // Added 4.4.2017 -- Color Light Blue
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
                        string cleanValue = RemoveInvalidCharacters(row[field].ToString());
                        tc.Append(new Paragraph(new Run(new Text(cleanValue))));
                        tr.Append(tc);
                    }

                    table.Append(tr);

                }

                // Insert File Name -- Added 9.18.2017
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text(string.Concat("File Name: ", FileName)));

                //appending table to body
                body.Append(table);
                // and body to the document
                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();

                returnValue = true;
            }


            return returnValue;
        }
    }
}


