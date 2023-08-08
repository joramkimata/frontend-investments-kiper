using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Deposits;

public class GetAllDepositsResponse
{
    public List<DepositType> getAllDeposits { get; set; }
}