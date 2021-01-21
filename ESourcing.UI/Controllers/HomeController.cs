using ESourcing.Core.Entities;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<Employee> _userManager { get; }
        public SignInManager<Employee> _signInManager { get; }

        public HomeController(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //Bununla amacımız eger daha once sıstemde coockie ıle kazayla kullanıcı bılgsı kalmıs ıse cıkıs yapıp yenı kullanıcıya ortamı hazırlıyrum.
                    await _signInManager.SignOutAsync();

                    //isPersint => benı hatırla ozellıgı
                    //louckOutfail ozellıgı=> basarısız gırıslerede kullanıcıyı kıtleme ozellıgı
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    //result.RequiresTwoFactor => bununla ıkılı dogrulama ıstıyor mu check edılıyor.
                    if (result.Succeeded)
                      return  RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email address is not valid or password");
                }

            }

            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.FirstName = model.FirstName;
                emp.Email = model.Email;
                emp.LastName = model.LastName;
                emp.PhoneNumber = model.PhoneNumber;
                emp.UserName = model.UserName;
                emp.IsAdmin = emp.IsAdmin;
                emp.IsActive = emp.IsActive;
                emp.IsSupplier = emp.IsSupplier;

                var result = await _userManager.CreateAsync(emp, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
