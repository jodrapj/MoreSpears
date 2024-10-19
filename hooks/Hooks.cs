using MoreSpears.Spears;
using IL;

namespace MoreSpears
{
    public partial class MoreSpears
    {
        private void SpearHook()
        {
            On.Spear.LodgeInCreature_CollisionResult_bool += Spear_LodgeInCreature_CollisionResult_bool;

        }



        private void Spear_LodgeInCreature_CollisionResult_bool(On.Spear.orig_LodgeInCreature_CollisionResult_bool orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is TranqSpear spear)
            {
                spear.Tranquilize(result.obj);
            }
        }
    }
}
