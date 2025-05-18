using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Context;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;

namespace FilomenoMauiMidterm.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        User _user;
        PostService _postService;
        UserService _userService;
        [ObservableProperty]
        List<Post> _posts;

        [ObservableProperty]
        string _userId;

        [ObservableProperty]
        string _fullName;
        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _isNotBusy;

        [ObservableProperty]
        int _numberOfPosts;

        public ProfileViewModel(PostService postService, UserService userService, NavigationId navigationId )
        {
            _postService = postService;
            _userService = userService;
            //Static for now
            UserId = navigationId.Id;


        }
        [RelayCommand]
        private async Task BackToHome()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            IsNotBusy = false;
            try
            {
                var posts = await _postService.GetPosts();
                User = await _userService.GetUser(UserId);
                FullName = $"{User?.FirstName} {User?.LastName}";
                Posts = PostService.GetPostsbyUser(posts, UserId);
                NumberOfPosts = Posts.Count;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                IsNotBusy = true;
            }
        }




    }
}
