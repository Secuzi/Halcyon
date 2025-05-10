using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Models;
using Microsoft.Maui.Layouts;

namespace FilomenoMauiMidterm.Services
{
    public class UserService
    {
        
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        public UserService(HttpClient client, JsonSerializerOptions serializerOptions)
        {
            _client = client;

            _serializerOptions = serializerOptions;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                var url = $"{_client.BaseAddress}/user";
                string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
                StringContent content = new(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, content);
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API error: {response.StatusCode} - {errorContent}");


            }
            catch (Exception e)
            {
              
                throw new InvalidDataException(e.Message);
            }


        }

        public async Task<User> GetUser (string id)
        {
            try
            {
                id = id.Trim();
                if (string.IsNullOrEmpty(id))
                {
                    await Shell.Current.DisplayAlert("Cannot be empty", "Input cannot be empty!", "Ok");
                    return null;
                }

                if (!int.TryParse(id, out int parsedInt) || parsedInt <= 0)
                {
                    await Shell.Current.DisplayAlert("Invalid operator", "Input must be a number and must be greater than 1", "Ok");
                    return null;
                }

                var url = $"{_client.BaseAddress}/user/{id}";
                var response = await _client.GetStreamAsync(url);
                var user = await JsonSerializer.DeserializeAsync<User>(response, _serializerOptions) ?? throw new InvalidOperationException("Failed to deserialize user");

                return user;
            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }



        }

    }
}
