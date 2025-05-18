using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Context;
using FilomenoMauiMidterm.Services;
using Imagekit;

namespace FilomenoMauiMidterm.ViewModels
{
    public partial class PostViewModel:ObservableObject
    {
        [ObservableProperty]
        string _postDescription;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasImage))]
        string _postImage;

        ImageKitService _imageKitService;
        PostService _postService;
        [ObservableProperty]
        LoggedUser _loggedUserProp;

        [ObservableProperty]
        private bool _isImageLoading;
        
        public bool HasImage => !string.IsNullOrEmpty(PostImage);

        public PostViewModel(ImageKitService imagekit, PostService postService, LoggedUser loggedUser)
        {
            _imageKitService = imagekit;
            _postService = postService;
            LoggedUserProp = loggedUser;

            //LoggedUserProp.User = new Models.User()
            //{
            //    Id = "1",
            //    Avatar = "https://cdn.jsdelivr.net/gh/faker-js/assets-person-portrait/male/512/10.jpg",
            //    FirstName = "Dena",
            //    LastName = "Weimann"
            //};



        }
        [RelayCommand]
        private async Task PickPhoto()
        {
            try
            {
                var fileResult = await MediaPicker.Default.PickPhotoAsync();
                if (fileResult == null) return;
                IsImageLoading = true;

                await using var stream = await fileResult.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var imageUrl = await _imageKitService.UploadImageAsync(
                    memoryStream.ToArray(),
                    fileResult.FileName);
                PostImage = imageUrl;
                IsImageLoading = false;
            }
            catch (Exception ex)
            {
                //Errror
            }
        }
        [RelayCommand]
        private void ClearImage()
        {
            PostImage = string.Empty;
        }

        [RelayCommand]
        private async Task CreatePost()
        {
            try
            {
                if (string.IsNullOrEmpty(PostDescription))
                {
                    await Shell.Current.DisplayAlert("Wait lang", "Please input a text", "Okay", "No");


                    return;
                }
                if (string.IsNullOrEmpty(PostImage))
                {
                    await Shell.Current.DisplayAlert("Wait lang", "Waitg for the image to load", "Okay??", "Okay");


                    return;
                }
               

                await _postService.AddPost(new Models.Post()
                {
                    Description = PostDescription,
                    Image = PostImage,
                    UserId = "userId " + LoggedUserProp.User.Id,
                    LikedByUsers = new(),
                    IsLiked = false,
                    Likes = 0
                    
                });

                await Shell.Current.GoToAsync("//home");

            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

    }
}
