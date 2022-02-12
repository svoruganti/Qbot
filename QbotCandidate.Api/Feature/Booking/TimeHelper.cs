namespace QbotCandidate.Api.Feature.Booking;

public static class TimeHelper
{
    public static TimeOnly GetEndTime(TimeOnly startTime, MeetingDuration duration)
    {
        return duration switch
        {
            MeetingDuration.Sixty => startTime.AddMinutes(60),
            MeetingDuration.Thirty => startTime.AddMinutes(30),
            _ => startTime
        };
    }
}