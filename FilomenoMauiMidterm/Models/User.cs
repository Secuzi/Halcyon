using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilomenoMauiMidterm.Models
{
	public class User
	{
		public string Username { get; set; }

		public string Password { get; set; }
		//public string Email { get; set; }
        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }
		public User(string username, string password, string email)
		{
			Username = username;
			Password = password;
		}

        public User()
        {
            
        }

     

    }
}
