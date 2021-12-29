using Pusok_Beata_Lab12.Services;
using Pusok_Beata_Lab12.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pusok_Beata_Lab12.Data;

namespace Pusok_Beata_Lab12
{
    public partial class App : Application
    {
        public static ShoppingListDatabase Database { get; private set; }
        public App()
        {
            /*  InitializeComponent();

              DependencyService.Register<MockDataStore>();
              MainPage = new AppShell();*/
            Database = new ShoppingListDatabase(new RestService());
            MainPage = new NavigationPage(new ListEntryPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
