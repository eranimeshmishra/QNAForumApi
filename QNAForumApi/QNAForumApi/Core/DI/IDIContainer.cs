namespace QNAForumApi.Core.DI
{
    public interface IDIContainer
    {
        TService GetInstance<TService>() where TService : class;
    }
}