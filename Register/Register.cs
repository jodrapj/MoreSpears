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
        public static AbstractPhysicalObject.AbstractObjectType tranqSpearAbstract;

        public static void RegisterValues()
        {
            tranqSpear = new AbstractPhysicalObject.AbstractObjectType("tranqspear", true);
            tranqSpearAbstract = new AbstractPhysicalObject.AbstractObjectType("tranqspearabstract", true);
        }

        public static void UnregisterValues()
        {
            AbstractPhysicalObject.AbstractObjectType tranqspear = tranqSpear;
            AbstractPhysicalObject.AbstractObjectType tranqspearabstract = tranqSpearAbstract;

            tranqspear?.Unregister();
            tranqspearabstract?.Unregister();
            tranqSpear = null;
            tranqSpearAbstract = null;
        }
    }
}
