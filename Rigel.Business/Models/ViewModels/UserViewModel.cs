using System.Collections.Generic;

namespace Rigel.Business.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<TodoViewModel> Todoes { get; set; }

    }
}