using System;
using System.Reflection;
using System.Text;
//Парсинг атрибутов api запроса в валидную форму
namespace ShikimoriMe
{
    static class ApiParser<T> where T : ApiRequestsBase
    {
        public static string ParseApi(T settings)
        {
            StringBuilder _params = new StringBuilder();
            Type type = settings.GetType();
            FieldInfo[] properties = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo property in properties)
            {
                object value = property.GetValue(settings);
                if (value != null)
                {
                    if (_params.Length > 0)
                        _params.Append("&");
                    _params.Append($"{property.Name}={value}");
                }
            }
            return _params.ToString();
        }
    }
}