using Orders.Api.Dtos;

namespace Orders.Api.Services;

/// <summary>
/// Cliente HTTP que consulta el microservicio Customers para obtener los datos del cliente al crear una orden.
/// </summary>
public class CustomersApiClient
{
    private readonly HttpClient _http;

    public CustomersApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<CustomerExternalDto?> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/customer/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<CustomerExternalDto>();
    }
}
