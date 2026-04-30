using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Client;
using Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var productsApiUrl = builder.Configuration["Services:ProductsApi"];
if (productsApiUrl == null)
{
    throw new InvalidOperationException("Falta la URL 'Services:ProductsApi' en appsettings.json.");
}
builder.Services.AddHttpClient<ProductsApiClient>(client =>
{
    client.BaseAddress = new Uri(productsApiUrl);
});

await builder.Build().RunAsync();
