using System;

namespace ConsoleApplication1
{
    [Serializable]
    public class Aspect
    {
        public string name;
        public MetaDatas metadatas { get; set; }

        public Aspect()
        {
            name = "";
            metadatas = new MetaDatas();
        }
    }
}
