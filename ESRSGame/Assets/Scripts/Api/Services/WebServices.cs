using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Api.Services
{
    public class WebServices : MonoBehaviour
    {
        public static string BaseUri = "http://esrs-server.eba-7ey4i4zp.eu-west-2.elasticbeanstalk.com/api";
        
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
            UnityWebRequest request = new UnityWebRequest(BuildUrl(path), "GET");
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
            UnityWebRequest request = new UnityWebRequest(BuildUrl(path), "POST");
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
            UnityWebRequest request = new UnityWebRequest(BuildUrl(path), "PUT");
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
            UnityWebRequest request = new UnityWebRequest(BuildUrl(path), "DELETE");
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