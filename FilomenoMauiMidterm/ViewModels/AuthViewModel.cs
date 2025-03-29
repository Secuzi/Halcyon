using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Views;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilomenoMauiMidterm.ViewModels
{

	[QueryProperty(nameof(RegisteredEmail), nameof(RegisteredEmail))]
	[QueryProperty(nameof(RegisteredPassword), nameof(RegisteredPassword))]
	[QueryProperty(nameof(RegisteredUsername), nameof(RegisteredUsername))]
	public class AuthViewModel : INotifyPropertyChanged 
	{
        public User User { get; set; }
		public Page Page { get; set; }

        private string _username;
        private string _password;
		private string _email; 
		private string _registeredUsername;
        private string _registeredPassword;
		private string _registeredEmail;
		public string Username {

			get => _username;
			set
			{
				if (_username != value)
				{
					_username = value;
					OnPropertyChanged(nameof(Username));
				}
			}
				
		}
		public string Password
		{

			get => _password;
			set
			{
				if (_password != value)
				{
					_password = value;
					OnPropertyChanged(nameof(Password));
				}
			}

		}
		public string Email
		{

			get => _email;
			set
			{
				if (_email != value)
				{
					_email = value;
					OnPropertyChanged(nameof(Email));
				}
			}

		}

		public string RegisteredUsername
		{

			get => _registeredUsername;
			set
			{
				if (_registeredUsername != value)
				{
					_registeredUsername = value;
					OnPropertyChanged(nameof(RegisteredUsername));
				}
			}

		}
		public string RegisteredPassword
		{

			get => _registeredPassword;
			set
			{
				if (_registeredPassword != value)
				{
					_registeredPassword = value;
					OnPropertyChanged(nameof(RegisteredPassword));
				}
			}

		}
		public string RegisteredEmail
		{

			get => _registeredEmail;
			set
			{
				if (_registeredEmail != value)
				{
					_registeredEmail = value;
					OnPropertyChanged(nameof(RegisteredEmail));
				}
			}

		}
		private ICommand _logInCommand;
		public ICommand RegisterUserCommand { get; private set; }
		public ICommand LogInUserCommand => _logInCommand = new Command(LogInUser);
		public AuthViewModel(Page page)
        {
			Page = page;	
			RegisterUserCommand = new Command(RegisterUser);
			User = new User("admin", "123", "a@a");
        }

		private async void RegisterUser()
		{
			if (Username == "" || Password == "" || Email == "")
			{
				Debug.WriteLine("Invalid credentials!");
				return;
			}

			User = new User(RegisteredUsername, RegisteredPassword, RegisteredEmail);
			//await Page.DisplayAlert("Success!", $"User Info:\nUsername: {User.Username}\nEmail:{User.Email}\nPassword:{User.Password}", "!skibidi", FlowDirection.LeftToRight);
			bool answer = await Page.DisplayAlert("Confirm", "Are you sure?", "Ok", "Cancel");
			if(answer)
			{
				await Shell.Current.GoToAsync($"//{nameof(LoginView)}?{nameof(RegisteredUsername)}={RegisteredUsername}&{nameof(RegisteredPassword)}={RegisteredPassword}&{nameof(RegisteredEmail)}={RegisteredEmail}");

			}

		}

		private async void LogInUser()
		{
			if (Username == "" || Password == "")
			{
				Debug.WriteLine("Input must not be empty!");
				return;
			}

			if (Username != RegisteredUsername || Password != RegisteredPassword)
			{
				Debug.WriteLine("Invalid information");
				return;
			}


			await Page.DisplayAlert("Success!", "nice one brother", "!skibidi", FlowDirection.LeftToRight);

		}


		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}
