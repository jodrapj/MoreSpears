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
        public static AbstractPhysicalObject.AbstractObjectType heavySpear;

        public static void RegisterValues()
        {
            tranqSpear = new AbstractPhysicalObject.AbstractObjectType("TranqSpear", true);
            heavySpear = new AbstractPhysicalObject.AbstractObjectType("HeavySpear", true);
        }

        public static void UnregisterValues()
        {
            AbstractPhysicalObject.AbstractObjectType tranqspear = tranqSpear;
            AbstractPhysicalObject.AbstractObjectType heavyspear = heavySpear;

            tranqspear?.Unregister();
            heavyspear?.Unregister();
            tranqSpear = null;
            heavySpear = null;
        }
    }
}
