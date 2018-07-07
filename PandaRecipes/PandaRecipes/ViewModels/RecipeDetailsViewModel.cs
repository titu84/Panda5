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
    public class RecipeDetailsViewModel : BaseViewModel
    {
        public Command<string> SaveRecipeCommand { get; private set; }
        public Command TakePhotoCommand { get; private set; }
        public Command PickPhotoCommand { get; private set; }
        public Command ShareRecipeCommand { get; private set; }
        public Command DeleteRecipeCommand { get; private set; }

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

        private int _recipeId;
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

        public string _photoPath;
        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                if (_photoPath != value)
                {
                    _photoPath = value;
                    RaisePropertyChanged(nameof(PhotoPath));
                }
            }
        }
        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        private string _category;

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    RaisePropertyChanged(nameof(Category));
                }
            }
        }

        public RecipeDetailsViewModel(Recipe recipe = null)
        {
            if (recipe != null && recipe.ID > 0)
            {
                _recipeId = recipe.ID;                
                Init();
            }
            else
            {
                _recipeId = 0;
                Title = string.Empty;
                PhotoPath = string.Empty;
                Description = string.Empty;
                Category = string.Empty;
            }
            SaveRecipeCommand = new Command<string>(async (s) => await SaveRecipe(s));
            TakePhotoCommand = new Command(async () => await TakePhoto());
            PickPhotoCommand = new Command(async () => await PickPhoto());
            ShareRecipeCommand = new Command(async () => await ShareRecipe());
            DeleteRecipeCommand = new Command(async () => await DeleteRecipe());
        }

        private async Task DeleteRecipe()
        {
            if (_recipeId == 0)
            {
                await AlertService.DisplayAlert("Nie możesz usunąć", "Ten przepis jest nowy i nie jest jeszcze zapisany w bazie");
                return;
            }
            if ( await AlertService.DisplayAlert("Usunąć?","Aktualny przepis zostanie usunięty", "OK", "Anuluj"))
            {
                await App.LocalDB.DeleteItem<Recipe>(await App.LocalDB.GetItemByID<Recipe>(_recipeId));                
                await NavigationService.Pop();
            }          
        }

        private async Task SaveRecipe(string s)
        {
            var recipe = new Recipe()
            {
                Title = Title,
                PhotoPath = PhotoPath,
                Description = Description,
                Category = Category
            };

            if (s == "UPDATE")
            {
                if (_recipeId == 0)
                {
                    await AlertService.DisplayAlert("Nie możesz edytować", "Kliknij Dodaj");
                    return;
                }                
                recipe.ID = _recipeId;
            }
            if (!string.IsNullOrEmpty(Title))
            {
                await App.LocalDB.SaveItem(recipe);
                await AlertService.DisplayAlert("Sukces", "Dane zostały zapisane");
                await NavigationService.Pop();
                return;
            }
            await AlertService.DisplayAlert("Nie dodano", "Wpisz tytuł");
        }

        private async void Init()
        {           
            var recipe = await App.LocalDB.GetItemByID<Recipe>(_recipeId);
            Title = recipe.Title;
            Description = recipe.Description;
            PhotoPath = recipe.PhotoPath;
            Category = recipe.Category;
            Img = ImageSource.FromFile(PhotoPath);
        }
        private async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await AlertService.DisplayAlert("Nie ma kamery", "Kamera jest niedostępna!");
                return;
            }
            string fileName = $"Zdjecie_{Title}.jpg";
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Przepisy",
                Name = fileName,
                SaveToAlbum = true
            });

            if (file == null)
                return;
            PhotoPath = file.Path;
            Img = ImageSource.FromFile(file.Path);
        }
        private async Task PickPhoto()
        {           
            var compressionQuality = 50;          

            if (!CrossMedia.IsSupported)
                return;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
                return;

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
            {
                CompressionQuality = compressionQuality,
                SaveMetaData = true
            });

            if (file == null)
                return;
            PhotoPath = file.Path;
            Img = ImageSource.FromFile(file.Path);
        }

        private async Task ShareRecipe()
        {
            if (_recipeId == 0)
            {
                await AlertService.DisplayAlert("Nie można udostepnić", "Zapisz przepis aby udostępnić");
                return;
            }
            if (!CrossShare.IsSupported)
                return;
            var recipe = await App.LocalDB.GetItemByID<Recipe>(_recipeId);
            await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage()
            {
                Title = recipe.Title,
                Text = recipe.Description
            });
        }
    }
}
