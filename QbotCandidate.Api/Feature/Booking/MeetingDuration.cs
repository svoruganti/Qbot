using System.Text.Json.Serialization;

namespace QbotCandidate.Api.Feature.Booking;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeetingDuration
{
    Thirty,
    Sixty
}