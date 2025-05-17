using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Input;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CommunityToolkit.Mvvm;
using FilomenoMauiMidterm.Context;

namespace FilomenoMauiMidterm.ViewModels
{

    public partial class LoginViewModel : ObservableValidator
    {
        [Required]
        [ObservableProperty]
        private string _username;

        [Required]
        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _errors;

        CancellationToken _cancellationToken;
        private readonly UserService _userService;
        private readonly JsonSerializerOptions _serializerOptions;
        private LoggedUser _loggedUser;
        public LoginViewModel(UserService userService, JsonSerializerOptions serializerOptions, LoggedUser loggedUser)
        {
            _userService = userService;
            _loggedUser = loggedUser;
            _serializerOptions = serializerOptions;
       
            Username = "graham";
            Password = "skibidi";
        }
        public async Task<User> ValidateUser(string username, string password)
        {
            try
            {
                var users = await _userService.GetUsers();
                var user = users.SingleOrDefault(user => user.Username == username && user.Password == password);
                return user;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


        [RelayCommand]
        public async Task LogInUser()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Errors = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
                Debug.WriteLine("Validation failed.");
                return;
            }

            try
            {
                var user = await ValidateUser(Username, Password);

                if (user != null)
                {
                    Debug.WriteLine("Login successful.");
                    _loggedUser.User = user;
                    Application.Current.MainPage = new AppShell();
                    //  Navigate to a home page
                    
                }
                else
                {
                    Debug.WriteLine("Invalid credentials.");
                  
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login error: {ex.Message}");
                
            }
        }



    }
}
