using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PaySpace_API.Models;
using PaySpayUI.Models;

namespace PaySpayUI.Pages;

public class TaxCalculatorModel : PageModel
{
    private readonly HttpClient _client;

    public TaxCalculatorModel(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _client = clientFactory.CreateClient("TaxApi");
    }

    [BindProperty] public TaxCalculationRequest? TaxRequest { get; set; }
    public decimal CalculatedTax { get; private set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var response =
            await _client.GetAsync(
                $"api/TaxCalculator/CalculateTax?postalCode={TaxRequest.PostalCode}&income={TaxRequest.Income}");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            try
            {
                var taxCalculationResponse = JsonConvert.DeserializeObject<TaxCalculationResponse>(jsonResponse);
                if (taxCalculationResponse != null) CalculatedTax = taxCalculationResponse.Tax;
            }
            catch (JsonException ex)
            {
                Console.Write(ex);
            }
        }

        return Page();
    }
}