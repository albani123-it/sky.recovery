﻿using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAydaServices
    {
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAyda(GetDetailAydaDTO Entity);
        public Task<(bool Status, string Message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAydaForApproval(int AydaId, int loanid, int CustomerId);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> DummyNasabah(int pagenumber, int pagesieze);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsertBulk(int banyak);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> GetMasterLoan();

        public Task<(bool? Status, GeneralResponsesV2 Returns)> AydaMonitoring(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AydaDraft(string userid,CreateAydaDTO Entity);
        public  Task<(bool? Status, GeneralResponsesV2 Returns)> AydaSubmit(string userid,CreateAydaDTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public  Task<(bool? Status, GeneralResponsesV2 Returns)> AydaTaskList(string UserId);

    }
}
