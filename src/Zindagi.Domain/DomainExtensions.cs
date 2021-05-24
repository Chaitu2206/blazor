using System;
using System.Reflection;
using Zindagi.Domain;
using Zindagi.Domain.UserAggregate;
using Zindagi.Domain.UserAggregate.ViewModels;

namespace Zindagi
{
    public static class DomainExtensions
    {
        public static Assembly Assembly() => typeof(DomainExtensions).Assembly;

        public static UserDto GetDto(this User user) => new()
        {
            Id = user.Id,
            AlternateId = user.AuthId.Value,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth == DateTime.MinValue ? null : user.DateOfBirth,
            BloodGroup = user.BloodGroup.Id,
            Email = user.Email,
            MobileNumber = user.MobileNumber
        };

        public static bool IsOpen(this DetailedStatus status) => status == DetailedStatus.None || status == DetailedStatus.Open;

        public static bool AllowCancel(this DetailedStatus status) => status.IsOpen() || status == DetailedStatus.Assigned;
    }
}
