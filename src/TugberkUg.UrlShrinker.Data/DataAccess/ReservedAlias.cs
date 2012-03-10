using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TugberkUg.UrlShrinker.Data.DataAccess {

    public class ReservedAlias {

        public int Id { get; set; }

        [Required, StringLength(10)]
        public string Name { get; set; }
    }
}
