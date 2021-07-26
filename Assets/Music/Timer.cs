using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    class Timer : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private Slider slider;
        private Text timerText;
        private float time=0;
        int time_milisec = 0;
        void Awake()
        {
            timerText = GetComponent<Text>();
        }

        void Start()
        {
            slider.maxValue = (int)(music.clip.length * 1000);
        }
        void Update()
        {
            if (music.isPlaying)
            {
                slider.value = music.time*1000;
            }
            else
            {
                music.time = (float)(1.0*slider.value/1000);
            }
            time = music.time;
            time_milisec = (int)(time * 1000);
            int min, sec, msec;
            min = time_milisec / 60000;
            sec = (time_milisec % 60000) / 1000;
            msec = time_milisec % 1000;
            timerText.text = min+ ":"+sec+":"+msec;
        }
    }
}
