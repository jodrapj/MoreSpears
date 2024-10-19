using System;
using MoreSpears;

namespace MoreSpears.Spears
{
    public class TranqSpearAbstract : AbstractSpear
    {
        public TranqSpearAbstract(World world, TranqSpear realizedObject, WorldCoordinate pos, EntityID ID) : base(world, realizedObject, pos, ID, false)
        {
            this.type = AbstractObjectType.Spear;
        }
    }
}
