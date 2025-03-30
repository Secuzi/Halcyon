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
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace FilomenoMauiMidterm.ViewModels
{

	[QueryProperty(nameof(RegisteredEmail), nameof(RegisteredEmail))]
	[QueryProperty(nameof(RegisteredPassword), nameof(RegisteredPassword))]
	[QueryProperty(nameof(RegisteredUsername), nameof(RegisteredUsername))]
	public class AuthViewModel : INotifyPropertyChanged 
	{
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
		public AuthViewModel()
        {
			RegisterUserCommand = new Command(RegisterUser);
        }
		const byte MINIMUM_PASSWORD_LENGTH = 6;
		readonly string EMAIL_PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
		private async void RegisterUser()
		{
			
			if (String.IsNullOrEmpty(RegisteredUsername) || String.IsNullOrEmpty(RegisteredPassword) || String.IsNullOrEmpty(RegisteredEmail))
			{
				await Shell.Current.DisplayAlert("Invalid details", "All inputs must not be empty!", "Ok");
				return;
			}
			Regex emailRegex = new Regex(EMAIL_PATTERN, RegexOptions.IgnoreCase);

			if (!emailRegex.IsMatch(RegisteredEmail))
			{
				await Shell.Current.DisplayAlert("Invalid details", "Invalid email!", "Ok");
				return;
			}

			if(RegisteredPassword.Length < MINIMUM_PASSWORD_LENGTH)
			{
				await Shell.Current.DisplayAlert("Invalid details", "Password must be greater than 6 characters!", "Ok");
				return;
			}

			//await Shell.Current.DisplayAlert("Success!", $"User Info:\nUsername: {User.Username}\nEmail:{User.Email}\nPassword:{User.Password}", "!skibidi", FlowDirection.LeftToRight);
			bool answer = await Shell.Current.DisplayAlert("Confirm", "Are you sure?", "Ok", "Cancel");
			if(answer)
			{
				Username = string.Empty;
				Password = string.Empty;
				await Shell.Current.GoToAsync($"//{nameof(LoginView)}?{nameof(RegisteredUsername)}={RegisteredUsername}&{nameof(RegisteredPassword)}={RegisteredPassword}&{nameof(RegisteredEmail)}={RegisteredEmail}"); 
			}

		}

		private async void LogInUser()
		{
			if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
			{
				Debug.WriteLine("Input must not be empty!");
				await Shell.Current.DisplayAlert("Invalid details", "Input must not be empty!", "Ok");
				return;
			}

			if (Username != RegisteredUsername || Password != RegisteredPassword)
			{
				Debug.WriteLine("Invalid information");
				await Shell.Current.DisplayAlert("Invalid details", "Incorrect details", "Ok");
				return;
			}


			await Shell.Current.DisplayAlert("Success!", "nice one fellow rizzler", "skibidi", FlowDirection.LeftToRight);

		}


		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}
