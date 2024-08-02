using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioManager : MonoBehaviour, ISoundManager
    {
        [SerializeField]private Sound[] _sounds;

        private void Awake()
        {
            foreach (Sound sound in _sounds)
            {

                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = 1;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(_sounds, sound => sound.name == name);
            if (s != null) {
                s.source.Play();
            }
            else
            {
                Debug.Log($"No sound {name}");
            }

        }
    }
}
