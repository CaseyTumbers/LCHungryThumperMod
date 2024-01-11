using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HungryThumper.Patches;
using HungryThumper.Utilities;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Mod to replace Thumper Noises with custom made noises. Voice by me Keplor ;P
// Credit to LCSoundTool for source code to help me get this working (Didn't want to depend on any other mods for this one).
// https://github.com/no00ob/LCSoundTool
namespace HungryThumper
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HungryThumperModBase : BaseUnityPlugin
    {
        private const string modGUID = "Tonic.HungryThumper";
        private const string modName = "Hungry Thumper";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static HungryThumperModBase Instance;

        internal ManualLogSource logger;
        public static Dictionary<string, string> customAudioClipsMatrix { get; } =
        new Dictionary<string, string> {
            { "ShortRoar1", "CustomShortRoar1" },
            { "HitCrawler", "CustomHitCrawler" },
            { "HitCrawler2", "CustomHitCrawler2" },
            { "StunCrawler", "CustomStunCrawler" },
            { "CrawlerDie", "CustomCrawlerDie" },
            { "LongRoar1", "CustomLongRoar1" },
            { "LongRoar2", "CustomLongRoar2" },
            { "LongRoar3", "CustomLongRoar3" },
            { "Stomp1", "CustomStomp1" },
            { "Stomp2", "CustomStomp2" },
            { "Stomp3", "CustomStomp3" },
            { "Ram1", "CustomRam1" },
            { "Ram2", "CustomRam2" },
            { "Ram3", "CustomRam3" },
            { "BitePlayer", "CustomBitePlayer" }
        };

        public static Dictionary<string, CustomAudioClip> replacedClips { get; private set; }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            replacedClips = new Dictionary<string, CustomAudioClip>();

            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            //logger.LogInfo("Test mod working???");

            harmony.PatchAll(typeof(HungryThumperModBase));
            harmony.PatchAll(typeof(ThumperNoisePatch));

            InitializeCustomAudioClips();
        }

        void InitializeCustomAudioClips()
        {
            string basePath = Path.Combine(Paths.PluginPath, "HungryThumper\\CustomSounds");
            foreach (var kvp in customAudioClipsMatrix)
            {
                string soundPath = Path.Combine(basePath, $"{kvp.Value}.mp3");
                AudioClip newClip = AudioUtilities.LoadFromDiskToAudioClip(soundPath);
                CustomAudioClip customClip = new CustomAudioClip(newClip, kvp.Key);
                replacedClips.Add(kvp.Key, customClip);
            }
        }
    }
}
