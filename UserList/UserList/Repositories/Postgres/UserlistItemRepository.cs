using UserList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserList.Repositories;
using DoToo.Repositories;

namespace UserList.Repositories.Postgres
{
    public class UserlistItemRepository : IUserlistItemRepository
    {
        public event EventHandler<UserlistItem> OnItemAdded;
        public event EventHandler<UserlistItem> OnItemUpdated;

        public Task AddItem(UserlistItem item)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdate(UserlistItem item)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserlistItem>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(UserlistItem item)
        {
            throw new NotImplementedException();
        }
    }
}
