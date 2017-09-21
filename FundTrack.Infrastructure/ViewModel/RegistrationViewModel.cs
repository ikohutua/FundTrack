using System.ComponentModel.DataAnnotations;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Registration view model
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Gets or sets first name of user
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [MaxLength(20, ErrorMessage = ErrorMessages.MaxLengthMessage)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name of user
        /// </summary>
        [Required(ErrorMessage= ErrorMessages.RequiredFieldMessage)]
        [MaxLength(20, ErrorMessage=ErrorMessages.MaxLengthMessage)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user password
        /// </summary>
        [MinLength(7, ErrorMessage = ErrorMessages.MinLengthMessage)]
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets user login
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [RegularExpression(RegexValidationMessages.LoginRegexValidationMessage,
                           ErrorMessage = ErrorMessages.PatternLoginMessage)]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets user email
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [RegularExpression(RegexValidationMessages.EmailRegexValidationMessage,
                          ErrorMessage = ErrorMessages.PatternEmailMessage)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user phone number
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [RegularExpression(RegexValidationMessages.PhoneRegexValidationMessage,
                       ErrorMessage = ErrorMessages.PatternPhoneMessage)]
        public string Phone { get; set; }
    }
}
