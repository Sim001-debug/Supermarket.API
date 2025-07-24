using Supermarket.API.Models;

namespace Supermarket.API.Services.Communication
{
    public class SaveCategoryResponse : BaseResponse
    {
        public Category Category { get; private set; }
        public SaveCategoryResponse(bool success, string message, Category category) 
            : base(success, message)
        {
            Category = category;
        }

        public SaveCategoryResponse(Category category) : this(true, string.Empty, category) { }

        public SaveCategoryResponse(string message) : this(false, message, null) { }
    }
}
