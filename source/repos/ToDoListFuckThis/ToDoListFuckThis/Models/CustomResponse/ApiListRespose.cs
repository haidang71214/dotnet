using System.Net;

namespace ToDoListFuckThis.Models.CustomResponse
{
    // này đơn giản thôi, kế thừa từ api response xong gọi lại
    public class ApiListResponse<T> : ApiResponse
    {
        public new List<T> Result { get; set; }

        public static ApiListResponse<T> Success(List<T> result, HttpStatusCode code = HttpStatusCode.OK)
            => new() { Result = result, StatusCode = code };

        public static ApiListResponse<T> Fail(string error, HttpStatusCode code = HttpStatusCode.BadRequest)
            => new() { IsSuccess = false, StatusCode = code, ErrorMessages = new() { error } };
    }

}
