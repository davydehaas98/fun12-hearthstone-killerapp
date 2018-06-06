using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneKillerApp
{
    class Keyword
    {
        private string name;
        private string description;
        public string Description { get { return description; } set { description = value; } }
        public string Name { get { return name; } set { name = value; } }
        public override string ToString()
        {
            return name.ToUpper() + " - " + description;
        }
    }
}
