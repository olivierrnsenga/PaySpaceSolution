using Microsoft.AspNetCore.Mvc;
using PaySpace_API.Service;

namespace PaySpace_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaxCalculatorController : ControllerBase
{
    private readonly ITaxCalculationService _taxCalculationService;

    public TaxCalculatorController(ITaxCalculationService taxCalculationService)
    {
        _taxCalculationService = taxCalculationService;
    }

    [HttpGet("CalculateTax")]
    public IActionResult CalculateTax(string postalCode, decimal income)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            return BadRequest("Postal code is required.");
        }

        if (income < 0)
        {
            return BadRequest("Income must be a non-negative value.");
        }

        try
        {
            var tax = _taxCalculationService.CalculateTax(postalCode, income);
            return Ok(new { PostalCode = postalCode, Income = income, Tax = tax });
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur
            return StatusCode(500, "An error occurred while calculating the tax: " + ex.Message);
        }
    }
}