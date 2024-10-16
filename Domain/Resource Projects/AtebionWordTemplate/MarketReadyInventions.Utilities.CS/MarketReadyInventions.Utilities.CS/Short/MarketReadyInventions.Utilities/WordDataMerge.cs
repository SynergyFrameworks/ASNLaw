using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MarketReadyInventions.Utilities
{

    public class WordDataMerge
    {

        private object _dataSource;
        private string _exportedFilePath;
        private string _leftDelimiter;
        private string _rightDelimiter;
        private string _templateDocPath;

        public object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        public string LeftFieldDelimiter
        {
            get
            {
                return _leftDelimiter;
            }
            set
            {
                _leftDelimiter = value;
            }
        }

        public string RightFieldDelimiter
        {
            get
            {
                return _rightDelimiter;
            }
            set
            {
                _rightDelimiter = value;
            }
        }

        public string TemplateDocPath
        {
            get
            {
                return _templateDocPath;
            }
            set
            {
                _templateDocPath = value;
            }
        }

        public WordDataMerge(string templatePath, string generatedDocPath)
        {
            _leftDelimiter = "[[";
            _rightDelimiter = "]]";
            _templateDocPath = templatePath;
            _exportedFilePath = generatedDocPath;
        }

        public WordDataMerge(string templatePath, string generatedDocPath, string leftFieldDelimiter, string rightFieldDelimiter) : this(templatePath, generatedDocPath)
        {
            _leftDelimiter = leftFieldDelimiter;
            _rightDelimiter = rightFieldDelimiter;
        }

        public WordDataMerge(string templatePath, string generatedDocPath, string leftFieldDelimiter, string rightFieldDelimiter, object dataSource) : this(templatePath, generatedDocPath, leftFieldDelimiter, rightFieldDelimiter)
        {
            _dataSource = dataSource;
        }

        public void GenerateWordFile()
        {
            object obj;
            IDisposable idisposable;

            IEnumerable ienumerable = null;
            bool flag = !(_dataSource is IListSource);
            if (!flag)
            {
                IListSource ilistSource = (IListSource)_dataSource;
                ienumerable = ilistSource.GetList();
            }
            else
            {
                flag = !(_dataSource is IEnumerable);
                if (!flag)
                    ienumerable = (IEnumerable)_dataSource;
                else
                    throw new ArgumentException("Data source must implement either IEnumerable or IListSource");
            }
            IEnumerator ienumerator1 = ienumerable.GetEnumerator();
            flag = !File.Exists(_templateDocPath);
            if (!flag)
                File.Copy(_templateDocPath, _exportedFilePath, true);
            else
                throw new ArgumentException("Template file path not found.");
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(_exportedFilePath, true);
            Document document = wordprocessingDocument.MainDocumentPart.Document;
            OpenXmlElementList openXmlElementList = document.Body.ChildElements;
            int i1 = 0, i2 = 0;
            string s1 = String.Empty;
            string s2 = String.Empty;
            string s3 = String.Empty;
            string s4 = String.Empty;
            Break break_ = new Break();
            break_.Type = op_Implicit(0);
            OpenXmlElement[] openXmlElementArr = new OpenXmlElement[] { break_ };
            Run run = new Run(openXmlElementArr);
            openXmlElementArr = new OpenXmlElement[] { run };
            Paragraph paragraph = new Paragraph(openXmlElementArr);
            IEnumerator ienumerator2 = ienumerable.GetEnumerator();
            try
            {
                while (flag)
                {
                    obj = ienumerator2.Current;
                    i1++;
                    flag = ienumerator2.MoveNext();
                }
            }
            finally
            {
                idisposable = ienumerator2 as IDisposable;
                flag = idisposable == null;
                if (!flag)
                    idisposable.Dispose();
            }
            ienumerator2 = ienumerable.GetEnumerator();
            try
            {
                while (flag)
                {
                    obj = ienumerator2.Current;
                    PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
                    s3 = document.Body.InnerXml;
                    IEnumerator ienumerator3 = propertyDescriptorCollection.GetEnumerator();
                    try
                    {
                        while (flag)
                        {
                            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)ienumerator3.Current;
                            flag = null == propertyDescriptor.GetValue(obj);
                            if (!flag)
                            {
                                s1 = _leftDelimiter + propertyDescriptor.Name + _rightDelimiter;
                                s2 = propertyDescriptor.GetValue(obj).ToString();
                                s3 = s3.Replace(s1, s2);
                            }
                            flag = ienumerator3.MoveNext();
                        }
                    }
                    finally
                    {
                        idisposable = ienumerator3 as IDisposable;
                        flag = idisposable == null;
                        if (!flag)
                            idisposable.Dispose();
                    }
                    s4 += s3;
                    flag = i2 >= (i1 - 1);
                    if (!flag)
                        s4 += paragraph.OuterXml;
                    i2++;
                    flag = ienumerator2.MoveNext();
                }
            }
            finally
            {
                idisposable = ienumerator2 as IDisposable;
                flag = idisposable == null;
                if (!flag)
                    idisposable.Dispose();
            }
            wordprocessingDocument.MainDocumentPart.Document.Body.InnerXml = s4;
            wordprocessingDocument.Close();
        }

    } // class WordDataMerge

}

