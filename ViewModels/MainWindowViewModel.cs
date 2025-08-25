using ReactiveUI;

namespace OrdersApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public FindWorkViewModel findWorkViewModel { get; set; } = new FindWorkViewModel();

        public FindEmployeeViewModel findEmployeeViewModel { get; set; } = new FindEmployeeViewModel();

        public ProfileViewModel profileViewModel { get; set; } = new ProfileViewModel();

        public SettingsViewModel settingsViewModel { get; set; } = new SettingsViewModel();
    }
}