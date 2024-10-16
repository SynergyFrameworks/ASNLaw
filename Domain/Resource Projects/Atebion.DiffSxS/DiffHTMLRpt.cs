using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Net;

using Atebion.Common;

namespace Atebion.DiffSxS
{
    public class DiffHTMLRpt
    {
        private const string TAB = "\t";
        private const string QUOTE = "\"";
        private string LINE = string.Concat(TAB, "<hr width=", QUOTE, "100%", QUOTE, "/>");

        private StringBuilder _sb = new StringBuilder();

        private TextWriter _writer;

        // Folders
        private string _CompareDirNotes = string.Empty;
        private string _CompareDirNotesHTML = string.Empty;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }


        public int CovertNotes2HTML(string CompareDirNotes, string CompareDirNotesHTML)
        {
            _CompareDirNotes = CompareDirNotes;
            _CompareDirNotesHTML = CompareDirNotesHTML;

            // Convert Notes RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            int qtyConverted = convert.ConvertFiles(_CompareDirNotes, _CompareDirNotesHTML);

            if (convert.ErrorMessage != string.Empty)
            {
                _ErrorMessage = convert.ErrorMessage;
                return -1;
            }

            return qtyConverted;
        }


        public void StartRpt_1(string pathFile)
        {
            _writer = File.CreateText(pathFile);

            _writer.WriteLine(string.Concat("<!DOCTYPE HTML PUBLIC ", QUOTE, "-//W3C//DTD HTML 4.0 Transitional//EN", QUOTE, ">"));
            _writer.WriteLine("<html>");
            _writer.WriteLine(string.Concat(TAB, "<head>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<title>Scion Analytics, Diff SxS Report</title>"));

        }

