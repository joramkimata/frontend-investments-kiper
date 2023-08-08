using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Deposits;

public class DeleteDepositsResponse
{
    public MutationResponse<DepositType> deleteDeposits { get; set; }
}