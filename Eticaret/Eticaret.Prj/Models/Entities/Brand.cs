﻿using Eticaret.Prj.Entities;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Prj.Models.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public required string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Logo")]
        public string? Logo { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        [Display(Name = "Sıra Numarası")]
        public int OrderNo { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Product>? Products { get; set; }

    }
}