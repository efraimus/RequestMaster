using ReactiveUI;
using RequestMaster.Tabs.RequestsTabMVVM.ViewModels;

namespace RequestMaster.ViewModels
{
    public class RequestsViewModel : ReactiveObject
    {
        public static CreateRequestViewModel createRequestViewModel { get; set; } = new CreateRequestViewModel();

        public static RequestDetailsViewModel requestDetailsViewModel { get; set; } = new RequestDetailsViewModel();

        public static RequestsGridViewModel requestsGridViewModel { get; set; } = new RequestsGridViewModel();
    }
}