        public void GenRtpStyles_2()
        {
            _writer.WriteLine(string.Concat(TAB, TAB, "<style type=", QUOTE, "text/css", QUOTE, ">"));
            _writer.WriteLine(@"/***************** Diff related styles *****************/");
            _writer.WriteLine(" ");
            _writer.WriteLine("ins {");
            _writer.WriteLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            _writer.WriteLine(TAB + " background-color: #cfc;");
            _writer.WriteLine(TAB + " text-decoration:underline;");
            _writer.WriteLine("}");
            _writer.WriteLine(" ");
            _writer.WriteLine("del {");
            _writer.WriteLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            _writer.WriteLine(TAB + " color:black;");
            _writer.WriteLine(TAB + " background-color:#FEC8C8;");
            _writer.WriteLine("}");
            _writer.WriteLine(" ");
            _writer.WriteLine("ins.mod {");
            _writer.WriteLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            _writer.WriteLine(TAB + " background-color: #FFE1AC;");
            _writer.WriteLine("}");
            _writer.WriteLine(" ");
            _writer.WriteLine(".diffmod {");
            _writer.WriteLine(TAB + " font-family: verdana, helvetica, arial, sans-serif;");
            _writer.WriteLine(TAB + " font-size: 12px;");
            _writer.WriteLine("}");
            _writer.WriteLine(" ");
            _writer.WriteLine(@"/***************** Detail Table - Change Type styles *****************/");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_unchanged"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color:white;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 10%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_modified"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color:yellow;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 10%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_added"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color:lightgreen;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 10%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_removed"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color:tomato;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 10%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(@"/***************** Detail Table styles *****************/");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_lineno"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 5%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:left;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 30%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style_table_data_notes"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: verdana, helvetica, arial, sans-serif;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align:left;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "width: 25%;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));


            _writer.WriteLine(@"/***************** Report styles *****************/");
            //style_table_data_caption
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
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family:", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: large;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style6"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: x-small;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style7"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: ", QUOTE, "Segoe UI", QUOTE, ";"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: medium;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align: right;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".style8"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-family: Segoe UI;"));
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
            _writer.WriteLine(" ");
            _writer.WriteLine(@"/***************** List Line Numbers styles *****************/");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "ul.tab"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "{"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "list-style-type: none;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "margin: 0;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "padding: 0;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "overflow: hidden;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "border: 1px solid #ccc;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "background-color: #f1f1f1;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "/* Float the list items side by side */"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "ul.tab li {float: left;}"));
            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "ul.tab li a {"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "display: inline-block;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "color: black;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-align: center;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "padding: 14px 16px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "text-decoration: none;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "transition: 0.3s;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "font-size: 14px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));
            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "/* Change background color of links on hover */"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "ul.tab li a:hover {background-color: #ddd;}"));
            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "/* Create an active/current tablink class */"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "ul.tab li a:focus, .active {background-color:white;}"));
            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "/* Style the tab content */"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, ".tabcontent {"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "display: none;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "padding: 6px 12px;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "border: 1px solid #ccc;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "border-top: none;"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "}"));


            _writer.WriteLine(" ");
            _writer.WriteLine(string.Concat(TAB, TAB, "</style>"));
            _writer.WriteLine(string.Concat(TAB, "</head>"));
        }

        public void GenRtpHeader_2(string OldFile, string NewFile)
        {
            _writer.WriteLine(string.Concat(TAB, "<body>"));
            _writer.WriteLine(string.Concat(TAB, "<!-- Header Area -->"));

            _writer.WriteLine(string.Concat(TAB, TAB, "<p>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, @"<span class=", QUOTE, "style5", QUOTE, ">Scion Analytics - Document Analyzer's Diff SxS Report</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</p>"));

            _writer.WriteLine(string.Concat(TAB, TAB, "<p>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, @"<span class=", QUOTE, "style8", QUOTE, ">File 1: ", OldFile, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</p>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "<p>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, @"<span class=", QUOTE, "style8", QUOTE, ">File 2: ", NewFile, "</span>"));
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

        public void GenSumQty_4(string Qty, string BlockColor, string Caption)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, TAB, "<!-- Block Summary Qtys -->"));
            CreateSummaryBlockItem(Qty, BlockColor, string.Empty, Caption);
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

        public void EndsumBlockArea_5()
        {

            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, "</tr>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "</tbody>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</table>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</center>"));
            _writer.WriteLine(string.Concat(TAB, TAB, " "));
            _writer.WriteLine(string.Concat(TAB, TAB, "<hr width=", QUOTE, "100%", QUOTE, "/>"));

        }

        public void GenChangeLines_6(ArrayList linesChanged)
        {
            _writer.WriteLine(string.Concat(TAB, " "));
            _writer.WriteLine(string.Concat(TAB, "<!-- Changed Lines -->"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Changed Lines</span>"));
            _writer.WriteLine(string.Concat(TAB, "<ul class=", QUOTE, "tab", QUOTE, ">"));

            foreach (int lineNo in linesChanged) // loop through each line number that changed
            {
                writeChangeLineNo(lineNo.ToString("00000"));
            }

            _writer.WriteLine(string.Concat(TAB, "</ul>"));
            _writer.WriteLine(string.Concat(TAB, " "));
            _writer.WriteLine(string.Concat(TAB, "<hr width=", QUOTE, "100% ", QUOTE, "/>"));

        }

        private void writeChangeLineNo(string lineNo)
        {
            _writer.WriteLine(string.Concat(TAB, TAB, "<li><a href=", QUOTE, "#", lineNo, QUOTE, " class=", QUOTE, "style9", QUOTE, " onclick=", QUOTE, "GoToLine(event, '", lineNo, "')", QUOTE, ">", lineNo, "</a></li>"));
        }



        private string _ModDir = string.Empty;
        public void GenDetails_7(DataTable dt, string ModDir)
        {
            _ModDir = ModDir;

            _writer.WriteLine(string.Concat(TAB, TAB, " "));
            _writer.WriteLine(string.Concat(TAB, "<!-- Details -->"));
            _writer.WriteLine(string.Concat(TAB, "<span class=", QUOTE, "style1", QUOTE, ">Document Comparision</span>"));
            _writer.WriteLine(string.Concat(TAB, "<table cellpadding=", QUOTE, "10", QUOTE, "cellspacing=", QUOTE, "10", QUOTE, ">"));

            _writer.WriteLine(string.Concat(TAB, TAB, "<tr class=", QUOTE, "style_table_data_caption", QUOTE, ">"));

            // Caption - Line
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td >"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">Line</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td >"));

            // Caption - Status
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td >"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">Status</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td >"));

            // Caption - Old File
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td >"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">File 1</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td >"));

            // Caption - New File
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td >"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">File 2</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td >"));

            // Caption - Notes
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td >"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">Notes</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td >"));

            _writer.WriteLine(string.Concat(TAB, TAB, "</tr>"));

            foreach (DataRow row in dt.Rows)
            {
                writeLineData(row);
            }

            _writer.WriteLine(string.Concat(TAB, "</table>"));
        }

        private void writeLineData(DataRow row)
        {
            string lineNo = row[ResultsFields.LineNo].ToString();
            _writer.WriteLine(string.Concat(TAB, TAB, "<tr id=", QUOTE, lineNo, QUOTE, ">"));

            // Line No.
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td class=", QUOTE, "style_table_data_lineno", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span>", lineNo, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));

            // Change Type
            string changeType = row[ResultsFields.ChangeType].ToString();
            if (changeType == "Modified")
            {
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data_modified", QUOTE, ">"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", changeType, "</span>"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));

                string file = string.Concat(Convert.ToInt32(lineNo).ToString(), ".html");
                string pathFile = Path.Combine(_ModDir, file);
                if (File.Exists(pathFile)) // Insert Modified text markup
                {
                    string modText = Files.ReadFile(pathFile);

                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data", QUOTE, ">"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<p> ", modText, " </p>"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));
                }
                else // Error
                {
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data", QUOTE, ">"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<p> Error: Unable to locate file: ", pathFile, " </p>"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));
                }

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data", QUOTE, ">"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<p> ", " ", " </p>"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));

            }
            else
            {
                if (changeType == "Inserted")
                {
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data_added", QUOTE, ">"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", changeType, "</span>"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));
                }
                else if (changeType == "Deleted")
                {
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data_removed", QUOTE, ">"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", changeType, "</span>"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));
                }
                else if (changeType == "Unchanged")
                {
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data_unchanged", QUOTE, ">"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style9", QUOTE, ">", changeType, "</span>"));
                    _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));
                }

                string oldText = row[ResultsFields.OldText].ToString();
                string newText = row[ResultsFields.NewText].ToString();

                oldText = CleanHTML2(oldText);
                newText = CleanHTML2(newText);

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data", QUOTE, ">"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<p> ", oldText, " </p>"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));

                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<td align=", QUOTE, "center", QUOTE, " class=", QUOTE, "style_table_data", QUOTE, ">"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<p> ", newText, " </p>"));
                _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</td>"));

            }

            // Notes
            int nLineNo = Convert.ToInt32(lineNo);
            string textHTML = getNotesHTML(nLineNo.ToString());
            //if (textHTML != string.Empty)
            //{
            //    textHTML = CleanHTML2(textHTML);
            //}
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<td class=", QUOTE, "style_table_data_notes", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, textHTML));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "</td>"));


            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</tr>")); // End Table Row

        }

        private string getNotesHTML(string lineNo)
        {
            string returnHTML = string.Empty;

            string file = string.Concat(Convert.ToInt32(lineNo).ToString(), ".html");
            string pathFile = Path.Combine(_CompareDirNotesHTML, file); 
            if (File.Exists(pathFile)) // Insert Modified text markup
            {
                returnHTML = Files.ReadFile(pathFile);
            }

            return returnHTML;
        }

        //private string CleanHTML(string txt)
        //{
        //    string textHTML;
        //    textHTML = System.Web.HttpUtility.HtmlEncode(txt);
        //    textHTML = txt.Replace("’", "&#39;"); // Such as ’s
        //    textHTML = textHTML.Replace("“", "&#34;");
        //    textHTML = textHTML.Replace("”", "&#34;");
        //    textHTML = textHTML.Replace("\n", "");

        //    return textHTML;

        //}

        private string CleanHTML2(string txt)
        {
            // call the normal HtmlEncode first
            char[] chars = HttpUtility.HtmlEncode(txt).ToCharArray();
            StringBuilder encodedValue = new StringBuilder();
            foreach (char c in chars)
            {
                if ((int)c > 127) // above normal ASCII
                    encodedValue.Append("&#" + (int)c + ";");
                else
                    encodedValue.Append(c);
            }
            return encodedValue.ToString();

        }

        public void End_8()
        {
            DateTime dt = DateTime.Now;

            _writer.WriteLine(string.Concat(TAB, TAB, "<hr width=", QUOTE, "100% ", QUOTE, "/>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<!-- Footer -->"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "<div class=", QUOTE, "style7", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "<span class=", QUOTE, "style6", QUOTE, ">"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, TAB, "This report was generated by Document Analyzer's Diff SxS, Copyright ", dt.Year.ToString(), " by Atebion, LLC"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, TAB, "</span>"));
            _writer.WriteLine(string.Concat(TAB, TAB, TAB, "</div>"));
            _writer.WriteLine(string.Concat(TAB, TAB, "</body>"));
            _writer.WriteLine(string.Concat(TAB, "</html>"));

            _writer.Close();
            _writer.Dispose();

        }


    }
}
