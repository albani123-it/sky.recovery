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
using sky.recovery.DTOs.TemplateExcelObject;

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


        static List<dynamic>  ReadExcelToList(string filePath)
        {
            var excelData = new List<List<object>>();
            var excelDataCell = new List<List<ExcelRange>>();
            ExcelRange DataCell;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));
            
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Where(ES=>ES.Name=="GeneralMasterLoan").FirstOrDefault();

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
                CellEmployeeName= es[3].ToString(),
                CellBranchCity = es[4].ToString()
            }).ToList();
            var p = new List<dynamic>();
            p.Add(e);
            return p;
        }

public async Task <List<dynamic>> ConvertDataExcel(string sheet, List<List<ExcelRange>> DataRange)
        {
            var ListData = new List<dynamic>();
            if(sheet=="GeneralMasterLoan")
            {
                var e = DataRange.Select(es => new GeneralMasterLoan
                {
                    Id = Convert.ToInt32(es[0].Value),
                    NamaNasabah = es[1].Value.ToString(),
                    KotaTinggal = es[2].Value.ToString(),
                    JumlahHutang = es[3].Value.ToString(),
                    Status = es[4].Value.ToString(),
                    DPD =  Convert.ToInt32(es[5].Value.ToString()),
                   cell_id = es[1].ToString(),
                    cell_NamaNasabah = es[1].ToString(),
                    cell_KotaTinggal = es[2].ToString(),
                    cell_DPD = es[5].ToString(),
                    cell_Status = es[4].ToString(),
                    cell_JumlahHutang = es[3].ToString()
                }).ToList();
                ListData.Add(e);
            }
            if(sheet=="GeneralMasterEmployee")
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
          
            return ListData;
        }
        static List<List<ExcelRange>> ReadExcelToListBySheet(string filePath, string sheet)
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


        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> RetrieveDataBySheet(string sheet)
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                string filePath = "D:\\PROJECT\\SKY.RECOVERY.LOCAL\\File\\DOCTemplateDUmmy.xlsx";
                var data = ReadExcelToListBySheet(filePath,sheet);
                var ConverDynamicExcel = await ConvertDataExcel(sheet,data);
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data= ConverDynamicExcel;
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
                wrap.Data = data;
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

            public List<dynamic> GetListSheet(string filePath)
        {
            var excelData = new List<dynamic>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(filePath));
           
                var worksheet = package.Workbook.Worksheets.Select(es=>new WorkSheetCustom { Name=es.Name}).ToList();

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
   

    }
}
