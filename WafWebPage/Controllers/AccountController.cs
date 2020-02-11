using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WafWebPage.ViewModels;
using WafWebPage.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WafWebPage.Controllers
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

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> LoggedIn(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("All", "School");
            } else
            {
                ApplicationUser dbUser = GetUser(loginVM.Email, loginVM.Password);
                var user = await _userManager.FindByNameAsync(dbUser.Email);
                await _signInManager.SignInAsync(user, true);

                if (user != null)
                {
                    return RedirectToAction("All", "School");
                }
                else
                    return View("Login");
            }

        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Registered(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View("Register");
            }

            var user = new ApplicationUser() { UserName = registerVM.Email, Email = registerVM.Email };
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("All", "School");
            }

            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("All", "School");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordVM)
        {
            ApplicationUser user = new ApplicationUser();
            string result = "";

           string connectionString = "Data Source=(local);Initial Catalog=SchoolManagement;"
                                        + "Integrated Security=true";

            string queryString = "select * from AspNetUsers where Email = '" + forgotPasswordVM.Email.ToString() + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    //result = reader;
                    while (reader.Read())
                    {
                        user.Id = reader[0].ToString();
                        user.AccessFailedCount = (int)reader[1];
                        user.ConcurrencyStamp = reader[2].ToString();
                        user.Email = reader[3].ToString();
                        user.EmailConfirmed = (bool)reader[4];
                        user.LockoutEnabled = (bool)reader[5];
                        user.LockoutEnd = (DateTime)reader[6];
                        user.NormalizedEmail = reader[7].ToString();
                        user.NormalizedUserName = reader[8].ToString();
                        user.PasswordHash = reader[9].ToString();
                        user.PhoneNumber = reader[10].ToString();
                        user.SecurityStamp = reader[11].ToString();
                        user.TwoFactorEnabled = (bool)reader[12];
                        user.UserName = reader[13].ToString();

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return Content(JsonConvert.SerializeObject(user));

            
        }



        public ApplicationUser GetUser(string email, string password)
        {
            ApplicationUser user = new ApplicationUser();

            string connectionString = "Data Source=(local);Initial Catalog=SchoolManagement;"
                                         + "Integrated Security=true";

            string queryString = "select * from AspNetUsers where Email = '" + email.ToString() + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    //result = reader;
                    //while (reader.Read())
                    //{
                    reader.Read();
                        user.Id = reader[0].ToString();
                        user.AccessFailedCount = (int)reader[1];
                        user.ConcurrencyStamp = reader[2].ToString();
                        user.Email = reader[3].ToString();
                        user.EmailConfirmed = (bool)reader[4];
                        user.LockoutEnabled = (bool)reader[5];
                        //user.LockoutEnd = (DateTime)reader[6];
                        user.NormalizedEmail = reader[7].ToString();
                        user.NormalizedUserName = reader[8].ToString();
                        user.PasswordHash = reader[9].ToString();
                        user.PhoneNumber = reader[10].ToString();
                        user.PhoneNumberConfirmed = (bool)reader[11];
                        user.SecurityStamp = reader[12].ToString();
                        user.TwoFactorEnabled = (bool)reader[13];
                        user.UserName = reader[14].ToString();

                    //}
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return user;

        }

    }
}