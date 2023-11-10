namespace INKIPER.GraphQL.QLs.Roles;

public class RolesGraphQLs
{
    public static String GET_ROLES = @"
        query getRoles {
          getRoles {
            uuid
          }
        }
    ";

    public static string GET_ROLES_PAGENATED = @"
        query getRolesPaginated($input: PaginatedInput!) {
          getRolesPaginated(input: $input) {
            totalPages
            items {
              uuid
              name
              displayName
            }
            totalCount
          }
        }
    ";

    public static string CREATE_ROLE = @"
      mutation createRole($input: RoleInput!) {
        createRole(input: $input) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";

    public static string UPDATE_ROLE = @"
        mutation updateRole($uuid: String!, $input: RoleInput!) {
          updateRole(uuid: $uuid, updateRoleInput: $input) {
            code
            data {
              uuid
            }
            status
            errorDescription
          }
        }
    ";

    public static string DELETE_ROLE = @"
      mutation deleteRole($uuid: String!) {
        deleteRole(uuid: $uuid) {
          code
          data {
            uuid
          }
          status
          errorDescription
        }
      }
    ";

    public static string GET_ROLE = @"
      query getRole($uuid: String!) {
        getRole(uuid: $uuid) {
          uuid
          name
          displayName
          description
          permissions {
            uuid
            name
            description
            permissionGroupName
          }
        }
        getAllPermissionsGroupedByPermissionGroupName(roleUuid: $uuid) {
          permissionGroupName
          permissions {
            uuid
            name
            displayName
            belongToThisRole
          }
        }
      }
    ";

    public static string ASSIGN_PERMISSIONS = @"
      mutation assignPermissions($input: AssignPermissionsInput!) {
        assignPermissions(input: $input) {
          code
          data {
            uuid
            name
            displayName
            description
          }
          status
          errorDescription
        }
      }
    ";
}