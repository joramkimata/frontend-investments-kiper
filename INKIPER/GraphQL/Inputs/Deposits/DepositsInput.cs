namespace INKIPER.GraphQL.Inputs.Deposits;

public class DepositsInput
{
    public required string accountsUid { get; set; }
    public required float amountDeposited { get; set; }
    public required string description { get; set; }
    public required string? depositedDate { get; set; }
    
    public required float currentBalance { get; set; }
}