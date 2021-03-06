using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Labont_Dumitru_Licenta.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Labont_Dumitru_Licenta.Data;
using Labont_Dumitru_Licenta.Models;

namespace Labont_Dumitru_Licenta.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterFurnizorModel : PageModel
    {
        private readonly SignInManager<Labont_Dumitru_LicentaUser> _signInManager;
        private readonly UserManager<Labont_Dumitru_LicentaUser> _userManager;
        private readonly ILogger<RegisterFurnizorModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Labont_Dumitru_Licenta.Data.ApplicationDbContext _context;

        public RegisterFurnizorModel(
            UserManager<Labont_Dumitru_LicentaUser> userManager,
            SignInManager<Labont_Dumitru_LicentaUser> signInManager,
            ILogger<RegisterFurnizorModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Nume")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Prenume")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Display(Name = "Judet")]
            public string County { get; set; }
            [Required]
            [Display(Name = "Oras")]
            public string City { get; set; }
            [Required]
            [Display(Name = "Strada")]
            public string Street { get; set; }
            [Required]
            [Display(Name = "Numarul Strazii")]
            public int StreetNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

       
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Labont_Dumitru_LicentaUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //daca utilizatorul a fost creat cu succes i se va atribui rolul de furnizor
                    await _userManager.AddToRoleAsync(user, "furnizor");
                    _logger.LogInformation("User created a new account with password.");
                    var userLocaion = new UserLocation();
                    userLocaion.County = Input.County;
                    userLocaion.City = Input.City;
                    userLocaion.Street = Input.Street;
                    userLocaion.StreetNumber = Input.StreetNumber;
                    userLocaion.Labont_Dumitru_LicentaUserID = user.Id;
                    _context.UserLocations.Add(userLocaion);


                    user.LastName = Input.LastName;
                    user.FirstName = Input.FirstName;
                    await _context.SaveChangesAsync();

                    return LocalRedirect(returnUrl);
                    //Verificarea email-ului e dezactivata din Services



                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
