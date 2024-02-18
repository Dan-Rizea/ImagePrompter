namespace Application.Services.ParentRefresh
{
    public class ParentRefreshService : IParentRefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
