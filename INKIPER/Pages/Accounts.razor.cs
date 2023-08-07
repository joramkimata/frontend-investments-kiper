using CurrieTechnologies.Razor.SweetAlert2;
using INKIPER.Components;
using INKIPER.Dtos;
using INKIPER.GraphQL;
using INKIPER.GraphQL.Inputs;
using INKIPER.GraphQL.QLs.Accounts;
using INKIPER.GraphQL.Responses.Accounts;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.Pages;

public partial class Accounts
{
    // Injects
    [Inject] private GraphqlService GraphQlService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }

    [Inject] private SweetAlertService Swal { get; set; }


    // Properties
    private MudTable<Account> table;
    private IEnumerable<Account> pageData;
    private int totalItems;
    private bool Executing;
    private string searchTerm = null;


    // Methods
    private async Task<TableData<Account>> ServerReload(TableState state)
    {
        var statePage = state.Page + 1;
        var statePageSize = state.PageSize;

        if (searchTerm != null)
        {
            var response = await GraphQlService.ExecGraphQLQuery<SearchAccountsPaginatedResponse>(
                AccountsGraphQLs.SEARCH_ACCOUNTS, new
                {
                    searchTerm = searchTerm,
                    input = new PaginatedInput()
                    {
                        pageNumber = statePage,
                        pageSize = statePageSize
                    }
                });

            totalItems = response.Data.searchAccountsPaginated.totalCount;
            pageData = response.Data.searchAccountsPaginated.items;
        }
        else
        {
            var response = await GraphQlService.ExecGraphQLQuery<GetMyAllAccountsPaginated>(
                AccountsGraphQLs.GET_MY_ALL_ACCOUNTS_PAGINATED, new
                {
                    input = new PaginatedInput()
                    {
                        pageNumber = statePage,
                        pageSize = statePageSize
                    }
                });

            totalItems = response.Data.getMyAllAccountsPaginated.totalCount;
            pageData = response.Data.getMyAllAccountsPaginated.items;
        }


        var enumerable = pageData as Account[] ?? pageData.ToArray();


        return new TableData<Account>() { TotalItems = totalItems, Items = enumerable };
    }

    private void OnSearch(string text)
    {
        searchTerm = text;
        table.ReloadServerData();
    }

    // Handlers

    private void AddAccounts()
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormAccounts>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);

        DialogService.Show<EditFormAccounts>("Create New Accounts", parameters, options);
    }

    public void HandleOnSaveForm()
    {
        table.ReloadServerData();
    }

    private void HandleDeleteAccountDetail(string Uuid)
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
        var response = await GraphQlService.ExecGraphQLMutation<DeleteAccountsResponse>(
            AccountsGraphQLs.DELETE_ACCOUNTS, new
            {
                uuid
            });

        Executing = false;
        StateHasChanged();

        GraphQlService.Notify(response.deleteAccounts, title: "Successfully deleted!");

        await table.ReloadServerData();
    }

    private void HandleEditAccountDetail(Account account)
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormAccounts>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);
        parameters.Add(x => x.EditAccountsDto, account);

        DialogService.Show<EditFormAccounts>("Update Accounts", parameters, options);
    }
}