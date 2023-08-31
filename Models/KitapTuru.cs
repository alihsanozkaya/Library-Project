using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje1.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "Kitap Tür adı boş geçilemez!")]
        [MaxLength(25)]
        [DisplayName("Kitap Tür Adı")]
        public string Ad { get; set; }
    }
}
