namespace BlazorProds.Utility
{
    public class UserStateService
    {
        public bool IsUserLoggedIn { get; private set; }

        public event Action OnChange;

        public void LoginUser()
        {
            IsUserLoggedIn = true;
            NotifyStateChanged();
        }

        public void LogoutUser()
        {
            IsUserLoggedIn = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
