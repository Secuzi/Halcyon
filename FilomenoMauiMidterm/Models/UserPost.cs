using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FilomenoMauiMidterm.Models
{
    public partial class UserPost: ObservableObject
    {
        [ObservableProperty]
        private string _userId;

        [ObservableProperty]
        private string _postId;

        [ObservableProperty]
        private string _avatar;

        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        
        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _username;
        
        [ObservableProperty]
        private int _likes;
        
        [ObservableProperty]
        private string _image;

        [ObservableProperty]
        private bool _isLiked;

    }
}
