﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.ReverseModels
{ // THIS IS THE JOIN TABLE
    public class AspNetUserRoles
    {
        [Key]
        [MaxLength(128)]
        public string UserId { get; set; }

        [Key]
        [MaxLength(128)]
        public string RoleId { get; set; }
    }
}