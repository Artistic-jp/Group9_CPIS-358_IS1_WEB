using System;

namespace KAU_Restaurant.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string DayOfWeek { get; set; }
    }
}
