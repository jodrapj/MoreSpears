using RWCustom;
using System;
using UnityEngine;

namespace MoreSpears.Spears
{
    public class TranqSpear : Spear
    {
        public int segments;
        public int loseEffectCounter;
        public int maxEffectLen = 200;
        public int minEffectLen = 100;
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

        public void Tranquilize(PhysicalObject otherObject)
        {
            if (!(otherObject is Creature) || loseEffectCounter == 0)
            {
                return;
            }

            Creature? creat = (otherObject as Creature);

            int rndLength = UnityEngine.Random.Range(minEffectLen, maxEffectLen);

            creat.Stun(rndLength);

            loseEffectCounter--;
        }

        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            base.ApplyPalette(sLeaser, rCam, palette);
            sLeaser.sprites[0].color = this.color;
            this.blackColor = palette.blackColor;
        }

        public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam) // Electric spear sprite (to be remade)
        {
            sLeaser.sprites = new FSprite[1 + this.segments];
            sLeaser.sprites[0] = new FSprite("SmallSpear", true);
            for (int i = 0; i < this.segments; i++)
            {
                if (i == 0)
                {
                    sLeaser.sprites[1 + i] = new FSprite("ShortcutArrow", true);
                }
                if (i == this.segments - 1)
                {
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        sLeaser.sprites[1 + i] = new FSprite("Pebble10", true);
                    }
                    else
                    {
                        sLeaser.sprites[1 + i] = new FSprite("Pebble9", true);
                    }
                }
                else
                {
                    sLeaser.sprites[1 + i] = new FSprite("Pebble" + UnityEngine.Random.Range(1, 12).ToString(), true);
                }
                sLeaser.sprites[1 + i].scale = Mathf.Lerp(0.4f, 1f, (float)i / (float)this.segments);
            }
            this.AddToContainer(sLeaser, rCam, null);
        }

        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            //base.AddToContainer(sLeaser, rCam, newContatiner);
            newContatiner = newContatiner ?? rCam.ReturnFContainer("Items");
            foreach (FSprite sprite in sLeaser.sprites)
            {
                sprite.RemoveFromContainer();
                newContatiner.AddChild(sprite);
            }
        }
        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos) // Electric spear sprite (to be remade)
        {
            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);
            for (int i = 0; i < this.segments; i++)
            {
                Vector2 vector = this.GreenSlopPos(timeStacker, i);
                sLeaser.sprites[1 + i].x = vector.x - camPos.x;
                sLeaser.sprites[1 + i].y = vector.y - camPos.y;
                sLeaser.sprites[1 + i].rotation = sLeaser.sprites[0].rotation;
                if (this.loseEffectCounter == 0)
                {
                    sLeaser.sprites[1 + i].color = this.blackColor;
                }
                else
                {
                    sLeaser.sprites[1 + i].color = Color.green;
                }
            }
        }

        public Vector2 GreenSlopPos(float timeStacker, int node)
        {
            Vector3 vector = Vector3.Slerp(this.lastRotation, this.rotation, timeStacker);
            Vector3 vector2 = Vector3.Slerp(this.lastRotation, this.rotation, timeStacker) * (float)node * -4f;
            return Vector2.Lerp(base.firstChunk.lastPos, base.firstChunk.pos, timeStacker) + new Vector2
                (vector.x, vector.y) * 30f + new Vector2(vector2.x, vector2.y);
        }
    }
}
