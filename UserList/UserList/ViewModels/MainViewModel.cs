using UserList.Models;
using UserList.Repositories;
using UserList.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UserList.ViewModels;
using UserList;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace UserList.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly UserlistItemRepository repository;

        public ObservableCollection<UserlistItemViewModel> Items { get; set; }

        public MainViewModel(UserlistItemRepository repository)
        {
            repository.OnItemAdded += (sender, item) =>
                Items.Add(CreateUserlistItemViewModel(item));

            repository.OnItemUpdated += (sender, item) =>
                Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        public UserlistItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(UserlistItemViewModel item)
        {

            if (item == null) return;
            var itemView = Resolver.Resolve<ItemView>();
            //vm é a viewmodel
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            await Navigation.PushAsync(itemView);
        }

        private async Task LoadData()
        {
            var items = await repository.GetItems();
            if (!ShowAll)
            {
                items = items.Where(w => w.Completed == false).ToList();
            }
            var itemViewModels = items.Select(i => CreateUserlistItemViewModel(i));
            Items = new ObservableCollection<UserlistItemViewModel>(itemViewModels);
        }

        private UserlistItemViewModel CreateUserlistItemViewModel(UserlistItem item)
        {
            var itemViewModel = new UserlistItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is UserlistItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItem(item.Item));

            }
        }

        public bool ShowAll { get; set; }

        public string FilterText => ShowAll ? "Todos" : "Ativos";

        public ICommand ToggleFilter => new Command(
            async () =>
            {
                ShowAll = !ShowAll;
                await LoadData();
            }
        );

        public ICommand AddItem => new Command(async () => {

            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);

        });
    }
}
