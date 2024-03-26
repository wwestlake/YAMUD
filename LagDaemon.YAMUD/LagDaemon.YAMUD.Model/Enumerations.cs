namespace LagDaemon.YAMUD.Model;

public enum Direction
{
    North,
    South,
    West,
    East,
    Up,
    Down
}

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