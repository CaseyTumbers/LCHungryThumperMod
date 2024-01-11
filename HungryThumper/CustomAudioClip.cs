using UnityEngine;

namespace HungryThumper
{
    public class CustomAudioClip
    {
        public AudioClip clip;
        public string source = string.Empty;
        public bool canPlay = true;

        private bool initialized = false;

        public CustomAudioClip(AudioClip newClip, string newSource)
        {
            if (!initialized)
                Initialize();

            clip = newClip;
            source = newSource;
        }

        private void Initialize()
        {
            initialized = true;
        }
    }
}
