using Orders.Api.Dtos;

namespace Orders.Api.Services;

/// <summary>
/// Cliente HTTP que consulta y actualiza el microservicio Products para obtener
/// los datos del producto al crear una orden y descontar el stock comprado.
/// </summary>
public class ProductsApiClient
{
    private readonly HttpClient _http;

    public ProductsApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<ProductExternalDto?> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/product/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<ProductExternalDto>();
    }

    public async Task UpdateAsync(int id, ProductExternalDto product)
    {
        var response = await _http.PutAsJsonAsync($"api/product/{id}", product);
        response.EnsureSuccessStatusCode();
    }
}
