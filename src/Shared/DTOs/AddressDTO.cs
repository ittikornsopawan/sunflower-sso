using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

/// <summary>
/// Represents a user address.
/// </summary>
public class AddressDTO
{
    /// <summary>
    /// Gets or sets the type of address (e.g., Home, Work).
    /// </summary>
    [Required(ErrorMessage = "Address type is required.")]
    [Display(Name = "Address Type")]
    public required string type { get; set; }

    /// <summary>
    /// Gets or sets the main address line.
    /// </summary>
    [Required(ErrorMessage = "Address is required.")]
    [Display(Name = "Address")]
    public required string address { get; set; }

    /// <summary>
    /// Gets or sets the additional address details (optional).
    /// </summary>
    [Display(Name = "Additional Address")]
    public string? addressAdditional { get; set; }

    /// <summary>
    /// Gets or sets the country code (ISO 3166-1 alpha-2) (optional).
    /// </summary>
    [Display(Name = "Country Code")]
    public string? countryCode { get; set; }

    /// <summary>
    /// Gets or sets the country name (optional).
    /// </summary>
    [Display(Name = "Country Name")]
    public string? countryName { get; set; }

    /// <summary>
    /// Gets or sets the state or province (optional).
    /// </summary>
    [Display(Name = "State/Province")]
    public string? state { get; set; }

    /// <summary>
    /// Gets or sets the city (optional).
    /// </summary>
    [Display(Name = "City")]
    public string? city { get; set; }

    /// <summary>
    /// Gets or sets the district (optional).
    /// </summary>
    [Display(Name = "District")]
    public string? district { get; set; }

    /// <summary>
    /// Gets or sets the sub-district (optional).
    /// </summary>
    [Display(Name = "Sub-District")]
    public string? subDistrict { get; set; }

    /// <summary>
    /// Gets or sets the postal code (optional).
    /// </summary>
    [Display(Name = "Postal Code")]
    public string? postalCode { get; set; }
}
