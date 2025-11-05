using System.Net;

namespace ToDoListFuckThis.Models.CustomResponse
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new();
        public object Result { get; set; }

        public static ApiResponse Success(object result, HttpStatusCode code = HttpStatusCode.OK)
         => new() { Result = result, StatusCode = code };

        public static ApiResponse Fail(string error, HttpStatusCode code = HttpStatusCode.BadRequest)
            => new() { IsSuccess = false, StatusCode = code, ErrorMessages = new() { error } };
    }
}
