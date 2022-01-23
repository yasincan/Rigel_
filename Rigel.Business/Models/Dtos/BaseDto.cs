using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rigel.Business.Models.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "MM/dd/yyyy HH:mm:ss", NullDisplayText = "Silinmemiştir")]
        [Sortable]
        [Display(Name = "Silinme Tarihi")]
        public DateTime? DeletedDate { get; set; }

       
        [DisplayFormat(DataFormatString = "MM/dd/yyyy HH:mm:ss")]
        [Display(Name = "Oluşturulma Tarihi")]
        [Sortable(Default =true)]
        public DateTime CreatedDate { get; set; }


        [DisplayFormat(DataFormatString = "MM/dd/yyyy HH:mm:ss", NullDisplayText = "Güncellenmemiştir")]
        [Display(Name = "Güncelleme Tarihi")]
        [Sortable]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name ="Aktif / Pasif")]
        [Sortable]
        public bool IsActive { get; set; }
    }
}
