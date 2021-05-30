using AutoService.Models;
using AutoService.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private AutoServiceContext db = new AutoServiceContext();
        private LoginMessage loginMessage = new LoginMessage("");

        public async Task<ActionResult> Login() //Авторизация
        {
            if (await Tool.GetOwner(Request, db) != null)
                return RedirectToAction("UserProfile");

            loginMessage = new LoginMessage("");
            return View("Login", loginMessage);
        }

        [HttpPost]
        public async Task<ActionResult> Login(string login, string password) //Логика обработки данных с формы
        {
            if (await Tool.GetOwner(Request, db) != null)
                return RedirectToAction("UserProfile");

            Owner owner;
            try { owner = db.Owners.FirstOrDefault(u => u.Login == login && u.Password == password); }  //запрос проверки
            catch { owner = null; }

            if (owner != null)
            {
                Tool.AddLoginCookie(Response, login, password);
                return RedirectToAction("UserProfile");
            }

            loginMessage = new LoginMessage("Неправильный пароль!");
            return View(loginMessage);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(string task) //Логика обработки данных с формы
        {
            string reason = task;

            Owner owner = await Tool.GetOwner(Request, db);
            Car car = db.Cars.FirstOrDefault(c => c.OwnerId == owner.OwnerId);

            Order order = new Order
            {
                CarNumber = car.CarNumber,
                Reason = reason,
                Date = DateTime.Now,
                Status = "Создан"
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync();
            loginMessage = new LoginMessage("Неправильный пароль!");
            return RedirectToAction("UserProfile");
        }

        public async Task<ActionResult> SignUp()
        {
            if (await Tool.GetOwner(Request, db) != null)
                return RedirectToAction("UserProfile");

            loginMessage = new LoginMessage("");
            return View("SignUp", loginMessage);
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(string login, string password, string fullName, string phoneNumber, string driverLicense, string carMark, string carNumber)
        {
            if (await Tool.GetOwner(Request, db) != null)
                return RedirectToAction("UserProfile");

            var checedCar = db.Cars.FirstOrDefault(c => c.CarNumber == carNumber);

            if (checedCar != null)
            {
                var owner = new Owner()
                {
                    Login = login,
                    Password = password,

                    FIO = fullName,
                    Phone = phoneNumber,
                    DriverLicense = driverLicense
                };

                db.Owners.Add(owner);

                checedCar.Owner = owner;

                await db.SaveChangesAsync();
                Tool.AddLoginCookie(Response, login, password);
                return RedirectToAction("UserProfile");
            }

            if (!await Tool.UserExists(login, db)) // проверка на существование юзера с таким логином
            {
                var owner = new Owner() 
                {
                    Login = login,
                    Password = password,

                    FIO = fullName,
                    Phone = phoneNumber,
                    DriverLicense = driverLicense
                };

                db.Owners.Add(owner);

                var car = new Car()
                {
                    OwnerId = owner.OwnerId,
                    Mark = carMark,
                    CarNumber = carNumber
                };

                db.Cars.Add(car);

                await db.SaveChangesAsync();
                Tool.AddLoginCookie(Response, login, password);
                return RedirectToAction("UserProfile");
            }

            loginMessage = new LoginMessage("Пользователь с таким именем уже существует!");
            return View("SignUp", loginMessage);
        }

        public ActionResult Logout() //Очищаем куки (т.е. выходим из акк), и нас перекидывает на страницу Входа
        {
            Tool.ClearLoginCookie(Response);

            return RedirectToAction("Login");
        }

        public async Task<ActionResult> UserProfile() //Обработка страницы профиля, чтобы зайти можно было только с данными из бд
        {
            ProfileInfo info = new ProfileInfo();

            Owner owner = await Tool.GetOwner(Request, db);

            if (owner == null)
                return RedirectToAction("Login");

            Car car = db.Cars.FirstOrDefault(c=>c.OwnerId == owner.OwnerId);

            List<Order> orders = db.Orders.Where(o => o.CarNumber == car.CarNumber).ToList();

            info.Owner = owner;
            info.Orders = orders;

            return View(info);
        }

        public async Task<ActionResult> OrderDetails(int orderId) //Обработка страницы профиля, чтобы зайти можно было только с данными из бд
        {
            Owner owner = await Tool.GetOwner(Request, db);

            if (owner == null)
                return RedirectToAction("Login");

            OrderInfo info = new OrderInfo();

            var order = db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();

            var employee = order.Employee;

            List<TypeOfWork> works = order.OrderTypeOfWorks.Select(o => o.TypeOfWork).ToList();

            List<Sparepart> spareparts = order.OrderSpareparts.Select(o => o.Sparepart).ToList();


            info.Order = order;
            info.Employee = employee;
            info.Works = works;
            info.Spareparts = spareparts;

            return View(info);
        }
    }
}