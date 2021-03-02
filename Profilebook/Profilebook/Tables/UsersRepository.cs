using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Profilebook.Tables
{
    public class UsersRepository
    {
        SQLiteConnection usersDatabase;
        public UsersRepository(string databasePath)
        {
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "users.db");
            usersDatabase = new SQLiteConnection(databasePath);
            usersDatabase.CreateTable<User>();

        }

        #region --- Voids ---

        public bool ValidateUser(string userNameParam, string passwordParam)
        {
            var result = usersDatabase.Table<User>().Where(v => v.Username == userNameParam && v.Password == passwordParam).ToList();

            return (result.Count() > 0);
        }

        public bool UsernameCheck(string userNameParam)
        {
            var result = usersDatabase.Table<User>().Where(v => v.Username == userNameParam).ToList();

            return (result.Count() > 0);
        }

        public IEnumerable<User> GetItems()
        {
            return usersDatabase.Table<User>().ToList();
        }

        public User GetItem(int id)
        {
            return usersDatabase.Get<User>(id);
        }

        public int DeleteItem(int id)
        {
            return usersDatabase.Delete<User>(id);
        }

        public int SaveItem(User item)
        {
            if (item.UserId != 0)
            {
                usersDatabase.Update(item);
                return item.UserId;
            }
            else
            {
                return usersDatabase.Insert(item);
            }
        }
        #endregion
    }
}
