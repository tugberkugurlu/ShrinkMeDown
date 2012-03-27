using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugberkUg.UrlShrinker.Client.Models {

    public class ShortenedUrl {

        public int Id { get; set; }

        //[Required, StringLength(10, ErrorMessage = "{0} should be max. {1} chars")]
        public string Alias { get; set; }

        //[Required, StringLength(500)]
        public string Url { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}