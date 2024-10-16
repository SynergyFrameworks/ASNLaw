using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Import.Model
{
    //Errors that occur during Excel Workbook parsing
    // Workbook model level errors:  
    //                              cannot read workbook;
    //                              empty workbook;
    //                              user input header number is bigger than or equals to user input data row starting number;
    //worksheet model level errors: 
    public class ParseError
    {
        public string ErrorMessage { get; set; }
    }
}
