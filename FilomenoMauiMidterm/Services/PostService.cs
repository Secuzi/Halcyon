using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using FilomenoMauiMidterm.Models;
using System.Text.Json;


namespace FilomenoMauiMidterm.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient; //calling mockapi
        private readonly string baseURL = "https://681ebcd2c1c291fa6634fa21.mockapi.io/v1/post"; //endpoijt
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public PostService(HttpClient httpClient, JsonSerializerOptions serializerOptions)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = serializerOptions;
        }


        #region//homeviewmodel and searchviewmodel
        public async Task<List<Post>> GetPost(string postID)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync(baseURL);//get that files
                var posts = await JsonSerializer.DeserializeAsync<List<Post>>(response, _jsonSerializerOptions); //translator/converter
                return posts ?? new List<Post>();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Invalid", "An unexpected error occurred: " + ex.Message, "OK");
                return new List<Post>();
            }
        }
        #endregion
        #region //addpostviewmodel
        public async Task<Post> AddPost(Post post)
        {
            try
            {
                var json = JsonSerializer.Serialize(post, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(baseURL, content); //whole endpoint/post retrieber//requester

                // unsuccesfull response
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = $"Failed to create post. Status Code: {response.StatusCode}";
                    throw new Exception(errorMessage);
                }

                var responseStream = await response.Content.ReadAsStreamAsync();//slowly but surely retrieve files from mockapi

                // convert field from the mockapi
                var createdPost = await JsonSerializer.DeserializeAsync<Post>(responseStream, _jsonSerializerOptions);
                if (createdPost == null)
                {
                    throw new JsonException("Failed to retriever the created post.");
                }

                return createdPost;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Invalid", "An unexpected error occurred: " + ex.Message, "OK");
                return null;
            }
        }
        #endregion
        #region//showpagebiewmodel
        public async Task<Post> GetPostID(string postID)
        {
            try
            {
                postID = postID.Trim();
                if (string.IsNullOrEmpty(postID))
                {
                    await Shell.Current.DisplayAlert("Error", "No Post Available", "OK");
                    return null;
                }

                var url = $"{baseURL}/{postID}"; //retrieving it from the mockapi and specifically llok for the field name
                var response = await _httpClient.GetStreamAsync(url);//waiting....
                var post = await JsonSerializer.DeserializeAsync<Post>(response, _jsonSerializerOptions); //converter
                return post ?? throw new InvalidOperationException("Failed to load post.");//post or not(?)
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }
        #endregion
        #region   //editpageviewmodel
        public async Task<Post> UpdatePost(string postID, Post updatedPost)
        {
            try
            {
                var url = $"{baseURL}/{postID}";//getting information specifixally from the postID
                var json = JsonSerializer.Serialize(updatedPost, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, content); //updates the mockapi fields/faker.js
                if (!response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to update post.", "OK");
                    return null;
                }

                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Post>(responseStream, _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }
        #endregion
        #region//deletepost
        public async Task<bool> DeletePost(string postID)
        {
            //option to delete - added geature lawl
            bool confirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
            if (!confirm)
                return false;


            try
            {
                var url = $"{baseURL}/{postID}";//gets the dets from the mockapi if delete or ney
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Success", "Deleted successfully.", "OK");
                    return true;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to delete post.", "OK", "Cancel");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                return false;
            }
        }
        #endregion
    }

}
