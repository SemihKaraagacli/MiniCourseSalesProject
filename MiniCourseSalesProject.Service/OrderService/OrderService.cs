using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Repository.BasketItemRepository;
using MiniCourseSalesProject.Repository.BasketRepository;
using MiniCourseSalesProject.Repository.CategoryRepository;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.OrderRepository;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.BasketService.Dtos;
using MiniCourseSalesProject.Service.OrderService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.OrderService
{
    public class OrderService(IOrderRepository orderRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository, IBasketItemRepository basketItemRepository, IBasketRepository basketRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<ServiceResult<Guid>> CreateOrderAsync(OrderCreateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
            if (hasUser is null)
            {
                return ServiceResult<Guid>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var hasBasket = await basketRepository.GetBasketItemInCourseByUserAsync(request.UserId);
            if (hasBasket is null)
            {
                return ServiceResult<Guid>.Fail("Course not found.", HttpStatusCode.NotFound);
            }
            //var courseByBasket = await courseRepository.GetCourseByBasketAsync(request.BasketId);

            decimal totalAmount = 0;

            // Toplam fiyat hesapla ve stokları kontrol et
            foreach (var course in hasBasket.BasketItems)
            {
                var requestedQuantity = course.Quantity;
                totalAmount = totalAmount + (course.Course.Price * requestedQuantity);
            }

            var newOrder = new Order
            {
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                UpdatedDate = DateTime.UtcNow,
                BasketId = request.BasketId,
                OrderStatus = "Waiting",
            };
            await orderRepository.AddAsync(newOrder);
            await unitOfWork.CommitAsync();
            var orderToDTo = new OrderDto
            {
                Id = newOrder.Id,
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
            var coursesInBasket = await basketRepository.GetBasketItemInCourseByUserAsync(hasOrder.UserId);
            var a = await basketItemRepository.GetItemsByBasketIdAsync(hasOrder.BasketId);
            var orderToDTo = new OrderDto
            {
                Id = hasOrder.Id,
                UserId = hasOrder.UserId,
                BasketItemInCourseResponses = coursesInBasket.BasketItems.Select(x => new BasketItemInCourseResponse
                {
                    Id = x.Course.Id,
                    BasketId = x.BasketId,
                    BasketItemId = a.Id,
                    CategoryName = "qq",
                    Name = x.Course.Name,
                    CreatedDate = x.Course.CreatedDate,
                    Price = x.Course.Price,
                    Quantity = x.Quantity,
                    Description = x.Course.Description,
                }).ToList(),
                CreatedDate = hasOrder.CreatedDate,
                UpdatedDate = hasOrder.UpdatedDate,
                TotalAmount = hasOrder.TotalAmount,
                Status = hasOrder.OrderStatus,
            };
            return ServiceResult<OrderDto>.Success(orderToDTo, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<List<OrderDto>>> GetOrderAllAsync() // bunu user üzerinden göstermek icin alacaz
        {
            var allOrder = await orderRepository.GetAllAsync();
            var orderResponses = allOrder.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedDate = order.CreatedDate,
                TotalAmount = order.TotalAmount,
                Status = order.OrderStatus,
            }).ToList();
            return ServiceResult<List<OrderDto>>.Success(orderResponses, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<OrderDto>>> GetOrdersByUserIdAsync(Guid userId)
        {
            var hasOrder = await orderRepository.GetOrdersByUserIdAsync(userId);

            if (hasOrder is null)
            {
                return ServiceResult<List<OrderDto>>.Fail("Order not found.", HttpStatusCode.NotFound);
            }
            var orderDtos = hasOrder.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                TotalAmount = order.TotalAmount,
                Status = order.OrderStatus,
            }).ToList();
            return ServiceResult<List<OrderDto>>.Success(orderDtos, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> DeleteOrderAsync(Guid orderId)
        {
            var hasOrder = await orderRepository.GetByIdAsync(orderId);
            if (hasOrder is null)
            {
                return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
            }
            var basket = await basketRepository.GetByIdAsync(hasOrder.BasketId);
            var basketItem = await basketItemRepository.GetItemsByBasketIdAsync(hasOrder.BasketId);
            basketItemRepository.Delete(basketItem);
            basketRepository.Delete(basket);
            orderRepository.Delete(hasOrder);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
