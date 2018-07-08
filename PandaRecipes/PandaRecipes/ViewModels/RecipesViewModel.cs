using PandaRecipes.Model.Sqlite;
using PandaRecipes.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PandaRecipes.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }        
        public RecipesViewModel(string category = null)
        {
            Title = "Przepisy";
            Recipes = new ObservableCollection<Recipe>();
            Task.Run(async () => await Init(category));           
        }
        private async Task Init(string category = null)
        {
            List<Recipe> recipes = new List<Recipe>();
            if (category != null)
                recipes = await App.LocalDB.GetRecipeByCategory(category);
            else
                recipes = await App.LocalDB.GetItems<Recipe>();

            foreach (var r in recipes)
            {
                Recipes.Add(r);
            }
        }
        public static async void LvItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await NavigationService.NavigateTo(new Views.RecipeDetailsPage((Recipe)e.Item));
        }
    }
}
