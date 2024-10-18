using rainworldmod.Spears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainworldmod.hooks
{
    public partial class RainWorldMod
    {
        public void SpearHook()
        {
            On.Spear.LodgeInCreature_CollisionResult_bool += Spear_LodgeInCreature_CollisionResult_bool;
        }

        private void Spear_LodgeInCreature_CollisionResult_bool(On.Spear.orig_LodgeInCreature_CollisionResult_bool orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is TranqSpear)
            {
                ((TranqSpear)self).Tranquilize(result.obj);
            }
        }
    }
}
