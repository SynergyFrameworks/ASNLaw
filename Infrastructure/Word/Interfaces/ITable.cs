using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//using Novacode;

namespace Word
{
    public interface ITable
    {
        IList<IRow> GetRows();
        int Index { get; }

        /// <summary>
        /// Inserts a row at the bottom of the table
        /// </summary>
        /// <returns></returns>
        IRow InsertRow();

        /// <summary>
        /// Inserts a row at a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IRow InsertRow(int index);

        /// <summary>
        /// inserts a new row, copying the contents of the supplied index
        /// </summary>
        /// <param name="sourceRowIndex"></param>
        /// <returns></returns>
        IRow CopyRow(int sourceRowIndex);


        /// <summary>
        /// Inserts a new row, copying the contents of the supplied index, to the second index provided. 
        /// </summary>
        /// <param name="sourceRowIndex"></param>
        /// <param name="destinationIndex"></param>
        /// <returns></returns>
        IRow CopyRowToIndex(int sourceRowIndex, int destinationIndex);

        void RemoveRow(int rowIndex);
    }
}
