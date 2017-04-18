﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phoenix01.Models;
using Phoenix01.Models.ManageViewModels;
using Phoenix01.Services;
using Phoenix01.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Phoenix01.CustomExtensions;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Phoenix01.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _appEnv;
        private readonly ApplicationDbContext _context;

        public ManageController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory,
        IHostingEnvironment appEnv,
        ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _appEnv = appEnv;
            _context = context;
        }



        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.EditProfileSuccess ? "Your profile has been updated."
                : message == ManageMessageId.PhotoUploadSuccess ? "Your Photo uploaded."
                : message == ManageMessageId.FileExtensionError ? "You have file Extension Error. Must Be .PNG or .JPG or .GIF"
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user)
            };

            var model01 = new ForPictureViewModel { indexViewModel = model, applicationUser = user };
            return View(model01);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public IActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return RedirectToAction(nameof(VerifyPhoneNumber), new { PhoneNumber = model.PhoneNumber });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/SetPassword
        [HttpGet]
        public IActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //GET: /Manage/ManageLogins
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                 : message == ManageMessageId.PhotoUploadSuccess ? "Your photo has been uploaded."
                : message == ManageMessageId.FileExtensionError ? "Only jpg, png and gif file formats are allowed."
                : "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var otherLogins = _signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        //
        // GET: /Manage/LinkLoginCallback
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        // GET: /Manage/UserProfil
        public async Task<IActionResult> UserProfile(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.EditProfileSuccess ? "Your profile has been updated."
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var model = await EditUserProfile();
            return View();
        }

        // POST: /Manage/UserLanguages
        public async Task<IActionResult> EditUserLanguages(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            //
            if (user == null)
            {
                return View("Error");
            }

            var lang = _context.Languages
                    .Where(la => la.Name == model.RemoveUserLanguage)
                    .SingleOrDefault();

            if (!(lang == null))
            {
                var appUserLang = new ApplicationUserLanguage { ApplicationUserId = user.Id, LanguageId = lang.Id };
                _context.ApplicationUserLanguages.Remove(appUserLang);
            }


            return await EditUserProfile();
        }

        // GET: /Manage/EditUserProfile
        public async Task<IActionResult> EditUserProfile()
        {


            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var age = 0;
            var birthdate = "";
            if (user.BirthDate != null)
            {
                birthdate = ((DateTime)user.BirthDate).ToString("yyyy-MM-dd");
                age = DateTime.Today.Year - ((DateTime)user.BirthDate).Year;
                if (DateTime.Today < ((DateTime)user.BirthDate).AddYears(age)) age--;
            }

            UserProfileViewModel model = new UserProfileViewModel
            {
                RegistrationDate = user.RegistrationDate.ToString("yyyy-MM-dd"),
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                StreetName = user.StreetName,
                Zip = user.Zip,
                State = user.State,
                City = user.City,
                Country = user.Country,
                UserImage = user.UserImage,
                ChosenLanguages = _context.Languages.ToPresentLanguageListItems(_context.ApplicationUserLanguages, user),
                LanguagesDropDown = _context.Languages.ToSelectLanguageListItems(_context.ApplicationUserLanguages, user),
                LanguagesRemoveDropDown = _context.Languages.ToRemoveLanguageListItems(_context.ApplicationUserLanguages, user),

                BirthDate = birthdate,
                UserAge = age.ToString(),
            };

            var appUserHobbies = _context.ApplicationUserHobby.Where(uh => uh.ApplicationUserId == user.Id).ToList();
            var allHobbies = _context.Hobbies.OrderBy(h => h.Name).ToList();

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var hobby in allHobbies)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Id = hobby.Id,
                    Display = hobby.Name,
                    //We should have already-selected genres be checked
                    IsChecked = appUserHobbies.Where(x => x.HobbyId == hobby.Id).Any()
                });
            }
            model.Hobbies = checkBoxListItems;

            return View(model);
        }


        //POST: /Manage/EditUserProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.StreetName = model.StreetName;
                user.Zip = model.Zip;
                user.City = model.City;
                user.State = model.State;
                user.Country = model.Country;

                var lang = _context.Languages
                    .Where(la => la.Name == model.AddUserLanguage)
                    .SingleOrDefault();

                if (!(lang == null))
                {
                    var appUserLang = new ApplicationUserLanguage { ApplicationUserId = user.Id, LanguageId = lang.Id };
                    _context.ApplicationUserLanguages.Add(appUserLang);
                }

                var lang2 = _context.Languages
                    .Where(la => la.Name == model.RemoveUserLanguage)
                    .SingleOrDefault();

                if (!(lang2 == null))
                {
                    var appUserLang = new ApplicationUserLanguage { ApplicationUserId = user.Id, LanguageId = lang2.Id };
                    _context.ApplicationUserLanguages.Remove(appUserLang);
                }

                var selectedHobbies = model.Hobbies.Where(x => x.IsChecked).Select(x => x.Id).ToList();

                //MovieManager.Add(model.Title, model.ReleaseDate, model.RunningTimeMinutes, selectedGenres);

                foreach (var hobbyId in selectedHobbies)
                {
                    var hobby = _context.Hobbies.FirstOrDefault(h => h.Id == hobbyId);


                }

            }

            if (model.BirthDate != null && model.BirthDate != "")
                user.BirthDate = DateTime.Parse(model.BirthDate);


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserProfile), new
                {
                    Message = ManageMessageId.EditProfileSuccess
                });
            }

            return View(model);
        }

        #region Upload Photo

        public async Task<string> UploadPhoto()
        {
            var user = await GetCurrentUserAsync();
            return (user.UserImage);
        }


        [HttpPost]
        public async Task<IActionResult> UploadPhoto(ICollection<IFormFile> files)
        {
            var user = await GetCurrentUserAsync();
            var username = user.UserName;
            var fnm = username + ".png";

            if (User.Identity.IsAuthenticated)
            {
                if (user.Id != null)
                {
                    var uploads = Path.Combine(_appEnv.WebRootPath, "images");

                    foreach (var file in files)
                    {
                        var fileName = ContentDispositionHeaderValue
                              .Parse(file.ContentDisposition)
                              .FileName
                              .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3

                        if (fileName.ToLower().EndsWith(".png") || fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".gif"))// Important for security if saving in webroot
                        {
                            if (file.Length > 0)
                            {
                                var pictureFile = file.FileName + User.Identity.Name + ".png";

                                using (var fileStream = new FileStream(Path.Combine(uploads, pictureFile), FileMode.Create))
                                {
                                    user.UserImage = "\\images\\" + pictureFile;
                                    await _userManager.UpdateAsync(user);
                                    await file.CopyToAsync(fileStream);

                                }
                            }
                            return RedirectToAction("Index", new { Message = ManageMessageId.PhotoUploadSuccess });
                        }
                        else
                        {
                            return RedirectToAction("Index", new { Message = ManageMessageId.FileExtensionError });
                        }
                    }
                }
            }
            return View();
        }

        #endregion Upload Photo






        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            EditProfileSuccess,
            PhotoUploadSuccess,
            FileExtensionError,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion

    }
}
