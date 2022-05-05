using System;
using _Imported;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private Sounds m_Sounds;
        
        private AudioSource m_AS;

        private new void Awake()
        {
            base.Awake();
            
            m_AS = GetComponent<AudioSource>();
            m_AS.clip = m_Sounds.m_Menu;
            m_AS.Play();
        }

        public void Play(Sound sound)
        {
            m_AS.PlayOneShot(m_Sounds[sound]);
        }

        public void PlayMenuBGM()
        {
            m_AS.clip = m_Sounds.m_Menu;
            m_AS.Play();
        }

        public void PlayBGM()
        {
            m_AS.clip = m_Sounds.m_BGM[Random.Range((int)0, m_Sounds.m_BGM.Length)];
            m_AS.Play();
        }
    }
}