using MoreSpears.Spears;
using IL;
using System;
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

        public void RoomHook()
        {
            On.AbstractRoom.AddEntity += AbstractRoom_AddEntity;
        }

        private void AbstractRoom_AddEntity(On.AbstractRoom.orig_AddEntity orig, AbstractRoom self, AbstractWorldEntity ent)
        {
            float value = UnityEngine.Random.value;
            bool flag = ent is AbstractSpear && value < 0.25f && (ent as AbstractSpear).hue == 0f;
            if (flag)
            {
                ent = new AbstractTranqSpear(ent.world, null, ent.pos, ent.ID);
            }
            orig(self, ent);
        }

        public void Spear_LodgeInCreature_CollisionResult_bool(On.Spear.orig_LodgeInCreature_CollisionResult_bool orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is TranqSpear spear)
            {
                spear.Tranquilize(result.obj);
            }

            if (self.abstractPhysicalObject.type == Register.tranqSpear)
            {
                if (result.obj is BigSpider)
                {
                    (self as TranqSpear).loseEffectCounter = UnityEngine.Random.Range(1, 4);
                }
            }
        }
    }
}
