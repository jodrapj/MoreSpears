using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSpears.Spears.IceSpear
{
    public class AbstractIceSpear : AbstractSpear
    {
        public AbstractIceSpear(World world, IceSpear realizedObject, WorldCoordinate pos, EntityID ID) : base(world, realizedObject, pos, ID, false)
        {
            this.type = Registry.iceSpear;
        }
    }
}
