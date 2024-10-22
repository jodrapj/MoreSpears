namespace MoreSpears.Spears
{
    public class AbstractHeavySpear : AbstractSpear
    {
        public AbstractHeavySpear(World world, HeavySpear realizedObject, WorldCoordinate pos, EntityID ID) : base(world, realizedObject, pos, ID, false)
        {
            //this.type = Register.Spears["HeavySpear"];
            this.type = Register.heavySpear;
        }
    }
}
