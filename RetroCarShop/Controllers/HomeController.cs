using Newtonsoft.Json;
using RetroCarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RetroCarShop.Controllers
{
    public class HomeController : Controller
    {
        private CarContext context;

        public HomeController()
        {
            context = new CarContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// На цю сторінку LiqPay відправляє результат оплати. Вона вказана в data.result_url
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Redirect()
        {
            // --- Перетворюю відповідь LiqPay в Dictionary<string, string> для зручності:
            var request_dictionary = Request.Form.AllKeys.ToDictionary(key => key, key => Request.Form[key]);

            // --- Розшифровую параметр data відповіді LiqPay та перетворюю в Dictionary<string, string> для зручності:
            byte[] request_data = Convert.FromBase64String(request_dictionary["data"]);
            string decodedString = Encoding.UTF8.GetString(request_data);
            var request_data_dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);

            // --- Отримую сигнатуру для перевірки
            var mySignature = LiqPayHelper.GetLiqPaySignature(request_dictionary["data"]);

            // --- Якщо сигнатура серевера не співпадає з сигнатурою відповіді LiqPay - щось пішло не так
            if (mySignature != request_dictionary["signature"])
                return View("~/Views/Shared/_Error.cshtml");

            // --- Якщо статус відповіді "Тест" або "Успіх" - все добре
            if (request_data_dictionary["status"] == "sandbox" || request_data_dictionary["status"] == "success")
            {
                // Тут можна оновити статус замовлення та зробити всі необхідні речі. Id замовлення можна взяти тут: request_data_dictionary[order_id]
                // ...
                //return RedirectToAction("GetId", routeValues: new { id = request_data_dictionary["order_id"] });
                //return View("~/Views/Shared/Delete.cshtml",viewModel);

                int carId = Convert.ToInt32(request_data_dictionary["order_id"]);
                var carInDb = context.Cars.SingleOrDefault(c => c.Id == carId);
                carInDb.IsSold = true;
                context.SaveChanges();

                return View("~/Views/Shared/_Success.cshtml");
            }

            return View("~/Views/Shared/_Error.cshtml");
        }

        public string GetId(string id)
        {
            return id.ToString();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}