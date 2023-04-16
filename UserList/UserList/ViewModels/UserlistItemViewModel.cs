using UserList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UserList.ViewModels;
using Xamarin.Forms;

namespace UserList.ViewModels
{
    public class UserlistItemViewModel : ViewModel
    {
        public UserlistItem Item { get; private set; }

        public UserlistItemViewModel(UserlistItem item) => Item = item;

        public event EventHandler ItemStatusChanged;

        public string StatusText =>
            (Item.Completed ? "Reativar" : "Completo");


        public ICommand ToggleCompleted => new Command(
            (arg) => {
                Item.Completed = !Item.Completed;
                ItemStatusChanged?.Invoke(this, new EventArgs());
            }
        );

    }
}
