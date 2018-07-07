using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandaRecipes.Model.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PandaRecipes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeDetailsPage : ContentPage
	{
		public RecipeDetailsPage(Recipe recipe)
		{
			InitializeComponent();
            BindingContext = new ViewModels.RecipeDetailsViewModel(recipe);
		}
	}
}