using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

namespace Assets.Music
{
    class LoadMusicFromFile : MonoBehaviour
    {
        private AudioClip clip=new AudioClip();
        private WWW www;
        void Awake()
        {
            www = new WWW("file:///" + GlobalValues.GlobalMap.path+GlobalValues.GlobalMap.Settings.GeneralInfo.AudioName);
        }

        void Start()
        {
            clip = NAudioPlayer.FromMp3Data(www.bytes);
            clip.name = "audio_from_file";

            AudioSource music = GetComponent<AudioSource>();
            music.clip = clip;
        }



    }
}
