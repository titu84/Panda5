using PandaRecipes.Model.Sqlite;
using PandaRecipes.Utils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PandaRecipes.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<string> Categories { get; set; }  
   
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

        public CategoriesViewModel()
        {
            Title = "Kategorie";
            Categories = new ObservableCollection<string>();
            Task.Run(async () => await Init());            
        }

        private async Task Init()
        {
            var categories = await App.LocalDB.GetCategories();
            foreach (var c in categories)
            {
                Categories.Add(c);
            }
        }
        public static async void LvItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await NavigationService.NavigateTo(new Views.RecipesPage(e.Item.ToString()));
        }
    }
}
