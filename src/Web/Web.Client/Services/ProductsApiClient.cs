using System.Net.Http.Json;
using Web.Client.Models;

namespace Web.Client.Services;

public class ProductsApiClient
{
    private readonly HttpClient _http;

    public ProductsApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ProductDto>>("api/product");
        if (result == null)
        {
            return new List<ProductDto>();
        }
        return result;
    }

    public async Task CreateAsync(ProductDto product)
    {
        await _http.PostAsJsonAsync("api/product", product);
    }

    public async Task UpdateAsync(int id, ProductDto product)
    {
        await _http.PutAsJsonAsync($"api/product/{id}", product);
    }

    public async Task DeleteAsync(int id)
    {
        await _http.DeleteAsync($"api/product/{id}");
    }
}
