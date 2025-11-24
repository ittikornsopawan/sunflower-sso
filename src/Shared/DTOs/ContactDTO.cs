using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

/// <summary>
/// Represents a user contact method.
/// </summary>
public class ContactDTO
{
    /// <summary>
    /// Gets or sets the contact channel (e.g., Email, Mobile, Line).
    /// </summary>
    [Required(ErrorMessage = "Contact channel is required.")]
    [Display(Name = "Contact Channel")]
    public required string channel { get; set; }

    /// <summary>
    /// Gets or sets the contact information (e.g., email address or phone number).
    /// </summary>
    [Required(ErrorMessage = "Contact is required.")]
    [Display(Name = "Contact")]
    public required string contact { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact person.
    /// </summary>
    [Required(ErrorMessage = "Contact name is required.")]
    [Display(Name = "Contact Name")]
    public required string contactName { get; set; }

    /// <summary>
    /// Gets or sets the availability status of the contact (optional).
    /// </summary>
    [Display(Name = "Availability")]
    public string? available { get; set; }

    /// <summary>
    /// Gets or sets any additional remarks about the contact (optional).
    /// </summary>
    [Display(Name = "Remark")]
    public string? remark { get; set; }
}