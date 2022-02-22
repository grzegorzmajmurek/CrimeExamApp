using System;
using System.ComponentModel.DataAnnotations;

namespace CrimeService
{
    public record CrimeDto(Guid Id,
                       DateTimeOffset EventDate,
                       EventType EventType,
                       string Description,
                       string EventPlace,
                       string Email,
                       Status Status,
                       string LawEnforcementId);
    public record CreateCrimeDto([Required][Range(0, 2)] EventType EventType,
                                 [Required][StringLength(100)] string Description,
                                 [Required][StringLength(50)] string EventPlace,
                                 [Required][StringLength(20)] string Email,
                                 [Required][Range(0, 2)] Status Status,
                                 [Required] string LawEnforcementId);
    public record UpdateCrimeDto([Required][Range(0, 2)] EventType EventType,
                                 [Required][StringLength(100)] string Description,
                                 [Required][StringLength(50)] string EventPlace,
                                 [Required][StringLength(20)] string Email,
                                 [Required][Range(0, 2)] Status Status,
                                 [Required] string LawEnforcementId);

    public enum EventType { Burglary, Assault, Murder }
    public enum Status { Waiting, Finished, Declined }
}
