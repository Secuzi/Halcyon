using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Context;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;
using FilomenoMauiMidterm.Views;
using FilomenoMauiMidterm.Views.Tabs;

namespace FilomenoMauiMidterm.ViewModels
{
    public partial class HomeViewModel: ObservableObject
    {
        HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;
        UserService _userService;
        PostService _postService;
        
        [ObservableProperty]
        ObservableCollection<UserPost> _userPosts;
        [ObservableProperty]
        string _isLiked;

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _isNotBusy;
        [ObservableProperty]
        UserPost _selectedPost;

        

        [ObservableProperty]
        bool _isDeleteModalEnabled;
        //[ObservableProperty]
        //string _fullName;
        CancellationToken _cancellationToken;

        [ObservableProperty]
        LoggedUser _loggedUserProp;

        [ObservableProperty]
        bool _isPostFromUser;

        NavigationId _navigationId;

        SelectedPost _globalSelectedPost;
        public HomeViewModel(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions, PostService postService, UserService userService, LoggedUser loggedUser, NavigationId navigationId, SelectedPost selectedPost, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClient;
            _serializerOptions = jsonSerializerOptions;
            _postService = postService;
            _userService = userService;
            _cancellationToken = cancellationToken;
            IsDeleteModalEnabled = false;
            LoggedUserProp = loggedUser;
            _navigationId  = navigationId;
            
            _globalSelectedPost = selectedPost;
        }

        [RelayCommand]
        private void ShowPostOptions(UserPost post)
        {
            var page = (Shell.Current.CurrentPage as HomeView);
           
            SelectedPost = post;

            if (SelectedPost.UserId == LoggedUserProp.User.Id)
            {
                IsPostFromUser = true;
            }
            else
            {
                IsPostFromUser = false;
            }
            page?.FindByName<Syncfusion.Maui.Toolkit.BottomSheet.SfBottomSheet>("PostOptionsBottomSheet")?.Show();
        }
        [RelayCommand]
        private async void DeletePost()
        {
            var postToRemove = UserPosts.FirstOrDefault(_selectedPost => _selectedPost.PostId == SelectedPost.PostId);
            if (postToRemove != null)
            {
                UserPosts.Remove(postToRemove);
            }
            await _postService.DeletePost(SelectedPost.PostId, _cancellationToken);
            SelectedPost = null;
            IsDeleteModalEnabled = false;
            HidePostOptions();
        }
        [RelayCommand]
        private void OpenDeleteModal()
         {
            IsDeleteModalEnabled = true;
        

        }
        [RelayCommand]
        private void CloseDeleteModal()
        {
            IsDeleteModalEnabled = false;
            if (!IsDeleteModalEnabled) SelectedPost = null;
        }

        [RelayCommand]
        private void HidePostOptions()
        {
            var page = (Shell.Current.CurrentPage as HomeView);
            page?.FindByName<Syncfusion.Maui.Toolkit.BottomSheet.SfBottomSheet>("PostOptionsBottomSheet")?.Close();
        }

        [RelayCommand]
        private async Task EditPost()
        {
            _globalSelectedPost.SelectedUserPost = SelectedPost;
            await Shell.Current.GoToAsync($"edit");

        }

        public async Task LoadDataAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            if (IsBusy) return;
            IsBusy = true;
            IsNotBusy = false;
            try
            {
                _cancellationToken.ThrowIfCancellationRequested();
                var postsTask = _postService.GetPosts(_cancellationToken);
                var usersTask = _userService.GetUsers(_cancellationToken);
                await Task.WhenAll(postsTask, usersTask);
                var posts = await postsTask;
                var users = await usersTask;
                var userPosts = await UserPostService.GetUserPosts(users, posts, LoggedUserProp.User);

                UserPosts = userPosts;
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine($"Canclled task");
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




        [RelayCommand]
        private async Task GotoProfileView(string id)
        {
            _navigationId.Id = id;
            await Shell.Current.GoToAsync("profile");
        }


        [RelayCommand]
        public async Task LikeTapped(UserPost selectedItem)
        {
            selectedItem.IsLiked = !selectedItem.IsLiked;
            var post = await _postService.GetPost(selectedItem.PostId, _cancellationToken);
            var newPost = new Post()
            {
                Description = selectedItem.Description,
                UserId = $"userId {selectedItem.UserId}",
                Image = selectedItem.Image,
                PostId = selectedItem.PostId,
                LikedByUsers = post.LikedByUsers
            };


            if (selectedItem.IsLiked)
            {
                //Change to userId
                var userId = $"userId {LoggedUserProp.User.Id}";
                if(post.LikedByUsers.Contains(userId))
                {
                    return;
                }
                selectedItem.Likes++;
                newPost.LikedByUsers.Add($"userId {LoggedUserProp.User.Id}");
            }
            else
            {
                newPost.LikedByUsers.Remove($"userId {LoggedUserProp.User.Id}");
                if(selectedItem.Likes > 0)
                {

                    selectedItem.Likes--;
                }
                
            }



            await _postService.UpdatePost(newPost, _cancellationToken);
            //    Debug.WriteLine($"Liked post: {selectedItem.IsLiked}"); // Example: Accessing properties of the liked post
            //                                                    // Your logic to update the like status for this specific post
        }

    }
}
