using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneKillerApp
{
    class Rarity
    {
        private string name;
        private int dustcost;
        private int disenchant;
        public string Name { get { return name; } set { name = value; } }
        public int Dustcost { get { return dustcost; } set { dustcost = value; } }
        public int Disenchant { get { return disenchant; } set { disenchant = value; } }
        public override string ToString()
        {
            return "RARITY: " + name + Environment.NewLine + "CRAFTING COST: " + dustcost + Environment.NewLine + "DISENCHANTMENT: " + disenchant;
        }
    }
}
