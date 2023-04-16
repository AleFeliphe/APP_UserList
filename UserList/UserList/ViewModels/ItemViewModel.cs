using System;
using System.Collections.Generic;
using System.Text;
using UserList.Models;
using DoToo.Repositories;
using System.Windows.Input;
using Xamarin.Forms;
using UserList.Repositories;


namespace UserList.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private readonly UserlistItemRepository repository;

        public UserlistItem Item { get; set; }

        public ItemViewModel(UserlistItemRepository repository)
        {
            this.repository = repository;
            Item = new UserlistItem() { Due = DateTime.Now.AddDays(1) };
        }

        public ICommand Save => new Command(async () => {

            await repository.AddOrUpdate(Item);
            await Navigation.PopAsync();

        });

    }
}
