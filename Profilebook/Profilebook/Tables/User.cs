using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Profilebook.Tables
{
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
