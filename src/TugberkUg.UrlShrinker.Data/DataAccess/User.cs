using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugberkUg.UrlShrinker.Data.DataAccess {

    public class User {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HashAlgorithm { get; set; }
        public bool IsApproved { get; set; }
    }
}