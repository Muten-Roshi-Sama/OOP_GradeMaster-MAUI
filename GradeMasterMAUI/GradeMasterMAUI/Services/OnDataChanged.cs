namespace GradeMasterMAUI.Services
{
    public class DataChangedNotifier
    {
        public static event Action OnDataChanged;

        public static void NotifyDataChanged()
        {
            OnDataChanged?.Invoke();
        }
    }

}
