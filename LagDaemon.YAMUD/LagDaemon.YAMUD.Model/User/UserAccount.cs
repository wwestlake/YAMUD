﻿

namespace LagDaemon.YAMUD.Model.User
{
    public enum UserAccountStatus
    {
        New,
        Verified,
        Locked,
    }

    public enum UserAccountRoles
    {
        Founder = 1,
        Owner = 2,
        Admin = 4,
        Moderator = 8,
        Player = 16
    }

    public class UserAccount
    {
        public UserAccount()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set;  }
        public string DisplayName { get; set; }
        public string HashedPassword { get; set; }
        public string EmailAddress { get; set; }
        public UserAccountStatus Status { get; set; }
        public UserAccountRoles Roles { get; set; }
        public Guid VerificationToken { get; set; }
    }


}
