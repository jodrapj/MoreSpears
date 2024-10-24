using System;
using BepInEx;
using BepInEx.Logging;
using MoreSpears.Spears;
using MoreSpears.Spears.IceSpear;

namespace MoreSpears
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public partial class MoreSpears : BaseUnityPlugin
    {
        public const string GUID = "jodrapj.morespears";
        public const string NAME = "MoreSpears";
        public const string VERSION = "0.0.31";
        public static MoreSpears Instance;
        public static bool isInit = false;
        public static ManualLogSource logger;

        public void OnEnable()
        {
            Instance = this;
            logger = this.Logger;
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.AbstractPhysicalObject.Realize += AbstractPhysicalObject_Realize;
        }

        public void AbstractPhysicalObject_Realize(On.AbstractPhysicalObject.orig_Realize orig, AbstractPhysicalObject self)
        {
            orig(self);
            if (!Registry.registered)
                return;
            Logger.LogInfo($"Trying to realize. Data: {self.realizedObject}");
            //if (self.type == Register.Spears["TranqSpear"])
            if (self.type == Registry.tranqSpear)
            {
                self.realizedObject = new TranqSpear((AbstractTranqSpear)self, self.world);
                Logger.LogMessage("Realized TranqSpear");
            }
            //if (self.type == Register.Spears["HeavySpear"])
            if (self.type == Registry.heavySpear)
            {
                self.realizedObject = new HeavySpear((AbstractHeavySpear)self, self.world);
                Logger.LogMessage("Realized HeavySpear");
            }
            if (self.type == Registry.iceSpear)
            {
                self.realizedObject = new IceSpear((AbstractIceSpear)self, self.world);
                Logger.LogMessage("Realized IceSpear");
            }
        }

        public void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);

            if (isInit) return;
            isInit = true;

            try 
            {

                if (!Registry.registered)
                {
                    Registry.RegisterValues();
                }

                SpearHook();
                RoomHook();
                PlayerHook();
                UnityEngine.Debug.Log("Spears mod loaded");

            }
            catch(Exception ex)
            {
                base.Logger.LogError(ex);
            }
        }
    }
}
