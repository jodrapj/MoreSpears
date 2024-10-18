using System;
using BepInEx;
using On;
using rainworldmod.Spears;

namespace rainworldmod
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public partial class RainWorldMod : BaseUnityPlugin
    {
        public const string GUID = "jodrapj.rainworldmod";
        public const string NAME = "rainworldmod";
        public const string VERSION = "0.0.1";
        public static RainWorldMod Instance;
        public static bool isInit = false;

        private void OnAwake()
        {
            Instance = this;
            On.RainWorld.OnModsEnabled += RainWorld_OnModsEnabled;
            On.RainWorld.OnModsDisabled += RainWorld_OnModsDisabled;
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.AbstractPhysicalObject.Realize += AbstractPhysicalObject_Realize;
        }

        private void AbstractPhysicalObject_Realize(On.AbstractPhysicalObject.orig_Realize orig, AbstractPhysicalObject self)
        {
            orig(self);
            if (self.type == Register.tranqSpear)
                self.realizedObject = new TranqSpear(self, self.world);
        }

        private void RainWorld_OnModsDisabled(On.RainWorld.orig_OnModsDisabled orig, RainWorld self, ModManager.Mod[] newlyDisabledMods)
        {
            orig(self, newlyDisabledMods);
            foreach (ModManager.Mod mod in newlyDisabledMods)
                if (mod.id == GUID)
                    Register.UnregisterValues();
        }

        private void RainWorld_OnModsEnabled(On.RainWorld.orig_OnModsEnabled orig, RainWorld self, ModManager.Mod[] newlyEnabledMods)
        {
            orig(self, newlyEnabledMods);
            Register.RegisterValues();
        }

        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);

            if (isInit) return;
            isInit = true;

            try
            {

            }
            catch(Exception ex)
            {
                base.Logger.LogError(ex);
            }
        }
    }
}
