using LagDaemon.YAMUD.Model.Items;
using System.Runtime.InteropServices;

namespace LagDaemon.YAMUD.Model.User;



public class UserAccount
{
    public UserAccount()
    {
        ID = Guid.NewGuid();
        PlayerState = new PlayerState();
        UserRoles = new List<UserRole>();
        Inventory = new Inventory();
    }

    public Guid ID { get; set;  }
    public string DisplayName { get; set; }
    public string HashedPassword { get; set; }
    public string EmailAddress { get; set; }
    public UserAccountStatus Status { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public Guid VerificationToken { get; set; }
    public PlayerState PlayerState { get; set; }

    public Inventory? Inventory { get; set; }
}
