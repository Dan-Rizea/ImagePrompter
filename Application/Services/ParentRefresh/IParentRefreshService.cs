namespace Application.Services.ParentRefresh
{
    public interface IParentRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}
