namespace sky.recovery.Model
{
    public class GeneralParamHeader
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int? ParamHeaderId { get; set; }

        public string DetailTitle { get; set; }
        public string DetailDescriptions { get; set; }
    }
}
