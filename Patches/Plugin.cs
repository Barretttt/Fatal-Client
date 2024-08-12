using BepInEx;
using Patches;
using System.ComponentModel;

namespace Patches
{
    [Description(Sigma.PluginInfo.Description)]
    [BepInPlugin(Sigma.PluginInfo.GUID, Sigma.PluginInfo.Name, Sigma.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }
    }
}
