using AplicatieSalariati.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicatieSalariati.Startup))]
namespace AplicatieSalariati
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("SuperUser"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperUser";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "supersuper";
                user.Email = "supersuper@gmail.com";

                string userPWD = "supersuper";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperUser");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Pacient"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pacient";
                roleManager.Create(role);

                var userMedic = new ApplicationUser();
                userMedic.UserName = "pacient";
                userMedic.Email = "pacient@gmail.com";

                string userMedicPWD = "pacient123";

                var chkMedicUser = UserManager.Create(userMedic, userMedicPWD);

                //Add default User to Role Admin   
                if (chkMedicUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(userMedic.Id, "Pacient");

                }

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Farmacist"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Farmacist";
                roleManager.Create(role);

                var userMedic = new ApplicationUser();
                userMedic.UserName = "farmacist";
                userMedic.Email = "farmacist@gmail.com";

                string userMedicPWD = "farmacist123";

                var chkMedicUser = UserManager.Create(userMedic, userMedicPWD);

                //Add default User to Role Admin   
                if (chkMedicUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(userMedic.Id, "Farmacist");

                }
            }

            if (!roleManager.RoleExists("Medic"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Medic";
                roleManager.Create(role);

                var userMedic = new ApplicationUser();
                userMedic.UserName = "medic";
                userMedic.Email = "medic@gmail.com";

                string userMedicPWD = "medic123";

                var chkMedicUser = UserManager.Create(userMedic, userMedicPWD);

                //Add default User to Role Admin   
                if (chkMedicUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(userMedic.Id, "Medic");

                }

            }
        }
    }
}
