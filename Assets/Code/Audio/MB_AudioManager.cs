using System.Collections.Generic;
using Code.Utils;
using MyBox;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.Audio {
    public class MB_AudioManager : MonoBehaviour {
        #region Members
        [Foldout("AudioManager", true)]
        [SerializeField] private protected AudioResource[] m_CardDrawnSounds;
        [SerializeField] private protected AudioResource[] m_CardScoredSounds;
        [SerializeField] private protected AudioResource[] m_JokerTriggeredSounds;
        #endregion

        #region Getters / Setters
        private AudioResource[] CardDrawnSounds { get => this.m_CardDrawnSounds; }
        private AudioResource[] CardScoredSounds { get => this.m_CardScoredSounds; }
        private AudioResource[] JokerTriggeredSounds { get => this.m_JokerTriggeredSounds; }
        #endregion

        #region Static / Readonly / Const
        #endregion

        #region Unity methods
        #endregion

        private void PlaySoundEffect(ICollection<AudioResource> resources, float volume, float pitch) {
            AudioResource resource = SC_Utils.Sample(resources);
            AudioSource audioSource = this.AddComponent<AudioSource>();
            audioSource.resource = resource;
            audioSource.loop = false;
            audioSource.volume = volume;
            audioSource.pitch = Random.Range(.95f, 1.05f) * pitch;
            audioSource.Play();
            this.InSeconds(audioSource.clip.length, () => Destroy(audioSource));
        }

        public void PlayCardDrawn(float volume = 1, float pitch = 1) => this.PlaySoundEffect(this.CardDrawnSounds, volume, pitch);
        public void PlayCardScored(float volume = 1, float pitch = 1) => this.PlaySoundEffect(this.CardScoredSounds, volume, pitch);
        public void PlayJokerTriggered(float volume = 1, float pitch = 1) => this.PlaySoundEffect(this.JokerTriggeredSounds, volume, pitch);
    }
}
