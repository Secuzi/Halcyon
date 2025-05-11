using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


 public async Task<List<Post>> GetPost(string postID)
 {
     try
     {
         var response = await _httpClient.GetStreamAsync(baseURL);
         var posts = await JsonSerializer.DeserializeAsync<List<Post>>(response, _jsonSerializerOptions); //translator/converter
         return posts ?? new List<Post>();
     }
     catch (Exception ex) 
     {
         await Shell.Current.DisplayAlert("Invalid", "An unexpected error occurred: " + ex.Message, "OK");
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

         var response = await _httpClient.PostAsync(baseURL, content);

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
    }
}
