using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string Name { get; set; }

        public List<Contributor> Contributors { get; set; } // 1 to many (boards) relationship


    }
}