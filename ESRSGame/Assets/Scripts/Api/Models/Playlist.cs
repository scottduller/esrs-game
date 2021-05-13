using System;

namespace Api.Models
{
    [Serializable]
    public class Playlist
    {
        public string _id;
        public string name;
        // level ids!!
        public string[] levels;
        public string user;
        public string createdAt;
        public string updatedAt;

        public Playlist(string name, string[] levels)
        {
            this.name = name;
            this.levels = levels;
        }

        public Playlist(string id)
        {
            _id = id;
        }

        public Playlist(string id, string name, string[] levels, string user, string createdAt, string updatedAt)
        {
            _id = id;
            this.name = name;
            this.levels = levels;
            this.user = user;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
        
        public override string ToString()
        {
            return $"_id: {_id} \n" +
                   $"Name: {name} \n" +
                   $"Levels: {levels} \n" +
                   $"User ID: {user} \n" +
                   $"Created At: {createdAt} \n" +
                   $"Updated At: {updatedAt}";
        }
    }
}