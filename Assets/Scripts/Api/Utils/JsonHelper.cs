using System;
using UnityEngine;

namespace Api.Utils
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string fixedJson = FixJson(json);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(fixedJson);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        private static string FixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
        
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}