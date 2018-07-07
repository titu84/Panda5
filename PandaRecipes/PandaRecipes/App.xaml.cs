using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PandaRecipes.Data;
using PandaRecipes.Services.Interfaces;
using Xamarin.Forms;

namespace PandaRecipes
{
	public partial class App : Application
	{
        private static LocalDB localDB;

        public static LocalDB LocalDB
        {
            get
            {
                if (localDB == null)
                {
                    var fileHelper = DependencyService.Get<IFileHelper>();
                    var filename = fileHelper.GetLocalFilePath("app.db3");
                    localDB = new LocalDB(filename);
                }

                return localDB;
            }
        }

		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new PandaRecipes.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
