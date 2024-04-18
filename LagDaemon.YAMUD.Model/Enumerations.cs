namespace LagDaemon.YAMUD.Model;


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
    Admin = 3,
    Moderator = 4,
    Player = 5
}