﻿using UserList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UserList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            ItemsListView.ItemSelected += (s, e) => ItemsListView.SelectedItem = null; 
        }
    }
}