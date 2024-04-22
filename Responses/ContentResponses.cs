using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Entities;
using System.Collections.Generic;

namespace sky.recovery.Responses
{
    public class ContentResponsesEntity
    {
        public int userId { get; set; }
    }

    public class ContentResponses
    {
        public List<ListNasabahDTO> Nasabah { get; set; }
    }
}
