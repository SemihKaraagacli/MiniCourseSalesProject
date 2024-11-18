using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.OrderRepository;
using MiniCourseSalesProject.Repository.PaymentRepository;
using MiniCourseSalesProject.Repository.UnitOfWork;
using MiniCourseSalesProject.Service.PaymentService.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.PaymentService
{
    public class PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
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
                return ServiceResult<PaymentDto>.Fail($"insufficient balance:{priceDifference}", HttpStatusCode.BadRequest);
            }
            if (userWallet == orderAmount)
            {
                return ServiceResult<PaymentDto>.Fail("insufficient balance", HttpStatusCode.BadRequest);
            }

            var newPayment = new Payment
            {
                Amount = orderAmount,
                OrderId = request.OrderId,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Completed,
            };
            await paymentRepository.AddAsync(newPayment);
            hasOrder.OrderStatus = OrderStatus.Completed;
            orderRepository.Update(hasOrder);
            var moneyLeft = userWallet - orderAmount;
            hasUser.Wallet = moneyLeft;
            await userManager.UpdateAsync(hasUser);
            await unitOfWork.CommitAsync();
            var entityToDto = new PaymentDto
            {
                Id = newPayment.Id,
                Amount = newPayment.Amount,
                OrderId = newPayment.OrderId,
                PaymentDate = newPayment.PaymentDate,
                PaymentStatus = newPayment.PaymentStatus,
            };
            return ServiceResult<PaymentDto>.Success(entityToDto, HttpStatusCode.BadRequest);

        }
    }
}
