using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


// Entity is our Object Relational Mapper (ORM)
namespace FakeTrello.Models
{
    public class TrelloUser
    {
        [Key] // ONLY Applies to first property this is a constraint annotations
        public int TrelloUserId { get; set; } //  PRIMARY KEY

        // Stacking on properties applies multiple annotations
        // to the following property
        [MinLength(10)]
        [MaxLength(60)]
        public string Email { get; set; }

        [MaxLength(60)]
        public string Username { get; set; }

        [MaxLength(60)]
        public string Fullname { get; set; }

        public ApplicationUser BaseUser { get; set; } // 1 to 1 relationship

        public List<Board> Boards { get; set; } // 1 to many (boards) relationship

        public List<Contributor> Contributors { get; set; } // 1 to many (boards) relationship


    }
}