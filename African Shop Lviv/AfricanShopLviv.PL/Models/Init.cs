namespace AfricanShopLviv.PL.Models
{
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;
    using System.Configuration;
    using System;
    using System.Linq;

    public class Init
    {
        Random rand = new Random();

        public static string MainPath { get; } = AppDomain.CurrentDomain.BaseDirectory;
        public static string ConnectionStr { get; } = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task<ApplicationUser> InitDefaultUser(ApplicationUserManager UserManager, ApplicationSignInManager SignInManager)
        {
            var defaultId = rand.Next(4000, new Random().Next(6000, 9000));
            var cntUsrs = UserManager.Users.ToList().Count();
            var defaultPass = $"1qaz!QAZ{defaultId}-{rand.Next(1,10000)}".ToString();
            var defaultEmail = $"Guest-{cntUsrs + 1}@gmail.com";
            var user = new ApplicationUser
            {
                UserId = cntUsrs+1,
                UserName = defaultEmail,
                Email = defaultEmail,
                PhoneNumber = $"Default Phone - {defaultId}",
                LastName = $"Default Last Name - {defaultId}",
                FirstName = $"Default First Name - {defaultId}",
                Role = Role.Guest.ToString(),
                DateRegister = DateTime.Now
            };
            // Add default user(Role: Guest) on database:
            var result = await UserManager.CreateAsync(user, defaultPass);
            // Do auth in background:
            var recentUser = UserManager.FindByEmail(user.Email);
            SignInStatus resultAuth = await SignInManager.PasswordSignInAsync(recentUser.Email, defaultPass, false, shouldLockout: false);
            using (var sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "StoragePass.txt", true))
                sw.WriteLine(DateTime.Now + " => " + user.Email + " | " + defaultPass);

            return recentUser;
        }

        public void Proccess()
        {

        }
    }
}