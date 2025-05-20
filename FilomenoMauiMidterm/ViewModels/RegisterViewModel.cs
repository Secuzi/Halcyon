using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;
using FilomenoMauiMidterm.Views;

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
        [ObservableProperty]
        private string _firstName;
        [Required]
        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private string _errors;

        [ObservableProperty]
        private bool _isUserNameTaken;

        private JsonSerializerOptions _serializerOptions;
        private HttpClient _client;
        private UserService _userService;
       
        public RegisterViewModel(HttpClient client, JsonSerializerOptions serializerOptions, UserService userService)
        {
            _serializerOptions = serializerOptions;
            _client = client;
            _userService = userService;
            Username = "graham";
            Password = "111111";
            FirstName = "harold";
            LastName = "vincent";
        }

        public string GetRandomImage()
        {
            var faker = new Faker();

      
            return $"https://picsum.photos/300/300?random={faker.Random.Int()}";

           
        }

        [RelayCommand]
        public async Task<bool> RegisterUser()
        {
            IsUserNameTaken = false;
            ValidateAllProperties();
            
            if (HasErrors)
            {
                Debug.WriteLine("Validation error: ");
                Errors = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
                return false ;
            }
            try
            {
                var users = await _userService.GetUsers();

                var doesUserExist = users.Any(user => user.Username == Username);
                
                if (doesUserExist)
                {
                    IsUserNameTaken=true;
                    Errors = "Username is already taken!";
                    
                    return false;

                }
                IsUserNameTaken = false;
                Errors = "";
                
                var user = new User()
                {
                    Username = Username,
                    Password = Password,
                    FirstName = FirstName,
                    LastName = LastName,
                    Avatar = GetRandomImage()
                };


                var response = await _userService.AddUser(user);
                if (!response)
                {
                    Debug.WriteLine("Invalid");
                }
             return true ;
            }
            catch (Exception e)
            {

                throw new InvalidDataException(e.Message);
            }

        }


    }
}
