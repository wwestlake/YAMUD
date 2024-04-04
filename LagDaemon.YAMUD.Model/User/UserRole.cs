using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.User;

public class UserRole
{
    public UserRole()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserAccountRoles Role { get; set; }
    [JsonIgnore]
    public UserAccount User { get; set; }
}
