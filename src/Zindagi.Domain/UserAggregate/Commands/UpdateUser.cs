using System;
using MediatR;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

#nullable disable

namespace Zindagi.Domain.UserAggregate.Commands
{
    public class UpdateUser : IRequest<Result<UserDto>>
    {
        public UpdateUser(long userId, string firstName, string middleName, string lastName, string email, string mobileNumber, BloodGroup bloodGroup, DateTime dateOfBirth)
        {
            UserId = userId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            MobileNumber = mobileNumber;
            BloodGroup = bloodGroup;
            DateOfBirth = dateOfBirth;
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
