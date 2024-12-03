using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Repository.BasketItemRepository;
using MiniCourseSalesProject.Repository.BasketRepository;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.OrderRepository;
using MiniCourseSalesProject.Repository.PaymentRepository;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.PaymentService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.PaymentService
{
    public class PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IBasketItemRepository basketItemRepository, IBasketRepository basketRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IPaymentService
    {

        public async Task<ServiceResult<PaymentDto>> ProcessPaymentAsync(PaymentCreateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.userId.ToString());
            if (hasUser is null)
            {
                return ServiceResult<PaymentDto>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var hasOrder = await orderRepository.GetByIdAsync(request.OrderId);
            if (hasOrder is null)
            {
                return ServiceResult<PaymentDto>.Fail("User not found.", HttpStatusCode.NotFound);
            }

            var userWallet = hasUser.Wallet;
            var orderAmount = hasOrder.TotalAmount;

            if (userWallet < orderAmount)
            {
                var priceDifference = orderAmount - userWallet;
                var newInsufficientPayment = new Payment
                {
                    PaymentStatus = "Failed",
                };
                var insufficientPayment = new PaymentDto
                {
                    PaymentStatus = newInsufficientPayment.PaymentStatus,
                };
                return ServiceResult<PaymentDto>.Success(insufficientPayment, HttpStatusCode.OK);
            }
            if (userWallet == orderAmount)
            {
                var newInsufficientPayment = new Payment
                {
                    PaymentStatus = "Failed",
                };
                var insufficientPayment = new PaymentDto
                {
                    PaymentStatus = newInsufficientPayment.PaymentStatus,
                };
                return ServiceResult<PaymentDto>.Success(insufficientPayment, HttpStatusCode.OK);
            }

            var newPayment = new Payment
            {
                Amount = orderAmount,
                OrderId = request.OrderId,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = "Completed",
            };


            var basket = await basketRepository.GetByIdAsync(hasOrder.BasketId);
            var basketItem = await basketItemRepository.GetItemsByBasketIdAsync(hasOrder.BasketId);



            await paymentRepository.AddAsync(newPayment);

            hasOrder.OrderStatus = "Completed";
            orderRepository.Update(hasOrder);

            var moneyLeft = userWallet - orderAmount;
            hasUser.Wallet = moneyLeft;
            await userManager.UpdateAsync(hasUser);

            basketItemRepository.Delete(basketItem);
            basketRepository.Delete(basket);

            await unitOfWork.CommitAsync();
            var entityToDto = new PaymentDto
            {
                Id = newPayment.Id,
                Amount = newPayment.Amount,
                OrderId = newPayment.OrderId,
                PaymentDate = newPayment.PaymentDate,
                PaymentStatus = newPayment.PaymentStatus,
            };
            return ServiceResult<PaymentDto>.Success(entityToDto, HttpStatusCode.OK);

        }
    }
}
