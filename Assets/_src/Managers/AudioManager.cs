using UnityEngine;

namespace _src.Managers
{
    public enum MusicTracks
    {
        MainMusic = 0,
        ShopMusic
    }
    
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Tooltip("Order of tracks must be same as MusicTracks enum.")]
        [SerializeField] private AudioClip[] musicTracks;

        public void ChangeAudioClip(MusicTracks musicTrack)
        {
            AudioClip currentTrack = musicTracks[(int)musicTrack];
            _audioSource.clip = currentTrack;

            if (!_audioSource.isPlaying.Equals(currentTrack))
            {
                _audioSource.Play();
            }
        }
    }
}