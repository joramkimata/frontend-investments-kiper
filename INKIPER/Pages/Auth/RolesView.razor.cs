using INKIPER.GraphQL;
using INKIPER.GraphQL.Inputs.Roles;
using INKIPER.GraphQL.QLs.Roles;
using INKIPER.GraphQL.Responses.Roles;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;

namespace INKIPER.Pages.Auth;

public partial class RolesView
{
    [Parameter]
    public string uuid { get; set; } 
    
    private RoleType? mRole { get; set; }
    
    private List<GroupPermissions>? GroupPermissionsList { get; set; }

    [Inject] protected GraphqlService GraphQlService { get; set; }
    
    protected bool Executing;

    protected override async Task OnInitializedAsync()
    {
        Executing = true;
        var response = await GraphQlService.ExecGraphQLQuery<GetRoleResponse>(RolesGraphQLs.GET_ROLE, Variables: new
        {
            uuid = uuid
        });

        mRole = response.Data.getRole;

        GroupPermissionsList = response.Data.getAllPermissionsGroupedByPermissionGroupName;

        Executing = false;
    }

    private async Task HandleAssignPermissions(List<PermissionType> permissionTypes)
    {
        Executing = true;
        List<string> permx = permissionTypes.Where(p => p.belongToThisRole).Select(p => p.uuid).ToList();

        var response = await GraphQlService.ExecGraphQLMutation<AssignPermissionsResponse>(
            RolesGraphQLs.ASSIGN_PERMISSIONS, new
            {
                input = new AssignPermissionsInput()
                {
                    roleUUID = uuid,
                    permissionUUIDs = permx
                }
            });
        
        GraphQlService.Notify(response.assignPermissions!, () =>
        {
            Executing = false;
        });
        
    }
}