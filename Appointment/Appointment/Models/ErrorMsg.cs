using System.Net;
 
namespace appoimntlastlq.Models.DB
{
    public class ErrorMsg
    {
        public ErrorMsg() { }
        public ErrorMsg(int state)
        {
            Status = state;
          //  Title = CusStatusCode.Msg[state];
        }
        public ErrorMsg(HttpStatusCode stateEnum)
        {
            Status = (int)stateEnum;
          //  Title = CusStatusCode.Msg[Status];
        }

        public string Type { set; get;  }
        public int Status { set; get; }
        public string Title { set; get; }
        public string Message { set; get; }
        public string TraceId { set; get; }
        
    }
}
