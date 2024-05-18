using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IDocServices
    {
       // static List<List<object>> ReadExcelToList(string filePath);
        public  Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ExcelReader();

    }
}
