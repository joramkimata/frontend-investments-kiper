using INKIPER.GraphQL.Types;

namespace INKIPER.GraphQL.Responses.Roles;

public class GetRolesPaginatedResponse
{
    public PaginatedDataResponse<RoleType> getRolesPaginated { get; set; }
}