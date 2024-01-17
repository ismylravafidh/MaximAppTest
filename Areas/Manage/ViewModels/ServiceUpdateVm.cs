using System.ComponentModel.DataAnnotations;

namespace MaximAppTest.Areas.Manage.ViewModels
{
    public class ServiceUpdateVm
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Icon { get; set; }
    }
}
