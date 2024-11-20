using MiniCourseSalesProject.Web.Models.Dtos;

namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class CourseUpdateTupleViewModel
    {
        public List<CategoryResponse> Categories { get; set; }
        public CourseUpdateRequest Course { get; set; }
        public CourseResponse CourseResponse { get; set; }
    }
}
