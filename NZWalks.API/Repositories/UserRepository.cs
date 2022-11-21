using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        #region Authenticate
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.UserName.ToLower() == username.ToLower() && x.Password == password);

            if(user == null)
            {
                return null;
            }
            //add roles
            var userRoles = await context.User_Roles.Where(x=>x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                foreach (var userRole in userRoles)
                {
                    user.Roles = new List<string>();
                    var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
        #endregion

        #region All methods

        
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIDAsync(Guid id)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
            if( user == null)
            {
                return null;
            }
            
            
            return user;
        }

        #endregion
    }
}
