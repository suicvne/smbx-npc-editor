using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace visualNPCEditor
{
    public class serializeNPC
    {
        private List<NPC> npcs;

        public List<NPC> NPCS
        {
            get { return this.npcs; }
            set { this.npcs = value; }
        }

        public serializeNPC() {}
    }
}
