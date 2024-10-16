using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using ESCommon.Rtf;
using ESCommon;

namespace Atebion.RTF.Generation
{
    public class RTFGen
    {
        public void writeRTF(DataTable dt, string headerCaptions, string columnWidths, string pathFile, string DocType, string PropName, string PropIAm, string DocTypeIAm)
        {
            RtfDocument rtf = new RtfDocument();
            rtf.FontTable.Add(new RtfFont("Calibri"));
            rtf.FontTable.Add(new RtfFont("Constantia"));

            RtfParagraphFormatting LeftAligned12 = new RtfParagraphFormatting(12, RtfTextAlign.Left);
            RtfParagraphFormatting LeftAligned10 = new RtfParagraphFormatting(10, RtfTextAlign.Left);
            RtfParagraphFormatting Centered10 = new RtfParagraphFormatting(10, RtfTextAlign.Center);
            RtfParagraphFormatting Centered12 = new RtfParagraphFormatting(12, RtfTextAlign.Center);
            

            RtfFormattedParagraph header = new RtfFormattedParagraph(new RtfParagraphFormatting(16, RtfTextAlign.Center));
            RtfFormattedParagraph p1 = new RtfFormattedParagraph(new RtfParagraphFormatting(12, RtfTextAlign.Left));

            header.Formatting.SpaceAfter = TwipConverter.ToTwip(12F, MetricUnit.Point);
            header.AppendText(new RtfFormattedText(String.Concat(PropIAm, ": ", PropName, "  ", DocTypeIAm, ": ", DocType), RtfCharacterFormatting.Bold));



            // Get Table Captions
            string[] Captions = headerCaptions.Split('|');
            int headerCount = Captions.Length;

            int recordCount = dt.Rows.Count;

            // Create Table
            RtfTable t = new RtfTable(RtfTableAlign.Left, headerCount, recordCount + 1); // recordCount + 1 for Table Header

            // Set Column Widths
            string[] ColumnWidths = columnWidths.Split('|');
            for (int i = 0; i < ColumnWidths.Length; i++)
            {
                int x = Convert.ToInt32(ColumnWidths[i]);
                t.Columns[i].Width = TwipConverter.ToTwip(x, MetricUnit.Centimeter);
            }


            // Insert Table Header
            for (int i = 0; i < Captions.Length; i++)
            {
                string s = Captions[i];

                t[i, 0].Definition.Style = new RtfTableCellStyle(RtfBorderSetting.All, Centered10, RtfTableCellVerticalAlign.Center);
                t[i, 0].AppendText(new RtfFormattedText(s, RtfCharacterFormatting.Bold));
            }

            int rowCounter = 1; // row is Zero Base, but we added a row already for the Table Header
            // Intert Data into table
            foreach (DataRow row in dt.Rows)
            {
                
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string s = row[i].ToString();

                    t[i, rowCounter].Definition.Style = new RtfTableCellStyle(RtfBorderSetting.All, LeftAligned10, RtfTableCellVerticalAlign.Center);

                    t[i, rowCounter].AppendText(new RtfFormattedText(s, RtfCharacterFormatting.Regular));
                }

                rowCounter++;
            }

            rtf.Contents.AddRange(new RtfDocumentContentBase[] {
                header,
                t,
                p1,
            });

            using (TextWriter writer = new StreamWriter(pathFile))
            {
                RtfWriter rtfWriter = new RtfWriter();

                rtfWriter.Write(writer, rtf);
            }


        }
    }
}
