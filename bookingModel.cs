using System.Text.Json.Serialization;

public class BookingModel
{
    public string? RestaurantName { get; set; }
    public string? BookingDate { get; set; }
    public string? BookingTime { get; set; }
    public int NumberOfPeople { get; set; }
    public string? CustomerName { get; set; }
    public string BookingMadeAt { get; set; }
}


public class TimezoneResponse
{
    public string DateTime { get; set; }
}