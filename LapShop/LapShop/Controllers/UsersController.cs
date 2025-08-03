using Bl;
using Bl.Migrations;
using Domains;
using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _usermanager;
        SignInManager<ApplicationUser> _signmanager;
        RoleManager<IdentityRole> _rolemanager;
        LapShopContext context;
        public UsersController(UserManager<ApplicationUser> usermanager,SignInManager<ApplicationUser> signmanager,RoleManager<IdentityRole> rolemanager,
            LapShopContext ctx)
        {
            _usermanager = usermanager;   
            _signmanager = signmanager;
            _rolemanager = rolemanager;
            context = ctx;
        }
        public IActionResult Login(string returnUrl)
        {
          
            UserModel model = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        public async Task< IActionResult> LoginOut()
        {
            await _signmanager.SignOutAsync();
            return Redirect("~/");
        } 
        public IActionResult Register()
        {
           
            return View(new UserModel());
        }
        [AcceptVerbs("Get","Post")]
        public async Task< IActionResult> EmailInUse(string email)
        {
            var user= await _usermanager.FindByEmailAsync(email);
            if (user == null)
            { 
               return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid) 
                return View("Register", model);
            

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName=model.Email
            };
            try
            {
                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded) 
                {
                  var loginresult= await _signmanager.PasswordSignInAsync(user, model.Password,true,true);
                    var myUser = await _usermanager.FindByEmailAsync(user.Email);
                    
                    await _usermanager.AddToRoleAsync(myUser, "Customer");
                    
                    if (loginresult.Succeeded)
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        return View("Register",model);
                    }
                }
                else
                {
                    return View("Register", model);
                }

            }
            catch (Exception ex) 
            {
            
            }
            return View(new UserModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            else
            {


                try
                {
                    var loginresult = await _signmanager.PasswordSignInAsync(model.Email, model.Password, model.rememberme, false);
                    if (loginresult.Succeeded)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                            return Redirect("~/");
                        else
                            return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return View("Register", model);
                    }


                }
                catch (Exception ex)
                {

                }
                return View(new UserModel());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> CreateRole(RoleModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateRole", model);
            if(model.id == null)
            {
                IdentityRole user = new IdentityRole()
                {
                    Name = model.name.ToUpper()
                };
                IdentityResult result = await _rolemanager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Rolelist");
                }
                else
                {
                    return View("CreateRole", model);
                }
            }
            else
            {
                
               var Role = await _rolemanager.FindByIdAsync(model.id);
                Role.Name = model.name;
                var result= await _rolemanager.UpdateAsync(Role);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Rolelist");

                }
                else
                {
                    return View("CreateRole", model);

                }
            }
                                                  
           
          

        }
        public async Task< IActionResult> CreateRole(string? id)
        {
            var role =  new RoleModel();
            if (id != null)
            {
                var roleid = await _rolemanager.FindByIdAsync(id);
                role.name = roleid.Name;
            }
            return View(role);

        }
        public IActionResult Rolelist()
        {
            var roles = _rolemanager.Roles.ToList();
            return View(roles);
        }
        public async Task<IActionResult> DeleteRole(string id)
        {

            var roles = await _rolemanager.FindByIdAsync(id);
            if (roles != null)
            {
                await _rolemanager.DeleteAsync(roles);
            }
            else
            {
                return RedirectToAction("Rolelist");

            }

            return RedirectToAction("Rolelist");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserRoleModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateUser", model);
            if (model.UserId == null)
            {
              ApplicationUser user=new ApplicationUser();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName= model.Email;
                var adduser=await _usermanager.CreateAsync(user,model.Password);
                if (adduser.Succeeded)
                {
                    var myUser = await _usermanager.FindByEmailAsync(user.Email);
                    var myRole = await _rolemanager.FindByIdAsync(model.RoleId);

                    var loginresult = await _usermanager.AddToRoleAsync(myUser, myRole.Name);

                    if (loginresult.Succeeded)
                    {
                        return Redirect("Userlist");
                    }
                    else
                    {
                        return View("CreateUser", model);
                    }
                }
                else
                {
                    return View("CreateUser", model);

                }
            }
            else
            {
                var newuser = await _usermanager.FindByIdAsync(model.UserId);

                var currentrole = await _usermanager.GetRolesAsync(newuser);

                var roleid = await _rolemanager.FindByIdAsync(model.RoleId);

                var removeresult = await _usermanager.RemoveFromRolesAsync(newuser, currentrole);

                var login = await _usermanager.AddToRoleAsync(newuser, roleid.Name);
                newuser.Email = model.Email;
                newuser.FirstName = model.FirstName;
                newuser.LastName = model.LastName;
                var newpassword = await _usermanager.ChangePasswordAsync(newuser, newuser.PasswordHash, model.Password);

                var x =await _usermanager.UpdateAsync(newuser);
               

                if (x.Succeeded)
                {
                        return RedirectToAction("Userlist");  
                }
                else
                {
                    return View("CreateUser",model);
                }

            }




        }
        public async Task<IActionResult> CreateUser(string? id)
        {
            ViewBag.role = _rolemanager.Roles.ToList();

            var role = new UserRoleModel();
            if (id != null)
            {
                var userid = await _usermanager.FindByIdAsync(id);
                role.UserId = userid.Id;
                role.FirstName = userid.FirstName;
                role.LastName = userid.LastName;
                role.Password = userid.PasswordHash;
                role.Email = userid.Email;
                role.concurrencyStamp = userid.ConcurrencyStamp;
            }
            return View(role);

        }
        public IActionResult Userlist()
        {
            var roleuser = (from user in context.Users from role in context.Roles 
                           join r in context.Roles on role.Id equals r.Id
                           
                           select new RoleUser()
                           {
                               rolename = r.Name,
                               username=user.UserName,
                               email=user.Email,
                               userid = user.Id,

                               
                           }).ToList();
            return View(roleuser);

        }
        public async Task<IActionResult> DeleteUser(string id)
        {

            var roles = await _usermanager.FindByIdAsync(id);
            if (roles != null)
            {
                await _usermanager.DeleteAsync(roles);
                return RedirectToAction("Userlist");
            }
            else
            {
                return RedirectToAction("Userlist");
            }



        }
    }
}
