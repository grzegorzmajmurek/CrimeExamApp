using CrimeService.Entities;

namespace CrimeService
{
    public static class Extensions
    {
        public static CrimeDto AsDto(this Crime crime)
        {
            return new CrimeDto(crime.Id, crime.EventDate, crime.EventType, crime.Email, crime.EventPlace, crime.Description, crime.Status, crime.LawEnforcementId);
        }
    }
}
