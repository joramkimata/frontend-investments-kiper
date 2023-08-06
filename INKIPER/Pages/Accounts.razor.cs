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

    
    // Methods
    private async Task<TableData<Account>> ServerReload(TableState state)
    {
        var statePage = state.Page + 1;
        var statePageSize = state.PageSize;

        var response = await GraphQlService.ExecGraphQLQuery<GetAllAccountsPaginated>(AccountsGraphQLs.GET_ALL_ACCOUNTS_PAGINATED, new
        {
            input = new PaginatedInput()
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
        parameters.Add(x => x.OnDeleteFunc, HandleOnDeleteFunc);
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        DialogService.Show<ConfirmDeleteBox>("Delete", parameters, options);
    }

    private void HandleOnDeleteFunc(string uuid)
    {
        Executing = true;
        StateHasChanged();
        
    }

    private void HandleEditAccountDetail(Account account)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormAccounts>(); 
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);
        parameters.Add(x => x.EditAccountsDto, account);
            
        DialogService.Show<EditFormAccounts>("Update Accounts", parameters, options);
    }
}