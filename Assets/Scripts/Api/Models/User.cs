using System;

namespace Api.Models
{
    [Serializable]
    public class User
    {
        public string _id;
        public string username;
        public string password;
        public string token;
        public string createdAt;
        public string updatedAt;

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

        public User(string id, string username, string password, string token, string createdAt, string updatedAt)
        {
            _id = id;
            this.username = username;
            this.password = password;
            this.token = token;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
        
        public override string ToString()
        {
            return $"_id: {_id} \n" +
                   $"Username: {username} \n" +
                   $"Password: {password} \n" +
                   $"Token: {token} \n" +
                   $"Created At: {createdAt} \n" +
                   $"Updated At: {updatedAt}";
        }
    }
}