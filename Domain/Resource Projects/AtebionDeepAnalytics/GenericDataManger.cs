using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace test
{
    class GenericDataManger
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage { get { return _ErrorMessage; } }

        public void SaveDataXML(DataSet ds, string pathFile)
        {
            try
            {
                ds.WriteXml(pathFile, XmlWriteMode.WriteSchema);
            }
            catch (Exception e)
            {
                _ErrorMessage = string.Concat("Saving Data an XML file: ", pathFile, " -- Error: ", e.ToString());
            }

        }

        public DataSet LoadDatasetFromXml(string fileName)
        {
            DataSet ds = new DataSet();
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fs))
                {
                    ds.ReadXml(reader);
                }
            }
            catch (Exception e)
            {
                _ErrorMessage = string.Concat("Loading Data From XML file: ", fileName, " -- Error: ", e.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return ds;
        }


        public bool IsFieldValuesUnique(DataTable dt, string FieldName, out string problemValue)
        {
            string ChkValue = string.Empty;
            int valuesFound = 0;
            string select = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                ChkValue = row[FieldName].ToString();
                valuesFound = 0;
                select = string.Concat(FieldName, " = '", ChkValue, "'");
                foreach (DataRow dr in dt.Select(select))
                {
                    valuesFound++;
                }

                if (valuesFound > 1)
                {
                    problemValue = string.Format("Found {0} for {1}", valuesFound.ToString(), ChkValue);
                    return false;
                }
            }

            problemValue = string.Empty;
            return true;
        }




        ///// <summary>
        ///// This function will encrypt the datatable and write to xml into the filePath provided
        ///// </summary>
        ///// <param name="dataTable">DataTable to Write XML</param>
        ///// <param name="filePath">Output FilePath</param>
        ///// <returns>Returns Nothing</returns>
        //public void EncryptFile(DataTable dataTable, string outPutFilePath)
        //{
        //    XmlTextWriter Xtw = new XmlTextWriter(outPutFilePath, Encoding.UTF8);
        //    UnicodeEncoding aUE = new UnicodeEncoding();
        //    byte[] key = aUE.GetBytes("fewlines4biju");
        //    RijndaelManaged RMCrypto = new RijndaelManaged();
        //    CryptoStream aCryptoStream = new CryptoStream(Xtw.BaseStream, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);
        //    dataTable.WriteXml(aCryptoStream);
        //    aCryptoStream.Close();
        //}



        public void EncryptDataSet(DataSet ds, string pathFile)
        {
            // Create the DES encryption provider:
            DES des = new DESCryptoServiceProvider();
            // Serialize the DES provider's key and IV to disk for decryption later:
            using (StreamWriter sw = new StreamWriter("DES.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                byte[][] stuff = new byte[2][];
                stuff[0] = des.Key;
                stuff[1] = des.IV;
                bf.Serialize(sw.BaseStream, stuff);
            }
            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt the DataSet to the file:
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(ds.GetXml());
                    }
                }
            }
            // Now write the DataSet schema to disk:
            ds.WriteXmlSchema("DataSet.xsd");
        }


        public DataSet DecryptDataSet(string pathFile)
        {
            DataSet ds = new DataSet();
            // Setup the DES encryption provider:
            DES des = new DESCryptoServiceProvider();
            // Deserialize the DES provider's key and IV from disk:
            using (StreamReader sr = new StreamReader("DES.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                byte[][] stuff = (byte[][])bf.Deserialize(sr.BaseStream);
                des.Key = stuff[0];
                des.IV = stuff[1];
            }
            // Decrypt the Encrypted DataSet:
            using (FileStream fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
            {
                // Decrypt the DataSet and store it into an instance:
                using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    ds.ReadXml(cs);
                }
            }
            // Now read the DataSet schema from disk:
            ds.ReadXmlSchema("DataSet.xsd");
            return ds;
        }



        ///// <summary>
        ///// This function will Decrypt the xml file and returns the dataset
        ///// </summary>
        ///// <param name="filePath">input FilePath</param>
        ///// <returns>Returns Dataset</returns>
        //public DataSet DecryptFile(string inputFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    FileStream aFileStream = new FileStream(inputFilePath, FileMode.Open);
        //    StreamReader aStreamReader = new StreamReader(aFileStream);
        //    UnicodeEncoding aUE = new UnicodeEncoding();
        //    byte[] key = aUE.GetBytes("fewlines4biju");
        //    RijndaelManaged RMCrypto = new RijndaelManaged();
        //    CryptoStream aCryptoStream = new CryptoStream(aFileStream, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

        //    //Restore the data set to memory.
        //    ds.ReadXml(aCryptoStream);
        //    aStreamReader.Close();
        //    aFileStream.Close();
        //    return ds;
        //}



    }
    
}
