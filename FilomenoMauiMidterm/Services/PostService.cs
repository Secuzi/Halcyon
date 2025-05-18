using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FilomenoMauiMidterm.Models;


using Microsoft.Maui.Controls;

namespace FilomenoMauiMidterm.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseURL = "https://681ebcd2c1c291fa6634fa21.mockapi.io/v1/post";
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public PostService(HttpClient httpClient, JsonSerializerOptions serializerOptions)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = serializerOptions;
        }
        public static List<Post> GetPostsbyUser(List<Post> posts, string userId)
        {
            return posts.FindAll(post => post.UserId.Split(' ')[1] == userId);
        }


        public async Task<List<Post>> GetPosts(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/post", cancellationToken);
                response.EnsureSuccessStatusCode();
                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var posts = await JsonSerializer.DeserializeAsync<List<Post>>(
                    responseStream,
                    _jsonSerializerOptions,
                    cancellationToken);

                return posts ?? new List<Post>();
            }
            catch (OperationCanceledException)
            {
                return new List<Post>();
            }
            catch (Exception ex)
            {
                //await Shell.Current.DisplayAlert("Invalid", "An unexpected error occurred: " + ex.Message, "OK");

                return new List<Post>();
            }
        }
        //addpostviewmodel
        public async Task<Post> AddPost(Post post)
        {
            try
            {
                var json = JsonSerializer.Serialize(post, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress.OriginalString}/post", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = $"Failed to create post. Status Code: {response.StatusCode}";
                    throw new Exception(errorMessage);
                }

                var responseStream = await response.Content.ReadAsStreamAsync();

                var createdPost = await JsonSerializer.DeserializeAsync<Post>(responseStream, _jsonSerializerOptions);
                if (createdPost == null)
                {
                    throw new JsonException("Failed to deserialize the created post.");
                }

                return createdPost;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Invalid", "An unexpected error occurred: " + ex.Message, "OK");
                return null;
            }
        }
        public async Task<bool> DeletePost(string postID, CancellationToken cancellationToken)
        {
            try
            {
                var url = $"{_httpClient.BaseAddress.OriginalString}/post/{postID}";//gets the dets from the mockapi if delete or ney
                var response = await _httpClient.DeleteAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                return false;
            }
        }
        public async Task<Post> GetPost(string postId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress.OriginalString}/post/{postId}");
                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var post = await JsonSerializer.DeserializeAsync<Post>(
                responseStream,
                _jsonSerializerOptions,
                cancellationToken);

                return post;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdatePost(Post postToUpdate, CancellationToken cancellationToken = default)
        {
            try
            {
                if (postToUpdate?.PostId == null)
                {
                    // Handle the case where the Post doesn't have an ID for updating
                    System.Diagnostics.Debug.WriteLine("Error: Post ID is null for update.");
                    return false;
                }

                var json = JsonSerializer.Serialize(postToUpdate, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Assuming your API endpoint for updating a post includes the Post ID
                var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/post/{postToUpdate.PostId}", content, cancellationToken);

                response.EnsureSuccessStatusCode(); // Will throw an exception for unsuccessful status codes

                // If the update was successful, you might want to read the updated Post back
                // or simply return true if the EnsureSuccessStatusCode didn't throw.
                // For this example, we'll assume a successful status code means the update worked.
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}

