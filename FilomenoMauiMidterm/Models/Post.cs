using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilomenoMauiMidterm.Models
{
    public class Post
    {
        public int PostID {  get; set; } // uniqie id
        public string Description {  get; set; } // post desciption
        public int Likes {  get; set; } // like counts per post

        public string Image {  get; set; } // string since in the mockapi website string bases


    }
}
