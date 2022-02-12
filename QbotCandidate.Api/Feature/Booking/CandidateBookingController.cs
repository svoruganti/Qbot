using Microsoft.AspNetCore.Mvc;

namespace QbotCandidate.Api.Feature.Booking;

[ApiController, Route("candidate/{candidateId}/bookings")]
public class CandidateBookingController : ControllerBase
{
    private readonly IBookingClient _bookingClient;

    public CandidateBookingController(IBookingClient bookingClient)
    {
        _bookingClient = bookingClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings(string candidateId)
    {
        var bookings = await _bookingClient.GetBookings(candidateId);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromForm]BookingRequest request, string candidateId)
    {
        await _bookingClient.CreateBooking(GetBooking(request), candidateId);
        return StatusCode(201);
    }

    [HttpPut("{bookingId:int}")]
    public async Task<IActionResult> UpdateBooking([FromForm] BookingRequest request, string candidateId, int bookingId)
    {
        request.Id = bookingId;
        var response = await _bookingClient.UpdateBooking(GetBooking(request), candidateId);
        return Ok(response);
    }

    [HttpDelete("{bookingId:int}")]
    public async Task<IActionResult> DeleteBooking(string candidateId, int bookingId)
    {
        await _bookingClient.DeleteBooking(bookingId, candidateId);
        return Ok();
    }

    private static Booking GetBooking(BookingRequest request)
    {
        var startTime = new TimeOnly(request.StartTimeHours, request.StartTimeMinutes, 0);
        var endTime = TimeHelper.GetEndTime(startTime, request.Duration.Value);
        return new Booking
        {
            Id = request.Id,
            Description = request.Description,
            StartDateTime = new DateTime(request.BookingDate.Year, request.BookingDate.Month, request.BookingDate.Day,
                startTime.Hour, startTime.Minute, 0),
            EndDateTime = new DateTime(request.BookingDate.Year, request.BookingDate.Month, request.BookingDate.Day,
                endTime.Hour, endTime.Minute, 0)
        };
    }
}