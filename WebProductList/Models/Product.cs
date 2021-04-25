using System.ComponentModel.DataAnnotations;

namespace WebProductList.Models {
    public class Product : BriefProduct {
        [Required(ErrorMessage = "Заполните описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
