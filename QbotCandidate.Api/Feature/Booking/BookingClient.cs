using Microsoft.Extensions.Options;

namespace QbotCandidate.Api.Feature.Booking;

public class BookingClient : IBookingClient
{
    private readonly IHttpClientFactory _factory;
    private readonly IOptions<BookingClientSettings> _settings;

    public BookingClient(IHttpClientFactory factory, IOptions<BookingClientSettings> settings)
    {
        _factory = factory;
        _settings = settings;
    }
    
    public async Task CreateBooking(Booking booking, string candidateId)
    {
        var response = await GetClient().PostAsJsonAsync($"{_settings.Value.Url}/{candidateId}", booking);
    }

    public async Task<IEnumerable<Booking>> GetBookings(string candidateId)
    {
        var response = await GetClient().GetAsync($"{_settings.Value.Url}/{candidateId}");
        return await response.Content.ReadFromJsonAsync<List<Booking>>();
    }

    public async Task<Booking> GetBooking(int bookingId, string candidateId)
    {
        var response = await GetClient().GetAsync($"{_settings.Value.Url}/{candidateId}/{bookingId}");
        return await response.Content.ReadFromJsonAsync<Booking>();
    }

    public async Task UpdateBooking(Booking booking, string candidateId)
    {
        await GetClient().PutAsJsonAsync($"{_settings.Value.Url}/{candidateId}/{booking.Id}", booking);
    }

    public async Task DeleteBooking(int bookingId, string candidateId)
    {
        await GetClient().DeleteAsync($"{_settings.Value.Url}/{candidateId}/{bookingId}");
    }

    private HttpClient GetClient()
    {
        var client = _factory.CreateClient();
        client.BaseAddress = new Uri(_settings.Value.Url);
        return client;
    }
}

public class BookingClientSettings
{
    public string Url { get; set; }
}