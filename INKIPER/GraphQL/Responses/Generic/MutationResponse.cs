using INKIPER.GraphQL.Enums;
using MudBlazor;

namespace INKIPER.GraphQL.Responses;

public class MutationResponse<T>
{

    private ISnackbar Snackbar;

    public MutationResponse(ISnackbar snackbar)
    {
        Snackbar = snackbar;
    }

    public HttpStatusCode Code { get; set; }
    
    public T Data { get; set; }

    public bool Status { get; set; } = true;
    
    public string ErrorDescription { get; set; }

    public void Notify(string message, bool error = false)
    {
        if (error)
        {
            Snackbar.Add(message, Severity.Error);
        }
        else
        {
            Snackbar.Add(message, Severity.Success);
        }
    }
}