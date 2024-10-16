using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;


namespace Atebion.HTML.Generation
{
    public class HTMLGen
    {


        public void WriteHTML(DataTable dt, string headerCaptions, string columnWidths, string pathFile, string DocType, string PropName, string PropIAm, string DocTypeIAm, string[] SelKeywords)
        {
            using (TextWriter writer = File.CreateText(pathFile))
            {
                writer.Write("<!DOCTYPE html>");
                writer.Write("    <title>Atebion Document Analyzer, Export Parsed Sections</title>");
                writer.Write("    <style type=\"text/css\">");
                writer.Write("        .style_header");
                writer.Write("        {");
                writer.Write("          font-family: verdana, helvetica, arial, sans-serif;");
                writer.Write("          font-size: 16px;");
                writer.Write("          margin-top: 10px;");
                writer.Write("          margin-bottom: 10px;");
                writer.Write("          font-weight: normal; ");
                writer.Write("          background-color: rgb(255, 255, 255);");
                writer.Write("          color: rgb(0, 0, 0); ");
                writer.Write("          font-style: normal; ");
                writer.Write("          font-variant: normal; ");
                writer.Write("          letter-spacing: normal; ");
                writer.Write("          line-height: normal; ");
                writer.Write("          orphans: 2; text-align: -webkit-auto; ");
                writer.Write("          text-indent: 0px; ");
                writer.Write("          text-transform: none; ");
                writer.Write("          white-space: normal; ");
                writer.Write("          word-spacing: 0px; ");
                writer.Write("          -webkit-text-size-adjust: auto; ");
                writer.Write("          -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table");
                writer.Write("        {");
                writer.Write("            border: 1px solid rgb(195, 195, 195); ");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; background-color: rgb(255, 255, 255); ");
                writer.Write("            border-collapse: collapse; ");
                writer.Write("            width: 1085px; ");
                writer.Write("            color: rgb(0, 0, 0); ");
                writer.Write("            font-style: normal; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: normal; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table_header");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            background-color: rgb(229, 238, 204); ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            vertical-align: top;");
                writer.Write("        }");
                writer.Write("        .style_table_data");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-top: 3px; ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            padding-left: 3px; ");
                writer.Write("            vertical-align: top;  ");
                writer.Write("        }");
                writer.Write("        .style_copyright");
                writer.Write("        {");
                writer.Write("            color: rgb(154, 166, 180); ");
                writer.Write("            font-family: Georgia, 'Times New Roman', Times, serif; ");
                writer.Write("            font-size: 11px; ");
                writer.Write("            font-style: italic; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: 19px; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px; ");
                writer.Write("            background-color: rgb(255, 255, 255); ");
                writer.Write("            display: inline !important; ");
                writer.Write("            float: none; ");
                writer.Write("        }");
                writer.Write("        </style>");
                writer.Write("</head>");
                writer.Write("<body>");
                writer.Write("    <h2 class=\"style_header\">");
                string docHeader = String.Concat("        ", PropIAm, ": ", PropName, "&nbsp;&nbsp;", DocTypeIAm, ": ", DocType);
                //writer.Write("        Proposal: Test1&nbsp;&nbsp; Section: C</h2>");
                writer.Write(docHeader);
                writer.Write("    <table class=\"style_table\" >");

                // ----------------- Table Header ------------------------
                writer.Write("         <tr>"); // Start Table Header Row

                // Get Table Captions
                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;

                string[] ColumnWidths = columnWidths.Split('|');
                for (int i = 0; i < headerCount; i++)
                {
                    //writer.Write("            <th class=\"style_table_header\" width=\"10%\">");

                    int columnWidth = 1;
                    if (ColumnWidths[i] != string.Empty)
                        columnWidth = Convert.ToInt16(ColumnWidths[i]) * 10;

                    string tableHeader = string.Concat(@"            <th class=""style_table_header"" width="", """, columnWidth.ToString(), "%\">");
                    writer.Write(tableHeader);
                    //writer.Write("                Number");
                    string sHData = string.Concat("                ", Captions[i]);
                    writer.Write(sHData);
                    writer.Write("            </th>");
                }

                writer.Write("        </tr>"); // End Table Header Row

                // ----------------- Table Data ------------------------
                string sData = string.Empty;
                //  string htmlEncodedData = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    writer.Write("        <tr>"); // Start Table Data Row
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        // htmlEncodedData = HttpContext.Current.Server.HtmlEncode(row[i].ToString());

                        writer.Write("            <td align=\"left\" class=\"style_table_data\"> ");
                        //writer.Write("                1");
                        if (row[i].ToString().Trim() == string.Empty)
                            sData = "                &nbsp;";
                        else
                        {
                            sData = string.Concat("                ", System.Web.HttpUtility.HtmlEncode(row[i].ToString()));

                            // The HtmlEncode does not fix all special char.s -- Added 06.06.2014
                            sData = sData.Replace("’", "&#39;"); // Such as ’s
                            sData = sData.Replace("“", "&#34;");
                            sData = sData.Replace("”", "&#34;");
                            sData = sData.Replace("\n", "<br>");

                            // Highlight Keywords -- Added 06.06.2014
                            if (dt.Columns[i].ColumnName == "Text")
                            {
                                string highlightKeyword;
                                string pattern;
                                foreach (string keyword in SelKeywords)
                                {
                                    // highlightKeyword = string.Concat("<mark>", keyword, "</mark>");

                                    // '\u0022' = " -- Add Font Background color to support MS Word reading HTML
                                    highlightKeyword = string.Concat("<FONT style=", '\u0022', "BACKGROUND-COLOR: yellow", '\u0022', ">", keyword, "</FONT>");

                                    pattern = string.Concat(@"\b", keyword, @"\b");
                                    sData = Regex.Replace(sData, pattern, highlightKeyword, RegexOptions.IgnoreCase);

                                    // sData = sData.Replace(keyword, highlightKeyword);
                                }
                            }
                            //

                        }




                        writer.Write(sData); // Write Data
                        writer.Write("            </td>");
                    }
                    writer.Write("        </tr>"); // End Table Data Row
                }


