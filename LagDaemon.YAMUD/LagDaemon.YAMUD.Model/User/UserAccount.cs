namespace LagDaemon.YAMUD.Model.User;



public class UserAccount
{
    public UserAccount()
    {
        ID = Guid.NewGuid();
        PlayerState = new PlayerState();
    }

    public Guid ID { get; set;  }
    public string DisplayName { get; set; }
    public string HashedPassword { get; set; }
    public string EmailAddress { get; set; }
    public UserAccountStatus Status { get; set; }
    public UserAccountRoles Roles { get; set; }
    public Guid VerificationToken { get; set; }
    public PlayerState PlayerState { get; set; }
}
