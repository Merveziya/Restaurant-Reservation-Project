using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using Newtonsoft.Json;
using System.Threading.Tasks;


public class BookingsPlugin
{


    // Dummy data 
    private readonly List<RestaurantModel> _restaurantList = new List<RestaurantModel>
    {
        new RestaurantModel { Name = "Italian Delight", CuisineType = "Italian", Location = "New York", BookingUrl = "http://italiandelight.com" },
        new RestaurantModel { Name = "Sushi World", CuisineType = "Japanese", Location = "Los Angeles", BookingUrl = "http://sushiworld.com" },
        new RestaurantModel { Name = "Kebab Paradise", CuisineType = "Turkish", Location = "İstanbul", BookingUrl = "http://kebabparadise.com" },
        new RestaurantModel { Name = "Le Gourmet Bistro", CuisineType = "French", Location = "Paris", BookingUrl = "http://legourmetbistro.com" },
        new RestaurantModel { Name = "Tacos El Rey", CuisineType = "Mexican", Location = "Austin", BookingUrl = "http://tacorelrey.com" },
        new RestaurantModel { Name = "Café Bella", CuisineType = "Mediterranean", Location = "Miami", BookingUrl = "http://cafebella.com" },
        new RestaurantModel { Name = "The Curry House", CuisineType = "Indian", Location = "London", BookingUrl = "http://thecurryhouse.com" },
        new RestaurantModel { Name = "Seafood Haven", CuisineType = "Seafood", Location = "Boston", BookingUrl = "http://seafoodhaven.com" },
    };

    [KernelFunction("web_restaurant_search")]
    [Description("Searches for a restaurant based on cuisine type and location.")]
    public string SearchRestaurant([Description("The type of cuisine (e.g., Italian, Japanese)")] string cuisineType,
                                   [Description("The location of the restaurant (e.g., New York, Paris)")] string location)
    {
        var matchingRestaurants = _restaurantList
            .Where(r => r.CuisineType.Equals(cuisineType, StringComparison.OrdinalIgnoreCase) && r.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matchingRestaurants.Count == 0)
        {
            return "No restaurants found for the given criteria.";
        }

        return JsonConvert.SerializeObject(matchingRestaurants, Formatting.Indented);
    }

    [KernelFunction("web_restaurant_add_booking")]
    [Description("Creates a booking for a restaurant.")]
    public async Task<string> WebAddBooking([Description("The restaurant")] string restaurant,
                                            [Description("The date of booking")] string date_booking,
                                            [Description("The time of booking")] string time_booking,
                                            [Description("The number of people")] int number_of_people,
                                            [Description("The customer name")] string customer_name)
    {
        var booking = new BookingModel
        {
            RestaurantName = restaurant,
            BookingDate = date_booking,
            BookingTime = time_booking,
            NumberOfPeople = number_of_people,
            CustomerName = customer_name,
            BookingMadeAt = await GetIstanbulTimeAsync()
        
    };

        return JsonConvert.SerializeObject(booking, Formatting.Indented);
    }

    // Using OpenAPI World Time API to get Istanbul time
    private async Task<string> GetIstanbulTimeAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync("http://worldtimeapi.org/api/timezone/Europe/Istanbul");
            var timezoneData = JsonConvert.DeserializeObject<TimezoneResponse>(response);
            return timezoneData?.DateTime ?? "Unknown time";
        }
    }
}
