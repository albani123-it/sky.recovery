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
using sky.recovery.Entities.RecoveryConfig;
using sky.recovery.Services.DBConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using sky.recovery.Entities;
using sky.recovery.Insfrastructures;
using Microsoft.EntityFrameworkCore;

namespace sky.recovery.Services
{
    public class DocumentServices: SkyCoreConfig , IDocServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IWebHostEnvironment _environment;

        private IUserService _User { get; set; }
        public DocumentServices(IWebHostEnvironment environment, IUserService User, IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _environment = environment;
            _User = User;

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

        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ReadExcelByUpload(UploadExcelDTO Entity)
        {
            var wrap = _DataResponses.ExcelReturn();
            var getCallBy = await _User.GetDataUser(Entity.userid);

            try
            {
                if (Entity==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Request Not Valid";
                }
                if (Entity.File.Length < 0)
                {
                    wrap.Status = false;
                    wrap.Message = "File harus diupload";
                }
                if (Entity.File.FileName == null)
                {
                    wrap.Status = false;
                    wrap.Message = "File harus diupload";
                }
                var path = Path.Combine(_environment.WebRootPath, "File");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string ext = Path.GetExtension(Entity.File.FileName);

                var nm = Path.Combine(path, Entity.File.FileName + ext);

                using (FileStream filestream = System.IO.File.Create(nm))
                {
                    Entity.File.CopyTo(filestream);
                    filestream.Flush();
                    //  return "\\Upload\\" + objFile.files.FileName;
                }
                var Data = new masterrepository()
                {
                    userid=getCallBy.Returns.Data.FirstOrDefault().iduser,
                     uploaddated=DateTime.Now,
                     filename=Entity.File.FileName,
                     fileurl=nm,
                     isactive=1

                };
                masterrepository.Add(Data);
                await SaveChangesAsync();
                var ReturnSheet =await ReadExcelSheetByFileUpload(nm);
                if(ReturnSheet.Status==true)
                {
                    wrap.Status = true;
                    wrap.Message = ReturnSheet.Returns.Message;
                    wrap.Data = ReturnSheet.Returns.Data;
                    wrap.DataFile = Data;

                }
                else
                {
                    wrap.Status = false;
                    wrap.Message = ReturnSheet.Returns.Message;
                    wrap.Data = ReturnSheet.Returns.Data;

                }
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
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

        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ReadExcelSheetByFileExisting(int Id)
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                var Data = masterrepository.Where(es => es.id == Id).FirstOrDefault();
                var ReturnSheet = await ReadExcelSheetByFileUpload(Data.fileurl);
                if (ReturnSheet.Status == true)
                {
                    wrap.Status = true;
                    wrap.Message = ReturnSheet.Returns.Message;
                    wrap.Data = ReturnSheet.Returns.Data;
                    wrap.DataFile = Data;

                }
                else
                {
                    wrap.Status = false;
                    wrap.Message = ReturnSheet.Returns.Message;
                    wrap.Data = ReturnSheet.Returns.Data;

                }
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }


        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> RetriveAllListByUserId(string userid)
        {
            var wrap = _DataResponses.ExcelReturn();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            try
            {
                var Datauser = getCallBy.Returns.Data.FirstOrDefault().iduser;
                var Data =await  masterrepository.Where(es => es.userid == Datauser).ToListAsync();
                ListData.Add(Data);
               
                    wrap.Status = true;
                    wrap.Message = "OK";
                wrap.Data = ListData;

                
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }
        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ReadExcelSheetByFileUpload(string filename)
        {
            var wrap = _DataResponses.ExcelReturn();
            try
            {
                string filePath = "D:\\PROJECT\\SKY.RECOVERY.LOCAL\\File\\DOCTemplateDUmmy.xlsx";
                var data = GetListSheet(filename);
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
