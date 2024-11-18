using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Kategorilerle ilişkili kursları getir
        public async Task<List<Course>> GetCoursesByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Courses.Where(c => c.CategoryId == categoryId).ToListAsync();
        }
    }
}
