using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Application.Abstractions
{
    public interface IIdentityService
    {
        Task<ClaimsIdentity> Login(string userName, string password, CancellationToken cancellationToken);
        Task Register(string userName, string password, CancellationToken cancellationToken);
    }
}
