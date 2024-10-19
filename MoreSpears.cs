using System;
using System.Runtime.CompilerServices;
using BepInEx;
using BepInEx.Logging;
using MoreSpears.Spears;

namespace MoreSpears
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public partial class MoreSpears : BaseUnityPlugin
    {
        public const string GUID = "jodrapj.morespears";
        public const string NAME = "MoreSpears";
        public const string VERSION = "0.0.21";
        public static MoreSpears Instance;
        public static bool isInit = false;
        public static ManualLogSource logger;

        public void OnEnable()
        {
            Instance = this;
            logger = this.Logger;
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.RainWorld.OnModsEnabled += RainWorld_OnModsEnabled;
            On.RainWorld.OnModsDisabled += RainWorld_OnModsDisabled;
            On.AbstractPhysicalObject.Realize += AbstractPhysicalObject_Realize;
        }

        public void AbstractPhysicalObject_Realize(On.AbstractPhysicalObject.orig_Realize orig, AbstractPhysicalObject self)
        {
            orig(self);
            if (self.type == AbstractPhysicalObject.AbstractObjectType.Spear && self is TranqSpear)
            {
                self.realizedObject = new TranqSpear((TranqSpearAbstract)self, self.world);
                Logger.LogMessage("Realized TranqSpear");
            }
        }

        public void RainWorld_OnModsDisabled(On.RainWorld.orig_OnModsDisabled orig, RainWorld self, ModManager.Mod[] newlyDisabledMods)
        {
            orig(self, newlyDisabledMods);
            foreach (ModManager.Mod mod in newlyDisabledMods)
                if (mod.id == GUID)
                {
                    Logger.LogDebug("Spears mod unloaded1");
                }
        }

        public void RainWorld_OnModsEnabled(On.RainWorld.orig_OnModsEnabled orig, RainWorld self, ModManager.Mod[] newlyEnabledMods)
        {
            orig(self, newlyEnabledMods);
            UnityEngine.Debug.Log("Spears mod loaded");
        }

        public void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);

            if (isInit) return;
            isInit = true;

            try 
            {            
                SpearHook();
                UnityEngine.Debug.Log("Spears mod loaded");
            }
            catch(Exception ex)
            {
                base.Logger.LogError(ex);
            }
        }
    }
}
