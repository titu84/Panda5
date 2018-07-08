using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandaRecipes.Model.Sqlite;
using PandaRecipes.Utils;
using PandaRecipes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PandaRecipes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesPage : ContentPage
	{
		public CategoriesPage()
		{
			InitializeComponent();
            BindingContext = new CategoriesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new CategoriesViewModel();
            lvItems.ItemTapped -= CategoriesViewModel.LvItems_ItemTapped;
            lvItems.ItemTapped += CategoriesViewModel.LvItems_ItemTapped;
        }
    }
}