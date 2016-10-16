using FreshMvvm;
using FreshTinyIoC;
using PokeApp.Interfaces;
using PokeApp.PageModels;
using PokeApp.Services;
using Xamarin.Forms;

namespace PokeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterDependencies();

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>());
        }

        private void RegisterDependencies()
        {
            FreshTinyIoCContainer.Current.Register<IPokeApiService>(new PokeApiService());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}