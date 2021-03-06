using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace QbotCandidate.Api.Feature.Booking;

public class BookingRequest : IValidatableObject
{
    [Required, SwaggerSchema("Booking Id")] 
    public int Id { get; set; }
    
    [SwaggerSchema("Date format: yyyy/mm/dd",Format = "date"), Required]
    public DateTime BookingDate { get; set; }

    [Range(9, 17, ErrorMessage = "Start time hours must be between 9:00 and 17:00"), Required]
    public int StartTimeHours { get; set; }

    [Range(0, 59, ErrorMessage = "Start time minutes must be between 0 and 59"), Required]
    public int StartTimeMinutes { get; set; }

    [Required] 
    public string Description { get; set; }
    [Required] 
    public MeetingDuration? Duration { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        ValidateDayOfWeek(errors);
        ValidateBookingEndTime(errors);
        ValidateBookingStartDateTime(errors);
        return errors;
    }

    private void ValidateDayOfWeek(ICollection<ValidationResult> errors)
    {
        var dayOfWeek = BookingDate.DayOfWeek;
        if (dayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            errors.Add(new ValidationResult("Bookings can only be from Monday to Friday",
                new List<string> { nameof(BookingDate) }));
    }

    private void ValidateBookingEndTime(ICollection<ValidationResult> errors)
    {
        var startTime = new TimeOnly(StartTimeHours, StartTimeMinutes, 0);
        var endTime = TimeHelper.GetEndTime(startTime, Duration.Value);
        
        if (endTime.Hour >= 17 && endTime.Minute > 0)
            errors.Add(new ValidationResult("Booking end time can't be later than 17:00",
                new List<string> { nameof(StartTimeHours), nameof(StartTimeMinutes) }));
    }

    private void ValidateBookingStartDateTime(ICollection<ValidationResult> errors)
    {
        var bookingStartDateTime = new DateTime(BookingDate.Year, BookingDate.Month, BookingDate.Day, StartTimeHours,
            StartTimeMinutes, 0);
        if (bookingStartDateTime < DateTime.Now)
            errors.Add(new ValidationResult("Booking start date and time can't be in the past",
                new List<string> { nameof(BookingDate), nameof(StartTimeHours), nameof(StartTimeMinutes) }));
    }
}