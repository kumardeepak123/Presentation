﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "varchar(15)")]
        public string Name { get; set; }

        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }

        [MaxLength(10)]
        [DataType(DataType.Password)]
        [Column(TypeName = "varchar(8)")]
        public string Password { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(11)")]
        public string Phone { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "varchar(10)")]
        public string Organization { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(50)")]
        public string ProfileImageName { get; set; }
        

        
        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }
        [NotMapped]
        public string ProfileImageSrc { get; set; }

       
    }
}
