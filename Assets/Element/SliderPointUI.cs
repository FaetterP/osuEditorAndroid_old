using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Element
{
    class SliderPointUI : MonoBehaviour
    {
        public SliderPoint point;
        [SerializeField] private Sprite static_sprite, not_static_sprite;
        private AudioSource music;
        private OsuSlider slider;
        void Awake()
        {
            music = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        }
        void Start()
        {
            if (point.GetIsLocked())
            {
                GetComponent<Image>().sprite = static_sprite;
            }
            slider = GlobalValues.Selected_Element as OsuSlider;
        }

        void Update()
        {
            if (music.time * 1000 < slider.timestamp - GlobalValues.AR_in_ms || music.time * 1000 > (int)(slider.timestamp + slider.sum_time * slider.count_of_slides)) { Destroy(gameObject); }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    Click();
                }
            }
        }

        void OnMouseDown()
        {
            Click();
        }

        private void Click()
        {
            if (GlobalValues.sliderStatus == "add")
            {
                point.SwitchLocked();
                if (point.GetIsLocked())
                {
                    GetComponent<Image>().sprite = static_sprite;
                }
                else { GetComponent<Image>().sprite = not_static_sprite; }
            }
            else if (GlobalValues.sliderStatus == "remove")
            {
                Debug.Log(7879789); slider.points.Remove(point);
            }
        }
    }
}
