using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Linq;

namespace Excel.EPPlus
{
    public class EPPlusWorksheets : IWorksheets
    {
        private ExcelWorksheets _worksheets;
        private IList<EPPlus.EPPlusWorksheet> _convertedWorkSheets = new List<EPPlusWorksheet>();

        public EPPlusWorksheets(ExcelWorksheets worksheets)
        {
            _worksheets = worksheets;
            _convertedWorkSheets = new List<EPPlusWorksheet>(worksheets.Select(workSheet => new EPPlusWorksheet(workSheet)));
        }
        public IEnumerator<IWorksheet> GetEnumerator()
        {
            return _convertedWorkSheets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _convertedWorkSheets.Count(); }
        }

        IWorksheet IWorksheets.this[int position]
        {
            get { return _convertedWorkSheets.ElementAt(position); }
        }

        IWorksheet IWorksheets.this[string name]
        {
            get { return _convertedWorkSheets.FirstOrDefault(wrappedWorkSheet => wrappedWorkSheet.Name == name); }
        }

        public IWorksheet Add(string name)
        {
            var newWorkSheet = new EPPlusWorksheet(_worksheets.Add(name));
            _convertedWorkSheets.Add(newWorkSheet);
            return newWorkSheet;
        }

        public void Remove(string name)
        {
            var worksheetToDelete = _worksheets[name];
            _convertedWorkSheets.RemoveAt(worksheetToDelete.Index);
            _worksheets.Delete(worksheetToDelete);
        }

        public IWorksheet Copy(string existingWorksheet, string newWorksheet)
        {
            return new EPPlusWorksheet(_worksheets.Copy(existingWorksheet, newWorksheet));
        }
    }
}
