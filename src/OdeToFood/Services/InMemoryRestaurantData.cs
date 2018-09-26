using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {


        public InMemoryRestaurantData()
        {


            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id=1,Name="My Pizza Place"},
                new Restaurant{Id=2,Name="Over the moon"},
                new Restaurant{Id=3,Name="Foodie"}

            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(o => o.Name);
        }


        List<Restaurant> _restaurants;
    }
}
