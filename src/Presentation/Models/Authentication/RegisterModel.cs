using System;
using System.ComponentModel.DataAnnotations;
using Shared.DTOs;

namespace Presentation.Models.Authentication;

/// <summary>
/// Represents the data required for user registration.
/// Includes user information, authentication details, addresses, and contact methods.
/// </summary>
public class RegisterModel : BaseModel
{
    /// <summary>
    /// Gets or sets the user information.
    /// </summary>
    [Required(ErrorMessage = "User information is required.")]
    [Display(Name = "User Information")]
    public required UserDTO user { get; set; }

    /// <summary>
    /// Gets or sets the user authentication details, including password and password hash.
    /// </summary>
    [Required(ErrorMessage = "User authentication details are required.")]
    [Display(Name = "Authentication Details")]
    public required UserAuthenticationDTO userAuthentication { get; set; }

    /// <summary>
    /// Gets or sets the list of user addresses. Optional.
    /// </summary>
    [Display(Name = "User Addresses")]
    public List<AddressDTO>? userAddresses { get; set; }

    /// <summary>
    /// Gets or sets the list of user contact methods. Optional.
    /// </summary>
    [Display(Name = "User Contacts")]
    public List<ContactDTO>? userContact { get; set; }
}
