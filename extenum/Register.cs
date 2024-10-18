using MoreSpears.Spears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSpears
{
    public static class Register
    {
        public static AbstractPhysicalObject.AbstractObjectType tranqSpear;
        public static void RegisterValues()
        {
            tranqSpear = new AbstractPhysicalObject.AbstractObjectType("TranqSpear", true);
        }
        public static void UnregisterValues()
        {
            AbstractPhysicalObject.AbstractObjectType tranqspear = tranqSpear;
            tranqspear?.Unregister();
            tranqSpear = null;
        }
    }
}
