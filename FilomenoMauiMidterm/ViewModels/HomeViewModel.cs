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
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;

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

        //[ObservableProperty]
        //string _fullName;
        CancellationToken _cancellationToken;

        string TestUserId = "1";
        public HomeViewModel(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions, PostService postService, UserService userService, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClient;
            _serializerOptions = jsonSerializerOptions;
            _postService = postService;
            _userService = userService;
            _cancellationToken = cancellationToken;
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
                var userPosts = await UserPostService.GetUserPosts(users, posts);
                
                UserPosts = userPosts;
            }
            catch(OperationCanceledException)
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
        public async Task LikeTapped(UserPost selectedItem)
        {
            selectedItem.IsLiked = !selectedItem.IsLiked;
            var post = await _postService.GetPost(selectedItem.PostId, _cancellationToken);
            var newPost = new Post()
            {
                Description = selectedItem.Description,
                UserId = selectedItem.UserId,
                Image = selectedItem.Image,
                PostID = selectedItem.PostId,
            
            };


            if (selectedItem.IsLiked)
            {
                selectedItem.Likes++;
            }
            else
            {
                selectedItem.Likes--;
            }
            
            //await _postService.UpdatePost(newPost, _cancellationToken);
            //    Debug.WriteLine($"Liked post: {selectedItem.IsLiked}"); // Example: Accessing properties of the liked post
            //                                                    // Your logic to update the like status for this specific post
        }

    }
}
