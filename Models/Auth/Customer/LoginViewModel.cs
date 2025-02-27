using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopHoa.Models.Auth.Customer
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Vui lòng nhập tài khoản!")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ!")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mạt khẩu!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}