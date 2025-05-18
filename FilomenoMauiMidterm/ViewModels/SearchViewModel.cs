using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilomenoMauiMidterm.Context;
using FilomenoMauiMidterm.Models;
using FilomenoMauiMidterm.Services;

namespace FilomenoMauiMidterm.ViewModels
{
    public partial class SearchViewModel:ObservableObject
    {

        UserService _userService;
        [ObservableProperty]
        ObservableCollection<User> _users;
        [ObservableProperty]
        bool _isBusy;
        [ObservableProperty]
        bool _isNotBusy;
        [ObservableProperty]
        ObservableCollection<User> _searchedUsers;
        NavigationId _navigationId;
        CancellationToken _cancellationToken;
        [ObservableProperty]
        string _searchQuery;

        public SearchViewModel(UserService userService, NavigationId navigationId)
        {
            _userService = userService;

            _navigationId = navigationId;
            SearchQuery = string.Empty;
        }
        public void FilterUsers(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                SearchedUsers = new ObservableCollection<User>();
            }
            else
            {
                var filteredUsers = Users
                    .Where(user =>
                        user.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        user.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        user.Username.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                SearchedUsers = new ObservableCollection<User>(filteredUsers);
            }
        }

        [RelayCommand]
        private async Task GotoProfileView(string id)
        {
            _navigationId.Id = id;
            await Shell.Current.GoToAsync("profile");
        }
        [RelayCommand]
        private void ClearSearch()
        {
            SearchQuery = string.Empty;
            //await Shell.Current.GoToAsync("//home");
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
            
                var users = await _userService.GetUsers(_cancellationToken);

                Users = new ObservableCollection<User>(users);
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


    }
}
