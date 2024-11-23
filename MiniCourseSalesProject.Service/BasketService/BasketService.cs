using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Repository.BasketItemRepository;
using MiniCourseSalesProject.Repository.BasketRepository;
using MiniCourseSalesProject.Repository.CategoryRepository;
using MiniCourseSalesProject.Repository.CourseRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.BasketService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.BasketService
{
    public class BasketService(IBasketRepository basketRepository, UserManager<AppUser> userManager, ICourseRepository courseRepository, ICategoryRepository categoryRepository, IBasketItemRepository basketItemRepository, IUnitOfWork unitOfWork) : IBasketService
    {
        public async Task<ServiceResult<Guid>> CreateBasketAsync(BasketCreateRequest request)
        {
            var basket = await basketRepository.GetBasketByUserAsync(request.UserId);
            if (basket == null)
            {
                // Yeni bir sepet oluşturuluyor
                basket = new Basket
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    BasketItems = new List<BasketItem>()
                };
                await basketRepository.AddAsync(basket);
                await unitOfWork.CommitAsync(); // await kullanarak işlemin tamamlanmasını sağlıyoruz
            }
            else
            {
                // Sepet zaten mevcut, bağlanıyor
                unitOfWork.Attach(basket);  // Attach etme işlemi doğru
            }

            var basketByCourse = await basketRepository.GetBasketItemByUserAsync(request.UserId);
            // Sepet öğelerini güncelleme işlemi
            var existingItem = basketByCourse.BasketItems.FirstOrDefault(bi => bi.CourseId == request.CourseId);
            if (existingItem != null)
            {
                // Eğer zaten varsa, miktarı arttır
                existingItem.Quantity += request.Quantity;
                await unitOfWork.CommitAsync();
            }
            else
            {
                var newBasketIte = new BasketItem
                {
                    Id = Guid.NewGuid(),
                    BasketId = basket.Id,
                    CourseId = request.CourseId,
                    Quantity = request.Quantity,
                };
                await basketItemRepository.AddAsync(newBasketIte);
                basket.BasketItems.Add(newBasketIte);
                await unitOfWork.CommitAsync();
            }



            return ServiceResult<Guid>.Success(basket.Id, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> DeleteBasketAsync(Guid BasketId)
        {
            var hasBasket = await basketRepository.GetByIdAsync(BasketId);
            if (hasBasket is null)
            {
                return ServiceResult.Fail("Basket not found.", HttpStatusCode.NotFound);
            }
            basketRepository.Delete(hasBasket);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult> DeleteCourseFromBasketAsync(Guid UserId, Guid BasketItemId)
        {
            var hasBasket = await basketRepository.GetBasketItemByUserAsync(UserId);
            if (hasBasket is null)
            {
                return ServiceResult.Fail("Basket not found.", HttpStatusCode.NotFound);
            }
            var basketItem = await basketItemRepository.GetByIdAsync(BasketItemId);
            if (basketItem != null)
            {
                basketItemRepository.Delete(basketItem);
            }
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<BasketItemInCourseResponse>>> GetBasketItemInCourseAsync(Guid UserId)
        {
            var basket = await basketRepository.GetBasketItemInCourseByUserAsync(UserId);
            if (basket == null)
            {
                return ServiceResult<List<BasketItemInCourseResponse>>.Success(null, HttpStatusCode.OK);
            }
            var basketItemresult = await basketItemRepository.GetItemsByBasketIdAsync(basket.Id);
            var entityToDto = basket.BasketItems.Select(item => new BasketItemInCourseResponse
            {
                Id = item.Id,
                BasketId = basketItemresult.BasketId,
                BasketItemId = basketItemresult.Id,
                Name = item.Course.Name,
                Price = item.Course.Price,
                Description = item.Course.Description,
                Quantity = basketItemresult.Quantity,
                CreatedDate = item.Course.CreatedDate,
                UpdatedDate = item.Course.UpdatedDate,
            }).ToList();

            return ServiceResult<List<BasketItemInCourseResponse>>.Success(entityToDto, HttpStatusCode.OK);
        }
    }
}
