using System.Collections.Generic;

namespace sky.recovery.Responses
{
    public class GeneralResponsesV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }

       public List<dynamic> Data { get; set; }

    }

    public class GeneralResponsesDetailRestrukturV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DataDetailRestruktur Data { get; set; }

    }
    public class Data
    {
        public List<dynamic> DetailNasabah { get; set; }
        public List<dynamic> FasilitasLainnya { get; set; }
       public List<dynamic> Restrukturisasi { get; set; }
        public List<dynamic> MasterLoan { get; set; }

    }
    public class DataDetailRestruktur
    {
        public List<dynamic> DetailNasabah { get; set; }
        public List<dynamic> FasilitasLainnya { get; set; }
        public List<dynamic> DataAgunan { get; set; }
        public List<dynamic> Permasalahan { get; set; }


    }
    public class ModellingGeneralResponsesV2
    {
        public GeneralResponsesV2 Return()
        {
            var Data = new GeneralResponsesV2()
            {
                Status=false,
                Message="",
                Data=null
            };
            return Data;
        }
        public GeneralResponsesDetailRestrukturV2 GeneralResponseDetailRestruktur()
        {
            var Data = new GeneralResponsesDetailRestrukturV2()
            {
                Status = false,
                Message = "",
                Data = null
            };
            return Data;
        }
    }
}
