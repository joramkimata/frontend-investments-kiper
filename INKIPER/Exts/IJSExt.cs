using Microsoft.JSInterop;

namespace INKIPER.Exts;

public static class IJSExt
{
    public static async ValueTask InitInactivityTimer<T>(this IJSRuntime js,
        DotNetObjectReference<T> dotNetObjectReference, int millseconds) where T : class
    {
        await js.InvokeVoidAsync("initInactivityTimer", dotNetObjectReference, millseconds);
    }
}