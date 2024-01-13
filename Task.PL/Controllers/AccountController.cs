// AccountController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApp.DAL.Entities;
using TaskApp.PL.Models;
using TaskApp.PL.Models.Account;

namespace TaskApp.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private string GenerateRandomOTP()
        {
            const string characters = "0123456789";
            const int otpLength = 6;

            Random random = new Random();
            char[] otp = new char[otpLength];

            for (int i = 0; i < otpLength; i++)
            {
                otp[i] = characters[random.Next(characters.Length)];
            }

            return new string(otp);
        }

        private void SaveUserOTP(ApplicationUser user, string otp)
        {
            user.OTP = otp;
            _userManager.UpdateAsync(user).GetAwaiter().GetResult();
        }

        private void SendOTPEmail(string email, string otp)
        {
            // Example email sending logic using SMTP (replace with your actual email sending code)
            Console.WriteLine($"Sending OTP ({otp}) to {email}");
            Console.WriteLine("Email sent successfully!");
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

                if (user != null)
                {
                    var password = await _userManager.CheckPasswordAsync(user, signinViewModel.Password);

                    if (password)
                    {
                        if (signinViewModel.UseOTP)
                        {
                            var otp = GenerateRandomOTP();
                            SaveUserOTP(user, otp);

                            // Assuming you have a SendOTPEmail method to send the OTP to the user's email
                            SendOTPEmail(user.Email, otp);

                            return RedirectToAction("ConfirmOTP", new { email = signinViewModel.Email });
                        }

                        var result = await _signInManager.PasswordSignInAsync(user, signinViewModel.Password, signinViewModel.RememberMe, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("AddCustomerData", "CustomerData");
                        }
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

        public IActionResult ConfirmOTP(string email)
        {
            // Pass the email to the view so that it can be used to identify the user
            return View(new ConfirmOTPViewModel { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOTP(ConfirmOTPViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && model.OTP == user.OTP)
            {
                // OTP is correct, sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("AddCustomerData", "CustomerData");
            }

            ModelState.AddModelError(String.Empty, "Invalid OTP");
            return View(model);
        }

    }
}
