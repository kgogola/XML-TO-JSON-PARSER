using System.Collections.Generic;

namespace swi.Data
{
    public class SingleObject
    {
        public string Name { get; set; }
        public List<Field> fields { get; set; } = new List<Field>();
    }
}
