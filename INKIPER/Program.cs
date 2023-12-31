
using CurrieTechnologies.Razor.SweetAlert2;
using INKIPER.Auth;
using INKIPER.GraphQL;
using INKIPER.Services;
using INKIPER.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSweetAlert2();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Constants.BASE_URL) });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<GraphqlService>();

builder.Services.AddScoped<MyUserService>();
builder.Services.AddScoped<MyAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp 
    => sp.GetRequiredService<MyAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();