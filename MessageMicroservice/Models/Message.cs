using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageMicroservice.Models
{
    public class Message
    {
        /// <summary>
        /// id_poruke
        /// </summary>
        
        public int id_poruke { get; set; }

        public DateTime vreme { get; set; }

        public string tekst { get; set; }

        public int id_kanala { get; set; }

        public int id_ucesnik { get; set; }
    }
}