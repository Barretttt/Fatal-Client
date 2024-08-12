using System;

namespace Misc
{
    public class ButtonHelper
    {
        public string String = "-";
        public string overlapText = null;
        public Action ExecutePath = null;
        public Action enableMethod = null;
        public Action disableMethod = null;
        public bool enabled = false;
        public bool Tog = true;

        public ButtonHelper(string Sting, Action executePath = null, Action disableMethod = null, bool enabled = false, bool tog = true)
        {
            this.String = Sting;
            this.ExecutePath = executePath;
            this.disableMethod = disableMethod;
            this.enabled = enabled;
            this.Tog = tog;
        }
    }
}
