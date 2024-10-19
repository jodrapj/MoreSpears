using RWCustom;
using System;
using UnityEngine;

namespace MoreSpears.Spears
{
    public class TranqSpear : Spear
    {

        public int segments;
        public int loseEffectCounter;
        public int effectLen = 340;
        public Color tranqColor;
        public Color blackColor;

        public TranqSpear(TranqSpearAbstract abstractPhysicalObject, World world) : base(abstractPhysicalObject, world) // Наследование конструктора класса-родителя
        {
            this.segments = 2;
            UnityEngine.Random.State state = UnityEngine.Random.state;
            this.tranqColor = Custom.HSL2RGB(0.99f, 0.48f, 0.20f);
            UnityEngine.Random.InitState(abstractPhysicalObject.ID.RandomSeed);
            UnityEngine.Random.state = state;

            loseEffectCounter = UnityEngine.Random.Range(1, 4);
            UnityEngine.Debug.Log("tranqspear ctor init'ed");
        }

        public override void Update(bool eu)
        {
            base.Update(eu);
        }

        public void Tranquilize(PhysicalObject otherObject)
        {
            if (!(otherObject is Creature) || loseEffectCounter == 0)
            {
                return;
            }

            var creat = (otherObject as Creature);

            creat.Stun(this.effectLen);

            loseEffectCounter--;

            MoreSpears.logger.LogDebug($"Tranquilized {creat}");
        }

        public override void Collide(PhysicalObject otherObject, int myChunk, int otherChunk)
        {
            base.Collide(otherObject, myChunk, otherChunk);
            UnityEngine.Debug.Log("Hit something");
        }

        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            base.ApplyPalette(sLeaser, rCam, palette);
            sLeaser.sprites[0].color = this.color;
            this.blackColor = palette.blackColor;
        }

        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            base.AddToContainer(sLeaser, rCam, newContatiner);
            newContatiner = newContatiner ?? rCam.ReturnFContainer("Items");
            foreach (FSprite sprite in sLeaser.sprites)
            {
                sprite.RemoveFromContainer();
                newContatiner.AddChild(sprite);
            }
        }
        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);
        }
    }
}
