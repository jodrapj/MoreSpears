using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoreSpears.Extension
{
    public static class PhysicalObjectExt
    {
        public static void Replace(this PhysicalObject @object, PhysicalObject otherObject)
        {
            @object.abstractPhysicalObject.pos = otherObject.abstractPhysicalObject.pos;
            @object.abstractPhysicalObject.ID = new EntityID();
            @object.abstractPhysicalObject.type = otherObject.abstractPhysicalObject.type;
            @object.abstractPhysicalObject.stuckObjects = otherObject.abstractPhysicalObject.stuckObjects;
        }
    }
}
