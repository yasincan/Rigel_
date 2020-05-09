using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Rigel.ViewModels
{
    public class Todo : BaseModel
    {
        [Display(Name = "Todo Adı")]
        public string TodoName { get; set; }

        [Display(Name ="Todo Acıklaması")]
        public string Description { get; set; }
    }
}
