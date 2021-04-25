using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProductList.Models {
    public class User {
        //[Required(ErrorMessage = "Не указан логин")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Не указан пароль")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public override bool Equals(object obj) {
            if (obj is User user) {
                return Name == user.Name & Password == user.Password;
            }
            return false;
        }

        public override int GetHashCode() {
            int hash = 11;
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Password.GetHashCode();
            return hash;
        }
    }
}
