﻿using System.Linq;
using UnityEngine;

namespace MoreSpears.Spears
{
    public class HeavySpear : Spear
    {
        public int segments;
        public Color blackColor;
        public HeavySpear(AbstractHeavySpear abstractPhysicalObject, World world) : base(abstractPhysicalObject, world)
        {
            this.alwaysStickInWalls = true;
            this.bounce = 0.2f;
            this.bodyChunks[0] = new BodyChunk(this, 0, new Vector2(0f, 0f), 10f, 0.35f);
            this.spearDamageBonus = 3f;
            this.segments = 2;
            UnityEngine.Random.State state = UnityEngine.Random.state;
            UnityEngine.Random.InitState(abstractPhysicalObject.ID.RandomSeed);
            UnityEngine.Random.state = state;

            UnityEngine.Debug.Log("heavyspear ctor init'ed");
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
                    sLeaser.sprites[1 + i] = new FSprite("SmallSpear", true);
                }
            }
            this.AddToContainer(sLeaser, rCam, null);
        }
        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            base.ApplyPalette(sLeaser, rCam, palette);
            sLeaser.sprites[0].color = this.color;
            this.blackColor = palette.blackColor;
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

        public override void Update(bool eu)
        {
            base.Update(eu);
        }

        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos) // Electric spear sprite (to be remade)
        {
            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);
            for (int i = 0; i < this.segments; i++)
            {
                Vector2 vector = this.ExtendPos(timeStacker, i);
                sLeaser.sprites[1 + i].x = vector.x - camPos.x;
                sLeaser.sprites[1 + i].y = vector.y - camPos.y;
                sLeaser.sprites[1 + i].rotation = sLeaser.sprites[0].rotation;
                sLeaser.sprites[1 + i].color = this.blackColor;
            }
        }
        public Vector2 ExtendPos(float timeStacker, int node)
        {
            Vector3 vector = Vector3.Slerp(this.lastRotation, this.rotation, timeStacker);
            Vector3 vector2 = Vector3.Slerp(this.lastRotation, this.rotation, timeStacker) * (float)node * -4f;
            return Vector2.Lerp(base.firstChunk.lastPos, base.firstChunk.pos, timeStacker) + new Vector2
                (vector.x, vector.y) * 30f + new Vector2(vector2.x, vector2.y);
        }
    }
}
