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
using sky.recovery.Helper;

namespace sky.recovery.Services
{
    public class DocumentServices: SkyCoreConfig , IDocServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        DocumentHelper _DocHelper = new DocumentHelper();
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
                var data = _DocHelper.ReadExcelToList(filePath);
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


      



        public async Task<(bool? Status, GeneralResponsesV2DocExcel Returns)> ReadExcelByFileExisting(string path,string sheet)
        {
            var wrap = _DataResponses.ExcelReturn();

            try
            {
                //var  data = _DocHelper.ReadExcelToListBySheet(path, sheet);
                var data = _DocHelper.ReadExcelToListDynamic(path, sheet);

                //var ConverDynamicExcel = await _DocHelper.ConvertDataExcel(sheet, data);
                //var ConverDynamicExcel = await _DocHelper.ConvertDataExcel_Dummy(sheet,data.Data,data.Cell);

                wrap.Status = true;
                wrap.Message = "OK";
                wrap.DataFile = data;
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

        public async Task<(bool? Status, GeneralResponsesPDFV2 Datareturn)> GetTemplateLetter()
        {
            var wrap = _DataResponses.PDFReturn();
            //var getCallBy = await _User.GetDataUser(Entity.userid);

            try
            {
                var GetData = await _DocHelper.GenerateLetterDummy();
                wrap.Status = true;
                wrap.Message = "ok";
                wrap.Data = GetData;
                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);

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
                var ListData = new List<dynamic>();
                ListData.Add(Data);
                if(ReturnSheet.Status==true)
                {
                    wrap.Status = true;
                    wrap.Message = ReturnSheet.Returns.Message;
                    wrap.DataSheet = ReturnSheet.Returns.Data;
                    wrap.Data = ListData;

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
                var data = _DocHelper.ReadExcelToListBySheet(filePath,sheet);
                var ConverDynamicExcel = await _DocHelper.ConvertDataExcel(sheet,data);
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
            var listdata = new List<dynamic>();
            try
            {
                var Data = masterrepository.Where(es => es.id == Id).FirstOrDefault();
                var ReturnSheet = await ReadExcelSheetByFileUpload(Data.fileurl);
                if (ReturnSheet.Status == true)
                {
                    wrap.Status = true;
                    wrap.Message = ReturnSheet.Returns.Message;
                    listdata.Add(Data);       
                    wrap.Data = listdata;
                    wrap.DataSheet = ReturnSheet.Returns.Data;
                   // wrap.DataFile = Data;

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
                var data = _DocHelper.GetListSheet(filename);
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
                var data = _DocHelper.GetListSheet(filePath);
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

   

    }
}
