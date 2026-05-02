using System.Net.Http.Json;
using Web.Client.Models;

namespace Web.Client.Services;

public class OrdersApiClient
{
    private readonly HttpClient _http;

    public OrdersApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<OrderDto>>("api/order");
        if (result == null)
        {
            return new List<OrderDto>();
        }
        return result;
    }

    public async Task CreateAsync(OrderRequest request)
    {
        await _http.PostAsJsonAsync("api/order", request);
    }
}
