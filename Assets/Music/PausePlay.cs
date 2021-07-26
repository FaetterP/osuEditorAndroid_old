using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Music
{
    class PausePlay : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private Sprite pause, play;
        private Image thisImage;
        
        void Awake()
        {
            thisImage = GetComponent<Image>();
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    ChangeMusicPlayedStatus();
                }
            }
        }

        void OnMouseDown()
        {
            ChangeMusicPlayedStatus();
        }

        private void ChangeMusicPlayedStatus()
        {
            if (music.isPlaying) { music.Pause(); thisImage.sprite = play; }
            else { music.Play(); thisImage.sprite = pause; }
        }

    }
}
