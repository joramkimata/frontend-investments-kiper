namespace INKIPER.GraphQL.Types;

public class DepositType: BaseType
{
    public string depositedDate { get; set; }
    public string description { get; set; }
    public string amountDeposited { get; set; }
    public AccountType accounts { get; set; }
}