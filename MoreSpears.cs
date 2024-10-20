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
        public const string VERSION = "0.0.31";
        public static MoreSpears Instance;
        public static bool isInit = false;
        public static ManualLogSource logger;

        public void OnEnable()
        {
            Instance = this;
            logger = this.Logger;
            On.RainWorld.OnModsEnabled += RainWorld_OnModsEnabled;
            On.RainWorld.OnModsDisabled += RainWorld_OnModsDisabled;
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.AbstractPhysicalObject.Realize += AbstractPhysicalObject_Realize;
        }

        public void AbstractPhysicalObject_Realize(On.AbstractPhysicalObject.orig_Realize orig, AbstractPhysicalObject self)
        {
            orig(self);
            Logger.LogInfo($"Trying to realize. Data: {self.realizedObject}");
            if (self.type == Register.tranqSpear)
            {
                self.realizedObject = new TranqSpear((AbstractTranqSpear)self, self.world);
                Logger.LogMessage("Realized TranqSpear");
            } else if (self.type == Register.heavySpear)
            {
                self.realizedObject = new HeavySpear((AbstractHeavySpear)self, self.world);
                Logger.LogMessage("Realized HeavySpear");
            }
        }

        public void RainWorld_OnModsDisabled(On.RainWorld.orig_OnModsDisabled orig, RainWorld self, ModManager.Mod[] newlyDisabledMods)
        {
            orig(self, newlyDisabledMods);
            foreach (ModManager.Mod mod in newlyDisabledMods)
                if (mod.id == GUID)
                {
                    Register.UnregisterValues();
                    Logger.LogDebug("Spears mod unloaded1");
                }
        }

        public void RainWorld_OnModsEnabled(On.RainWorld.orig_OnModsEnabled orig, RainWorld self, ModManager.Mod[] newlyEnabledMods)
        {
            orig(self, newlyEnabledMods);
            Register.RegisterValues();
            UnityEngine.Debug.Log("Spears mod loaded!!");
        }

        public void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);

            if (isInit) return;
            isInit = true;

            try 
            {            
                SpearHook();
                RoomHook();
                UnityEngine.Debug.Log("Spears mod loaded");
            }
            catch(Exception ex)
            {
                base.Logger.LogError(ex);
            }
        }
    }
}
