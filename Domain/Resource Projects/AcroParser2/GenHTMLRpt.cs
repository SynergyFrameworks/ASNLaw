using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Net;
using Atebion.Common;
using System.Collections;

namespace AcroParser2
{
    class GenHTMLRpt
    {

        private const string TAB = "\t";
        private const string QUOTE = "\"";
        private string LINE = string.Concat(TAB, "<hr width=", QUOTE, "100%", QUOTE, "/>");
        //   private string HR = Environment.NewLine;

        private StringBuilder _sb = new StringBuilder();

        private TextWriter _writer;

        public string HtmlEncode(string text)
        {

            //text = System.Web.HttpUtility.HtmlEncode(text);

            //text = CleansText4HTML(text);

            //  return text;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char c in text)
            {
                if (c > 127) // special chars
                    sb.Append(String.Format("&#{0};", (int)c));
                else
                    sb.Append(c);
            }
            return sb.ToString();

        }

        public void StartRpt_1(string pathFile)
        {
            _writer = File.CreateText(pathFile);

            _writer.WriteLine(string.Concat("<!DOCTYPE HTML PUBLIC ", QUOTE, "-//W3C//DTD HTML 4.0 Transitional//EN", QUOTE, ">"));
            _writer.WriteLine("<html>");
            _writer.WriteLine(string.Concat(TAB, "<head>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<title>Scion Analytics, AcroSeeker Report</title>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<style type=", QUOTE, "text/css", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style1"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style3"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "color: #000000;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: large;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style4"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align: left;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style5"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: large;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style6"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: x-small;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style7"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align: right;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style8"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "color: #000000;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: small;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style9"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: small;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style11"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color: #FFFF00;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</style>"));
            _writer.WriteLine(string.Concat(TAB, "</head>"));
        }

        public void GenRtpHeader_2(string OrgFileName)
        {
            _writer.WriteLine(string.Concat(TAB, "<body>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<!-- Header Area -->"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<p class=", QUOTE, "style4", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<span class=", QUOTE, "style5", QUOTE, ">Scion Analytics - AcroSeeker's Acronym Report for ", OrgFileName, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</p>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<div class=", QUOTE, "style7", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<span class=", QUOTE, "style6", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "Report generated on ", DateTime.Now.ToString("F"), " ")); // Example: Friday, February 27, 2009 12:12:22 PM
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</div>"));
            _writer.WriteLine(LINE); // Draw Line
        }

        public void GenSummaryArea_3()
        {
            _writer.WriteLine(string.Concat(TAB, TAB, "<!-- Summary -->"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<span class=", QUOTE, "style1", QUOTE, ">Summary</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<center>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<table align=", QUOTE, "center", QUOTE, " cellpadding=", QUOTE, "20", QUOTE, " cellspacing=", QUOTE, "20", QUOTE, ">")); // Start of the Table to hold Summary Info
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<tbody>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, "<tr>")); // Table Row  
        }

        public void GenSumQtyOfSentences_4(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Sentences -->"));
            CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Sentences");
        }

