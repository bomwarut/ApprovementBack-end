namespace ApprovementBack_end.Models
{
    public class ApproveRequest
    {
        public int ID { get; set; }
        public string REASON { get; set; }
        public int STATUS { get; set; }
        public DateTime CREATE_AT { get; set; }
        public DateTime UPDATE_AT { get; set; }
    }
}
