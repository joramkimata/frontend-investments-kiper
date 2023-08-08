using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Deposits;

public class CreateDepositsResponse
{
    public MutationResponse<DepositType> createDeposits { get; set; }
}