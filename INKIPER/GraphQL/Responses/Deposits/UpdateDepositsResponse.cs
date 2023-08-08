using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Deposits;

public class UpdateDepositsResponse
{
    public MutationResponse<DepositType> updateDeposits { get; set; }
}