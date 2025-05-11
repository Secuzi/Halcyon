using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;
using CommunityToolkit.Mvvm.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CommunityToolkit.Mvvm;


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

        private readonly UserService _userService;
        private readonly JsonSerializerOptions _serializerOptions;

        public LoginViewModel(UserService userService, JsonSerializerOptions serializerOptions)
        {
            _userService = userService;
            _serializerOptions = serializerOptions;
            Username = "";
            Password = "";
        }

        [RelayCommand]
        public async Task LoginUser()
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
                bool isValidUser = await _userService.ValidateUser(Username, Password);

                if (isValidUser)
                {
                    Debug.WriteLine("Login successful.");
                    await Shell.Current.DisplayAlert("Success", "You are logged in!", "OK");

                    // Example: Navigate to a home page
                    await Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    Debug.WriteLine("Invalid credentials.");
                    await Shell.Current.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login error: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Something went wrong during login.", "OK");
            }
        }

        

        public LoginViewModel()
        {
            
        }


    }
}
