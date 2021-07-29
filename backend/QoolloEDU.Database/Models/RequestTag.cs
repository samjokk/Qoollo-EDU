namespace QoolloEDU.Database.models
{
    public class RequestTag
    {
        public int TagId { get; set; }
        public int RequestId { get; set; }
        
        public Tag Tag { get; set; }
        public Request Request { get; set; }
    }
}