using INKIPER.Components;
using INKIPER.Components.EditForms;
using INKIPER.GraphQL;
using INKIPER.GraphQL.Inputs;
using INKIPER.GraphQL.QLs.Roles;
using INKIPER.GraphQL.Responses.Roles;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.Pages.Auth;



public partial class Roles
{
    // Injects
    [Inject] protected GraphqlService GraphQlService { get; set; }

    [Inject] protected IDialogService DialogService { get; set; }
    
    [Inject] protected NavigationManager NavigationManager { get; set; }

    // Properties
    protected MudTable<RoleType> table;
    protected IEnumerable<RoleType> pageData;
    protected int totalItems;
    protected bool Executing;
    protected string searchTerm = null;

    private async Task<TableData<RoleType>> ServerReload(TableState state)
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
            var response = await GraphQlService.ExecGraphQLQuery<GetRolesPaginatedResponse>(
                RolesGraphQLs.GET_ROLES_PAGENATED, new
                {
                    input = new PaginatedInput()
                    {
                        pageNumber = statePage,
                        pageSize = statePageSize
                    }
                });

            totalItems = response.Data.getRolesPaginated.totalCount;
            pageData = response.Data.getRolesPaginated.items;
        }

        var enumerable = pageData as RoleType[] ?? pageData.ToArray();


        return new TableData<RoleType>() { TotalItems = totalItems, Items = enumerable };
    }

    private Task OnSearch(string s)
    {
        throw new NotImplementedException();
    }

    private void AddRole()
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormRoles>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);

        DialogService.Show<EditFormRoles>("Create Role", parameters, options);
    }

    private void HandleOnSaveForm()
    {
        table.ReloadServerData();
    }

    private void HandleEditDetail(RoleType roleType)
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormRoles>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);

        DialogService.Show<EditFormRoles>("Edit Role", parameters, options);
        parameters.Add(x => x.EditRoleTypeDto, roleType);
    }

    private void HandleDeleteDetail(string? Uuid)
    {
        var parameters = new DialogParameters<ConfirmDeleteBox>();
        parameters.Add(x => x.Uuid, Uuid);
        parameters.Add(x => x.OnDeleteFunc, EventCallback.Factory.Create<string>(this, HandleOnDeleteFunc));
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        DialogService.Show<ConfirmDeleteBox>("Delete", parameters, options);
    }
    
    private async Task HandleOnDeleteFunc(string uuid)
    {
        Executing = true;
        StateHasChanged();
        var response = await GraphQlService.ExecGraphQLMutation<DeleteRoleResponse>(
            RolesGraphQLs.DELETE_ROLE, new
            {
                uuid
            });

        Executing = false;
        StateHasChanged();

        GraphQlService.Notify(response.deleteRole, title: "Successfully deleted!");

        await table.ReloadServerData();
    }

    private void HandleViewDetail(string? uuid)
    {
        NavigationManager.NavigateTo($"/roles/{uuid}/view");
    }
}