namespace QbotCandidate.Api.Feature.Booking;

public interface IBookingClient
{
    Task CreateBooking(Booking booking, string candidateId);
    Task<IEnumerable<Booking>> GetBookings(string candidateId);
    Task<Booking> UpdateBooking(Booking booking, string candidateId);
    Task DeleteBooking(int bookingId, string candidateId);
}