using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Users;

public class GetAllUserPaginatedResponse
{
    public PaginatedDataResponse<UserType> getAllUserPaginated { get; set; }
}