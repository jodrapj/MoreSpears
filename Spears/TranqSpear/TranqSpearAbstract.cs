namespace MoreSpears.Spears
{
    public class AbstractTranqSpear : AbstractSpear
    {
        public AbstractTranqSpear(World world, TranqSpear realizedObject, WorldCoordinate pos, EntityID ID) : base(world, realizedObject, pos, ID, false)
        {
            //this.type = Register.Spears["TranqSpear"];
            this.type = Register.tranqSpear;
        }
    }
}
