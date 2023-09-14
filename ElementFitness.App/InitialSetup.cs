using ElementFitness.Utils.Configurations;
using Microsoft.AspNetCore.Identity;

namespace ElementFitness.App{

    public class InitialSetup{
        public static async Task BuildDefaultIdentityAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var roleExists = await roleManager.RoleExistsAsync("administrator");
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

    }


}