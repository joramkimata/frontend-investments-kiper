using INKIPER.Components.EditForms;
using INKIPER.GraphQL;
using INKIPER.GraphQL.QLs.Users;
using INKIPER.GraphQL.Responses.Users;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.Pages.Auth;

public partial class UsersView
{
    [Parameter] public string uuid { get; set; }

    // Injects

    [Inject] protected IDialogService DialogService { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }

    private UserType? mUser { get; set; }

    private List<RoleType> roles { get; set; }

    [Inject] protected GraphqlService GraphQlService { get; set; }

    protected bool Executing;


    protected override async Task OnInitializedAsync()
    {
        await LoadUserRoles();
    }
    
    async Task LoadUserRoles()
    {
        Executing = true;
        var response = await GraphQlService.ExecGraphQLQuery<GetUserResponse>(UsersGraphQLs.GET_USER_INFO,
            Variables: new
            {
                uuid
            });

        mUser = response.Data.getUser;

        roles = response.Data.getRoles;

        Executing = false;
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/users");
    }

    private void AssignRoles()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true, Position = DialogPosition.Center, FullWidth = true, CloseButton = true,
            MaxWidth = MaxWidth.Large
        };
        var parameters = new DialogParameters<EditFormAssignRoles>();
        parameters.Add(x => x.OnSaveForm, EventCallback.Factory.Create(this, HandleOnSaveForm));
        parameters.Add(x => x.UserType, mUser);
        parameters.Add(x => x.Roles, roles);

        DialogService.Show<EditFormAssignRoles>("Assign Roles", parameters, options);
    }

    private async Task HandleOnSaveForm()
    {
        await LoadUserRoles();
    }
}