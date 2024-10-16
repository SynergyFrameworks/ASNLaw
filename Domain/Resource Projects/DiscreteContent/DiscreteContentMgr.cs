using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

using Atebion.Common;
using Atebion.DiscreteContent;

namespace Atebion.ConceptAnalyzer
{
    public class DiscreteContentMgr
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private DataSet CreateEmpty_EmailsContent_DataTable()
        {
            DataSet dsContent = new System.Data.DataSet();

            DataTable table = new DataTable(DiscreteContentFields.TableName_Emails);

            table.Columns.Add(DiscreteContentFields.UID, typeof(int));
            table.Columns.Add(DiscreteContentFields.SegmentUID, typeof(int));
            table.Columns.Add(DiscreteContentFields.Content, typeof(string));

            dsContent.Tables.Add(table);

            return dsContent;
        }

        private DataSet CreateEmpty_URLsContent_DataTable()
        {
            DataSet dsContent = new System.Data.DataSet();

            DataTable table = new DataTable(DiscreteContentFields.TableName_URLs);

            table.Columns.Add(DiscreteContentFields.UID, typeof(int));
            table.Columns.Add(DiscreteContentFields.SegmentUID, typeof(int));
            table.Columns.Add(DiscreteContentFields.Content, typeof(string));

            dsContent.Tables.Add(table);

            return dsContent;
        }

        private DataSet CreateEmpty_DatesContent_DataTable()
        {
            DataSet dsContent = new System.Data.DataSet();

            DataTable table = new DataTable(DiscreteContentFields.TableName_Dates);

            table.Columns.Add(DiscreteContentFields.UID, typeof(int));
            table.Columns.Add(DiscreteContentFields.SegmentUID, typeof(int));
            table.Columns.Add(DiscreteContentFields.Content, typeof(string));

            dsContent.Tables.Add(table);

            return dsContent;
        }

        public int FindDiscreteContent(string AnalysisName, string DocumentName, string DocXMLPath, string DocParseSegPath, bool FindEmails, bool FindDates, bool FindURLs, out int EmailQTY, out int DateQty, out int URLQTY)
        {
            int count = 0;
            EmailQTY = 0;
            DateQty = 0;
            URLQTY = 0;

            _ErrorMessage = string.Empty;

            Atebion.DiscreteContent.DiscreteContent DiscContentMgr = new DiscreteContent.DiscreteContent();

            DataSet dsEmails;
            DataSet dsDates;
            DataSet dsURLs;
   
            dsEmails = CreateEmpty_EmailsContent_DataTable(); 
            dsDates = CreateEmpty_DatesContent_DataTable();
            dsURLs = CreateEmpty_URLsContent_DataTable();  

            string ParseResultsFile = Path.Combine(DocXMLPath, ParseResultsFields.XMLFile);
            if (!File.Exists(ParseResultsFile))
            {
                _ErrorMessage = string.Concat("Unable to find the Parse Segments/Paragraphs file: ", ParseResultsFile);
                return count;
            }

            DataSet dsParseResults = Files.LoadDatasetFromXml(ParseResultsFile);

            string txt = string.Empty;
            string uid = string.Empty;
            string segFile = string.Empty;
            string segPathFile = string.Empty;

            string[] emails;
            string[] dates;
            string[] urls;

            int email_UID = 0;
            int date_UID = 0;
            int url_UID = 0;

            DataRow newRow;

            RichTextBox rtfCrtl = new RichTextBox();


            foreach (DataRow row in dsParseResults.Tables[0].Rows)
            {
                uid = row["UID"].ToString();

                segFile = string.Concat(uid, ".rtf");
                segPathFile = Path.Combine(DocParseSegPath, segFile);
                if (!File.Exists(segPathFile))
                {
                    _ErrorMessage = string.Concat("Unable to find the Parse Segment/Paragraph file: ", segPathFile);
                    return -1;
                }

               
                // Read Parse Segment text
                rtfCrtl.LoadFile(segPathFile, System.Windows.Forms.RichTextBoxStreamType.RichText);
                txt = rtfCrtl.Text;

                // Find Emails
                if (FindEmails)
                {
                    emails = DiscContentMgr.FindEmails(txt);
                    if (emails.Length > 0)
                    {
                        for (int i = 0; i < emails.Length; i++)
                        {
                            newRow = dsEmails.Tables[0].NewRow();

                            newRow[DiscreteContentFields.UID] = email_UID;
                            newRow[DiscreteContentFields.SegmentUID] = uid;
                            newRow[DiscreteContentFields.Content] = emails[i];

                            dsEmails.Tables[0].Rows.Add(newRow);
                            email_UID++;
                        }
                        EmailQTY = EmailQTY + emails.Length;
                    }
                }

                // Find Dates
                if (FindDates)
                {
                    dates = DiscContentMgr.FindDates(txt);
                    if (dates.Length > 0)
                    {
                        for (int i = 0; i < dates.Length; i++)
                        {
                            newRow = dsDates.Tables[0].NewRow();

                            newRow[DiscreteContentFields.UID] = date_UID;
                            newRow[DiscreteContentFields.SegmentUID] = uid;
                            newRow[DiscreteContentFields.Content] = dates[i];

                            dsDates.Tables[0].Rows.Add(newRow);
                            date_UID++;
                        }
                        DateQty = DateQty + dates.Length;
                    }
                }

                // Find ULRs
                if (FindURLs)
                {
                    urls = DiscContentMgr.FindURLs(txt);
                    if (urls.Length > 0)
                    {
                        for (int i = 0; i < urls.Length; i++)
                        {
                            newRow = dsURLs.Tables[0].NewRow();

                            newRow[DiscreteContentFields.UID] = url_UID;
                            newRow[DiscreteContentFields.SegmentUID] = uid;
                            newRow[DiscreteContentFields.Content] = urls[i];

                            dsURLs.Tables[0].Rows.Add(newRow);
                            url_UID++;
                        }
                        URLQTY = URLQTY + urls.Length;
                    }
                }
            }

            count = URLQTY + DateQty + EmailQTY;

            GenericDataManger gdManager = new GenericDataManger();


            // Save Emails found
            if (FindEmails)
            {
                string xmlEmailPathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_Emails);
                gdManager.SaveDataXML(dsEmails, xmlEmailPathFile);
            }

            // Save Dates found
            if (FindDates)
            {
                string xmlDatePathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_Dates);
                gdManager.SaveDataXML(dsDates, xmlDatePathFile);
            }

            // Save URLs found
            if (FindURLs)
            {
                string xmlURLPathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_URLs);
                gdManager.SaveDataXML(dsURLs, xmlURLPathFile);
            }
            

            return count;
        }
   

    }
}
