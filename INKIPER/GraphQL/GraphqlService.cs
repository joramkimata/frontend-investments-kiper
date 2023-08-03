using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using INKIPER.Auth;
using INKIPER.GraphQL.Responses;
using INKIPER.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace INKIPER.GraphQL;


public class GraphqlService
{
    private NavigationManager _navigationManager;

    private MyUserService _userService;

    private ISnackbar _snackbar;
    
    private static readonly GraphQLHttpClient _graphqlClient =
        new(Constants.GRAPHQL_URL, new NewtonsoftJsonSerializer());

    public GraphqlService(NavigationManager navigationManager, MyUserService userService, ISnackbar snackbar)
    {
        _navigationManager = navigationManager;
        _userService = userService;
        _snackbar = snackbar;
    }

    public async Task<QueryResponse<T>> ExecGraphQLQuery<T>(string Query, object? Variables = null)
    {
        
        var res = new QueryResponse<T>();

        try
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = Query,
            };


            if (Variables != null)
            {
                graphQlRequest.Variables = Variables;
            }

            var fetchUserFromBrowserAsync = await _userService.FetchUserFromBrowserAsync();
        
            _graphqlClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", fetchUserFromBrowserAsync?.AccessToken);

            var fetchQuery = await _graphqlClient.SendQueryAsync<T>(graphQlRequest);

            if (fetchQuery.Errors != null && fetchQuery.Errors.Any())
            {
                // Check if the error is related to authentication (UNAUTHENTICATED)
                var authenticationError = fetchQuery.Errors.FirstOrDefault(error =>
                    error.Extensions != null && error.Extensions.ContainsKey("code") &&
                    error.Extensions["code"].ToString() == "UNAUTHENTICATED");

                if (authenticationError != null)
                {
                    _snackbar.Add("UNAUTHENTICATED", Severity.Error);
                    _navigationManager.NavigateTo("/login", true, true);
                }
                else
                {
                    _snackbar.Add(fetchQuery.Errors[0].Message, Severity.Error);
                    res.Message = fetchQuery.Errors[0].Message;
                    res.Error = true;
                    return res;
                }
            }

            res.Data = fetchQuery.Data;
        }
        catch (Exception e)
        {
            res.Message = e.Message;
            res.Error = true;
            _snackbar.Add(e.Message, Severity.Error);
        }

        

        return res;
    }
}