using UnityEngine;
using RWCustom;

namespace MoreSpears.Spears.IceSpear
{
    public class IceSpear : Spear
    {
        public IceSpear(AbstractIceSpear abstractPhysicalObject, World world) : base(abstractPhysicalObject, world)
        {
            this.alwaysStickInWalls = true;
            MoreSpears.logger.LogDebug("icespear ctor init'd");
        }

        //public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        //{
        //    sLeaser.sprites = new FSprite[1];
        //    sLeaser.sprites[0] = new FSprite("../resources/icicle.png");
        //}

        public override void Destroy()
        {
            this.room.PlaySound(SoundID.Spear_Bounce_Off_Creauture_Shell, this.firstChunk);
            for (int i = 0; i < 2; i++)
            {
                this.room.AddObject(new ExplosiveSpear.SpearFragment(this.firstChunk.pos, Custom.RNV() * Mathf.Lerp(20f, 40f, UnityEngine.Random.value)));
            }
            base.Destroy();
        }

        public override void ChangeMode(Mode newMode)
        {
            base.ChangeMode(newMode);
            if (newMode == Weapon.Mode.StuckInWall || newMode == Weapon.Mode.StuckInCreature ||
                this.lastMode == Weapon.Mode.Thrown && this.mode == Weapon.Mode.Free)
            {
                this.Destroy();
            }
        }

        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            base.ApplyPalette(sLeaser, rCam, palette);
            this.color = Color.blue;
            sLeaser.sprites[0].color = this.color;
        }
    }
}
