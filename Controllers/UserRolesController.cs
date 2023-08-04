using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IES_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {

        private readonly RoleManager<IdentityRole> userroleManager;

        public UserRolesController(RoleManager<IdentityRole> roleManager)
        {
            userroleManager = roleManager;
        }

        // List all the role created by user 
        public IActionResult Index()
        {
            var userrole = userroleManager.Roles;
            return View(userrole);
        }

        [HttpGet]

        public IActionResult Create()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(IdentityRole model)
        {
            //avoid duplicate roles
            if (!userroleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()) 
            { 
                userroleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            
            }

            return RedirectToAction("Index");
        }
    }
}
