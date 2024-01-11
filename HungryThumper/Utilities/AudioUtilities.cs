using System;
using UnityEngine;
using UnityEngine.Networking;

namespace HungryThumper.Utilities
{
    public static class AudioUtilities
    {
        public static AudioClip LoadFromDiskToAudioClip(string path)
        {
            AudioClip clip = null;
            using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
            {
                uwr.SendWebRequest();

                // we have to wrap tasks in try/catch, otherwise it will just fail silently
                try
                {
                    while (!uwr.isDone)
                    {

                    }

                    if (uwr.result != UnityWebRequest.Result.Success)
                        HungryThumperModBase.Instance.logger.LogError($"Failed to load MP3 AudioClip from path: {path} Full error: {uwr.error}");
                    else
                    {
                        HungryThumperModBase.Instance.logger.LogInfo($"Uwr: {uwr}");
                        clip = DownloadHandlerAudioClip.GetContent(uwr);
                        HungryThumperModBase.Instance.logger.LogInfo($"Successfully load MP3 AudioClip from path: {path}, with clip: {clip.name}");
                    }
                }
                catch (Exception err)
                {
                    HungryThumperModBase.Instance.logger.LogError($"{err.Message}, {err.StackTrace}");
                }
            }

            return clip;
        }
    }
}
