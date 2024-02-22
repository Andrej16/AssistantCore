using AssistantCore.DataSources;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace AssistantCore.Serialization
{
    public class SerializeAnDeserialize
    {
        public static int TestXmlSerialize()
        {
            // Creates a new TestSimpleObject object.
            ObjectForSerialization obj = new ObjectForSerialization();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            // Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open("data.xml", FileMode.Create);
            XmlSerializer ser = new XmlSerializer(typeof(ObjectForSerialization));

            ser.Serialize(stream, obj);
            stream.Close();

            // Empties obj.
            obj = null;

            // Opens file "data.xml" and deserializes the object from it.
            stream = File.Open("data.xml", FileMode.Open);
            ser = new XmlSerializer(typeof(ObjectForSerialization));

            obj = (ObjectForSerialization)ser.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();

            return 1;
        }

        public static int TestBinarySerialize()
        {
            // Creates a new TestSimpleObject object.
            ObjectForSerialization obj = new ObjectForSerialization();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            // Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open("data.bin", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            // Empties obj.
            obj = null;

            // Opens file "data.xml" and deserializes the object from it.
            stream = File.Open("data.bin", FileMode.Open);
            formatter = new BinaryFormatter();

            obj = (ObjectForSerialization)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();

            return 1;
        }
    }
}