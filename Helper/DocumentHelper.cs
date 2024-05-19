using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using sky.recovery.DTOs.HelperDTO;
using sky.recovery.DTOs.TemplateExcelObject;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
namespace sky.recovery.Helper
{
    public class DocumentHelper
    {


        public List<dynamic> ReadExcelToList(string filePath)
        {
            var excelData = new List<List<object>>();
            var excelDataCell = new List<List<ExcelRange>>();
            ExcelRange DataCell;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));

            ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES => ES.Name == "GeneralMasterLoan").FirstOrDefault();

            if (worksheet != null)
            {
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 3; row <= rowCount; row++)
                {
                    var rowData = new List<object>();
                    var rowDataCell = new List<ExcelRange>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Value;
                        DataCell = worksheet.Cells[row, col];
                        rowData.Add(cellValue);
                        rowDataCell.Add(DataCell);
                    }
                    excelData.Add(rowData);
                    excelDataCell.Add(rowDataCell);

                }


            }
            else
            {
                Console.WriteLine("No worksheet found in the Excel file.");
            }


            var e = excelDataCell.Select(es => new DummyExcelDTO
            {
                Id = Convert.ToInt32(es[0].Value),
                NIP = es[1].Value.ToString(),
                Email = es[2].Value.ToString(),
                EmployeeName = es[3].Value.ToString(),
                BranchCity = es[4].Value.ToString(),
                CellId = es[0].ToString(),
                CellNIP = es[1].ToString(),
                CellEmail = es[2].ToString(),
                CellEmployeeName = es[3].ToString(),
                CellBranchCity = es[4].ToString()
            }).ToList();
            var p = new List<dynamic>();
            p.Add(e);
            return p;
        }


        public List<List<ExcelRange>> ReadExcelToListBySheet(string filePath, string sheet)
        {
            var excelDataCell = new List<List<ExcelRange>>();
            ExcelRange DataCell;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));

            ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES => ES.Name == sheet).FirstOrDefault();

            if (worksheet != null)
            {
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 3; row <= rowCount; row++)
                {
                    var rowData = new List<object>();
                    var rowDataCell = new List<ExcelRange>();

                    for (int col = 1; col <= colCount; col++)
                    {

                        DataCell = worksheet.Cells[row, col];
                        rowDataCell.Add(DataCell);
                    }
                    excelDataCell.Add(rowDataCell);
                }
                return excelDataCell;


            }
            else
            {
                return null;
            }

        }


        public List<dynamic> GetListSheet(string filePath)
        {
            var excelData = new List<dynamic>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));

            var worksheet = package.Workbook.Worksheets.Select(es => new WorkSheetCustom { Name = es.Name }).ToList();

            if (worksheet != null)
            {

                excelData.Add(worksheet);

                return excelData;

            }
            else
            {
                return null;
            }
        }

        public class DataExcel
        {
            public string Data { get; set; }
            public string Cell { get; set; }
        }


        public List<List<object>> ReadExcelToListDynamic(string filePath,string sheet)
        {
            var excelData = new List<List<object>>();
            //var excelDatacell = new List<List<object>>();
         
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));

            ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES => ES.Name == sheet).FirstOrDefault();

            if (worksheet != null)
            {
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 3; row <= rowCount; row++)
                {
                    var rowData = new List<object>();
                    var rowDataCell = new List<object>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Value;
                       // var DataCell = worksheet.Cells[row, col];
                        rowData.Add(cellValue);
                       // rowDataCell.Add(DataCell);
                    }
                    excelData.Add(rowData);
                   // excelDatacell.Add(rowDataCell);

                }


            }
            else
            {
                Console.WriteLine("No worksheet found in the Excel file.");
            }

            

            return excelData;
        }

        public async Task<MemoryStream> GenerateLetterDummy()
        {
            return null;
        }


            


            public async Task<List<dynamic>> ConvertDataExcel_Dummy(string sheet,  List<List<object>> Data,
            List<List<object>> Cell)
        {
            var ListData = new List<object>();
            var ListCell = new List<dynamic>();

            ListData.Add(Data);
            ListData.Add(Cell);
            

            return ListData;
        }


        public async Task<List<dynamic>> ConvertDataExcel(string sheet, List<List<ExcelRange>> DataRange)

        {
            var ListData = new List<dynamic>();
            var ListCell = new List<dynamic>();
            var po = new List<DataExcel>();
            if (sheet == "GeneralMasterLoan")
            {
                var e = DataRange.Select(es => new GeneralMasterLoan
                {
                    Id = Convert.ToInt32(es[0].Value),
                    NamaNasabah = es[1].Value.ToString(),
                    KotaTinggal = es[2].Value.ToString(),
                    JumlahHutang = es[3].Value.ToString(),
                    Status = es[4].Value.ToString(),
                    DPD = Convert.ToInt32(es[5].Value.ToString()),
                    cell_id = es[1].ToString(),
                    cell_NamaNasabah = es[1].ToString(),
                    cell_KotaTinggal = es[2].ToString(),
                    cell_DPD = es[5].ToString(),
                    cell_Status = es[4].ToString(),
                    cell_JumlahHutang = es[3].ToString()
                }).ToList();
                ListData.Add(e);
            }
            if (sheet == "GeneralMasterEmployee")
            {
                var e = DataRange.Select(es => new GeneralMasterEmployee
                {
                    Id = Convert.ToInt32(es[0].Value),
                    NIP = es[1].Value.ToString(),
                    Email = es[2].Value.ToString(),
                    EmployeeName = es[3].Value.ToString(),
                    BranchCity = es[4].Value.ToString(),
                    CellId = es[0].ToString(),
                    CellNIP = es[1].ToString(),
                    CellEmail = es[2].ToString(),
                    CellEmployeeName = es[3].ToString(),
                    CellBranchCity = es[4].ToString()
                }).ToList();
                ListData.Add(e);

            }
            else
            {
                foreach (var x in DataRange)
                {
                    foreach (var p in x)
                    {
                        var Data = new DataExcel();
                        if (!String.IsNullOrEmpty(p.Text))
                        {
                            Data.Data = p.Text.ToString();
                            Data.Cell = p.Address;
                            po.Add(Data);

                        }

                    }
                };
                var op = po.Where(es => es != null).ToList();
                ListData.Add(op);
                
            }
            return ListData;
        }

    }
}
