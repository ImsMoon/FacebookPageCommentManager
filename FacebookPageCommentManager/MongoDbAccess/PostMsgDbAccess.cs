using FacebookPageCommentManager.DataModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPageCommentManager.MongoDbAccess
{
    static class PostMsgDbAccess
    {
        static MongoClient mClient = new MongoClient();
        static IMongoDatabase mDb = mClient.GetDatabase("fbCommentMsg");
        static IMongoCollection<PostMsg> mCollection = mDb.GetCollection<PostMsg>("PostMsg");
        private static List<PostMsg> postlist;

        public static void Create(PostMsg pm)
        {
            mCollection.InsertOne(pm);
        }

        public static void Update(PostMsg pm)
        {
            var update = Builders<PostMsg>.Update.Set("message", pm.Message);
            mCollection.UpdateOne(s => s.PostId == pm.PostId, update);
        }

        public static List<PostMsg> GetMsg()
        {
            return  postlist = mCollection.AsQueryable().ToList<PostMsg>();
        }

    }
}
