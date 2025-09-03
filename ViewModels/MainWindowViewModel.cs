using ReactiveUI;

namespace RequestMaster.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public RequestsViewModel findWorkViewModel { get; set; } = new RequestsViewModel();

        public CreateRequestViewModel findEmployeeViewModel { get; set; } = new CreateRequestViewModel();

        public ProfileViewModel profileViewModel { get; set; } = new ProfileViewModel();

        public SettingsViewModel settingsViewModel { get; set; } = new SettingsViewModel();
    }
}