using System.ComponentModel.DataAnnotations;

namespace MyCompany.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}