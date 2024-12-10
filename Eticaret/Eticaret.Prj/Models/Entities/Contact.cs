using System.ComponentModel.DataAnnotations;

namespace Eticaret.Prj.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad"), Required(ErrorMessage = "Ad alanı boş geçilemez.")]
        public string Name { get; set; }
        [Display(Name = "Soyad"), Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        public string Surname { get; set; }
        [Display(Name = "E-Posta"), Required(ErrorMessage = "E-Posta alanı boş geçilemez."), EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        public string? Phone { get; set; }
        [Display(Name = "Mesaj"), Required(ErrorMessage = "Mesaj alanı boş geçilemez.")]
        public string Message { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate{ get; set; } = DateTime.Now;
    }
}
