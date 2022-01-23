using System.ComponentModel.DataAnnotations;

namespace Rigel.Business.Models.Dtos
{
    public class TodoDto : BaseDto
    {
        [Display(Name = "İş")]
        public string TodoName { get; set; }

        [Display(Name ="Açıklama")]
        public string Description { get; set; }
    }
}
