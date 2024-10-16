using Domain.Common;
using hOOt;
using RaptorDB;
using System.ComponentModel;

namespace Domain.DeepAnalytics
{
    public class HootSearchEng
    {

        ~HootSearchEng()
        {
            if (hoot != null)
            {

                hoot.FreeMemory();
                hoot.Shutdown();
                hoot = null;
            }

            System.GC.Collect();
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private Hoot hoot;

        private BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        private string _IndexPath = string.Empty;
        private string _SearchPath = string.Empty;

        private string[] _Found;
        private int _FoundQty = 0;
        private string _SearchCriteria = string.Empty;
        private string _Information = string.Empty;

        public string[] FoundResults
        {
            get { return _Found; }
        }

        public int FoundQty
        {
            get { return _FoundQty; }
        }

        public string SearchCriteria
        {
            get { return _SearchCriteria; }
        }

        public string Information
        {
            get { return _Information; }
        }


        public bool SearchFor(string SearchCriteria, string IndexPath, string SearchPath)
        {
            _IndexPath = IndexPath;
            _SearchPath = SearchPath;
            _SearchCriteria = SearchCriteria;

            try
            {
                ChkRunIndexer(_IndexPath, _SearchPath);

                if (hoot == null)
                {
                    hoot = new Hoot(_IndexPath, "index", true);

                }

                List<string> list = new List<string>();

                DateTime dt = DateTime.Now;

                string fileName = string.Empty;
                foreach (var d in hoot.FindDocumentFileNames(_SearchCriteria))
                {
                    fileName = Files.GetFileNameWOExt(d);
                    //if (DataFunctions.IsNumeric(fileName))
                    list.Add(fileName);
                }

                _Found = list.ToArray();
                _FoundQty = _Found.Length;

                _Information = string.Concat("Found: ", _FoundQty.ToString(), Environment.NewLine, Environment.NewLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());

            }
            catch (Exception ex)
            {
                //if (hoot != null)
                //{
                //    hoot.FreeMemory();
                //    hoot.Shutdown();
                //    hoot = null;
                //}

                _ErrorMessage = string.Concat("Error: ", ex.Message);
                return false;
            }

            if (hoot != null)
            {
                hoot.FreeMemory();
                hoot.Shutdown();
                hoot = null;
            }

            return true;

        }

        public void ChkRunIndexer(string IndexPath, string SearchPath)
        {
            _IndexPath = IndexPath;
            _SearchPath = SearchPath;

            if (hoot != null)
            {
                hoot.FreeMemory();
                hoot.Shutdown();
                hoot = null;
            }

            // Delete all Index files -- Added 07.12.2014
            //System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(AppFolders.DocParsedSecIndex);

            //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            //{
            //    file.Delete();
            //}
            //


            try
            {
                hoot = new Hoot(_IndexPath, "index", true);


                RunIndexer();


            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Search Loading Error: ", ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];
            BackgroundWorker wrk = sender as BackgroundWorker;
            int i = 0;
            foreach (string fn in files)
            {
                if (wrk.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                backgroundWorker1.ReportProgress(1, fn);
                try
                {
                    if (hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    hoot.Save();
                }
            }
            hoot.Save();
            hoot.OptimizeIndex();
        }

        private void RunIndexer()
        {

            if (hoot == null)
                hoot = new Hoot(_IndexPath, "index", true);

            string[] files = Directory.GetFiles(_SearchPath, "*", SearchOption.TopDirectoryOnly);


            //  backgroundWorker1.RunWorkerAsync(files);

            //string[] files = e.Argument as string[];
            //BackgroundWorker wrk = sender as BackgroundWorker;
            int i = 0;
            foreach (string fn in files)
            {
                //if (wrk.CancellationPending)
                //{
                //    e.Cancel = true;
                //    break;
                //}
                backgroundWorker1.ReportProgress(1, fn);
                try
                {
                    if (hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                //if (i > 1000)
                //{
                //    i = 0;
                //    hoot.Save();
                //}
            }
            hoot.Save();
            hoot.OptimizeIndex();

            hoot.FreeMemory();
            hoot.Shutdown(); // 2.14.2015
            if (hoot != null) // 7.7.2015
            {
                ((IDisposable)hoot).Dispose(); // 7.7.2015
            }
            // hoot = null; // 2.14.2015


        }
    }


}
