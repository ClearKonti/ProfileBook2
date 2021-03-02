using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Profilebook.Model
{
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }

    }
}
