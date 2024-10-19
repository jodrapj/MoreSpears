using MoreSpears.Spears;
using IL;
using System;

namespace MoreSpears
{
    public partial class MoreSpears
    {
        public void SpearHook()
        {
            try
            {
                On.Spear.LodgeInCreature_CollisionResult_bool += Spear_LodgeInCreature_CollisionResult_bool1;
                MoreSpears.logger.LogDebug($"Loaded hooks");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException( ex );
            }
        }

        public void Spear_LodgeInCreature_CollisionResult_bool1(On.Spear.orig_LodgeInCreature_CollisionResult_bool orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is TranqSpear spear)
            {
                spear.Tranquilize(result.obj);
                MoreSpears.logger.LogDebug("Tranquilized creature");
            }
        }
    }
}
