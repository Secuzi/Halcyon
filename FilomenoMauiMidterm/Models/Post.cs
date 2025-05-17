using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilomenoMauiMidterm.Models
{
    public class Post
    {

        public string UserId { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public List<string> LikedByUsers { get; set; }

        //public int Likes { get; set; }

        public string PostId { get; set; }

        public bool IsLiked { get; set; }

    }
}
