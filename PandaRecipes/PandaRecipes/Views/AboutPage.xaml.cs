using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PandaRecipes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Animate();
        }
        private async void Animate()
        {
            name.Text = "Czekaj cierpliwie...";
            await Task.Delay(3000);
            name.Text = "Krzysztof Sauermann 10691";
            await Task.Delay(500);
            name.Text = "Łukasz Nowak 10603";
            await Task.Delay(500);
            name.Text = "Ewelina Molenda 10763";
            await Task.Delay(500);
            name.Text = "Krzysztof Sauermann 10691\n" +
                        "Łukasz Nowak 10603\n" +
                        "Ewelina Molenda 10763";

            image.Opacity = 1;

        }
    }
}