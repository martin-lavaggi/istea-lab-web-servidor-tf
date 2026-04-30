using System.Net.Http.Json;
using Web.Client.Models;

namespace Web.Client.Services;

public class CustomersApiClient
{
    private readonly HttpClient _http;

    public CustomersApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<CustomerDto>>("api/customer");
        if (result == null)
        {
            return new List<CustomerDto>();
        }
        return result;
    }

    public async Task CreateAsync(CustomerDto customer)
    {
        await _http.PostAsJsonAsync("api/customer", customer);
    }

    public async Task UpdateAsync(int id, CustomerDto customer)
    {
        await _http.PutAsJsonAsync($"api/customer/{id}", customer);
    }

    public async Task DeleteAsync(int id)
    {
        await _http.DeleteAsync($"api/customer/{id}");
    }
}
