using INKIPER.Components;
using INKIPER.GraphQL;
using INKIPER.GraphQL.Inputs;
using INKIPER.GraphQL.QLs.Users;
using INKIPER.GraphQL.Responses.Users;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.Pages.Auth;

public partial class Users
{
    // Injects
    [Inject] protected GraphqlService GraphQlService { get; set; }

    [Inject] protected IDialogService DialogService { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }

    // Properties
    protected MudTable<UserType> table;
    protected IEnumerable<UserType> pageData;
    protected int totalItems;
    protected bool Executing;
    protected string searchTerm = null;

    private async Task<TableData<UserType>> ServerReload(TableState state)
    {
        var statePage = state.Page + 1;
        var statePageSize = state.PageSize;

        if (searchTerm != null)
        {
            // Executing = true;
            // var response = await GraphQlService.ExecGraphQLQuery<GetAllDistrictsResponse>(
            //     DistrictsGraphQLs.GET_ALL_DISTRICTS);
            //
            // pageData = response.Data.getAllDistricts;
            //
            // Executing = false;
            // StateHasChanged();
            //
            // pageData = pageData.Where(district =>
            // {
            //     if (string.IsNullOrWhiteSpace(searchTerm))
            //         if (string.IsNullOrWhiteSpace(searchTerm))
            //             return true;
            //     if (district.name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            //         return true;
            //     
            //     if (district.region.name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            //         return true;
            //
            //     return false;
            // });
        }
        else
        {
            var response = await GraphQlService.ExecGraphQLQuery<GetAllUserPaginatedResponse>(
                UsersGraphQLs.GET_USERS_PAGINATED, new
                {
                    input = new PaginatedInput()
                    {
                        pageNumber = statePage,
                        pageSize = statePageSize
                    }
                });

            totalItems = response.Data.getAllUserPaginated.totalCount;
            pageData = response.Data.getAllUserPaginated.items;
        }

        var enumerable = pageData as UserType[] ?? pageData.ToArray();


        return new TableData<UserType>() { TotalItems = totalItems, Items = enumerable };
    }

    private Task OnSearch(string s)
    {
        throw new NotImplementedException();
    }

    private Task HandleEditDetail(UserType context)
    {
        throw new NotImplementedException();
    }

    private Task HandleDeleteDetail(string? contextUuid)
    {
        throw new NotImplementedException();
    }

    private void HandleBlockUser(string? Uuid)
    {
        var parameters = new DialogParameters<ConfirmBox>();
        parameters.Add(x => x.Uuid, Uuid);
        parameters.Add(x => x.Text, "User will be blocked");
        parameters.Add(x => x.ConfirmBtnText, "OK");
        parameters.Add(x => x.OnConfirm, EventCallback.Factory.Create<string>(this, HandleBlockClicked));
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true};
        DialogService.Show<ConfirmBox>("Are you sure?", parameters, options);
    }

    private async Task HandleBlockClicked(string uuid)
    {
        Executing = true;
        StateHasChanged();
        var response = await GraphQlService.ExecGraphQLMutation<BlockUserResponse>(
            UsersGraphQLs.BLOCK_USER, new
            {
                uuid 
            });

        Executing = false;
        StateHasChanged();

        GraphQlService.Notify(response.blockUser, title: "Successfully blocked!");

        await table.ReloadServerData();
    }

    private void HandleActivateUser(string? Uuid)
    {
        var parameters = new DialogParameters<ConfirmBox>();
        parameters.Add(x => x.Uuid, Uuid);
        parameters.Add(x => x.Text, "User will be activated");
        parameters.Add(x => x.ConfirmBtnText, "OK");
        parameters.Add(x => x.OnConfirm, EventCallback.Factory.Create<string>(this, HandleActiveClicked));
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true};
        DialogService.Show<ConfirmBox>("Are you sure?", parameters, options);
    }

    private async Task HandleActiveClicked(string uuid)
    {
        Executing = true;
        StateHasChanged();
        var response = await GraphQlService.ExecGraphQLMutation<ActivateUserResponse>(
            UsersGraphQLs.ACTIVATE_USER, new
            {
                uuid 
            });

        Executing = false;
        StateHasChanged();

        GraphQlService.Notify(response.activateUser, title: "Successfully activated!");

        await table.ReloadServerData();
    }

    private void HandleChangeUserPassword(string? Uuid)
    {
        var parameters = new DialogParameters<ChangePassword>();
        parameters.Add(x => x.UserUuid, Uuid);
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.Center, FullWidth = true, CloseButton = true };
        DialogService.Show<ChangePassword>("Change Password", parameters, options);
    }

    private void HandleUserDetail(string? Uuid)
    {
       NavigationManager.NavigateTo($"/users/{Uuid}/view");
    }
}