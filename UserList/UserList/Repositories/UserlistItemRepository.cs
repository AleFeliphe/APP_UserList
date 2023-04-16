using UserList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using DoToo.Repositories;

namespace UserList.Repositories
{
    public class UserlistItemRepository : IUserlistItemRepository
    {
        private SQLiteAsyncConnection connection;

        private async Task CreateConnection()
        {
            if (connection != null)
                return;

            var documentPath =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments
                );

            var databasePath =
                Path.Combine(documentPath, "Userlists.db");

            connection = new SQLiteAsyncConnection(databasePath);

            await connection.CreateTableAsync<UserlistItem>();

            if (await connection.Table<UserlistItem>().CountAsync() == 0)
            {
                await connection.InsertAsync(
                    new UserlistItem()
                    {
                        Name = "Rivaldo",
                        Surname = "Suriname",
                        Due = DateTime.Now,
                        Generous = "Indefinido",
                        Adress = "Rua PNC",
                        Country = "Sur e name"
                    }
                );
            }
        }

        public event EventHandler<UserlistItem> OnItemAdded;
        public event EventHandler<UserlistItem> OnItemUpdated;

        public async Task AddItem(UserlistItem item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdate(UserlistItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }

        public async Task<List<UserlistItem>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<UserlistItem>().ToListAsync();
        }

        public async Task UpdateItem(UserlistItem item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
    }
}
