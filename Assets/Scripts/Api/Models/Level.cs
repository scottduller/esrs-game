using System;

namespace Models
{
    [Serializable]
    public class Level
    {
        public string _id;
        public string name;
        public string description;
        public int votes;
        public string levelData;
        public User user;
        public DateTime createdAt;
        public DateTime updatedAt;

        public Level(string name, string description, int votes, string levelData)
        {
            this.name = name;
            this.description = description;
            this.votes = votes;
            this.levelData = levelData;
        }

        public Level(string id, string name, string description, int votes, string levelData, User user, DateTime createdAt, DateTime updatedAt)
        {
            _id = id;
            this.name = name;
            this.description = description;
            this.votes = votes;
            this.levelData = levelData;
            this.user = user;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
    }
}
