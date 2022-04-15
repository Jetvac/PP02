using System;
using System.Collections.Generic;
using System.Linq;

namespace Ratep.Models.ApiModels
{
    public partial class Contract
    {
        public Contract()
        {
            Orders = new HashSet<Order>();
        }

        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RegDate { get; set; }

        public virtual Client Client { get; set; } = null;
        public virtual Employee Employee { get; set; } = null;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Order Order { get { return Orders.FirstOrDefault(); } }
    }
}
