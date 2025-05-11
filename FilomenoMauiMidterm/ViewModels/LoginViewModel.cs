using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

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
       


        public LoginViewModel()
        {
            
        }


    }
}
