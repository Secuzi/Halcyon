using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;

namespace FilomenoMauiMidterm.ViewModels
{
    public partial class RegisterViewModel : ObservableValidator
    {
        [Required]
        [ObservableProperty]
        private string _username;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long!")]
        [ObservableProperty]
        private string _password;

        [Required]
        [EmailAddress]
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _errors;


        private JsonSerializerOptions _serializerOptions;
        private HttpClient _client;
        private UserService _userService;
       
        public RegisterViewModel(HttpClient client, JsonSerializerOptions serializerOptions, UserService userService)
        {
            _serializerOptions = serializerOptions;
            _client = client;
            _userService = userService;
            Username = "";
            Password = "";
            Email = "";
        }
        
        [RelayCommand]
        public async Task RegisterUser()
        {
            ValidateAllProperties();
            
            if (HasErrors)
            {
                Debug.WriteLine("Validation error: ");
                Errors = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
                return;
            }
            try
            {
                var user = new User(Username, Password, Email);
                var response = await _userService.AddUser(user);
                if (!response)
                {
                    Debug.WriteLine("Invalid");
                }
            }
            catch (Exception e)
            {

                throw new InvalidDataException(e.Message);
            }

        }


    }
}
