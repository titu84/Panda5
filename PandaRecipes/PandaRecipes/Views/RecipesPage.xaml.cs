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
	public partial class RecipesPage : ContentPage
	{
		public RecipesPage()
		{
			InitializeComponent();
            BindingContext = new RecipesViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lvItems.ItemsSource = await App.LocalDB.GetItems<Recipe>();
            lvItems.ItemTapped -= LvItems_ItemTapped;
            lvItems.ItemTapped += LvItems_ItemTapped;
        }

        private async void LvItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {           
            await NavigationService.NavigateTo(new Views.RecipeDetailsPage((Recipe)e.Item));
        }
    }
}