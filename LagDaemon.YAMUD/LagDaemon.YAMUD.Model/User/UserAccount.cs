
using MongoDB.Bson;

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
        User = 1,
        Moderator = 2,
        Administrator = 4,
        Owner = 8,
        Founder = 16
    }

    public class UserAccount
    {
        public UserAccount()
        {
            ID = Guid.NewGuid();
        }

        public ObjectId _id { get; set; }
        public Guid ID { get; set;  }
        public string? DisplayName { get; set; }
        public string? HashedPassword { get; set; }
        public string? EmailAddress { get; set; }
        public UserAccountStatus Status { get; set; }
        public UserAccountRoles Roles { get; set; }
    }


}
