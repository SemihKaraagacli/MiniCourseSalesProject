using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.PaymentRepository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        // Ödeme işlemleri ile ilgili ek işlemler
        Task<Payment> GetPaymentByOrderIdAsync(Guid orderId);
    }
}
