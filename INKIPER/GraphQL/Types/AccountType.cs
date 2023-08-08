namespace INKIPER.GraphQL.Types;

public class AccountType: BaseType
{
    public string name { get; set; }
    public string accountNumber { get; set; }
    
    public string description { get; set; }
}