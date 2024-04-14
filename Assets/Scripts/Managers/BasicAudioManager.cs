using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace RobbieWagnerGames.Common
{
    public enum AudioSourceName
    {
        UINav = 0,
        UISelect = 1,
        UIExit = 2,
        Bounce = 3,
        PointGain = 4,
        Purchase = 5,
        Music = 6
    }
    public class BasicAudioManager : MonoBehaviour
    {
        [SerializeField][SerializedDictionary("source type", "source")] private SerializedDictionary<AudioSourceName, AudioSource> audioSources;

        public static BasicAudioManager Instance {get; private set;}

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(gameObject); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        public void PlayAudioSource(AudioSourceName name)
        {
            if(audioSources.ContainsKey(name) && audioSources[name] != null)
            {
                audioSources[name].Play();
            }
        }
    }
}