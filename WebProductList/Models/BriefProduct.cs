using System.ComponentModel.DataAnnotations;

namespace WebProductList.Models {
    public class BriefProduct {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Продукт")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}
