namespace INKIPER.GraphQL.Inputs.Accounts;

public class AccountsInput
{
    public required string name { get; set; }
    public required string accountNumber { get; set; }
    public string description { get; set; }
}