using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

/// <summary>
/// Represents a user data transfer object used for authentication purposes.
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    [Display(Name = "User Id")]
    public Guid? id { get; set; }

    /// <summary>
    /// Gets or sets the user code (unique identifier for user registration flow).
    /// </summary>
    [Display(Name = "Code")]
    public string? code { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    [Display(Name = "Username")]
    public required string username { get; set; }

    /// <summary>
    /// Gets or sets the authentication type.
    /// Input includes: 'PASSWORD', 'OAUTH', 'EMAIL_Otp', 'MOBILE_Otp'.
    /// </summary>
    [Required(ErrorMessage = "Authentication type is required.")]
    [Display(Name = "Authentication Type")]
    public required string authenticationType { get; set; }
}
