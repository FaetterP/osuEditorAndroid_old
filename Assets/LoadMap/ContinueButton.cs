using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

namespace Assets.LoadMap
{
    class ContinueButton : MonoBehaviour
    {
        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    if (IsContainsAudioFile())
                    {
                        SceneManager.LoadScene(1);
                    }
                    else
                    {
                        Debug.Log("audio not found");
                    }
                }
            }
        }

        void OnMouseDown()
        {
            if (IsContainsAudioFile())
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("audio not found");
            }
        }

        private bool IsContainsAudioFile()
        {
            if (File.Exists(GlobalValues.GlobalMap.path + "audio.mp3") && File.Exists(GlobalValues.GlobalMap.path + "background.jpg")) { return true; }
            return false;
        }
    }
}
