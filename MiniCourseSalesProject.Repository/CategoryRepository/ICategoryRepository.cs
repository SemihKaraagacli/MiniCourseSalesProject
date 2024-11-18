using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        // Kategoriye özgü ek işlemler
        Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId);
    }
}
