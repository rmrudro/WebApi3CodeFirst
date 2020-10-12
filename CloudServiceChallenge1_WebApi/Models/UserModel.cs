using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge1_WebApi.Models
{
    public class UserModel
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string UserName { get; set; }
        public int RankPoints { get; set; }
    }
}
