using System;

namespace AssistantCore.DataSources
{
    // A test object that needs to be serialized.
    [Serializable]
    public class ObjectForSerialization
    {
        public int member1;
        public string member2;
        public string member3;
        public double member4;

        // A field that is not serialized.
        [NonSerialized()] 
        public string member5;

        public SubObject SubObject;

        public ObjectForSerialization()
        {

            member1 = 11;
            member2 = "hello";
            member3 = "hello";
            member4 = 3.14159265;
            member5 = "hello world!";

            SubObject = new SubObject();
        }

        public void Print()
        {

            Console.WriteLine("member1 = '{0}'", member1);
            Console.WriteLine("member2 = '{0}'", member2);
            Console.WriteLine("member3 = '{0}'", member3);
            Console.WriteLine("member4 = '{0}'", member4);
            Console.WriteLine("member5 = '{0}'", member5);
            Console.WriteLine("Sub object = {0}", SubObject.subMember1);
        }
    }

    [Serializable]
    public class SubObject
    {
        public int subMember1;

        public SubObject()
        {
            subMember1 = 11;
        }
    }
}