using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace N5.User.Domain.Entities
{
    public class UserPermission : Entity<int>
    {
        public string Name { get; set; }

        public string Title { get; set; }

    }
}