using System.Text.Json.Serialization;

namespace QbotCandidate.Api.Feature.Booking;

public class Booking
{
    [JsonPropertyName("bookingId")]
    public int Id { get; set; }
    [JsonPropertyName("bookingStart")]
    public DateTime StartDateTime { get; set; }
    [JsonPropertyName("bookingEnd")]
    public DateTime EndDateTime { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
}