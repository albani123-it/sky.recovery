namespace sky.recovery.Responses
{
    public class GeneralResponses
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public ContentResponses Data { get; set; }
        public ContentResponsesEntity DataEntities { get; set; }
        public ContentResponsesGenericParam DataGenericParam { get; set; }
    }
}
