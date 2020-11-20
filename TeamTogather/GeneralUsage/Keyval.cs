using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralUsage
{
    class Keyval
    {
        public int key { get; set; }
        public int val { get; set; }

        public Keyval(int key, int val)
        {
            this.key = key;
            this.val = val;
        }
    }
}
