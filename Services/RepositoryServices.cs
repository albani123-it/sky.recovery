using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using sky.recovery.Helper.Config;
using sky.recovery.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using System.Collections.Generic;
using sky.recovery.DTOs.RepositoryDTO;
using Microsoft.Extensions.Configuration;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using static Dapper.SqlMapper;

namespace sky.recovery.Services
{
    public class RepositoryServices : IRepositoryServices
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _RecoveryContext = new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;

        private IUserService _User { get; set; }
        public RepositoryServices(IConfiguration configuration,IWebHostEnvironment environment, IUserService User, IOptions<DbContextSettings> appsetting)
        {
            _environment = environment;
            _User = User;
            _config = configuration;

        }

        public async Task<(bool status,string message, string path,string name)>RetrieveFilePath(int id, int fiturid)
        {
            try
            {
                var CheckFiturId = await _RecoveryContext.Generalparamdetail.Where(es => es.Id == fiturid).FirstOrDefaultAsync();
                if(CheckFiturId.Title=="Restrukture")
                {
                    var GetData = await _RecoveryContext.Restrukturedokumen.Where(es => es.Id == id).Select(es =>new { filepath = es.Filepath, filename = es.Fileurl }).FirstOrDefaultAsync();
                    return (true, "OK", GetData.filepath,GetData.filename);
                }
                else
                {
                    var GetData = await _RecoveryContext.Masterrepository.Where(es => es.Id == id).Select(es=>new { filepath = es.Fileurl, filename = es.Filename }).FirstOrDefaultAsync();
                    return (true, "OK", GetData.filepath,GetData.filename);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null,null);
            }
        }

