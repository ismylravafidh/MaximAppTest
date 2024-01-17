using System.ComponentModel.DataAnnotations;

namespace MaximAppTest.Areas.Manage.ViewModels
{
    public class ServiceCreateVm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Icon { get; set; }
    }
}
