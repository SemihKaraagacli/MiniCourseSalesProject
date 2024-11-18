using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.GenericRepository;

namespace MiniCourseSalesProject.Repository.PaymentRepository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Sipariş ID'sine göre ödeme bilgisi getir
        public async Task<Payment> GetPaymentByOrderIdAsync(Guid orderId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }
    }
}
