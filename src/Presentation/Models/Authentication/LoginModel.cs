using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Authentication;

/// <summary>
/// Represents the model for user login credentials.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    [Display(Name = "Username")]
    public required string username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user wants to stay logged in.
    /// </summary>
    [Display(Name = "Stay Logged In")]
    public bool stayLoggedIn { get; set; }
}