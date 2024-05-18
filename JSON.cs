using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using OpenTK;


namespace ProGrafica
{
    internal class JSON
    {
        public static void Save(string path, object objectType)
        {
            string output = JsonConvert.SerializeObject(objectType, new MatrizConversor());
            File.WriteAllText(path, output);
        }
        public static T Load<T>(string path)
        {
            string output = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(output);
        }


    }

    class MatrizConversor : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Matrix4 matriz = new Matrix4();
            return objectType == matriz.GetType();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, float> matrizCargada = new Dictionary<string, float>();
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new JsonException();
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    return new Matrix4(
                        matrizCargada["M11"], matrizCargada["M12"], matrizCargada["M13"], matrizCargada["M14"],
                        matrizCargada["M21"], matrizCargada["M22"], matrizCargada["M23"], matrizCargada["M24"],
                        matrizCargada["M31"], matrizCargada["M32"], matrizCargada["M33"], matrizCargada["M34"],
                        matrizCargada["M41"], matrizCargada["M42"], matrizCargada["M43"], matrizCargada["M44"]);
                }
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propiedad = (string)reader.Value;
                    reader.Read();
                    if (reader.TokenType == JsonToken.Float)
                    {
                        matrizCargada.Add(propiedad, (float)reader.Value);
                    }
                }

            }
            throw new JsonException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Matrix4 matriz = (Matrix4)value;
            writer.WriteStartObject();
            writer.WritePropertyName("M11");
            writer.WriteValue(matriz.M11);
            writer.WritePropertyName("M12");
            writer.WriteValue(matriz.M12);
            writer.WritePropertyName("M13");
            writer.WriteValue(matriz.M13);
            writer.WritePropertyName("M14");
            writer.WriteValue(matriz.M14);
            writer.WritePropertyName("M21");
            writer.WriteValue(matriz.M21);
            writer.WritePropertyName("M22");
            writer.WriteValue(matriz.M22);
            writer.WritePropertyName("M23");
            writer.WriteValue(matriz.M23);
            writer.WritePropertyName("M24");
            writer.WriteValue(matriz.M24);
            writer.WritePropertyName("M31");
            writer.WriteValue(matriz.M31);
            writer.WritePropertyName("M32");
            writer.WriteValue(matriz.M32);
            writer.WritePropertyName("M33");
            writer.WriteValue(matriz.M33);
            writer.WritePropertyName("M34");
            writer.WriteValue(matriz.M34);
            writer.WritePropertyName("M41");
            writer.WriteValue(matriz.M41);
            writer.WritePropertyName("M42");
            writer.WriteValue(matriz.M42);
            writer.WritePropertyName("M43");
            writer.WriteValue(matriz.M43);
            writer.WritePropertyName("M44");
            writer.WriteValue(matriz.M44);
            writer.WriteEndObject();
        }
    }
}
