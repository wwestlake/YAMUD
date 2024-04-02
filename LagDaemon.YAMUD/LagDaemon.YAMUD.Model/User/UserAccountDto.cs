namespace LagDaemon.YAMUD.Model.User;
public class UserAccountDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }

    public string Role { get; set; }
}
