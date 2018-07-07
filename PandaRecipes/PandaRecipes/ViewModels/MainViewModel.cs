using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PandaRecipes.Model.Sqlite;
using PandaRecipes.Utils;
using Plugin.Media;
using Plugin.Share;
using Xamarin.Forms;

namespace PandaRecipes.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Command<string> GoToPageCommand { get; private set; }     

        public MainViewModel()
        {
            GoToPageCommand = new Command<string>(async (p) => await GotoPage(p));
            Title = "Przepisy WSEI";
            Img = ImageSource.FromFile("logo.png");            
        }       

        private ImageSource _img;
        public ImageSource Img
        {
            get => _img;
            set
            {
                if (_img != value)
                {
                    _img = value;
                    RaisePropertyChanged(nameof(Img));
                }
            }
        }
      
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
        private async Task GotoPage(string p)
        {
            ContentPage page = null;
            switch (p)
            {
                case nameof(Views.RecipesPage):
                    page = new Views.RecipesPage();
                    break;
                case nameof(Views.RecipeDetailsPage):
                    page = new Views.RecipeDetailsPage(null);
                    break;
                case nameof(Views.AboutPage):
                    page = new Views.AboutPage();
                    break;
                default:
                    page = new MainPage();
                    break;
            }
            await NavigationService.NavigateTo(page);
        }
    }
}
