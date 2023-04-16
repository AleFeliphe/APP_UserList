using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserList.Models;

namespace DoToo.Repositories
{
    public interface IUserlistItemRepository
    {
        event EventHandler<UserlistItem> OnItemAdded;
        event EventHandler<UserlistItem> OnItemUpdated;

        Task<List<UserlistItem>> GetItems();
        Task AddItem(UserlistItem item);
        Task UpdateItem(UserlistItem item);
        Task AddOrUpdate(UserlistItem item);
    }
}
