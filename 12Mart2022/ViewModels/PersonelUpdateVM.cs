using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class PersonelUpdateVM
    {
        public PersonelUpdateVM()
        {
            this.Resim = new HashSet<Resim>();
        }

        public int Id { get; set; }
        [Display(Name = "Departman Adı")]
        public Nullable<int> DepartmanId { get; set; }
        [Display(Name = "Personel Adı")]
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Ad { get; set; }
        [Display(Name = "Personel Soyadı")]
        [Required(ErrorMessage = "Lütfen soy adınızı giriniz")]
        public string Soyad { get; set; }
        [Display(Name = "Maaşınız")]
        [Required(ErrorMessage = "Lütfen maaşınızı giriniz")]
        [Range(999, 199999, ErrorMessage = "Lütfen 1000 ve 200.000 arasında bir maaş giriniz!")]
        public Nullable<short> Maas { get; set; }
        [Display(Name = "Doğum Tarihiniz")]
        [Required(ErrorMessage = "Lütfen doğum tarihinizi giriniz")]
        public Nullable<System.DateTime> DogumTarihi { get; set; }
        [Required(ErrorMessage = "Lütfen cinsiyetinizi giriniz")]
        public bool Cinsiyet { get; set; }
        [Display(Name = "Evlilik Durumunuz")]
        public bool EvliMi { get; set; }
        public string PersonelGorsel { get; set; }

        public IEnumerable<Departman> DepartmanlarListesi;
        public virtual Departman Departman { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resim { get; set; }

        public ICollection<HttpPostedFileBase> fileBases { get; set; }

        public List<string> Resimler = new List<string>();
    }
}