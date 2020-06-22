using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesProject
{
    class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

        public double Score { get; set; }

        public int Time { get; set; }

       
    }
}
