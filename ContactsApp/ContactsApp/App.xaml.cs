using Prism;
using Prism.Ioc;
using ContactsApp.ViewModels;
using ContactsApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Services;
using ContactsApp.Data;
using ContactsApp.Helpers;
using ContactsApp.Bussiness;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ContactsApp
{
    public partial class App
    {
        string DbPath => FileHelper.GetLocalFilePath("people.db3");
        public static PersonRepository PersonRepo { get; private set; }
        public static DataService DataService { get; private set; }
        public static bool IsLogged { get; internal set; }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            IsLogged =  await UserProcess.IsLoggued();
            PersonRepo = new PersonRepository(DbPath);
            DataService = new DataService();
            if (IsLogged)
            {
                await NavigationService.NavigateAsync("http://myapp.com/" + nameof(NavigationPage) +"/"+ nameof(ContactsListPage));
            }
            else await NavigationService.NavigateAsync("http://myapp.com/" + nameof(NavigationPage) +"/"+nameof(LoginPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<ContactsListPage, ContactsListViewModel>();
            containerRegistry.RegisterForNavigation<ContactDetailPage, ContactDetailViewModel>();
            containerRegistry.Register<IPageDialogService, PageDialogService>();
        }
    }
}
