﻿using sky.recovery.DTOs.RepositoryDTO;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRepositoryServices
    {
        public Task<(bool status, string message)> DocumentNotes(List<DocNotesDTO> Entity);

        public Task<(bool Status, string Message)> RemoveDoc(int id);
        public Task<(bool Status, string Message)> UploadServices(string userid, RepoReqDTO Entity);

    }
}
