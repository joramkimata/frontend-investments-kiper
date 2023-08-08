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

    private Task HandleEditDetail(DepositType context)
    {
        throw new NotImplementedException();
    }

    private Task HandleDeleteDetail(string contextUuid)
    {
        throw new NotImplementedException();
    }

    private void HandleOnSaveForm()
    {
        table.ReloadServerData();
    }
}