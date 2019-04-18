using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MosMos
{
    public class AudioData : MonoBehaviour
    {
        private List<AudioSource> _audios;

        /// <summary>
        /// List of audio-sources
        /// </summary>
        public List<AudioSource> Audios
        {
            get { return _audios; }
        }

        public void AudioDataInit()
        {
            _audios = GetComponentsInChildren<AudioSource>().ToList();
        }
    }
}