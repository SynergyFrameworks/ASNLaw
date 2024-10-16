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
            System.IDisposable idisposable;

            System.Collections.IEnumerable ienumerable = null;
            bool flag = !(_dataSource is System.ComponentModel.IListSource);
            if (!flag)
            {
                System.ComponentModel.IListSource ilistSource = (System.ComponentModel.IListSource)_dataSource;
                ienumerable = ilistSource.GetList();
            }
            else
            {
                flag = !(_dataSource is System.Collections.IEnumerable);
                if (!flag)
                    ienumerable = (System.Collections.IEnumerable)_dataSource;
                else
                    throw new System.ArgumentException("Data source must implement either IEnumerable or IListSource");
            }
            System.Collections.IEnumerator ienumerator1 = ienumerable.GetEnumerator();
            flag = !System.IO.File.Exists(_templateDocPath);
            if (!flag)
                System.IO.File.Copy(_templateDocPath, _exportedFilePath, true);
            else
                throw new System.ArgumentException("Template file path not found.");
            DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(_exportedFilePath, true);
            DocumentFormat.OpenXml.Wordprocessing.Document document = wordprocessingDocument.MainDocumentPart.Document;
            DocumentFormat.OpenXml.OpenXmlElementList openXmlElementList = document.Body.ChildElements;
            int i1 = 0, i2 = 0;
            string s1 = System.String.Empty;
            string s2 = System.String.Empty;
            string s3 = System.String.Empty;
            string s4 = System.String.Empty;
            DocumentFormat.OpenXml.Wordprocessing.Break break_ = new DocumentFormat.OpenXml.Wordprocessing.Break();
            break_.Type = op_Implicit(0);
            DocumentFormat.OpenXml.OpenXmlElement[] openXmlElementArr = new DocumentFormat.OpenXml.OpenXmlElement[] { break_ };
            DocumentFormat.OpenXml.Wordprocessing.Run run = new DocumentFormat.OpenXml.Wordprocessing.Run(openXmlElementArr);
            openXmlElementArr = new DocumentFormat.OpenXml.OpenXmlElement[] { run };
            DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(openXmlElementArr);
            System.Collections.IEnumerator ienumerator2 = ienumerable.GetEnumerator();
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
                idisposable = ienumerator2 as System.IDisposable;
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
                    System.ComponentModel.PropertyDescriptorCollection propertyDescriptorCollection = System.ComponentModel.TypeDescriptor.GetProperties(obj);
                    s3 = document.Body.InnerXml;
                    System.Collections.IEnumerator ienumerator3 = propertyDescriptorCollection.GetEnumerator();
                    try
                    {
                        while (flag)
                        {
                            System.ComponentModel.PropertyDescriptor propertyDescriptor = (System.ComponentModel.PropertyDescriptor)ienumerator3.Current;
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
                        idisposable = ienumerator3 as System.IDisposable;
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
                idisposable = ienumerator2 as System.IDisposable;
                flag = idisposable == null;
                if (!flag)
                    idisposable.Dispose();
            }
            wordprocessingDocument.MainDocumentPart.Document.Body.InnerXml = s4;
            wordprocessingDocument.Close();
        }

    } // class WordDataMerge

}

