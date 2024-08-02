using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource source;
    }
}
