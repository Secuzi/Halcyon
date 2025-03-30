using FilomenoMauiMidterm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilomenoMauiMidterm.Services
{
	public class AuthService
	{
		public ObservableCollection<User> Users { get; } = new()
		{
			new User("admin", "123", "admin@gmail.com") // Default user
		};

	}
}
