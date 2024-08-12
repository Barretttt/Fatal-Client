using Menu;
using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermo_Template.Mods
{
    internal class EnterThings
    {
        public static void EnterTab(int Index)
        {
            Variables.TabIndex = Index;
            Variables.pageNumber = 0;
        }

    }
}
