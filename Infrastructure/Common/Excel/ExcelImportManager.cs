using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using log4net;
using Excel.Transformers;

namespace Excel
{
    public  class ExcelImportManager : IExcelImportManager
    {
        public IExcelManager ExcelManager { get; set; }
        public IDictionary<string, IExcelTransformer> Transformers { get; set; }
        private ILog Log = LogManager.GetLogger(typeof (ExcelImportManager));

        public Dictionary<string, T> ParseExcelFiles<T>(Dictionary<string,Stream> files)
        {
            var objs = new Dictionary<string,T>();
            foreach (var file in files)
            {
                //store file name in Dictionary<string,object>
                objs.Add(file.Key, ParseExcelFile<T>(file.Value));
            }
            return objs;
        }


        public T ParseExcelFile<T>(Stream file, Dictionary<string, object> data = null)
        {
            var sw = new Stopwatch();
            sw.Start();
            var type = typeof(T);
            string typeName;
            if (type.Name == "Dictionary`2")
            {
                var val = type.GetGenericArguments().LastOrDefault();
                typeName = val.IsGenericType ? val.GetGenericArguments().FirstOrDefault().Name : val.Name;
            }
            else
                typeName = type.IsGenericType ? type.GetGenericArguments().FirstOrDefault().Name : type.Name;
            if (!Transformers.ContainsKey(typeName))
                throw new NotImplementedException($"Can't find transformer for {typeName}");

            var parsed = (T)Transformers[typeName].ParseExcelFile(file, data);
            if(parsed.GetType().GetInterfaces().Any(i=> i.Name == "List"))
            {
                var list = parsed as List<object>;
                Log.InfoFormat("Parsed {0} {1} in: {2}", list != null ? list.Count.ToString() : "1", typeof (T),
                    sw.Elapsed);
            }
            return parsed;
        }     

    }
}
