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
using FilomenoMauiMidterm.Views;

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

        LoggedUser _loggedUser;

        [ObservableProperty]
        bool _canUserLogout;

        LoginViewModel _loginViewModel;

        RegisterViewModel _registerViewModel;

        public ProfileViewModel(PostService postService, UserService userService, NavigationId navigationId, LoggedUser loggedUser, LoginViewModel loginViewModel, RegisterViewModel registerViewModel )
        {
            _postService = postService;
            _userService = userService;
            //Static for now
            UserId = navigationId.Id;
            _loggedUser = loggedUser;
            CanUserLogout = _loggedUser.User.Id == UserId;
            _loginViewModel = loginViewModel;
            _registerViewModel = registerViewModel;
        }
        [RelayCommand]
        private async Task BackToHome()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async Task Logout()
        {
            _loggedUser.User = new User();
            Application.Current.MainPage = new NavigationPage(new LoginView(_loginViewModel, _registerViewModel));
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
