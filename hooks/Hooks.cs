using MoreSpears.Spears;
using IL;
using System;
using MoreSpears.Extension;
using System.Linq;

namespace MoreSpears
{
    public partial class MoreSpears
    {
        public void SpearHook()
        {
            try
            {
                On.Spear.LodgeInCreature_CollisionResult_bool += Spear_LodgeInCreature_CollisionResult_bool;
                MoreSpears.logger.LogDebug($"Loaded hooks");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException( ex );
            }
        }

        public void Spear_LodgeInCreature_CollisionResult_bool(On.Spear.orig_LodgeInCreature_CollisionResult_bool orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is TranqSpear spear)
            {
                spear.Tranquilize(result.obj);
            }

            

            if (self.abstractPhysicalObject.type == AbstractPhysicalObject.AbstractObjectType.Spear)
            {
                if (self.abstractSpear.explosive == false && self.abstractSpear.electric == false && result.obj is BigSpider)
                {
                    self.abstractPhysicalObject = new AbstractPhysicalObject(self.room.world, Register.tranqSpear, new TranqSpear(self.abstractPhysicalObject as TranqSpearAbstract, self.room.world), self.abstractPhysicalObject.pos, self.room.game.GetNewID());
                }
            }
        }
    }
}
