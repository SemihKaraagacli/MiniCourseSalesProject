using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.CourseRepository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Kategoriye göre kursları getir
        public async Task<List<Course>> GetCoursesByCategoryAsync(Guid categoryId)
        {
            return await _context.Courses.Where(c => c.CategoryId == categoryId).ToListAsync();
        }
    }
}