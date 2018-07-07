using PandaRecipes.Model.Sqlite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public RecipesViewModel()
        {
            Title = "Przepisy";
            Recipes = new ObservableCollection<Recipe>();
            Task.Run(async () => await Init());            
        }

        private async Task Init()
        {
            var recipes = await App.LocalDB.GetItems<Recipe>();
            foreach (var r in recipes)
            {
                Recipes.Add(r);
            }
        }      
    }
}