                writer.Write("");
                writer.Write("    </table>");
                writer.Write("    <p>");
                writer.Write("        <span class=\"style_copyright\">");
                writer.Write(System.Web.HttpUtility.HtmlEncode("            Atebion Document Analyzer © Copyright 2014 by Atebion, LLC"));
                writer.Write("        </span>");
                writer.Write("    </p>");
                writer.Write("</body>");
                writer.Write("</html>");
            }
        }


        public void WriteHTML2(DataTable dt, string headerCaptions, string columnWidths, string pathFile, string DocType, string PropName, string PropIAm, string DocTypeIAm)
        {
            using (TextWriter writer = File.CreateText(pathFile))
            {
                writer.Write("<!DOCTYPE html>");
                writer.Write("    <title>Atebion Document Analyzer, Export Parsed Sections</title>");
                writer.Write("    <style type=\"text/css\">");
                writer.Write("        .style_header");
                writer.Write("        {");
                writer.Write("          font-family: verdana, helvetica, arial, sans-serif;");
                writer.Write("          font-size: 16px;");
                writer.Write("          margin-top: 10px;");
                writer.Write("          margin-bottom: 10px;");
                writer.Write("          font-weight: normal; ");
                writer.Write("          background-color: rgb(255, 255, 255);");
                writer.Write("          color: rgb(0, 0, 0); ");
                writer.Write("          font-style: normal; ");
                writer.Write("          font-variant: normal; ");
                writer.Write("          letter-spacing: normal; ");
                writer.Write("          line-height: normal; ");
                writer.Write("          orphans: 2; text-align: -webkit-auto; ");
                writer.Write("          text-indent: 0px; ");
                writer.Write("          text-transform: none; ");
                writer.Write("          white-space: normal; ");
                writer.Write("          word-spacing: 0px; ");
                writer.Write("          -webkit-text-size-adjust: auto; ");
                writer.Write("          -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table");
                writer.Write("        {");
                writer.Write("            border: 1px solid rgb(195, 195, 195); ");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; background-color: rgb(255, 255, 255); ");
                writer.Write("            border-collapse: collapse; ");
                writer.Write("            width: 1085px; ");
                writer.Write("            color: rgb(0, 0, 0); ");
                writer.Write("            font-style: normal; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: normal; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table_header");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            background-color: rgb(229, 238, 204); ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            vertical-align: top;");
                writer.Write("        }");
                writer.Write("        .style_table_data");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-top: 3px; ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            padding-left: 3px; ");
                writer.Write("            vertical-align: top;  ");
                writer.Write("        }");
                writer.Write("        .style_copyright");
                writer.Write("        {");
                writer.Write("            color: rgb(154, 166, 180); ");
                writer.Write("            font-family: Georgia, 'Times New Roman', Times, serif; ");
                writer.Write("            font-size: 11px; ");
                writer.Write("            font-style: italic; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: 19px; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px; ");
                writer.Write("            background-color: rgb(255, 255, 255); ");
                writer.Write("            display: inline !important; ");
                writer.Write("            float: none; ");
                writer.Write("        }");
                writer.Write("        </style>");
                writer.Write("</head>");
                writer.Write("<body>");
                writer.Write("    <h2 class=\"style_header\">");
                string docHeader = String.Concat("        ", PropIAm, ": ", PropName, "&nbsp;&nbsp;", DocTypeIAm, ": ", DocType);
                //writer.Write("        Proposal: Test1&nbsp;&nbsp; Section: C</h2>");
                writer.Write(docHeader);
                writer.Write("    <table class=\"style_table\" >");

                // ----------------- Table Header ------------------------
                writer.Write("         <tr>"); // Start Table Header Row

                // Get Table Captions
                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;
                columnWidths = "0|" + columnWidths;
                string[] ColumnWidths = columnWidths.Split('|');
                for (int i = 0; i < headerCount; i++)
                //for (int i = 1; i < headerCount; i++) // Added UID, ignore -- 08.20.2014
                {
                    //writer.Write("            <th class=\"style_table_header\" width=\"10%\">");
                    if (ColumnWidths.Count() > i)
                    {
                        int columnWidth = 1;
                        if (ColumnWidths[i] != string.Empty)
                            columnWidth = Convert.ToInt16(ColumnWidths[i]) * 10;

                        string tableHeader = string.Concat(@"            <th class=""style_table_header"" width="", """, columnWidth.ToString(), "%\">");
                        writer.Write(tableHeader);
                        //writer.Write("                Number");
                        string sHData = string.Concat("                ", Captions[i]);
                        writer.Write(sHData);
                        writer.Write("            </th>");
                    }

                }

                writer.Write("        </tr>"); // End Table Header Row

                // ----------------- Table Data ------------------------
                string sData = string.Empty;
                //  string htmlEncodedData = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    writer.Write("        <tr>"); // Start Table Data Row
                    for (int i = 0; i < dt.Columns.Count; i++)
                   // for (int 0 = 1; i < dt.Columns.Count; i++)
                    {

                        // htmlEncodedData = HttpContext.Current.Server.HtmlEncode(row[i].ToString());

                        writer.Write("            <td align=\"left\" class=\"style_table_data\"> ");
                        //writer.Write("                1");
                        if (row[i].ToString().Trim() == string.Empty)
                            sData = "                &nbsp;";
                        else
                        {

                            if (dt.Columns[i].ColumnName == "Text")
                            {
                                sData = string.Concat("                ", row[i].ToString());

                            }
                            else
                            {

                                sData = string.Concat("                ", System.Web.HttpUtility.HtmlEncode(row[i].ToString()));

                                // The HtmlEncode does not fix all special char.s -- Added 06.06.2014
                                sData = sData.Replace("’", "&#39;"); // Such as ’s
                                sData = sData.Replace("“", "&#34;");
                                sData = sData.Replace("”", "&#34;");
                                sData = sData.Replace("\n", "");
                            }

                        }

                        writer.Write(sData); // Write Data
                        writer.Write("            </td>");
                    }
                    writer.Write("        </tr>"); // End Table Data Row
                }


                writer.Write("");
                writer.Write("    </table>");
                writer.Write("    <p>");
                writer.Write("        <span class=\"style_copyright\">");
                writer.Write(System.Web.HttpUtility.HtmlEncode("            Atebion Document Analyzer © Copyright 2014 by Atebion, LLC"));
                writer.Write("        </span>");
                writer.Write("    </p>");
                writer.Write("</body>");
                writer.Write("</html>");
            }
        }


        public void WriteHTML3(DataTable dt, string headerCaptions, string columnWidths, string pathFile, string DocType, string PropName, string PropIAm, string DocTypeIAm)
        {
            using (TextWriter writer = File.CreateText(pathFile))
            {
                writer.Write("<!DOCTYPE html>");
                writer.Write("    <title>Atebion Document Analyzer, Export Parsed Sections</title>");
                writer.Write("    <style type=\"text/css\">");
                writer.Write("        .style_header");
                writer.Write("        {");
                writer.Write("          font-family: verdana, helvetica, arial, sans-serif;");
                writer.Write("          font-size: 16px;");
                writer.Write("          margin-top: 10px;");
                writer.Write("          margin-bottom: 10px;");
                writer.Write("          font-weight: normal; ");
                writer.Write("          background-color: rgb(255, 255, 255);");
                writer.Write("          color: rgb(0, 0, 0); ");
                writer.Write("          font-style: normal; ");
                writer.Write("          font-variant: normal; ");
                writer.Write("          letter-spacing: normal; ");
                writer.Write("          line-height: normal; ");
                writer.Write("          orphans: 2; text-align: -webkit-auto; ");
                writer.Write("          text-indent: 0px; ");
                writer.Write("          text-transform: none; ");
                writer.Write("          white-space: normal; ");
                writer.Write("          word-spacing: 0px; ");
                writer.Write("          -webkit-text-size-adjust: auto; ");
                writer.Write("          -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table");
                writer.Write("        {");
                writer.Write("            border: 1px solid rgb(195, 195, 195); ");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; background-color: rgb(255, 255, 255); ");
                writer.Write("            border-collapse: collapse; ");
                writer.Write("            width: 1085px; ");
                writer.Write("            color: rgb(0, 0, 0); ");
                writer.Write("            font-style: normal; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: normal; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table_header");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            background-color: rgb(229, 238, 204); ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            vertical-align: top;");
                writer.Write("        }");
                writer.Write("        .style_table_data");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-top: 3px; ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            padding-left: 3px; ");
                writer.Write("            vertical-align: top;  ");
                writer.Write("        }");
                writer.Write("        .style_copyright");
                writer.Write("        {");
                writer.Write("            color: rgb(154, 166, 180); ");
                writer.Write("            font-family: Georgia, 'Times New Roman', Times, serif; ");
                writer.Write("            font-size: 11px; ");
                writer.Write("            font-style: italic; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: 19px; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px; ");
                writer.Write("            background-color: rgb(255, 255, 255); ");
                writer.Write("            display: inline !important; ");
                writer.Write("            float: none; ");
                writer.Write("        }");
                writer.Write("        </style>");
                writer.Write("</head>");
                writer.Write("<body>");
                writer.Write("    <h2 class=\"style_header\">");
                string docHeader = String.Concat("        ", PropIAm, ": ", PropName, "&nbsp;&nbsp;", DocTypeIAm, ": ", DocType);
                //writer.Write("        Proposal: Test1&nbsp;&nbsp; Section: C</h2>");
                writer.Write(docHeader);
                writer.Write("    <table class=\"style_table\" >");

                // ----------------- Table Header ------------------------
                writer.Write("         <tr>"); // Start Table Header Row

                // Get Table Captions
                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;

                string[] ColumnWidths = columnWidths.Split('|');
                //for (int i = 0; i < headerCount; i++)
                for (int i = 1; i < headerCount; i++) // Added UID, ignore -- 08.20.2014
                {
                    //writer.Write("            <th class=\"style_table_header\" width=\"10%\">");

                    int columnWidth = 1;
                    if (ColumnWidths[i] != string.Empty)
                        columnWidth = Convert.ToInt16(ColumnWidths[i]) * 10;

                    string tableHeader = string.Concat(@"            <th class=""style_table_header"" width="", """, columnWidth.ToString(), "%\">");
                    writer.Write(tableHeader);
                    //writer.Write("                Number");
                    string sHData = string.Concat("                ", Captions[i]);
                    writer.Write(sHData);
                    writer.Write("            </th>");
                }

                writer.Write("        </tr>"); // End Table Header Row

                // ----------------- Table Data ------------------------
                string sData = string.Empty;
                //  string htmlEncodedData = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    writer.Write("        <tr>"); // Start Table Data Row
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {

                        // htmlEncodedData = HttpContext.Current.Server.HtmlEncode(row[i].ToString());

                        writer.Write("            <td align=\"left\" class=\"style_table_data\"> ");
                        //writer.Write("                1");
                        if (row[i].ToString().Trim() == string.Empty)
                            sData = "                &nbsp;";
                        else
                        {
                                                        
                            if (dt.Columns[i].ColumnName == "Text")
                            {
                                sData = string.Concat("                ", row[i].ToString());

                            }
                            else
                            {

                                sData = string.Concat("                ", System.Web.HttpUtility.HtmlEncode(row[i].ToString()));

                                // The HtmlEncode does not fix all special char.s -- Added 06.06.2014
                                sData = sData.Replace("’", "&#39;"); // Such as ’s
                                sData = sData.Replace("“", "&#34;");
                                sData = sData.Replace("”", "&#34;");
                                sData = sData.Replace("\n", "");
                            }

                        }

                        writer.Write(sData); // Write Data
                        writer.Write("            </td>");
                    }
                    writer.Write("        </tr>"); // End Table Data Row
                }


                writer.Write("");
                writer.Write("    </table>");
                writer.Write("    <p>");
                writer.Write("        <span class=\"style_copyright\">");
                writer.Write(System.Web.HttpUtility.HtmlEncode("            Atebion Document Analyzer © Copyright 2014 by Atebion, LLC"));
                writer.Write("        </span>");
                writer.Write("    </p>");
                writer.Write("</body>");
                writer.Write("</html>");
            }
        }

        public void WriteHTML5(DataTable dt, string headerCaptions, string columnWidths, string pathFile, string DocType, string PropName, string PropIAm, string DocTypeIAm)
        {
            using (TextWriter writer = File.CreateText(pathFile))
            {
                writer.Write("<!DOCTYPE html>");
                writer.Write("    <title>Atebion Document Analyzer, Export Parsed Sections</title>");
                writer.Write("    <style type=\"text/css\">");
                writer.Write("        .style_header");
                writer.Write("        {");
                writer.Write("          font-family: verdana, helvetica, arial, sans-serif;");
                writer.Write("          font-size: 16px;");
                writer.Write("          margin-top: 10px;");
                writer.Write("          margin-bottom: 10px;");
                writer.Write("          font-weight: normal; ");
                writer.Write("          background-color: rgb(255, 255, 255);");
                writer.Write("          color: rgb(0, 0, 0); ");
                writer.Write("          font-style: normal; ");
                writer.Write("          font-variant: normal; ");
                writer.Write("          letter-spacing: normal; ");
                writer.Write("          line-height: normal; ");
                writer.Write("          orphans: 2; text-align: -webkit-auto; ");
                writer.Write("          text-indent: 0px; ");
                writer.Write("          text-transform: none; ");
                writer.Write("          white-space: normal; ");
                writer.Write("          word-spacing: 0px; ");
                writer.Write("          -webkit-text-size-adjust: auto; ");
                writer.Write("          -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table");
                writer.Write("        {");
                writer.Write("            border: 1px solid rgb(195, 195, 195); ");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; background-color: rgb(255, 255, 255); ");
                writer.Write("            border-collapse: collapse; ");
                writer.Write("            width: 1085px; ");
                writer.Write("            color: rgb(0, 0, 0); ");
                writer.Write("            font-style: normal; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: normal; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px;");
                writer.Write("        }");
                writer.Write("        .style_table_header");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            background-color: rgb(229, 238, 204); ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            vertical-align: top;");
                writer.Write("        }");
                writer.Write("        .style_table_data");
                writer.Write("        {");
                writer.Write("            font-family: verdana, helvetica, arial, sans-serif; ");
                writer.Write("            font-size: 12px; ");
                writer.Write("            border-top-width: 1px; ");
                writer.Write("            border-right-width: 1px; ");
                writer.Write("            border-bottom-width: 1px; ");
                writer.Write("            border-left-width: 1px; ");
                writer.Write("            border-top-style: solid; ");
                writer.Write("            border-right-style: solid; ");
                writer.Write("            border-bottom-style: solid; ");
                writer.Write("            border-left-style: solid; ");
                writer.Write("            border-top-color: rgb(195, 195, 195); ");
                writer.Write("            border-right-color: rgb(195, 195, 195); ");
                writer.Write("            border-bottom-color: rgb(195, 195, 195); ");
                writer.Write("            border-left-color: rgb(195, 195, 195); ");
                writer.Write("            padding-top: 3px; ");
                writer.Write("            padding-right: 3px; ");
                writer.Write("            padding-bottom: 3px; ");
                writer.Write("            padding-left: 3px; ");
                writer.Write("            vertical-align: top;  ");
                writer.Write("        }");
                writer.Write("        .style_copyright");
                writer.Write("        {");
                writer.Write("            color: rgb(154, 166, 180); ");
                writer.Write("            font-family: Georgia, 'Times New Roman', Times, serif; ");
                writer.Write("            font-size: 11px; ");
                writer.Write("            font-style: italic; ");
                writer.Write("            font-variant: normal; ");
                writer.Write("            font-weight: normal; ");
                writer.Write("            letter-spacing: normal; ");
                writer.Write("            line-height: 19px; ");
                writer.Write("            orphans: 2; ");
                writer.Write("            text-align: -webkit-auto; ");
                writer.Write("            text-indent: 0px; ");
                writer.Write("            text-transform: none; ");
                writer.Write("            white-space: normal; ");
                writer.Write("            word-spacing: 0px; ");
                writer.Write("            -webkit-text-size-adjust: auto; ");
                writer.Write("            -webkit-text-stroke-width: 0px; ");
                writer.Write("            background-color: rgb(255, 255, 255); ");
                writer.Write("            display: inline !important; ");
                writer.Write("            float: none; ");
                writer.Write("        }");
                writer.Write("        </style>");
                writer.Write("</head>");
                writer.Write("<body>");
                writer.Write("    <h2 class=\"style_header\">");
                string docHeader = String.Concat("        ", PropIAm, ": ", PropName, "&nbsp;&nbsp;", DocTypeIAm, ": ", DocType);
                //writer.Write("        Proposal: Test1&nbsp;&nbsp; Section: C</h2>");
                writer.Write(docHeader);
                writer.Write("    <table class=\"style_table\" >");

                // ----------------- Table Header ------------------------
                writer.Write("         <tr>"); // Start Table Header Row

                // Get Table Captions
                string[] Captions = headerCaptions.Split('|');
                int headerCount = Captions.Length;

                string[] ColumnWidths = columnWidths.Split('|');
                for (int i = 0; i < headerCount; i++)
                //for (int i = 1; i < headerCount; i++) // Added UID, ignore -- 08.20.2014
                {
                    //writer.Write("            <th class=\"style_table_header\" width=\"10%\">");

                    int columnWidth = 1;
                    if (ColumnWidths[i] != string.Empty)
                        columnWidth = Convert.ToInt16(ColumnWidths[i]) * 10;

                    string tableHeader = string.Concat(@"            <th class=""style_table_header"" width="", """, columnWidth.ToString(), "%\">");
                    writer.Write(tableHeader);
                    //writer.Write("                Number");
                    string sHData = string.Concat("                ", Captions[i]);
                    writer.Write(sHData);
                    writer.Write("            </th>");
                }

                writer.Write("        </tr>"); // End Table Header Row

                // ----------------- Table Data ------------------------
                string sData = string.Empty;
                //  string htmlEncodedData = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    writer.Write("        <tr>"); // Start Table Data Row
                    for (int i = 0; i < dt.Columns.Count; i++)
                    // for (int 0 = 1; i < dt.Columns.Count; i++)
                    {

                        // htmlEncodedData = HttpContext.Current.Server.HtmlEncode(row[i].ToString());

                        writer.Write("            <td align=\"left\" class=\"style_table_data\"> ");
                        //writer.Write("                1");
                        if (row[i].ToString().Trim() == string.Empty)
                            sData = "                &nbsp;";
                        else
                        {

                            if (dt.Columns[i].ColumnName == "Text")
                            {
                                sData = string.Concat("                ", row[i].ToString());

                            }
                            else
                            {

                                sData = string.Concat("                ", System.Web.HttpUtility.HtmlEncode(row[i].ToString()));

                                // The HtmlEncode does not fix all special char.s -- Added 06.06.2014
                                sData = sData.Replace("’", "&#39;"); // Such as ’s
                                sData = sData.Replace("“", "&#34;");
                                sData = sData.Replace("”", "&#34;");
                                sData = sData.Replace("\n", "");
                            }

                        }

                        writer.Write(sData); // Write Data
                        writer.Write("            </td>");
                    }
                    writer.Write("        </tr>"); // End Table Data Row
                }


                writer.Write("");
                writer.Write("    </table>");
                writer.Write("    <p>");
                writer.Write("        <span class=\"style_copyright\">");
                writer.Write(System.Web.HttpUtility.HtmlEncode("            Atebion Document Analyzer © Copyright 2015 by Atebion, LLC"));
                writer.Write("        </span>");
                writer.Write("    </p>");
                writer.Write("</body>");
                writer.Write("</html>");
            }
        }
    }
}
