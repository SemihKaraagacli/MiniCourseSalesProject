using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class CourseByCategoryResponseViewModel
    {
        public List<CourseResponse> Courses { get; set; }
        public List<CategoryResponse> Categories { get; set; }
        public Guid? categoryId { get; set; } // Kullanıcının seçtiği kategori
    }
}