        public void GenSumAcronymsFound_5(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Found -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Yellow, string.Empty, "Acronyms Found");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Green, "#AcronymsFound", "Acronyms Found");
        }

        public void GenSumAcronymsDefined_6(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Defined -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Yellow, string.Empty, "Acronyms Defined");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Green, "#AcronymsDefined", "Acronyms Defined");
        }

        public void GenSumAcronymsLibDefined_6a(string Qty) // added in Beta2
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Dictionary Found -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Yellow, string.Empty, "Dictionary Acronyms");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Green, "#AcronymsLib", "Dictionary Acronyms");
        }

        public void GenSumAcronymsNotDefined_7(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Not Defined -->"));

            if (DataFunctions.IsNumeric(Qty))
            {
                int nQty = Convert.ToInt32(Qty);
                if (nQty == 0)
                    CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Not Defined");
                else if ((nQty < 11))
                    CreateSummaryBlockItem(Qty, HTMLColors.Yellow, "#AcronymsNotDefined", "Acronyms Not Defined");
                else
                    CreateSummaryBlockItem(Qty, HTMLColors.Red, "#AcronymsNotDefined", "Acronyms Not Defined");
            }
            else
            {
                CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Not Defined");
            }
        }

        public void GenSumAcronymsDefViaDic_7a(string Qty, int QtyNotDefined) // Added 9.23.2017
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Def. via Dictionary -->"));

            if (DataFunctions.IsNumeric(Qty))
            {
                int nQty = Convert.ToInt32(Qty);
                if (nQty == 0 && QtyNotDefined == 0)
                    CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Definition via Dictionary");
                else if (nQty == QtyNotDefined)
                    CreateSummaryBlockItem(Qty, HTMLColors.Green, "#AcronymsDefsDefinedDic", "Acronyms Definition via Dictionary");
                else if ((nQty == 0 && QtyNotDefined > 2))
                    CreateSummaryBlockItem(Qty, HTMLColors.Red, "#AcronymsDefsDefinedDic", "Acronyms Definition via Dictionary");
                else if ((nQty < QtyNotDefined))
                    CreateSummaryBlockItem(Qty, HTMLColors.Yellow, "#AcronymsDefsDefinedDic", "Acronyms Definition via Dictionary");
                else
                    CreateSummaryBlockItem(Qty, HTMLColors.Yellow, "#AcronymsDefsDefinedDic", "Acronyms Definition via Dictionary");
            }
            else
            {
                CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Definition via Dictionary");
            }
        }

        public void GenSumAcronymsMultiDefined_8(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Multi-Defined -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Multi-Defined");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Red, "#AcronymsMultiDefined", "Acronyms Multi-Defined");

        }

        public void GenSumAcronymsDefinedDiff_9(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Defined Diff -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Defined Diff");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Red, "#AcronymDefinedDiff", "Acronyms Defined Diff");

        }

        public void GenEndSumArea_10()
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "</tr>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</tbody>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</table>"));
            _writer.WriteLine(string.Concat(TAB, "</center>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenHeadingForAcronymsUsedBeforeDefinition(string Qty)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Qty of Acronyms Defined Diff -->"));

            if (Qty == "0")
                CreateSummaryBlockItem(Qty, HTMLColors.Green, string.Empty, "Acronyms Used Before Definition");
            else
                CreateSummaryBlockItem(Qty, HTMLColors.Red, "#AcronymsUsedBeforeDefinition", "Acronyms Used Before Definition");

        }

        private void CreateSummaryBlockItem(string Qty, string Color, string Tag, string Caption)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " height=", QUOTE, "80", QUOTE, " style=", QUOTE, "color: ", Color, "; background-color: ", Color, ";", QUOTE, " width=", QUOTE, "140", QUOTE, " class=", QUOTE, "style1", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style3", QUOTE, ">", Qty, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<table align=", QUOTE, "center", QUOTE, " cellpadding=", QUOTE, "0", QUOTE, " cellspacing=", QUOTE, "0", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<tbody>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<tr>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " bgcolor=", QUOTE, Color, QUOTE, " height=", QUOTE, "20", QUOTE, " width=", QUOTE, "140", QUOTE, " class=", QUOTE, "style1", QUOTE, ">"));
            if (Tag == string.Empty)
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style8", QUOTE, ">", Caption, "</span>"));
            else
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style8", QUOTE, "><a href=", QUOTE, Tag, QUOTE, ">", Caption, "</a></span>"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "</td>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "</tr>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "</tbody>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, TAB, "</table>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "</td>"));
        }
        public DataTable RemoveDuplicatesRecordsGeneric(DataTable dt)
        {
            //Returns just 5 unique rows
            if (dt.Rows.Count > 0)
            {
                var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
                DataTable dt2 = UniqueRows.CopyToDataTable();
                return dt2;
            }
            else
            {
                return dt;
            }
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

        public void GenAcronymsFoundDetails_10(DataView dv)
        {
            if (dv == null)
                return;
            DataTable dt = RemoveDuplicatesRecords(dv.ToTable());
            dv = dt.AsDataView();


            if (dv.ToTable().Rows.Count == 0)
                return;

            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Found -->"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Found</span>"));
            _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsFound", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));

            //   string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                if (acronym.Length > 0)
                    acronym = HtmlEncode(acronym);

                if (definition.Length > 0)
                    definition = HtmlEncode(definition);

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", definition, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line

        }

        public void GenAcronymsFoundDetails2_10(DataView dv) // Added in Beta 2
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Found -->"));
            //   _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Found in Document</span>"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsFound", QUOTE, ">Acronyms Found in Document</span>"));
            //    _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsFound", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));



            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                if (acronym.Length > 0)
                    acronym = HtmlEncode(acronym);

                if (definition.Length > 0)
                    definition = HtmlEncode(definition);

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", definition, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);
                if (definition != string.Empty)
                {
                    Sentence = HighLightTextYellow(Sentence, definition);

                    if (Sentence.Length > 0)
                        Sentence = HtmlEncode(Sentence);
                }

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenAcronymsDefinedDetails_11(DataView dv)
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Defined -->"));
            // _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Defined in Document</span>"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsDefined", QUOTE, ">Acronyms Defined in Document</span>"));
            //  _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsDefined", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);
                if (definition != string.Empty)
                {
                    Sentence = HighLightTextYellow(Sentence, definition);
                }

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenAcronymsUsedBeforeDefinition(DataView dv)
        {

            if (dv == null)
                return;

            if (dv.ToTable().Rows.Count == 0)
                return;
            DataTable dt = RemoveDuplicatesRecordsGeneric(dv.ToTable());
            dv = dt.AsDataView();

            if (dv != null)
            {
                _writer.WriteLine(string.Concat(TAB, "<!-- AcronymsUsedBeforeDefinition -->"));

                // _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Defined in Document</span>"));
                _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsUsedBeforeDefinition", QUOTE, ">Acronyms Used Before Definition</span>"));
                //  _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsDefined", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
                _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


                string Sentence = string.Empty;
                string acronym = string.Empty;
                string definition = string.Empty;


                foreach (DataRow row in dv.ToTable().Rows)
                {
                    acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                    definition = row[AcronymsFoundFieldConst.Definition].ToString();

                    _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                    Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                    Sentence = CleansText4HTML(Sentence);
                    Sentence = HighLightTextYellow(Sentence, acronym);
                    if (definition != string.Empty)
                    {
                        Sentence = HighLightTextYellow(Sentence, definition);
                    }

                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                    _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
                }

                _writer.WriteLine(string.Concat(TAB, "</table>"));
                _writer.WriteLine(LINE); // Insert Line
            }
        }
        public void GenAcronymsLibDetails_11a(DataView dv) // Added in Beta 2 03.26.2016
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Dictionary Acronyms Found -->"));
            //   _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Dictionary Acronyms in Document</span>"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsLib", QUOTE, ">Dictionary Acronyms in Document</span>"));

            // _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsLib", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);
                //if (definition != string.Empty)
                //{
                //    Sentence = HighLightTextYellow(Sentence, definition);
                //}

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenAcronymsNotDefinedDetails2_12(DataView dv) // Added in Beta 2 03.26.2016
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Not Defined in Document -->"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsNotDefined", QUOTE, ">Acronyms Not Defined in Document</span>"));

            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();


                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenAcronymsDefViaDic(DataView dv) // Added 9.23.2017
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Def.s Defined via Dictionarie(s) -->"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsDefsDefinedDic", QUOTE, ">Acronyms Definition Defined via Dictionaries (Acronym &ensp; Definition &ensp; Dictionary)</span>"));

            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));



            string acronym = string.Empty;
            string definition = string.Empty;
            string dictionary = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();
                dictionary = row[AcronymsFoundFieldConst.Dictionary].ToString();


                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", definition, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Dictionary
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", dictionary, "</span>")); // Acronym Def. found in Dictionary
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data

                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        //public void GenAcronymsNotDefinedDetails_12(DataView dv)
        //{
        //    _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Not Defined in Document -->"));
        //    //_writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Not Defined in Document</span>"));
        //    _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsNotDefined", QUOTE, ">Acronyms Not Defined in Document</span>"));
        //    //  _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsNotDefined", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
        //    _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


        //    string Sentence = string.Empty;
        //    string acronym = string.Empty;

        //    foreach (DataRow row in dv.ToTable().Rows)
        //    {
        //        acronym = row[AcronymsFoundFieldConst.Acronym].ToString();

        //        _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data

        //        Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
        //        Sentence = CleansText4HTML(Sentence);
        //        Sentence = HighLightTextYellow(Sentence, acronym);

        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
        //        _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row
        //    }

        //    _writer.WriteLine(string.Concat(TAB, "</table>"));
        //    _writer.WriteLine(LINE); // Insert Line

        //}

        public void GenAcronymsMultiDetails_13(DataView dv)
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Multi-Defined -->"));
            //    _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Multi-Defined in Document</span>"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymsMultiDefined", QUOTE, ">Acronyms Multi-Defined in Document</span>"));
            //  _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymsMultiDefined", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);
                if (definition != string.Empty)
                {
                    Sentence = HighLightTextYellow(Sentence, definition);
                }

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row

            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        public void GenAcronymsDefinedDiffDetails_14(DataView dv)
        {
            _writer.WriteLine(string.Concat(TAB, "<!-- Acronyms Defined Diff -->"));
            //  _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Acronyms Defined Different in Document</span>"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, " id=", QUOTE, "AcronymDefinedDiff", QUOTE, ">Acronyms Defined Different in Document</span>"));
            // _writer.WriteLine(string.Concat(TAB, "<table id=", QUOTE, "AcronymDefinedDiff", QUOTE, " cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));

            string Sentence = string.Empty;
            string acronym = string.Empty;
            string definition = string.Empty;

            foreach (DataRow row in dv.ToTable().Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                definition = row[AcronymsFoundFieldConst.Definition].ToString();

                _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

                Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                Sentence = CleansText4HTML(Sentence);
                Sentence = HighLightTextYellow(Sentence, acronym);
                if (definition != string.Empty)
                {
                    Sentence = HighLightTextYellow(Sentence, definition);
                }

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
                _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row

            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
            _writer.WriteLine(LINE); // Insert Line
        }

        //public void GenAcronymsUsedBeforeDefinition(DataView dv)
        //{

        //    _writer.WriteLine(string.Concat(TAB, TAB, "<span class=", QUOTE, "style1", QUOTE, "></span>"));
        //    _writer.WriteLine(string.Concat(TAB, TAB, "<center>"));
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<table align=", QUOTE, "left", QUOTE, ">")); // Start of the Table to hold Summary Info
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<tbody>"));
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, "<tr>")); // Table Row  
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td cellpadding=", QUOTE, "20", QUOTE, ">Acronyms Used Before Definition</td>"));
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "</tr>")); // Table Row  
        //    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, TAB, "</table> </br>")); // Table Row  



        //    //_writer.WriteLine(LINE);
        //    _writer.WriteLine(string.Concat(TAB, "<table align=", QUOTE, "left", QUOTE," cellpadding =", QUOTE, "10", QUOTE, " cellspacing=", QUOTE, "10", QUOTE, ">"));


        //    string Sentence = string.Empty;
        //    string acronym = string.Empty;
        //    string definition = string.Empty;

        //    foreach (DataRow row in dv.ToTable().Rows)
        //    {
        //        acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
        //        definition = row[AcronymsFoundFieldConst.Definition].ToString();

        //        _writer.WriteLine(string.Concat(TAB, TAB, "<tr>")); // Table Row
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", acronym, "</span>")); // Acronym Found
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // Acronym Definition
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", row[AcronymsFoundFieldConst.Definition].ToString(), "</span>")); // Acronym Definition
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td>")); // 

        //        Sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
        //        Sentence = CleansText4HTML(Sentence);
        //        Sentence = HighLightTextYellow(Sentence, acronym);
        //        if (definition != string.Empty)
        //        {
        //            Sentence = HighLightTextYellow(Sentence, definition);
        //        }

        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", Sentence, "</span>")); // Acronym Definition
        //        _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>")); // End Table Data
        //        _writer.WriteLine(string.Concat(TAB, TAB, "</tr>")); // End Table Row

        //    }

        //    _writer.WriteLine(string.Concat(TAB, "</table>"));






        //}

        private string HighLightTextYellow(string txt, string HighLightText)
        {
            string replacementText = string.Concat("<span class=", QUOTE, "style11", QUOTE, ">", HighLightText, "</span>");
            txt = txt.Replace(HighLightText, replacementText);

            return txt;
        }

        private string CleansText4HTML(string txt)
        {
            txt = System.Web.HttpUtility.HtmlEncode(txt);
            txt = txt.Replace("'", "&#39;"); // Such as ’s
            txt = txt.Replace("’", "&#39;"); // Such as ’s
            txt = txt.Replace("“", "&#34;");
            txt = txt.Replace("”", "&#34;");
            txt = txt.Replace("\n", "<br>");
            txt = txt.Replace("\n", "<br>");

            return txt;
        }


        public void GenFooterEnd_15()
        {
            _writer.WriteLine(string.Concat(TAB, TAB, "<!-- Footer -->"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<div class=", QUOTE, "style7", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<span class=", QUOTE, "style6", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "This report was generated by AcroSeeker, Copyright ", DateTime.Now.ToString("yyyy"), " by Scion Analytics"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</div>"));
            _writer.WriteLine(string.Concat(TAB, "</body>"));
            _writer.WriteLine("</html>");

            _writer.Close();
        }

    }
}
