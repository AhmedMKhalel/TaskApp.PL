using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApp.PL.Models.Account;

namespace TaskApp.PL.Controllers
{

    public class AccountController : Controller
    {
        public SignInManager<IdentityUser> _signInManager { get; }
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        


        #region Sign In

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signinViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signinViewModel.Email);
                if (user is not null)
                {
                    var password = await _userManager.CheckPasswordAsync(user, signinViewModel.Password);
                    if (password)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, signinViewModel.Password,
                            signinViewModel.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("AddCustomerData", "CustomerData");

                        

                    }

                }

                ModelState.AddModelError(String.Empty, "Invalid Data");

            }

            return View(signinViewModel);
        }

        #endregion

        


        public IActionResult Index()
        {
            return View();
        }


    }
}
