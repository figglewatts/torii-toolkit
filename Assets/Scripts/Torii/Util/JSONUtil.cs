using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

namespace Torii.Util
{
    public static class JSONUtil
    {
        /// <summary>
        /// Reads JSON from disk into JSONNode.
        /// </summary>
        /// <param name="path">Path to the json file.</param>
        /// <returns>JSONNode parsed from the file.</returns>
        public static JSONNode ReadJSONFromDisk(string path)
        {
            string jsonString = File.ReadAllText(path);
            JSONNode json = JSON.Parse(jsonString);
            return json;
        }
    }
}

namespace SimpleJSON
{
    public partial class JSONNode
    {
        // Implicit JSON conversion methods extend existing SimpleJSON functionality

        public static implicit operator JSONNode(Rect rect)
        {
            JSONNode node = new JSONObject();
            node["x"] = rect.x;
            node["y"] = rect.y;
            node["width"] = rect.width;
            node["height"] = rect.height;
            return node;
        }

        public static implicit operator Rect(JSONNode node)
        {
            return new Rect(node["x"], node["y"], node["width"], node["height"]);
        }

        public static implicit operator JSONNode(Vector2 vec2)
        {
            JSONNode node = new JSONObject();
            node["x"] = vec2.x;
            node["y"] = vec2.y;
            return node;
        }

        public static implicit operator Vector2(JSONNode node)
        {
            return new Vector2(node["x"], node["y"]);
        }

        public static implicit operator JSONNode(Vector3 vec3)
        {
            JSONNode node = new JSONObject();
            node["x"] = vec3.x;
            node["y"] = vec3.y;
            node["z"] = vec3.z;
            return node;
        }

        public static implicit operator Vector3(JSONNode node)
        {
            return new Vector3(node["x"], node["y"], node["z"]);
        }

        public static implicit operator JSONNode(Vector4 vec4)
        {
            JSONNode node = new JSONObject();
            node["x"] = vec4.x;
            node["y"] = vec4.y;
            node["z"] = vec4.z;
            node["w"] = vec4.w;
            return node;
        }

        public static implicit operator Vector4(JSONNode node)
        {
            return new Vector4(node["x"], node["y"], node["z"], node["w"]);
        }
    }
}