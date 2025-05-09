using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FilomenoMauiMidterm.ViewModels
{

    public class LoginViewModel : ObservableValidator
    {
        private string _username;

        private string _password;
        [Required]
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, true);
        }
        [Required]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public LoginViewModel()
        {
            
        }

        async void Register()
        {

        }






    }
}
