using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Deposits;

public class GetMyAllDepositsPaginated
{
    public PaginatedDataResponse<DepositType> getMyAllDepositsPaginated { get; set; }
}