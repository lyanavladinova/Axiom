using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.AspNetCore.Identity;
using Data.Models.Roles;

namespace AXIOM.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole<int>> roleManager;
        private UserManager<User> userManager;
        private AxiomDbContext context;

        public RolesController(AxiomDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(this.roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole<int> role)
        {
            if (role == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            IdentityResult result = await this.roleManager.CreateAsync(role);

            if (result.Succeeded) 
            {
                return RedirectToAction(nameof(Index), nameof(Article));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            IdentityRole<int> role = await this.roleManager.FindByIdAsync(id.ToString());

            if (role == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Index));
            }

            IdentityRole<int> role = await this.roleManager.FindByIdAsync(id.ToString());

            if (role == null) {
                return RedirectToAction(nameof(Index));
            }

            IdentityResult result = await this.roleManager.DeleteAsync(role);

            if (result.Succeeded) {
                return RedirectToAction(nameof(Index), nameof(Article));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            List<RoleDTO> roles = this.roleManager.Roles
                .Select(x => new RoleDTO { RoleName = x.Name, RoleId = x.Id })
                .ToList();


            List<UserDTO> users = this.userManager.Users
               .Select(x => new UserDTO { Username = x.UserName, Id = x.Id })
               .ToList();

            RoleAddViewModel viewModel = new RoleAddViewModel() {
                Roles = roles,
                Users = users
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(RoleDTO role)
        {
            if (this.User != null && this.User.Identity.IsAuthenticated) {
                IdentityUserRole<int> userRole = new IdentityUserRole<int>() {
                    RoleId = role.RoleId,
                    UserId = role.UserId
                };

                this.context.UserRoles.Add(userRole);
                this.context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove()
        {
            List<RoleDTO> roles = this.roleManager.Roles
                .Select(x => new RoleDTO { RoleName = x.Name, RoleId = x.Id })
                .ToList();

            List<UserDTO> users = this.userManager.Users
               .Select(x => new UserDTO { Username = x.UserName, Id = x.Id })
               .ToList();

            RoleAddViewModel viewModel = new RoleAddViewModel() {
                Roles = roles,
                Users = users
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Remove(RoleDTO role)
        {
            if (this.User != null && this.User.Identity.IsAuthenticated) {
                IdentityUserRole<int> userRole = new IdentityUserRole<int>() {
                    RoleId = role.RoleId,
                    UserId = role.UserId
                };

                this.context.UserRoles.Remove(userRole);
                this.context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
