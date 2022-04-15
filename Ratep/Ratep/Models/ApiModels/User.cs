﻿using System;
using System.Collections.Generic;

namespace Ratep.Models.ApiModels
{
    public partial class User
    {
        public User()
        {
            Clients = new HashSet<Client>();
            Employees = new HashSet<Employee>();
        }

        public int AccountId { get; set; }
        public string Login { get; set; } = null;
        public string Password { get; set; } = null;
        public string FullName { get; set; } = null;
        public string Token { get; set; } = null;

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
