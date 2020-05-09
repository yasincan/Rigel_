using System.Collections.Generic;

namespace Rigel.ViewModels
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Todo> Todoes { get; set; }

    }
}
