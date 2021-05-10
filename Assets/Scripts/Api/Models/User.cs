using System;

namespace Models
{
    [Serializable]
    public class User
    {
        public string _id;
        public string username;
        public string password;
        public string token;
        public DateTime createdAt;
        public DateTime updatedAt;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public User(string id, string username, string password, string token)
        {
            _id = id;
            this.username = username;
            this.password = password;
            this.token = token;
        }

        public User(string id, string username, string password, string token, DateTime createdAt, DateTime updatedAt)
        {
            _id = id;
            this.username = username;
            this.password = password;
            this.token = token;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
    }
}