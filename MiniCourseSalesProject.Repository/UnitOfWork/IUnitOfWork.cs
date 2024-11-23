using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task<Basket> Attach(Basket basket);
    }
}
