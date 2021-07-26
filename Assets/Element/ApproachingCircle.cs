using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class ApproachingCircle : MonoBehaviour
    {
        [SerializeField] private GameObject noteGOs;
        private AudioSource music;
        private int musicTime;
        public Note note;
        private RectTransform recttr;
        
        void Awake()
        {         
            note = transform.parent.GetComponent<Note>();
            recttr = GetComponent<RectTransform>();
            music = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        }

        void Start()
        {
            Color c = GlobalValues.GlobalMap.ComboColors[note.ComboColorNum];
            GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
        }

        void OnEnable()
        {
            Color c = GlobalValues.GlobalMap.ComboColors[note.ComboColorNum];
            GetComponent<Image>().color = new Color(c.r / 255.0f, c.g / 255.0f, c.b / 255.0f);
        }

        void Update()
        {
            musicTime = (int)(music.time * 1000);
            int razn = note.timestamp - musicTime;
            if (razn < 0 || razn>GlobalValues.AR_in_ms) { ControllerApproachingDetails.DeleteTimestampFromScreen(note.timestamp); Destroy(noteGOs); }
            if (razn < GlobalValues.AR_in_ms) { recttr.sizeDelta = new Vector2(100 + (razn / 10), 100 + (razn / 10)); }
        }
    }
}
