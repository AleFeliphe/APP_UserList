using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace UserList.Models
{
    public class UserlistItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Generous { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public bool Completed { get; set; }
        public DateTime Due { get; set; }
    }
}
