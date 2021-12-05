using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityConfigs
{
    public class UserStoreConfig : UserStore<User,Role,IdentityDataBaseContext,int>
    {
        public UserStoreConfig(IdentityDataBaseContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public override IdentityDataBaseContext Context => base.Context;

        public override IQueryable<User> Users => base.Users;

     
        public override Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            //return base.FindByNameAsync(normalizedUserName, cancellationToken);
            return Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }
        public override Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            //return base.FindByEmailAsync(normalizedEmail, cancellationToken);
            return Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
        public override async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Context.Add(user);
            await SaveChanges(cancellationToken);
            return IdentityResult.Success;
        }
        public override async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var query = from userRole in Context.UserRoles
                        join role in Context.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId.Equals(userId)
                        select role.Name;
            return await query.ToListAsync(cancellationToken);
        }
    }
}
