using MoreSpears.Spears;
using System;
using UnityEngine;
using RWCustom;

namespace MoreSpears
{
    public partial class MoreSpears
    {
        bool firsttime = true;
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

        public void PlayerHook()
        {
            try
            {
                On.Player.Grabability += Player_Grabability;
                On.Player.GraphicsModuleUpdated += Player_GraphicsModuleUpdated;

            } catch (Exception ex)
            {
                MoreSpears.logger.LogError( ex );
            }
        }

        private void Player_GraphicsModuleUpdated(On.Player.orig_GraphicsModuleUpdated orig, Player self, bool actuallyViewed, bool eu)
        {
            orig(self, actuallyViewed, eu);
            // flipdirection -1 = left 1 = right
            //for (int i = 0; i < 2; i++) // WIP
            //{
            //    if (self.grasps[i] != null)
            //    {
            //        if (self.grasps[i].grabbed is HeavySpear spear)
            //        {
            //            var module = (self.graphicsModule as PlayerGraphics);
            //            module.hands[i].pos = spear.bodyChunks[i].pos;
            //            if (firsttime)
            //            {
            //                spear.setRotation = (spear.rotation * new Vector2(45, 0)) * self.flipDirection;
            //                firsttime = false;
            //            }
            //            if (self.flipDirection != self.lastFlipDirection)
            //            {
            //                spear.setRotation = (spear.rotation * new Vector2(45, 0)) * self.flipDirection;
            //            }
            //        }
            //    } else
            //    {
            //        if (firsttime == false)
            //            firsttime = true;
            //    }
            //}
        }

        private Player.ObjectGrabability Player_Grabability(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is HeavySpear)
            {
                return Player.ObjectGrabability.TwoHands;
            }
            return orig(self, obj);
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

            //if (self.abstractPhysicalObject.type == Register.Spears["TranqSpear"])
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
