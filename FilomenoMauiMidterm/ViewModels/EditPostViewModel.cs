using System;
using System.Collections.Generic;
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
    public partial class EditPostViewModel: ObservableObject
    {
        [ObservableProperty]
        private bool _isImageLoading;
     
        [ObservableProperty]
        private UserPost _selectedUserPostProp;
        PostService _postService;
        ImageKitService _imageKitService;
        

        public EditPostViewModel(ImageKitService imageKitService, SelectedPost selectedPost, PostService postService)
        {
            _imageKitService = imageKitService;
            SelectedUserPostProp = selectedPost.SelectedUserPost;
            _postService = postService;
        }

        [RelayCommand]
        private async Task PickPhoto()
        {
            try
            {
                var fileResult = await MediaPicker.Default.PickPhotoAsync();
                if (fileResult == null) return;
                IsImageLoading = true;
                SelectedUserPostProp.Image = string.Empty;
                await using var stream = await fileResult.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var imageUrl = await _imageKitService.UploadImageAsync(
                    memoryStream.ToArray(),
                    fileResult.FileName);
                SelectedUserPostProp.Image = imageUrl;
                IsImageLoading = false;
            }
            catch (Exception ex)
            {
                //Errror
            }
        }
        [RelayCommand]
        private async Task UpdatePost()
        {
            if (IsImageLoading) return;

            if (string.IsNullOrEmpty(SelectedUserPostProp.Description))
            {
                await Shell.Current.DisplayAlert("Invalid input", "Please input a text", "Okay");


                return;
            }
            if (string.IsNullOrEmpty(SelectedUserPostProp.Image))
            {
                await Shell.Current.DisplayAlert("Invalid input", "Wait for the image to load", "Okay");


                return;
            }

            var post = await _postService.GetPost(SelectedUserPostProp.PostId);



            await _postService.UpdatePost(new Post()
            {
                Description = SelectedUserPostProp.Description,
                Image = SelectedUserPostProp.Image,
                IsLiked = SelectedUserPostProp.IsLiked,
                PostId = SelectedUserPostProp.PostId,
                UserId = "userId " + SelectedUserPostProp.UserId,
                LikedByUsers = post.LikedByUsers,
                Likes = post.Likes
            });

            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async Task CancelUpdate()
        {
            await Shell.Current.GoToAsync("..");
        }


    }
}
