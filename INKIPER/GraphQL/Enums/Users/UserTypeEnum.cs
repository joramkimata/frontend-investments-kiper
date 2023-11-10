namespace INKIPER.GraphQL.Enums.Users;

public enum UserTypeEnum
{
    // [StringValue("INVESTOR")]
    INVESTOR,
    
    // [StringValue("ADMIN")]
    ADMIN
}

public class StringValueAttribute : Attribute
{
    public string Value { get; }

    public StringValueAttribute(string value)
    {
        Value = value;
    }
}