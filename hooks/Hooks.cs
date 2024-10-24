using MoreSpears.Spears;
using System;
using UnityEngine;
using RWCustom;
using System.Drawing.Imaging;
using MoreSpears.Spears.IceSpear;

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
                On.Spear.HitSomething += Spear_HitSomething;
                On.Spear.HitSomethingWithoutStopping += Spear_HitSomethingWithoutStopping;
                On.Weapon.HitAnotherThrownWeapon += Weapon_HitAnotherThrownWeapon;
                logger.LogInfo("Loaded spear hooks");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException( ex );
            }
        }

        private void Spear_HitSomethingWithoutStopping(On.Spear.orig_HitSomethingWithoutStopping orig, Spear self, PhysicalObject obj, BodyChunk chunk, PhysicalObject.Appendage appendage)
        {
            if (self is IceSpear && obj != null && chunk != null)
            {
                logger.LogInfo("Hit");
                (self as IceSpear).Destroy();
            }
            orig(self, obj, chunk, appendage);
        }

        private bool Spear_HitSomething(On.Spear.orig_HitSomething orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
        {
            orig(self, result, eu);
            if (self is HeavySpear)
            {
                if (result.obj != null)
                    (result.obj as Creature).Violence(self.firstChunk, new Vector2?(self.firstChunk.vel * self.firstChunk.mass * 2f), result.chunk, result.onAppendagePos, Creature.DamageType.Stab, 5f, 20f);
            }
            if (self is IceSpear)
            {
                if (result.obj != null)
                {
                    logger.LogInfo("Hit");
                    (self as IceSpear).Destroy();
                }
            }
            return false;
        }

        public void PlayerHook()
        {
            try
            {
                On.Player.Grabability += Player_Grabability;
                On.Player.GraphicsModuleUpdated += Player_GraphicsModuleUpdated;
                On.Player.ThrowObject += Player_ThrowObject;
                logger.LogInfo("Loaded player hooks");
            } catch (Exception ex)
            {
                MoreSpears.logger.LogError( ex );
            }
        }

        private void Player_ThrowObject(On.Player.orig_ThrowObject orig, Player self, int grasp, bool eu)
        {
            IntVector2 intVector = new IntVector2(self.ThrowDirection, 0);
            Vector2 vector = self.firstChunk.pos + intVector.ToVector2() * 10f + new Vector2(0f, 4f);
            if (self.grasps[grasp] != null)
            {
                if (self.grasps[grasp].grabbed is IceSpear)
                {
                    (self.grasps[grasp].grabbed as IceSpear).Thrown(self, vector, new Vector2?(self.mainBodyChunk.pos - intVector.ToVector2() * 10f), intVector, Mathf.Lerp(1f, 1.5f, self.Adrenaline), eu);
                }
            }
            orig(self, grasp, eu);
        }

        private void Weapon_HitAnotherThrownWeapon(On.Weapon.orig_HitAnotherThrownWeapon orig, Weapon self, Weapon obj)
        {
            orig(self, obj);
            if (self is HeavySpear && !(obj is HeavySpear))
            {
                Vector2 vector = Custom.DegToVec(UnityEngine.Random.value * 360f);
                for (int i = 0; i < UnityEngine.Random.Range(1, 5); i++)
                {
                    self.room.AddObject(new Spark(obj.thrownPos, Custom.RNV() * Mathf.Lerp(4f, 30f, UnityEngine.Random.value), Color.white, null, 4, 18));
                }
                for (int i = 0; i < 2; i++)
                {
                    self.room.AddObject(new ExplosiveSpear.SpearFragment(obj.firstChunk.pos, Custom.RNV() * Mathf.Lerp(20f, 40f, UnityEngine.Random.value)));
                }
                self.room.PlaySound(SoundID.Spear_Bounce_Off_Creauture_Shell, vector);
                if (obj != null)
                    obj.Destroy();
            }
            if (self is IceSpear)
            {
                logger.LogInfo("Hit");
                (self as IceSpear).Destroy();
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
                return Player.ObjectGrabability.BigOneHand;
            }
            return orig(self, obj);
        }

        public void RoomHook()
        {
            On.AbstractRoom.AddEntity += AbstractRoom_AddEntity;
            logger.LogInfo("Loaded room hooks");
        }

        private void AbstractRoom_AddEntity(On.AbstractRoom.orig_AddEntity orig, AbstractRoom self, AbstractWorldEntity ent)
        {
            float spear = UnityEngine.Random.Range(1, 3);
            float value = UnityEngine.Random.value;
            bool flag = ent is AbstractSpear && value < 0.25f && (ent as AbstractSpear).hue == 0f;
            if (flag)
            {
                if (spear == 1)
                {
                    ent = new AbstractTranqSpear(ent.world, null, ent.pos, ent.ID);
                } else if (spear == 2)
                {
                    ent = new AbstractHeavySpear(ent.world, null, ent.pos, ent.ID);
                } else if (ModManager.MSC)
                {
                    if (spear == 3)
                    {
                        if (self.realizedRoom.game.GetStorySession.characterStats.name == MoreSlugcats.MoreSlugcatsEnums.SlugcatStatsName.Saint)
                            ent = new AbstractIceSpear(ent.world, null, ent.pos, ent.ID);
                    }
                }
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

            if (self is IceSpear ice)
            {
                ice.room.PlaySound(SoundID.Spear_Bounce_Off_Creauture_Shell, result.chunk);
                ice.Destroy();
            }

            //if (self.abstractPhysicalObject.type == Register.Spears["TranqSpear"])
            if (self.abstractPhysicalObject.type == Registry.tranqSpear)
            {
                if (result.obj is BigSpider)
                {
                    (self as TranqSpear).loseEffectCounter = UnityEngine.Random.Range(1, 4);
                }
            }
        }
    }
}
