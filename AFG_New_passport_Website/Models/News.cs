using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان خبر ضروری است")]
        [StringLength(200, ErrorMessage = "عنوان خبر نمی‌تواند بیشتر از 200 کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "وارد کردن متن خبر ضروری است")]
        [StringLength(4000, ErrorMessage = "متن خبر نمی‌تواند بیشتر از 4000 کاراکتر باشد")]
        public string Content { get; set; }

        [StringLength(255, ErrorMessage = "مسیر تصویر نمی‌تواند بیشتر از 255 کاراکتر باشد")]
        public string? ImagePath { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }
    }
}
