using System.Text.Json.Serialization;

namespace riesgos_backend.Models
{
    public class HttpResponseApi
    {
        public string TimeStamp { get; set; }
        public string Message { get; set; }
        public  int Code { get; set; }
        public string Status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Object? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        public HttpResponseApi(string message, int code, string status, Object? data=null, string? error=null)
        {
            TimeStamp = DateTime.UtcNow.ToString("o");
            Message = message;
            Code = code;
            Status = status;
            Data = data;
            Error = error;
        }
    }

    enum HttpCode
    {
        OK = 200,
        BAD_REQUEST = 400,
        UNAUTHORIZED_REQUEST = 401,
        NOT_FOUND = 404,
        CREATED = 201,
        INTERNAL_SERVER_ERROR = 500
    }

    enum HttpStatusCode
    {
        OK,
        BAD_REQUEST,
        NOT_FOUND,
        CREATED,
        INTERNAL_SERVER_ERROR,
        UNAUTHORIZED_REQUEST
    }
}
