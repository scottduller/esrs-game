using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Api
{
    public class WebServices : MonoBehaviour
    {
        public static string BaseUri = "http://localhost:5000/api";
        
        public static string AuthString
        {
            get => PlayerPrefs.GetString("Authorization");
            set => PlayerPrefs.SetString("Authorization", $"Bearer {value}");
        }
        
        public static string BuildUrl(string path)
        {
            return Path.Combine(BaseUri, path).Replace(Path.DirectorySeparatorChar, '/');
        }
        
        public static UnityWebRequest Get(string path)
        {
            var request = new UnityWebRequest(BuildUrl(path), "GET");
            if (!string.IsNullOrEmpty(AuthString))
            {
                request.SetRequestHeader("Authorization", AuthString);
            }
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
        
        public static UnityWebRequest Post(string path, string jsonString)
        {
            var request = new UnityWebRequest(BuildUrl(path), "POST");
            if (!string.IsNullOrEmpty(AuthString))
            {
                request.SetRequestHeader("Authorization", AuthString);
            }
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
        
        public static UnityWebRequest Put(string path, string jsonString)
        {
            var request = new UnityWebRequest(BuildUrl(path), "PUT");
            if (!string.IsNullOrEmpty(AuthString))
            {
                request.SetRequestHeader("Authorization", AuthString);
            }
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
        
        public static UnityWebRequest Delete(string path)
        {
            var request = new UnityWebRequest(BuildUrl(path), "DELETE");
            if (!string.IsNullOrEmpty(AuthString))
            {
                request.SetRequestHeader("Authorization", AuthString);
            }
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
    }
}