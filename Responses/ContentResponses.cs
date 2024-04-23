using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Entities;
using System.Collections.Generic;

namespace sky.recovery.Responses
{
    public class ContentResponsesEntity
    {
        public int userId { get; set; }
    }

    public class ContentResponsesGenericParam
    {
        public List<GenericParamDTO> DokumenRestruktur { get; set; }
        public List<GenericParamDTO> PolaRestruktur { get; set; }

    }
    public class ContentResponses
    {
        
        public List<ListNasabahDTO> Nasabah { get; set; }
        public List<ListRestructureDTO> RestructureDTOs { get; set; }
        public List<MonitoringDetailRestructureDTO> monitoringDetailRestructures { get; set; }
    public DetailNasabah DetailNasabah { get; set; }
        public List<DetailNasabahDTO> DetaillistNasabah { get; set; }
        public List<SegmentNasabahDTO> SegmentListNasabah{ get; set; }
        public DetailRestructure DetailRestructures { get; set; }
        public ListCashFlowDTO CashFlowDTO { get; set; }
    }
}
