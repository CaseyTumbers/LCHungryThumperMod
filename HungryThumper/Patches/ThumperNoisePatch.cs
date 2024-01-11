using HarmonyLib;
using UnityEngine;

namespace HungryThumper.Patches
{
    [HarmonyPatch(typeof(AudioSource))]
    internal class ThumperNoisePatch
    {
        [HarmonyPatch(nameof(AudioSource.PlayOneShot), new[] { typeof(AudioClip), typeof(float) })]
        [HarmonyPrefix]
        static void changeScreamSFX(ref AudioClip clip, float volumeScale)
        {
            // Check original exists
            if (clip == null)
            {
                //HungryThumperModBase.Instance.logger.LogWarning($"Apperently no clip???");
                return;
            }

            clip = ReplaceClipWithNew(clip);

        }

        private static AudioClip ReplaceClipWithNew(AudioClip original, AudioSource source = null)
        {
            // Setup Variables
            string clipName = original.name;

            //HungryThumperModBase.Instance.logger.LogInfo($"Orignial Name of clip: {clipName}");
            
            // Check if clipName exists in the dictionary
            if (HungryThumperModBase.replacedClips.ContainsKey(clipName))
            {
                if (!string.IsNullOrEmpty(HungryThumperModBase.replacedClips[clipName].source))
                {
                    return HungryThumperModBase.replacedClips[clipName].clip;
                }

            }

            return original;
        }
    }
}
