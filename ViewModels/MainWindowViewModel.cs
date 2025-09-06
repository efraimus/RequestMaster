using ReactiveUI;

namespace RequestMaster.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public RequestsViewModel requestsViewModel { get; set; } = new RequestsViewModel();

        public ProfileViewModel profileViewModel { get; set; } = new ProfileViewModel();

        public SettingsViewModel settingsViewModel { get; set; } = new SettingsViewModel();
    }
}