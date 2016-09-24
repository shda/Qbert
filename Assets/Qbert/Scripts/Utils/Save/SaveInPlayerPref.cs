using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Qbert.Scripts.Utils.Save
{
    public class SaveInPlayerPref<T>
    {
        public static void Load()
        {
            var type = typeof(T);

            var fields = type.GetFields();

            foreach (var fieldInfo in fields)
            {
                if (IsSaveAttribute(fieldInfo))
                {
                    string nameField = fieldInfo.Name;
                    object value = fieldInfo.GetValue(null);

                    if (fieldInfo.IsLiteral)
                        continue;

                    if (value is string)
                    {
                        fieldInfo.SetValue(null, PlayerPrefs.GetString(nameField, (string)value));
                    }
                    else if (value is int)
                    {
                        fieldInfo.SetValue(null, PlayerPrefs.GetInt(nameField, (int)value));
                    }
                    else if (value is bool)
                    {
                        fieldInfo.SetValue(null, PlayerPrefs.GetInt(nameField, (bool)value ? 1 : 0) == 1);
                    }
                }
            }
        }

        public static void Save()
        {
            var type = typeof(T);
            foreach (var fieldInfo in type.GetFields())
            {
                if (IsSaveAttribute(fieldInfo))
                {
                    string nameField = fieldInfo.Name;
                    object value = fieldInfo.GetValue(null);

                    if (fieldInfo.IsLiteral)
                        continue;

                    if (value is string)
                    {
                        PlayerPrefs.SetString(nameField, (string)value);
                    }
                    else if (value is int)
                    {
                        PlayerPrefs.SetInt(nameField, (int)value);
                    }
                    else if (value is bool)
                    {
                        PlayerPrefs.SetInt(nameField, (bool)value ? 1 : 0);
                    }
                }
            }

            PlayerPrefs.Save();
        }


        public static bool IsSaveAttribute(FieldInfo fieldInfo)
        {
            var attributes = fieldInfo.GetCustomAttributes(true);
            return attributes.Any(x => x is SaveFieldAttrebute);
        }


        public static void PrintAllValues()
        {
            string result = "";
            var type = typeof(T);
            foreach (var fieldInfo in type.GetFields())
            {
                result += fieldInfo.Name + " = " + fieldInfo.GetValue(null) + "\n";
            }
            Debug.Log(result);
        }
    }
}

