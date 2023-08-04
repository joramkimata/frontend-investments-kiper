using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using INKIPER.Auth;
using INKIPER.GraphQL.Enums;
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

    public async Task<T> ExecGraphQLMutation<T>(string Query, object? Variables = null)
    {

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

            await ExtractAccessToken<T>();

            var graphQlResponse = await _graphqlClient.SendMutationAsync<T>(graphQlRequest);

            if (graphQlResponse.Errors != null && graphQlResponse.Errors.Any())
            {
                var authenticationError = UnaAuthenticationError(graphQlResponse);

                if (authenticationError != null)
                {
                    _snackbar.Add("UNAUTHENTICATED", Severity.Error);
                    _navigationManager.NavigateTo("/login", true, true);
                }
                else
                {
                    _snackbar.Add(graphQlResponse.Errors[0].Message, Severity.Error);
                }
            }
            
            return graphQlResponse.Data;
        }
        catch (Exception e)
        {
            _snackbar.Add(e.Message, Severity.Error);
            throw;
        }
    }

    public void Notify<T>(MutationResponse<T> response, Action? action = null, bool defaultCall = false, string title = "Successfully saved!")
    {
        if (response.Code == HttpStatusCode.SUCCESS)
        {
            if (defaultCall)
            {
                _snackbar.Add(title, Severity.Success);
            }
            else
            {
                _snackbar.Add(title, Severity.Success);
                action?.Invoke();
            }
            
        }
        else
        {
            _snackbar.Add(response.ErrorDescription, Severity.Error);
        }
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

            await ExtractAccessToken<T>();

            var graphQlResponse = await _graphqlClient.SendQueryAsync<T>(graphQlRequest);

            if (graphQlResponse.Errors != null && graphQlResponse.Errors.Any())
            {
                var authenticationError = UnaAuthenticationError(graphQlResponse);

                if (authenticationError != null)
                {
                    _snackbar.Add("UNAUTHENTICATED", Severity.Error);
                    _navigationManager.NavigateTo("/login", true, true);
                }
                else
                {
                    _snackbar.Add(graphQlResponse.Errors[0].Message, Severity.Error);
                    res.Message = graphQlResponse.Errors[0].Message;
                    res.Error = true;
                    return res;
                }
            }

            res.Data = graphQlResponse.Data;
        }
        catch (Exception e)
        {
            res.Message = e.Message;
            res.Error = true;
            _snackbar.Add(e.Message, Severity.Error);
        }

        

        return res;
    }

    private async Task ExtractAccessToken<T>()
    {
        var fetchUserFromBrowserAsync = await _userService.FetchUserFromBrowserAsync();

        _graphqlClient.HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", fetchUserFromBrowserAsync?.AccessToken);
    }

    private static GraphQLError? UnaAuthenticationError<T>(GraphQLResponse<T> graphQlResponse)
    {
        // Check if the error is related to authentication (UNAUTHENTICATED)
        var authenticationError = graphQlResponse.Errors.FirstOrDefault(error =>
            error.Extensions != null && error.Extensions.ContainsKey("code") &&
            error.Extensions["code"].ToString() == "UNAUTHENTICATED");
        return authenticationError;
    }
    
    private static GraphQLError? AccessError<T>(GraphQLResponse<T> graphQlResponse)
    {
        // Check if the error is related to authentication (UNAUTHENTICATED)
        var authenticationError = graphQlResponse.Errors.FirstOrDefault(error =>
            error.Extensions != null && error.Extensions.ContainsKey("code") &&
            error.Extensions["code"].ToString() == "ACCESS_DENIED");
        return authenticationError;
    }
}