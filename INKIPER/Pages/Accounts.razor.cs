using INKIPER.Components;
using INKIPER.GraphQL;
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
    
    
    // Properties
    private MudTable<Account> table;
    private IEnumerable<Account> pageData;
    private int totalItems;

    
    // Methods
    private async Task<TableData<Account>> ServerReload(TableState state)
    {
        var statePage = state.Page + 1;
        var statePageSize = state.PageSize;

        var response = await GraphQlService.ExecGraphQLQuery<GetAllAccountsPaginated>(AccountsGraphQLs.GET_ALL_ACCOUNTS_PAGINATED, new
        {
            input = new
            {
                pageNumber = statePage,
                pageSize = statePageSize
            }
        });

        totalItems = response.Data.getAllAccountsPaginated.totalCount;
        pageData = response.Data.getAllAccountsPaginated.items;
        var enumerable = pageData as Account[] ?? pageData.ToArray();
        
        
        
        return new TableData<Account>() { TotalItems = totalItems, Items = enumerable };
    }
    
    private void OnSearch(string text)
    {
        table.ReloadServerData();
    }
    
    // Handlers
    private void HandleViewAccountDetail(string Uuid)
    {
        
    }

    private void AddAccounts()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        DialogService.Show<EditFormAccounts>("Create New Accounts", options);
    }
}