using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class Spinner : OsuElement, ICloneable
    {
        public int time_end;
        private AudioSource music;
        private Image thisImage;


        public object Clone()
        {
            return MemberwiseClone();
        }

        void Awake()
        {
            music = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
            thisImage = GetComponent<Image>();
        }

        void Update()
        {
            if (music.time * 1000 < timestamp || music.time * 1000 > time_end) { Destroy(gameObject); }
            else 
            { 
                transform.rotation = Quaternion.Euler(0,0,music.time*1000);
                thisImage.color = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b,(float)(0.2+(0.8*(music.time*1000.0-timestamp)/(time_end-timestamp))));

            }
        }

    }
}
