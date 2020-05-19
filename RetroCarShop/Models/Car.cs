namespace RetroCarShop.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int YearOfManufacturing { get; set; }

        public int Mileage { get; set; }

        public int Price { get; set; }

        public bool IsSold { get; set; }
    }
}