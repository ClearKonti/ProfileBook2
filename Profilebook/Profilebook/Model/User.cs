using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Profilebook.Model
{
    public class User : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
