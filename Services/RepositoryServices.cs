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

namespace sky.recovery.Services
{
    public class RepositoryServices : IRepositoryServices
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _RecoveryContext = new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();
        private readonly IWebHostEnvironment _environment;

        private IUserService _User { get; set; }
        public RepositoryServices(IWebHostEnvironment environment, IUserService User, IOptions<DbContextSettings> appsetting)
        {
            _environment = environment;
            _User = User;

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

        public async Task<(bool Status, string Message)> UploadServices(string userid, RepoReqDTO Entity)
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
                var path = Path.Combine(_environment.WebRootPath, "File/"+GetNamingFolder.Trim());
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


                var CheckExisting = await _RecoveryContext.Masterrepository.Where(es => es.Fiturid == Entity.FiturId
                && es.Doctype == Entity.DocType && es.Requestid == Entity.RequestId).ToListAsync();
                if (CheckExisting.Count < 0)
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

    }
}
