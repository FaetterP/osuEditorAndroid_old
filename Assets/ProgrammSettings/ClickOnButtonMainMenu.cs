using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.ProgrammSettings
{
    class ClickOnButtonMainMenu : MonoBehaviour
    {
        [SerializeField]private int NumberOfScene;
        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider == GetComponent<Collider>())
                {
                    SceneManager.LoadScene(NumberOfScene);
                }
            }
        }

        void OnMouseDown()
        {
            SceneManager.LoadScene(NumberOfScene);
        }
    }
}
