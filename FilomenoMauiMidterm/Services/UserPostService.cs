using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilomenoMauiMidterm.Models;

namespace FilomenoMauiMidterm.Services
{
    public class UserPostService
    {

        public UserPostService() { }

        public static Task<ObservableCollection<UserPost>> GetUserPosts(List<User> users, List<Post> posts, User currentUser)
        {
            return Task.Run(() =>
            {
                var joined = from user in users
                             join post in posts on user.Id equals post.UserId.Split(' ')[1]
                             select new UserPost
                             {
                                 UserId = user.Id,
                                 PostId = post.PostId,
                                 Avatar = user.Avatar,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Description = post.Description,
                                 Username = user.Username,
                                 Image = post.Image,
                                 //Idea himo ug array of userid tapos check if current user is naa sa list
                                 //If naa then isLiked is true
                                 //To get the number of likes kay ang length ra sa array
                                 Likes = post.LikedByUsers.Count,
                                 IsLiked = post.LikedByUsers.Any(userId => userId == "userId " + currentUser.Id),
                             };

                return new ObservableCollection<UserPost>(joined);
            });

        }
    }
}
