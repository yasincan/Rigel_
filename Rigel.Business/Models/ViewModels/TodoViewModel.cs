using System.ComponentModel.DataAnnotations;

namespace Rigel.Business.Models.ViewModels
{
    public class TodoViewModel : BaseViewModel
    {
        [Display(Name = "Todo Adı")]
        public string TodoName { get; set; }

        [Display(Name ="Todo Acıklaması")]
        public string Description { get; set; }
    }
}
