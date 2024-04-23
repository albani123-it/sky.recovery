using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.DTOs.ResponsesDTO
{
    public class GenericParamDTO
    {
       
        public int? glp_id { get; set; }

       
        public string? glp_name { get; set; }

        public string? glp_code { get; set; }
        public string? glp_type { get; set; }
    }
}
