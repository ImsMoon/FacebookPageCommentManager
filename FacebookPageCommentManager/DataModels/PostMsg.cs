using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPageCommentManager.DataModels
{
    public class PostMsg
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("postid")]
        public string PostId { get; set; }
        [BsonElement("message")]
        public string Message { get; set; }
    }
}
