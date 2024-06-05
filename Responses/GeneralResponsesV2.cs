using OfficeOpenXml;
using Org.BouncyCastle.Pkcs;
using sky.recovery.DTOs.HelperDTO;
using sky.recovery.Entities.RecoveryConfig;
using System.Collections.Generic;
using System.IO;

namespace sky.recovery.Responses
{
    public class GeneralResponsesV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }

       public List<dynamic> Data { get; set; }

    }
   
    public class GeneralResponsesDictionaryV2
    {
        public bool? Status { get; set; }
        public string Message { get; set; }

        public Dictionary<string,List<dynamic>> Data { get; set; }

    }
    public class GeneralResponsesPDFV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public MemoryStream Data { get; set; }

    }
    public class GeneralResponsesV2DocExcel
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public List<dynamic> Data { get; set; }
        public List<List<dynamic>> DataFile { get; set; }

        public List<dynamic> DataSheet { get; set; }

    }
    public class GeneralResponsesConfigV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public List<GeneralParamDetail> MetodeRestruktur { get; set; }
        public List<GeneralParamDetail> JenisPengurangan{ get; set; }
        public List<dynamic> BranchList{ get; set; }
        public List<dynamic> DataRestrukture { get; set; }

        public List<dynamic> Data { get; set; }
    }
    public class GeneralResponsesDocRestrukturV2
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DataDocRestrukture Data { get; set; }

    }

    public class DataDocRestrukture
    {
        public List<dynamic> DocStrukturRule { get; set; }
        public List<dynamic> DocRestruktur { get; set; }
     


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
    public class GeneralResponsesDictionary
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        Dictionary<string, List<dynamic>> Data { get; set; }


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

        
        public GeneralResponsesDictionaryV2 ReturnDictionary()
        {
            var Data = new GeneralResponsesDictionaryV2()
            {
                Status = false,
                Message = "",
                Data = null
            };
            return Data;
        }
        public GeneralResponsesPDFV2 PDFReturn()
        {
            var Data = new GeneralResponsesPDFV2()
            {
                Status = false,
                Message = "",
                Data = null
            };
            return Data;
        }
        public GeneralResponsesV2DocExcel ExcelReturn()
        {
            var Data = new GeneralResponsesV2DocExcel()
            {
                Status = false,
                Message = "",
              DataSheet=null,
             DataFile=null,
                Data = null
            };
            return Data;
        }

        public GeneralResponsesConfigV2 GeneralResponsesConfigData()
        {
            var Data = new GeneralResponsesConfigV2()
            {
                Status = false,
                Message = "",
                MetodeRestruktur=null,
                BranchList=null,
                DataRestrukture=null,
                Data = null
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

        public GeneralResponsesDocRestrukturV2 GeneralResponseDocRestruktur()
        {
            var Data = new GeneralResponsesDocRestrukturV2()
            {
                Status = false,
                Message = "",
                Data = null
            };
            return Data;
        }

    }
}
