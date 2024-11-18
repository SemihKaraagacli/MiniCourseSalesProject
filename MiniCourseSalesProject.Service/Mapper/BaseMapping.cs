using AutoMapper;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.CategoryService.Dtos;
using MiniCourseSalesProject.Service.CourseService.Dtos;
using MiniCourseSalesProject.Service.OrderService.Dtos;

namespace MiniCourseSalesProject.Service.Mapper
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Course, CourseDto>();

            CreateMap<Order, OrderDto>();
        }
    }
}
