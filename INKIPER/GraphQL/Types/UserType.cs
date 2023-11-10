using INKIPER.GraphQL.Enums.Users;

namespace INKIPER.GraphQL.Types;

public class UserType : BaseType
{
    public string fullName { get; set; }
    public string phoneNumber { get; set; }
    public UserTypeEnum userType { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    
    public bool active { get; set; }
    
    public List<RoleType> roles { get; set; } 
}