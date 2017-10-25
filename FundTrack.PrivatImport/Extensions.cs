using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FundTrack.PrivatImport
{
    public static class Extensions
    { 
        /// <summary>
        /// Make Stream from string
        /// </summary>
        private static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// parse XML string into object of T class using serialization
        /// </summary>
        public static T ParseXmlTo<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(),
                new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
            var serializer = new XmlSerializer(typeof(T));
            if (serializer.CanDeserialize(reader))
            {
                return serializer.Deserialize(reader) as T;
            }

            return new object() as T;
        }

        /// <summary>
        /// Serialize T object into XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }
}
