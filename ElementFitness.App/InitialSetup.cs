using ElementFitness.Utils.Configurations;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace ElementFitness.App{

    public class InitialSetup{
        public static async Task BuildDefaultIdentityAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                bool roleExists = await roleManager.RoleExistsAsync("administrator");
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("administrator"));
                }

                IdentityUser userExists = await userManager.FindByNameAsync("admin");
                if (userExists == null)
                {
                    IdentityUser newUser = new()
                    {
                        UserName = "admin",
                        PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, AppSettings.AdminPassword)
                    };

                    IdentityResult result = await userManager.CreateAsync(newUser);

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(newUser, "administrator").Wait();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Stopping Application. Could not Seed admin user");
                throw new Exception(ex.Message);
            }
        }

    }


}