using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

/// <summary>
/// Represents user authentication data, including password and its hash.
/// </summary>
public class UserAuthenticationDTO
{
    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation of the user's password.
    /// Must match <see cref="password"/>.
    /// </summary>
    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("password", ErrorMessage = "Passwords do not match.")]
    public required string confirmPassword { get; set; }
}
