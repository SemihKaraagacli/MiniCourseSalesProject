using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class CourseCreateTupleViewModel
    {
        public List<CategoryResponse> Categories { get; set; }
        public CourseCreateRequest Course { get; set; }
    }
}
