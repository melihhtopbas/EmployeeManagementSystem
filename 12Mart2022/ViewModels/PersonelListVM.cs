using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class PersonelListVM
    {
        public PersonelListVM()
        {
            Images = new HashSet<Resim>();
        }
        public int Id { get; set; }


        public Nullable<int> DepartmanId { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }


        public Nullable<short> Maas { get; set; }

        public Nullable<System.DateTime> DogumTarihi { get; set; }

        public bool Cinsiyet { get; set; }

        public string PersonelGorsel { get; set; }

        public bool EvliMi { get; set; }

        public virtual Departman Departman { get; set; }

        public IEnumerable<Departman> DepartmanlarListesi;

        public ICollection<Resim> Images;
        public virtual Resim Image { get; set; }

        public List<string> Resimler = new List<string>();


    }
}