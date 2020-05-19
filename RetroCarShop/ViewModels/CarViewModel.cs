using RetroCarShop.Models;
using System.ComponentModel.DataAnnotations;

namespace RetroCarShop.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter carname")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter production year")]
        [Display(Name = "Production Year")]
        public int YearOfManufacturing { get; set; }

        [Required(ErrorMessage = "Please, enter the mileage")]
        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Please, enter the price")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        public bool IsSold { get; set; }

        public LiqPayCheckoutFormModel LiqPayCheckoutFormModel { get; set; }
    }
}