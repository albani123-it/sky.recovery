﻿using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRecoveryServices
    {
        // public Task<(bool Error, GeneralResponses Returns)> ListCollection(string userid);
        public Task<(bool? Error, GenericResponses<ListRestructureDTO> Returns)> ListRestructure();
        public Task<(bool Error, GeneralResponses Returns)> MonitoringListDetail(string userid);
        public Task<(bool Error, GeneralResponses Returns)> GetGeneralDetailNasabah(RequestRestrukturDetail Entity);
        public  Task<(bool Error, GeneralResponses Returns)> GetRestrukturDetailByAccno(string accno);
        public Task<(bool Error, GeneralResponses Returns)> UpdatePengajuanRestrukturisasi(UpdateRestrukturisasi Entity);
        public Task<(bool Error, GeneralResponses Returns)> ListSearchRestructure(SearchListRestrucutre Entity);
        public  Task<(bool Error, GeneralResponses Returns)> ListSearchMonitoringListDetail(SearchListRestrucutre Entity);

        public Task<(bool Error, GeneralResponses Returns)> GetDokumenParam();

        public Task<(bool Error, GeneralResponses Returns)> GetPolaRestrukturParam();
        public Task<(bool Error, GeneralResponses Returns)> GetListBranch();
        public  Task<(bool Error, GeneralResponses Returns)> GetJenisPengurangan();
        public Task<(bool Error, GeneralResponses Returns)> CreateRestructure(CreateNewRestructure Entity);

    }
}
