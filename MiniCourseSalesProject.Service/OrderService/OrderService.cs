using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.OrderRepository;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.OrderService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.OrderService
{
    public class OrderService(IOrderRepository orderRepository, ICourseRepository courseRepository, UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<ServiceResult<Guid>> CreateOrderAsync(OrderCreateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
            if (hasUser is null)
            {
                return ServiceResult<Guid>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var hasCourses = await courseRepository.GetAllAsync();
            var orderedCourse = hasCourses.Where(x => request.CourseId.Contains(x.Id)).ToList();
            if (!orderedCourse.Any())
            {
                return ServiceResult<Guid>.Fail("Course not found.", HttpStatusCode.NotFound);
            }

            decimal totalAmount = 0;

            // Toplam fiyat hesapla ve stokları kontrol et
            foreach (var course in orderedCourse)
            {
                // Stok kontrolü
                var requestedQuantity = request.CourseId.Count(id => id == course.Id);
                totalAmount = totalAmount + (course.Price * requestedQuantity);
            }

            var newOrder = new Order
            {
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                UpdatedDate = DateTime.UtcNow,
                Courses = orderedCourse,
                CourseId = request.CourseId.ToList(),
                OrderStatus = "Waiting",
            };
            await orderRepository.AddAsync(newOrder);
            await unitOfWork.CommitAsync();
            var orderToDTo = new OrderDto
            {
                Id = newOrder.Id,
                CreatedDate = newOrder.CreatedDate,
                UpdatedDate = newOrder.UpdatedDate,
                TotalAmount = newOrder.TotalAmount,
                Status = newOrder.OrderStatus,
            };
            return ServiceResult<Guid>.Success(orderToDTo.Id, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<OrderDto>> GetOrderByIdAsync(Guid orderId)
        {
            var hasOrder = await orderRepository.GetByIdAsync(orderId);
            if (hasOrder is null)
            {
                return ServiceResult<OrderDto>.Fail("Order not found.", HttpStatusCode.NotFound);
            }
            var orderToDTo = new OrderDto
            {
                Id = hasOrder.Id,
                UserId = hasOrder.UserId,
                CreatedDate = hasOrder.CreatedDate,
                UpdatedDate = hasOrder.UpdatedDate,
                TotalAmount = hasOrder.TotalAmount,
                Status = hasOrder.OrderStatus,
            };
            return ServiceResult<OrderDto>.Success(orderToDTo, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<List<OrderDto>>> GetOrderAllAsync()
        {
            var allOrder = await orderRepository.GetAllAsync();
            var orderResponses = allOrder.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedDate = order.CreatedDate,
                TotalAmount = order.TotalAmount,
                Status = order.OrderStatus,
                CourseIds = order.CourseId // Siparişteki kursların ID'lerini al
            }).ToList();
            return ServiceResult<List<OrderDto>>.Success(orderResponses, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> UpdateOrderAsync(OrderUpdateRequest request)
        {
            var order = await orderRepository.GetByIdAsync(request.Id);
            if (order is null)
            {
                return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
            }

            // Kursları al
            var courses = await courseRepository.GetAllAsync();
            // Siparişin içindeki kurslar
            var orderedCourses = order.Courses.ToList();
            decimal newTotalAmount = 0;

            foreach (var course in order.Courses)
            {
                if (request.CourseIds.Contains(course.Id))
                {
                    var requestedQuantity = request.CourseIds.Count(id => id == course.Id);

                    newTotalAmount = newTotalAmount + (course.Price * requestedQuantity);
                }
            }
            order.TotalAmount = newTotalAmount;
            order.UpdatedDate = DateTime.UtcNow;
            orderRepository.Update(order);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public Task<ServiceResult<List<OrderDto>>> GetOrdersByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResult> DeleteOrderAsync(Guid orderId)
        {
            var hasOrder = await orderRepository.GetByIdAsync(orderId);
            if (hasOrder is null)
            {
                return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
            }
            orderRepository.Delete(hasOrder);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }




        //public async Task<ServiceResult<Guid>> Create(OrderCreateRequest orderCreateRequest)
        //{
        //    var hasUser = await userManager.FindByIdAsync(orderCreateRequest.UserId.ToString());
        //    if (hasUser is null)
        //    {
        //        return ServiceResult<Guid>.Fail("User not found.", HttpStatusCode.NotFound);
        //    }
        //    var hasCourse = await courseRepository.GetAsync(orderCreateRequest.CourseId);
        //    if (hasCourse is null)
        //    {
        //        return ServiceResult<Guid>.Fail("Course not found.", HttpStatusCode.NotFound);
        //    }

        //    var newOrder = new Order
        //    {
        //        OrderDate = orderCreateRequest.OrderDate,
        //        UserId = orderCreateRequest.UserId,
        //        Courses = hasCourse,
        //        Payment = new Payment { PaymentStatus = PaymentStatus.Pending }
        //    };
        //    orderRepository.CreateAsync(newOrder);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult<Guid>.Success(newOrder.Id, HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult> Update(OrderUpdateRequest orderUpdateRequest)
        //{
        //    var hasOrder = await orderRepository.GetAsync(orderUpdateRequest.Id);
        //    if (hasOrder is null)
        //    {
        //        return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
        //    }
        //    var hasCourse = await courseRepository.GetAsync(orderUpdateRequest.CourseId);
        //    if (hasCourse is null)
        //    {
        //        return ServiceResult.Fail("Course not found.", HttpStatusCode.NotFound);
        //    }
        //    hasOrder.OrderDate = orderUpdateRequest.OrderDate;
        //    hasOrder.Courses = orderUpdateRequest.CourseId.Select(courseId => new Course { Id = courseId }).ToList();
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult> Delete(Guid id)
        //{
        //    var hasOrder = await orderRepository.GetAsync(id);
        //    if (hasOrder is null)
        //    {
        //        return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
        //    }
        //    orderRepository.DeleteAsync(hasOrder);
        //    await unitOfWork.CommitAsync();
        //    return ServiceResult.Success(HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<List<OrderDto>>> Get()
        //{
        //    var allOrder = await orderRepository.GetAsync();
        //    var orderDto = allOrder.Select(x => new OrderDto(x.Id, x.OrderDate, x.Courses, x.UserId, x.Payment)).ToList();
        //    return ServiceResult<List<OrderDto>>.Success(orderDto, HttpStatusCode.OK);
        //}
        //public async Task<ServiceResult<OrderDto>> Get(Guid id)
        //{
        //    var selectOrder = await orderRepository.GetAsync(id);
        //    var map = mapper.Map<OrderDto>(selectOrder);
        //    return ServiceResult<OrderDto>.Success(map, HttpStatusCode.OK);
        //}
    }
}
