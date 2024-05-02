using System.Collections.Generic;

namespace sky.recovery.Responses
{
    public class GeneralResponsesV2
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public List<dynamic> Data { get; set; }
    }

    public class ModellingGeneralResponsesV2
    {
        public GeneralResponsesV2 Return()
        {
            var Data = new GeneralResponsesV2()
            {
                Error=false,
                Message="",
                Data=null
            };
            return Data;
        }
    }
}
