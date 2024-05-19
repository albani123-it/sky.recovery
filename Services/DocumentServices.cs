using sky.recovery.DTOs.RequestDTO;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using sky.recovery.DTOs.HelperDTO;
using Newtonsoft.Json;

namespace sky.recovery.Services
{
    public class DocumentServices: IDocServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private IUserService _User { get; set; }
        public DocumentServices()
        {

        }

        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ExcelReader()
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                string filePath = "D:\\PROJECT\\SKY.RECOVERY.LOCAL\\File\\DOCTemplateDUmmy.xlsx";
                var data = ReadExcelToList(filePath);
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data = data;
                return (wrap.Status, wrap);
            }
            catch(Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }

        }


        static List<DummyExcelDTO> ReadExcelToList(string filePath)
        {
            var excelData = new List<List<object>>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES=>ES.Name=="GeneralMasterLoan").FirstOrDefault();

                if (worksheet != null)
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var rowData = new List<object>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Value;
                           
                            rowData.Add(cellValue);
                        }
                        excelData.Add(rowData);
                    }
                  

                }
                else
                {
                    Console.WriteLine("No worksheet found in the Excel file.");
                }
            }
            var d = excelData.Select(es => new DummyExcelDTO
            {
                Id =Convert.ToInt32(es[0]),
                NIP = es[1].ToString(),
                Email = es[2].ToString(),
                EmployeeName = es[3].ToString(),
                BranchCity = es[4].ToString()

            }).ToList();
            return d;
        }


        static List<List<object>> ReadExcelToListBySheet(string filePath, string sheet)
        {
            var excelData = new List<List<object>>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES => ES.Name == sheet).FirstOrDefault();

                if (worksheet != null)
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var rowData = new List<object>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Value;

                            rowData.Add(cellValue);
                        }
                        excelData.Add(rowData);
                    }


                }
                else
                {
                    Console.WriteLine("No worksheet found in the Excel file.");
                }
            }
            return excelData;
        }


        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> RetrieveDataBySheet(string sheet)
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                string filePath = "D:\\PROJECT\\SKY.RECOVERY.LOCAL\\File\\DOCTemplateDUmmy.xlsx";
                var data = ReadExcelToListBySheet(filePath,sheet);
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.DataBySheet= data;
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }
        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ReadExcelSheet()
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                string filePath = "D:\\PROJECT\\SKY.RECOVERY.LOCAL\\File\\DOCTemplateDUmmy.xlsx";
                var data = GetListSheet(filePath);
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Worksheet = data;
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

            public List<WorkSheetCustom> GetListSheet(string filePath)
        {
            var excelData = new List<WorkSheetCustom>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.Select(es=>new WorkSheetCustom { Name=es.Name}).ToList();

                if (worksheet != null)
                {


                   
                    return worksheet;

                }
                else
                {
                    return null;
                }
            }
           
        }


    }
}
