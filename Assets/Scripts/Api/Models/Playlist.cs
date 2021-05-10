using System;

namespace Models
{
    [Serializable]
    public class Playlist
    {
        public string _id;
        public string name;
        public Level[] levels;
        public User user;
        public DateTime createdAt;
        public DateTime updatedAt;

        public Playlist(string name, Level[] levels)
        {
            this.name = name;
            this.levels = levels;
        }

        public Playlist(string id, string name, Level[] levels, User user, DateTime createdAt, DateTime updatedAt)
        {
            _id = id;
            this.name = name;
            this.levels = levels;
            this.user = user;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
    }
}