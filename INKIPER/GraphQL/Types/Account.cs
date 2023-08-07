namespace INKIPER.GraphQL.Types;

public class Account: BaseType
{
    public string name { get; set; }
    public string accountNumber { get; set; }
    
    public string description { get; set; }
}