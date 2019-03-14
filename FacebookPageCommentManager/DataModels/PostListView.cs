using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPageCommentManager.DataModels
{
   public class PostListView
    {
        public string postid { get; set; }
        public string pagename { get; set; }
        public string postname { get; set; }
        public string replies { get; set; }
        public string fromname { get; set; }
        public DateTime createdtime { get; set; }
        public string repliescount { get; set; }
        public string ishidden { get; set; }
        public string inbox { get; set; }
        public string postlink { get; set; }
        public string canreply { get; set; }
        public bool HaveAttachment { get; set; }
        public string fromid { get; set; }
    }
}
