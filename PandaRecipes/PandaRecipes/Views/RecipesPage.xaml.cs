﻿using System;
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
        string _category;
		public RecipesPage(string category = null)
		{
            _category = category == null ? "" : category;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            BindingContext = new RecipesViewModel(_category);
            base.OnAppearing();
            lvItems.ItemTapped -= RecipesViewModel.LvItems_ItemTapped;
            lvItems.ItemTapped += RecipesViewModel.LvItems_ItemTapped;
        }
    }
}