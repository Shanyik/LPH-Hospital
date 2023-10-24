using System.ComponentModel.DataAnnotations;

namespace lph_api.Contracts;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password,
    [Required]string Role,
    [Required]string PhoneNumber,
    [Required]string FirstName,
    [Required]string LastName,
    [Required]string Ward, 
    [Required]string MedicalNumber);