using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftSharp
{
    /// <summary>
    /// This class parses JSON data and returns an object describing that data.
    /// Really lightweight JSON handling by ORelio - (c) 2013 - 2020
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Parse some JSON and return the corresponding JSON object
        /// </summary>
        public static JSONData ParseJson(string json)
        {
            int cursorpos = 0;
            return String2Data(json, ref cursorpos);
        }

        /// <summary>
        /// The class storing unserialized JSON data
        /// The data can be an object, an array or a string
        /// </summary>
        public class JSONData : IJSONSerializable
        {
            public enum DataType { Object, Array, String };

            public DataType Type { get; }

            public readonly Dictionary<string, JSONData> Properties;
            public readonly List<JSONData> DataArray;
            public string StringValue;

            public JSONData(DataType datatype)
            {
                Type = datatype;
                Properties = new Dictionary<string, JSONData>();
                DataArray = new List<JSONData>();
                StringValue = string.Empty;
            }

            public string ToJson() // Serialize back to json string
            {
                return Type switch
                {
                    DataType.Object => "{" + string.Join(",", Properties.Select(x => $"\"{x.Key}\":{Object2Json(x.Value)}")) + "}",
                    DataType.Array  => "[" + string.Join(",", DataArray.Select(Object2Json)) + "]",

                    _               => $"\"{StringValue}\"",
                };
            }
        }

        /// <summary>
        /// Parse a JSON string to build a JSON object
        /// </summary>
        /// <param name="toParse">String to parse</param>
        /// <param name="cursorPos">Cursor start (set to 0 for function init)</param>
        private static JSONData String2Data(string toParse, ref int cursorPos)
        {
            try
            {
                JSONData data;
                SkipSpaces(toParse, ref cursorPos);
                switch (toParse[cursorPos])
                {
                    //Object
                    case '{':
                        data = new JSONData(JSONData.DataType.Object);
                        cursorPos++;
                        SkipSpaces(toParse, ref cursorPos);
                        while (toParse[cursorPos] != '}')
                        {
                            if (toParse[cursorPos] == '"')
                            {
                                JSONData propertyname = String2Data(toParse, ref cursorPos);
                                if (toParse[cursorPos] == ':') { cursorPos++; } else { /* parse error ? */ }
                                JSONData propertyData = String2Data(toParse, ref cursorPos);
                                data.Properties[propertyname.StringValue] = propertyData;
                            }
                            else cursorPos++;
                        }
                        cursorPos++;
                        break;

                    //Array
                    case '[':
                        data = new JSONData(JSONData.DataType.Array);
                        cursorPos++;
                        SkipSpaces(toParse, ref cursorPos);
                        while (toParse[cursorPos] != ']')
                        {
                            if (toParse[cursorPos] == ',') { cursorPos++; }
                            JSONData arrayItem = String2Data(toParse, ref cursorPos);
                            data.DataArray.Add(arrayItem);
                        }
                        cursorPos++;
                        break;

                    //String
                    case '"':
                        data = new JSONData(JSONData.DataType.String);
                        cursorPos++;
                        while (toParse[cursorPos] != '"')
                        {
                            if (toParse[cursorPos] == '\\')
                            {
                                try //Unicode character \u0123
                                {
                                    if (toParse[cursorPos + 1] == 'u'
                                        && IsHex(toParse[cursorPos + 2])
                                        && IsHex(toParse[cursorPos + 3])
                                        && IsHex(toParse[cursorPos + 4])
                                        && IsHex(toParse[cursorPos + 5]))
                                    {
                                        //"abc\u0123abc" => "0123" => 0123 => Unicode char n°0123 => Add char to string
                                        data.StringValue += char.ConvertFromUtf32(int.Parse(toParse.Substring(cursorPos + 2, 4), System.Globalization.NumberStyles.HexNumber));
                                        cursorPos += 6; continue;
                                    }
                                    else if (toParse[cursorPos + 1] == 'n')
                                    {
                                        data.StringValue += '\n';
                                        cursorPos += 2;
                                        continue;
                                    }
                                    else if (toParse[cursorPos + 1] == 'r')
                                    {
                                        data.StringValue += '\r';
                                        cursorPos += 2;
                                        continue;
                                    }
                                    else if (toParse[cursorPos + 1] == 't')
                                    {
                                        data.StringValue += '\t';
                                        cursorPos += 2;
                                        continue;
                                    }
                                    else cursorPos++; //Normal character escapement \"
                                }
                                catch (IndexOutOfRangeException) { cursorPos++; } // \u01<end of string>
                                catch (ArgumentOutOfRangeException) { cursorPos++; } // Unicode index 0123 was invalid
                            }
                            data.StringValue += toParse[cursorPos];
                            cursorPos++;
                        }
                        cursorPos++;
                        break;

                    //Number
                    case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9': case '.': case '-':
                        data = new JSONData(JSONData.DataType.String);
                        StringBuilder sb = new StringBuilder();
                        // 'e' or 'E' can appear in the middle in a number as the mark for
                        // scientific notation, but they're not supposed to be at the start
                        while ((toParse[cursorPos] >= '0' && toParse[cursorPos] <= '9') || toParse[cursorPos] == 'e' || toParse[cursorPos] == 'E' ||
                                toParse[cursorPos] == '.' || toParse[cursorPos] == '-' || toParse[cursorPos] == '+')
                        {
                            sb.Append(toParse[cursorPos]);
                            cursorPos++;
                        }
                        data.StringValue = sb.ToString();
                        break;

                    //Boolean : true
                    case 't':
                        data = new JSONData(JSONData.DataType.String);
                        cursorPos++;
                        if (toParse[cursorPos] == 'r') { cursorPos++; }
                        if (toParse[cursorPos] == 'u') { cursorPos++; }
                        if (toParse[cursorPos] == 'e') { cursorPos++; data.StringValue = "true"; }
                        break;

                    //Boolean : false
                    case 'f':
                        data = new JSONData(JSONData.DataType.String);
                        cursorPos++;
                        if (toParse[cursorPos] == 'a') { cursorPos++; }
                        if (toParse[cursorPos] == 'l') { cursorPos++; }
                        if (toParse[cursorPos] == 's') { cursorPos++; }
                        if (toParse[cursorPos] == 'e') { cursorPos++; data.StringValue = "false"; }
                        break;

                    //Null field
                    case 'n':
                        data = new JSONData(JSONData.DataType.String);
                        cursorPos++;
                        if (toParse[cursorPos] == 'u') { cursorPos++; }
                        if (toParse[cursorPos] == 'l') { cursorPos++; }
                        if (toParse[cursorPos] == 'l') { cursorPos++; data.StringValue = "null"; }
                        break;

                    //Unknown data
                    default:
                        cursorPos++;
                        return String2Data(toParse, ref cursorPos);
                }
                SkipSpaces(toParse, ref cursorPos);
                return data;
            }
            catch (IndexOutOfRangeException)
            {
                return new JSONData(JSONData.DataType.String);
            }
        }

        /// <summary>
        /// Implement this interface if you want custom
        /// json serialization for your class
        /// </summary>
        public interface IJSONSerializable
        {
            public string ToJson();
        }

        private static string Dictionary2Json(Dictionary<string, object> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(x => $"\"{x.Key}\":{ Object2Json(x.Value) }") ) + "}";
        }

        private static string List2Json(List<object> list)
        {
            return "[" + string.Join(",", list.Select(Object2Json) ) + "]";
        }

        private static string Array2Json(object[] array)
        {
            return "[" + string.Join(",", array.Select(Object2Json) ) + "]";
        }

        /// <summary>
        /// Serialize an object into JSON string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        public static string Object2Json(object obj)
        {
            return obj switch
            {
                // Nested object
                Dictionary<string, object> dict => Dictionary2Json(dict),
                // Object list
                List<object> list               => List2Json(list),
                // Object array
                object[] array                  => Array2Json(array),
                // User-defined json serialization
                IJSONSerializable objValue      => objValue.ToJson(),
                // String value, wrap with quotation marks
                string strValue                 => $"\"{strValue}\"",
                // Boolean value, should be lowercase 'true' or 'false'
                bool boolValue                  => boolValue ? "true" : "false",
                // Null value, should be lowercase 'null'
                null                            => "null",
                // Other types, just convert to string
                _                               => $"\"{obj}\""
            };
        }
        
        private static JSONData Dictionary2JSONData(Dictionary<string, object> dict)
        {
            var jsonData = new JSONData(JSONData.DataType.Object);
    
            foreach (var kvp in dict)
            {
                jsonData.Properties[kvp.Key] = Object2JSONData(kvp.Value);
            }
    
            return jsonData;
        }

        public static JSONData Object2JSONData(object value)
        {
            if (value == null)
            {
                return new JSONData(JSONData.DataType.String) { StringValue = "null" };
            }
    
            switch (value)
            {
                case Dictionary<string, object> nestedDict:
                    return Dictionary2JSONData(nestedDict);
            
                case List<object> list:
                    var listData = new JSONData(JSONData.DataType.Array);
                    foreach (var item in list)
                    {
                        listData.DataArray.Add(Object2JSONData(item));
                    }
                    return listData;
                
                case Array array:
                    var arrayData = new JSONData(JSONData.DataType.Array);
                    foreach (var item in array)
                    {
                        arrayData.DataArray.Add(Object2JSONData(item));
                    }
                    return arrayData;
            
                case string s:
                    return new JSONData(JSONData.DataType.String) { StringValue = s };
            
                default: // Handle numbers, booleans, etc.
                    return new JSONData(JSONData.DataType.String) { StringValue = value.ToString() };
            }
        }

        /// <summary>
        /// Check if a char is an hexadecimal char (0-9 A-F a-f)
        /// </summary>
        /// <param name="c">Char to test</param>
        /// <returns>True if hexadecimal</returns>
        private static bool IsHex(char c) { return c is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f'; }

        /// <summary>
        /// Advance the cursor to skip white spaces and line breaks
        /// </summary>
        /// <param name="toParse">String to parse</param>
        /// <param name="cursorPos">Cursor position to update</param>
        private static void SkipSpaces(string toParse, ref int cursorPos)
        {
            while (cursorPos < toParse.Length
                    && (char.IsWhiteSpace(toParse[cursorPos])
                    || toParse[cursorPos] == '\r'
                    || toParse[cursorPos] == '\n'))
                cursorPos++;
        }
    }
}
