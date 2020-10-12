//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
//using System.Web.Http;
//using Test.Context;
using CloudServiceChallenge1_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CloudServiceChallenge1_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GameUserContext _context;

        public UserController(GameUserContext context)
        {
            _context = context;
        }
        // GET: api/User
        [HttpGet]
       // [Route("api/User")]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            try
            {
                //Prepare data to be returned using Linq as follows  
                var result = from users in _context.Users
                             select new
                             {
                                 users.UserId,
                                 users.UserName,
                                 users.RankPoints
                             };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }

        }
        // POST: api/User
        [HttpPost]
       // [Route("api/User")]
        public IActionResult Create100Data()
        {
            UserModel usermodel = new UserModel();

            Random ran = new Random();
            int rankPoints = 0;
            String letters = "abcdefghijklmnopqrstuvwxyz";
            int length = 8;
            int maxid = 0;
            String randomName = "";
            try
            {
                for (int j = 0; j < 100; j++)
                {
                    rankPoints = ran.Next(100);
                    randomName = "";
                    var rec = _context.Users.FirstOrDefault();

                    if (rec == null)
                    {
                        maxid = 0;
                    }
                    else
                    {
                        maxid = _context.Users.Max(p => p.UserId);
                    }
                    for (int i = 0; i < length; i++)
                    {
                        int c = ran.Next(26);
                        randomName = randomName + letters.ElementAt(c);
                    }

                    usermodel.UserId = maxid + 1;
                    usermodel.RankPoints = rankPoints;
                    usermodel.UserName = randomName;

                    _context.Add(usermodel);
                    _context.Database.OpenConnection();
                    // _context.Database.OpenConnection();
                    try
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                        _context.SaveChanges();
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
                    }
                    finally
                    {
                        _context.Database.CloseConnection();
                    }

                }
                _context.SaveChangesAsync();
                return Ok(new { success = true, message = "User Inserted" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }


        [HttpPut]
      //  [Route("api/User")]
        public IActionResult RemoveRankPoints()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("UPDATE Users SET RankPoints=0 WHERE RankPoints<=50;");
                return Ok(new { success = true, message = "Updated" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }

        }
        //[System.Web.Http.AllowAnonymous]
        [HttpGet]
        [Route("TopTen")]
        public IActionResult SortUserData()
        {
            IEnumerable<UserModel> userList = _context.Users.ToList();

            var result = userList.OrderByDescending(e => e.RankPoints).ThenBy(e => e.UserName).Take(10);
            return Ok(result);

        }
    }
}
