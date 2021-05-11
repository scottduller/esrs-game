using System;

namespace Api.Models
{
    [Serializable]
    public class Level
    {
        public string _id;
        public string name;
        public string description;
        public string votes;
        public string levelData;
        public string user;
        public string createdAt;
        public string updatedAt;
        
        public Level(string name, string description, string votes, string levelData)
        {
            this.name = name;
            this.description = description;
            this.votes = string.IsNullOrEmpty(votes) ? 0.ToString() : votes;
            this.levelData = levelData;
        }
        
        public Level(string id,string name, string description, string votes, string levelData)
        {
            _id = id;
            this.name = name;
            this.description = description;
            this.votes = string.IsNullOrEmpty(votes) ? 0.ToString() : votes;
            this.levelData = levelData;
        }

        public Level(string id, string name, string description, string votes, string levelData, string user, string createdAt, string updatedAt)
        {
            _id = id;
            this.name = name;
            this.description = description;
            this.votes = string.IsNullOrEmpty(votes) ? 0.ToString() : votes;
            this.levelData = levelData;
            this.user = user;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
        
        public override string ToString()
        {
            return $"_id: {_id} \n" +
                   $"Name: {name} \n" +
                   $"Description: {description} \n" +
                   $"Votes: {votes} \n" +
                   $"Level Data: {levelData} \n" +
                   $"User ID: {user} \n" +
                   $"Created At: {createdAt} \n" +
                   $"Updated At: {updatedAt}";
        }
    }
}
