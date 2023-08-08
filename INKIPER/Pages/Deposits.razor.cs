using INKIPER.Components;
using INKIPER.Components.EditForms;
using INKIPER.GraphQL;
using INKIPER.GraphQL.Inputs;
using INKIPER.GraphQL.QLs.Deposit;
using INKIPER.GraphQL.Responses.Deposits;
using INKIPER.GraphQL.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.Pages;

public partial class Deposits
{
    
    // Injects
    [Inject] private GraphqlService GraphQlService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    
    // Properties
    private MudTable<DepositType> table;
    private IEnumerable<DepositType> pageData;
    private int totalItems;
    private bool Executing;
    private string searchTerm = null;


    // Methods
    private async Task<TableData<DepositType>> ServerReload(TableState state)
    {
        var statePage = state.Page + 1;
        var statePageSize = state.PageSize;

        if (searchTerm != null)
        {
            Executing = true;
            var response = await GraphQlService.ExecGraphQLQuery<GetAllDepositsResponse>(
                DepositGraphQLs.GET_ALL_DEPOSITS);

            pageData = response.Data.getAllDeposits;
            
            Executing = false;
            StateHasChanged();
            
            pageData = pageData.Where(deposit =>
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    if (string.IsNullOrWhiteSpace(searchTerm))
                        return true;
                if (deposit.accounts.name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (deposit.amountDeposited.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (deposit.depositedDate.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return true;
            
                return false;
            });
        }
        else
        {
            var response = await GraphQlService.ExecGraphQLQuery<GetMyAllDepositsPaginated>(
                DepositGraphQLs.GET_MY_ALL_DEPOSITS_PAGENATED, new
                {
                    input = new PaginatedInput()
                    {
                        pageNumber = statePage,
                        pageSize = statePageSize
                    }
                });

            totalItems = response.Data.getMyAllDepositsPaginated.totalCount;
            pageData = response.Data.getMyAllDepositsPaginated.items;
        }


        var enumerable = pageData as DepositType[] ?? pageData.ToArray();


        return new TableData<DepositType>() { TotalItems = totalItems, Items = enumerable };
    }

    private void OnSearch(string text)
    {
        searchTerm = text;
        table.ReloadServerData();
    }

    // Handlers

    public void AddDeposits()
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormDeposits>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);

        DialogService.Show<EditFormDeposits>("New Deposit", parameters, options);
    }

    private void HandleEditDetail(DepositType depositType)
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormDeposits>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);
        parameters.Add(x => x.EditDepositDto, depositType);

        DialogService.Show<EditFormDeposits>("Edit Deposit", parameters, options);
    }

    private void HandleDeleteDetail(string Uuid)
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
        var response = await GraphQlService.ExecGraphQLMutation<DeleteDepositsResponse>(
            DepositGraphQLs.DELETE_DEPOSIT, new
            {
                uuid
            });

        Executing = false;
        StateHasChanged();

        GraphQlService.Notify(response.deleteDeposits, title: "Successfully deleted!");

        await table.ReloadServerData();
    }

    private void HandleOnSaveForm()
    {
        table.ReloadServerData();
    }

    private void HandleViewDetail(DepositType depositType)
    {
        var options = new DialogOptions
            { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, FullWidth = true, CloseButton = true };
        var parameters = new DialogParameters<EditFormDeposits>();
        parameters.Add(x => x.OnSaveForm, HandleOnSaveForm);
        parameters.Add(x => x.EditDepositDto, depositType);
        parameters.Add(x => x.ViewOnly, true);

        DialogService.Show<EditFormDeposits>("View Deposit", parameters, options);
    }
}