using System.ComponentModel.DataAnnotations;

namespace MaximAppTest.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
