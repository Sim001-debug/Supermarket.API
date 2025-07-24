using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Services.Communication
{
    public class BaseResponse
    {
        [Required]
        public bool Success { get; private set; }
        [Required]
        public string Message { get; private set; }
        protected BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