        public async Task<(bool status,string message, MemoryStream x)> DownloadFromFTP(string url, string dest)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(url));
                //ftpRequest.Credentials = new NetworkCredential(userName, password);
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.EnableSsl = false;

                // Get the response stream
                var memory = new MemoryStream();
                var pathToLocal = Path.Combine(_environment.WebRootPath, "File/FTPDownload/");

                //if (!Directory.Exists(pathToLocal))
                //{
                //    Directory.CreateDirectory(pathToLocal);
                //}

                //var nm = Path.Combine(pathToLocal, dest);
                using (FtpWebResponse ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync())
                using (Stream responseStream = ftpResponse.GetResponseStream())
             

                using (var stream = new FileStream(url, FileMode.Open, FileAccess.Read))
                {
                    await responseStream.CopyToAsync(memory);
                    return (true,"OK",memory);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message,null);
            }
        }
        public async Task<(bool Status, string Message)> RemoveDoc(int id)
        {
            try
            {
                var GetData = await _RecoveryContext.Masterrepository.Where(es => es.Id == id).FirstOrDefaultAsync();
                GetData.Isactive = 0;
                _RecoveryContext.Entry(GetData).State = EntityState.Modified;

                await _RecoveryContext.SaveChangesAsync();
                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string message)>DocumentNotes(List<DocNotesDTO> Entity)
        {
            try
            {
                var FiturId = Entity.FirstOrDefault().FiturId;
                if (FiturId == _RecoveryContext.Generalparamdetail.Where(es => es.Title == "Restrukture").FirstOrDefault().Id)
                {
                    foreach (var x in Entity)
                    {
                        var Data = await _RecoveryContext.Restrukturedokumen.Where(es => es.Id == x.Id).FirstOrDefaultAsync();
                        Data.Keterangan = x.Notes;
                        Data.Status = x.Status;
                        _RecoveryContext.Entry(Data).State = EntityState.Modified;
                        await _RecoveryContext.SaveChangesAsync();

                    }
                    return (true, "OK");
                }
                else
                {
                    foreach (var x in Entity)
                    {
                        var Data = await _RecoveryContext.Masterrepository.Where(es => es.Id == x.Id).FirstOrDefaultAsync();
                        Data.Keterangan = x.Notes;
                        Data.Status = x.Status;
                        _RecoveryContext.Entry(Data).State = EntityState.Modified;
                        await _RecoveryContext.SaveChangesAsync();

                    }
                    return (true, "OK");
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }



        static bool CheckFtpDirectoryExists(string ftpUrl, string username, string password)
        {
            try
            {
                // Create the FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                // Provide the credentials
               // request.Credentials = new NetworkCredential(username, password);

                // Get the response
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    // If we get here, the directory exists
                    return true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is FtpWebResponse response)
                {
                    // If the status code is 550, the directory does not exist
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<(bool status, string message)> UploadToLocal(string paths,string fileurl, IFormFile files)
        {
            try
            {
                if (!Directory.Exists(paths))
                {
                    Directory.CreateDirectory(paths);
                }
                string ext = Path.GetExtension(fileurl);

                var nm = Path.Combine(paths, fileurl);

                using (FileStream filestream = System.IO.File.Create(nm))
                {
                    await files.CopyToAsync(filestream);
                    await filestream.FlushAsync();
                    //  return "\\Upload\\" + objFile.files.FileName;
                }
                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false,"Upload To Local Path Failed :" + ex.Message);
            }
        }

        public async Task<(bool status,string message)> UploadToFTP(string paths,string fileurl)
        {
            try
            {
               
                byte[] fileContents;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(paths);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                using (FileStream sourceStream = new FileStream(fileurl, FileMode.Open, FileAccess.Read))
                {
                    fileContents = new byte[sourceStream.Length];
                    await sourceStream.ReadAsync(fileContents, 0, (int)sourceStream.Length);
                }
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
                }

                FtpWebResponse responsexs =  (FtpWebResponse) await request.GetResponseAsync();
                File.Delete(fileurl);

                return (true, responsexs.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Status, string Message)> UploadToFTPServices(string userid, RepoReqDTO Entity)
        {
            try
            {
                if (Entity == null)
                {
                    return (false, "Request Not Valid");
                }

                if (Entity.File.Length < 0)
                {
                    return (false, "File Must Uploaded");
                }


                var getCallBy = await _User.GetDataUser(userid);
                var GetNamingFolder = await _RecoveryContext.Generalparamdetail.Where(es => es.Id == Entity.FiturId).Select(es => es.Title).FirstOrDefaultAsync();
                var pathToLocal = Path.Combine(_environment.WebRootPath, "File/FTP/" + GetNamingFolder.Trim());
                var path = Path.Combine(_config["Repository:Recovery"].ToString(), GetNamingFolder.Trim());
                var nm = Path.Combine(path, Entity.File.FileName);
                var nmToLocal = Path.Combine(pathToLocal, Entity.File.FileName);

                var Datax = await UploadToLocal(pathToLocal, Entity.File.FileName, Entity.File);
                if (Datax.status == false)
                {
                    return (Datax.status, Datax.message);
                }
                if (!CheckFtpDirectoryExists(path, null, null))
                {

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    FtpWebResponse responsexs = (FtpWebResponse)await request.GetResponseAsync();


                }
                var d = await UploadToFTP(nm, nmToLocal);
                if (d.status == false)
                {
                    return (false, "Upload To FTP Failed :" + d.message);
                }


                var CheckExisting = await _RecoveryContext.Masterrepository.Where(es => es.Fiturid == Entity.FiturId
                && es.Doctype == Entity.DocType && es.Requestid == Entity.RequestId).ToListAsync();
                if (CheckExisting.Count == 0)
                {
                    var Data = new Masterrepository()
                    {
                        Fiturid = Entity.FiturId,
                        Userid = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Uploaddated = DateTime.Now,
                        Filename = Entity.File.FileName,
                        Fileurl = nm,
                        Requestid = Entity.RequestId,
                        Isactive = 1,
                        Doctype = Entity.DocType
                    };
                    await _RecoveryContext.Masterrepository.AddAsync(Data);
                    await _RecoveryContext.SaveChangesAsync();
                }
                else
                {
                    var Data = CheckExisting.FirstOrDefault();
                    Data.Fiturid = Entity.FiturId;
                    Data.Requestid = Entity.RequestId;
                    Data.Fileurl = nm;
                    Data.Filename = Entity.File.FileName;
                    _RecoveryContext.Entry(Data).State = EntityState.Modified;
                    await _RecoveryContext.SaveChangesAsync();
                }

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Status, string Message)> UploadToLocalServices(string userid, RepoReqDTO Entity)
        {
            try
            {
                if (Entity == null)
                {
                    return (false, "Request Not Valid");
                }

                if (Entity.File.Length < 0)
                {
                    return (false, "File Must Uploaded");
                }


                var getCallBy = await _User.GetDataUser(userid);
                var GetNamingFolder = await _RecoveryContext.Generalparamdetail.Where(es => es.Id == Entity.FiturId).Select(es => es.Title).FirstOrDefaultAsync();
                var pathToLocal = Path.Combine(_environment.WebRootPath, "File/Local/"+GetNamingFolder.Trim());
                var nmToLocal = Path.Combine(pathToLocal, Entity.File.FileName);

                var Datax = await UploadToLocal(pathToLocal, Entity.File.FileName, Entity.File);
                if(Datax.status==false)
                {
                    return (Datax.status, Datax.message);
                }
               
                var CheckExisting = await _RecoveryContext.Masterrepository.Where(es => es.Fiturid == Entity.FiturId
                && es.Doctype == Entity.DocType && es.Requestid == Entity.RequestId).ToListAsync();
                if (CheckExisting.Count ==0)
                {
                    var Data = new Masterrepository()
                    {
                        Fiturid = Entity.FiturId,
                        Userid = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Uploaddated = DateTime.Now,
                        Filename = Entity.File.FileName,
                        Fileurl = nmToLocal,
                        Requestid = Entity.RequestId,
                        Isactive = 1,
                        Doctype = Entity.DocType
                    };
                    await _RecoveryContext.Masterrepository.AddAsync(Data);
                    await _RecoveryContext.SaveChangesAsync();
                }
                else
                {
                    var Data = CheckExisting.FirstOrDefault();
                    Data.Fiturid = Entity.FiturId;
                    Data.Requestid = Entity.RequestId;
                    Data.Fileurl = nmToLocal;
                    Data.Filename = Entity.File.FileName;
                    _RecoveryContext.Entry(Data).State = EntityState.Modified;
                    await _RecoveryContext.SaveChangesAsync();
                }

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }
}
