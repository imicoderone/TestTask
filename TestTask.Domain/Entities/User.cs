using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Abstractions;

namespace TestTask.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
