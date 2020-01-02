using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Utility {
    [Serializable]
	public class JsonInfo {
        /// <summary>
        /// Utility method to get the right String  representation of these objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetString(object obj) {
            string result;
            if(obj == null) {
                result = "null";
            } else {
                var fieldValues = obj.GetType()
                                     .GetFields()
                                     .Select(field => field.GetValue(obj))
                                     .ToList();

                var fieldNames = obj.GetType().GetFields()
                                    .Select(field => field.Name)
                                    .ToList();

                StringBuilder builder = new StringBuilder();

                for(int i = 0; i < fieldValues.Count; i++) {
                    object value = fieldValues[i];
                    string name = fieldNames[i];

                    string valueString;
                    if(value == null) {
                        valueString = "null";
                    } else {
                        if(value.GetType().IsArray) {
                            StringBuilder sb = new StringBuilder();
	
                            sb.Append("[");
                            if(value is IEnumerable values) {
                                foreach(object v in values) {
                                    sb.Append($"{v}, ");
                                }
                            }
	
                            sb.Remove(sb.Length - 2, 2);
                            sb.Append("]");
                            valueString = sb.ToString();
                        } else {
                            valueString = value.ToString();
                        }
                    }

                    builder.Append($"{name}={valueString} ");
                }

                builder.Remove(builder.Length - 1, 1);
                result = builder.ToString();
            }

            return result;
        }

        public override string ToString() {
            return $"[{GetString(this)}]";
        }
	}
}
