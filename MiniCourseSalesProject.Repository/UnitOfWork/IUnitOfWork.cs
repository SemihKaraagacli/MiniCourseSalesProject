namespace MiniCourseSalesProject.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
