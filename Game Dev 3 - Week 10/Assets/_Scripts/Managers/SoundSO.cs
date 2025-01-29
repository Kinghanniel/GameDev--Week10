using UnityEngine;

namespace GameDevWithMarco
{
    [CreateAssetMenu(fileName = "New Sound Data", menuName = "ScriptableObject Objects/Sound")]
    public class SoundSO : ScriptableObject
    {
        [Header("Sound Settings")]
        public float minPitchValue;
        public float maxPitchValue;
        public AudioClip clipToUse;
        public float soundVolume;

    }
}
