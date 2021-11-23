namespace TraderPanel.Core.Models
{
    public class Response
    {
        public Response()
        {
            Success = true;
            Status = 200;
        }

        public bool Success { get; set; }
        public string Error { get; set; }
        public dynamic Result { get; set; }
        public int Status { get; set; } 
    }
}
