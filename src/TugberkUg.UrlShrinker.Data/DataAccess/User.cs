using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TugberkUg.UrlShrinker.Data.DataAccess {

    public class User {

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Required]
        public string Password { get; set; }

        public string HashAlgorithm { get; set; }
        public bool IsApproved { get; set; }
    }
}