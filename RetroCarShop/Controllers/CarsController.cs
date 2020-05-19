using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RetroCarShop.Models;
using RetroCarShop.ViewModels;

namespace CarParking.Controllers
{
    public class CarsController : Controller
    {
        private CarContext context;

        public CarsController()
        {
            context = new CarContext();
        }

        public ActionResult Index()
        {
            var cars = context.Cars.ToList();

            List<CarViewModel> carViewModels = new List<CarViewModel>();

            foreach (var car in cars)
            {
                CarViewModel carViewModel = new CarViewModel
                {
                    Id = car.Id,
                    Name = car.Name,
                    YearOfManufacturing = car.YearOfManufacturing,
                    Mileage = car.Mileage,
                    Price = car.Price,
                    IsSold = car.IsSold,
                    LiqPayCheckoutFormModel = LiqPayHelper.GetLiqPayModel(car.Id.ToString(), car.Name, car.Price)
                };

                carViewModels.Add(carViewModel);
            }

            return View("Index", carViewModels);
        }

        public ActionResult New()
        {
            CarViewModel carViewModel = new CarViewModel() { Id = 0 };

            return View("CarForm", carViewModel);
        }

        public ActionResult Edit(int carId)
        {
            var car = context.Cars.SingleOrDefault(c => c.Id == carId);

            CarViewModel carViewModel = new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                YearOfManufacturing = car.YearOfManufacturing,
                Mileage = car.Mileage,
                Price = car.Price,
                IsSold = car.IsSold
            };

            return View("CarForm", carViewModel);
        }

        public ActionResult Delete(int carId)
        {
            var carInDb = context.Cars.SingleOrDefault(c => c.Id == carId);

            context.Cars.Remove(carInDb);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Save(CarViewModel car)
        {
            if (!ModelState.IsValid)
            {
                var carViewModel = new CarViewModel() { Id = 0 };

                return View("CarForm", carViewModel);
            }
            if (car.Id == 0)
            {
                context.Cars.Add(new Car
                {
                    Name = car.Name,
                    YearOfManufacturing = car.YearOfManufacturing,
                    Mileage = car.Mileage,
                    Price = car.Price,
                    IsSold = car.IsSold
                });
            }
            else
            {
                var carInDb = context.Cars.SingleOrDefault(c => c.Id == car.Id);

                carInDb.Name = car.Name;
                carInDb.YearOfManufacturing = car.YearOfManufacturing;
                carInDb.Mileage = car.Mileage;
                carInDb.Price = car.Price;
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}