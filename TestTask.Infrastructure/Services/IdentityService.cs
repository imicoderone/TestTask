using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TestTask.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestTask.Application.Abstractions;
using TestTask.Application.Common;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Security;

namespace TestTask.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _context;

        public IdentityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClaimsIdentity> Login(string userName, string password, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserName == userName, cancellationToken);

            if (user == null || !PasswordPolicy.ValidatePassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return identity;
        }

        public async Task Register(string userName, string password, CancellationToken cancellationToken)
        {
            var entity = new User()
            {
                UserName = userName,
                PasswordHash = PasswordPolicy.HashPassword(password),
                Role = RoleConstants.UserRoleName
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
