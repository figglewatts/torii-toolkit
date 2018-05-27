using System;
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

        /// <summary>
        /// Get a value from JSON, returning a default value if key was not present.
        /// </summary>
        /// <typeparam name="T">Type to return, must be JSONNode or child class.</typeparam>
        /// <param name="node">The JSON node.</param>
        /// <param name="key">The key we're looking for.</param>
        /// <param name="defaultValue">The default value if no key was found.</param>
        /// <returns>The value of key, or defaultValue if key was not present.</returns>
        public static T GetValueOrDefault<T>(this JSONNode node, string key, JSONNode defaultValue) where T : JSONNode
        {
            if (node.IsArray) throw new InvalidOperationException("Cannot use GetValueOrDefault<T>() on JSONArray!");

            return node[key] != null ? (T)node[key] : (T)defaultValue;
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

        public static implicit operator JSONNode(Color color)
        {
            JSONNode node = new JSONObject();
            node["r"] = color.r;
            node["g"] = color.g;
            node["b"] = color.b;
            node["a"] = color.a;
            return node;
        }

        public static implicit operator Color(JSONNode node)
        {
            return new Color(node["r"], node["g"], node["b"], node["a"]);
        }

        public static implicit operator JSONNode(RectOffset offset)
        {
            JSONNode node = new JSONObject();
            node["top"] = offset.top;
            node["bottom"] = offset.bottom;
            node["left"] = offset.left;
            node["right"] = offset.right;
            return node;
        }

        public static implicit operator RectOffset(JSONNode node)
        {
            return new RectOffset(node["left"], node["right"], node["top"], node["bottom"]);
        }
    }

    public partial class JSONString
    {
        public static implicit operator string(JSONString str)
        {
            return str.m_Data;
        }

        public static implicit operator JSONString(string str)
        {
            return new JSONString(str);
        }
    }

    public partial class JSONBool
    {
        public static implicit operator bool(JSONBool boolean)
        {
            return boolean.m_Data;
        }

        public static implicit operator JSONBool(bool boolean)
        {
            return new JSONBool(boolean);
        }
    }

    public partial class JSONNumber
    {
        public static implicit operator int(JSONNumber number)
        {
            return (int)number.m_Data;
        }

        public static implicit operator JSONNumber(int number)
        {
            return new JSONNumber(number);
        }

        public static implicit operator double(JSONNumber number)
        {
            return number.m_Data;
        }

        public static implicit operator JSONNumber(double number)
        {
            return new JSONNumber(number);
        }

        public static implicit operator float(JSONNumber number)
        {
            return (float)number.m_Data;
        }

        public static implicit operator JSONNumber(float number)
        {
            return new JSONNumber(number);
        }
    }
}