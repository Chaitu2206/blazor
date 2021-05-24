using System;
using System.Collections.Generic;
using MimeKit;
using Zindagi.Domain.Common.SystemEvents;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Domain.UserAggregate.Commands;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate
{
    public class User : AggregateBase
    {
#pragma warning disable 8618
        protected User() { }
#pragma warning restore 8618

        private User(VendorId authId, string email, DateTime dateOfBirth, string firstName, string? middleName = null, string? lastName = null,
                    string? mobileNumber = null, string? alternateMobileNumber = null, Status? isEmailVerified = null,
                    Status? isActive = null, Status? isDonor = null, BloodGroup? bloodGroup = null, string? pictureUrl = null)
        {
            AuthId = authId;
            FirstName = firstName.GetCleanString();
            MiddleName = middleName.GetCleanString();
            LastName = lastName.GetCleanString();
            MobileNumber = mobileNumber.GetCleanString();
            AlternateMobileNumber = alternateMobileNumber.GetCleanString();
            Email = email.GetCleanString();
            IsEmailVerified = isEmailVerified ?? Status.None;
            IsActive = isActive ?? Status.None;
            IsDonor = isDonor ?? Status.None;
            DateOfBirth = dateOfBirth;
            BloodGroup = bloodGroup ?? BloodGroup.None;
            PictureUrl = pictureUrl.GetCleanString();
        }


        public VendorId AuthId { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
        public string MobileNumber { get; private set; }
        public string AlternateMobileNumber { get; private set; }
        public string Email { get; private set; }
        public Status IsEmailVerified { get; private set; }
        public Status IsActive { get; private set; }
        public Status IsDonor { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public BloodGroup BloodGroup { get; private set; }
        public string PictureUrl { get; private set; }

        public virtual IReadOnlyList<BloodRequest> CreatedBloodRequests { get; set; } = null!;
        public virtual IReadOnlyList<BloodRequest> AssignedBloodRequests { get; set; } = null!;

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age -= 1;

                return DateOfBirth == DateTime.MinValue ? 0 : age;
            }
        }

        public void SetId(long id) => Id = id;
        public void Update(UpdateUser user)
        {
            FirstName = user.FirstName.GetCleanString();
            MiddleName = user.MiddleName.GetCleanString();
            LastName = user.LastName.GetCleanString();
            Email = user.Email.GetCleanString();
            MobileNumber = user.MobileNumber.GetCleanString();
            BloodGroup = user.BloodGroup;
            DateOfBirth = user.DateOfBirth;
        }

        public override string GetPersistenceKey() => AuthId.GetPersistenceKey();

        public static User Create(OpenIdUser user, bool loggedInEvent)
        {
            var result = new User(user.AuthId, user.Email, DateTime.MinValue, user.FirstName,
                                  isEmailVerified: user.IsEmailVerified ? Status.Active : Status.Inactive,
                                  pictureUrl: user.PictureUrl);
            result.SetId(user.SerialNumber);

            if(!loggedInEvent)
                result.AddDomainEvent(new SendEmailNotification(new List<MailboxAddress> { new(result.FullName, result.Email) }, "User Registered", "User Registered"));

            return result;
        }


    }
}
