using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPageCommentManager.DataModels
{
   public class CommentsScrapperView
    {
        public string postid { get; set; }
        public string postname { get; set; }
        public string message { get; set; }
        public string fromname { get; set; }
        public string fromid { get; set; }
        public string createdtime { get; set; }
        public string replies { get; set; }
        public string likes { get; set; }
        public string islike { get; set; }
        public string ishidden { get; set; }
        public string inbox { get; set; }
        public string canreply { get; set; }
        public string postlink { get; set; }

        public DateTime testDate { get; set; }
    }
}
