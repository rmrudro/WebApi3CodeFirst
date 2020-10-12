//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge1_WebApi.Models
{
    public class GameUserContext: DbContext
    {
        public GameUserContext(DbContextOptions<GameUserContext> options) : base(options)
        {

        }
       
        public DbSet<UserModel> Users { get; set; }
    }
}
