using System.Collections.Generic;

namespace Refactoring.Web.Services.Helpers
{
    public static class LocalBusinessHelper
    {
        private static string BarberShop => "BarberShop";
        private static string ShoeStore => "Shoe Store";
        private static string PizzaPlace => "Pizza Place";
        private static string Diner => "Diner";
        private static string AutoRepair => "AutoRepair";
        private static string Pharmacy => "Pharmacy";
        private static string Grocery => "Grocery";

        public static HashSet<string> AllBusinesses = new HashSet<string>
        {
            BarberShop,
            ShoeStore,
            PizzaPlace,
            Diner,
            AutoRepair,
            Pharmacy,
            Grocery
        };


        //"Barbershop", "Bakery", "Shoe Store", "Pizza Place", "Diner", "Auto Repair", "Pharmacy", "Grocery", "Bakery"
    }
}