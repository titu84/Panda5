using PandaRecipes.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PandaRecipes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MainViewModel();
            logo.RotateTo(360, 5000, Easing.BounceIn);
        }
    }
}
