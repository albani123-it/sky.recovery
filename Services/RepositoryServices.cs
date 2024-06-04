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
        public async Task<(bool Status, string Message)> UploadServices(RepoReqDTO Entity)
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


                var getCallBy = await _User.GetDataUser(Entity.UserId);
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
               
                var Data = new  Masterrepository()
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

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }
}
