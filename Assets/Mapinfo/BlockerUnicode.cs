using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mapinfo
{
    class BlockerUnicode : MonoBehaviour
    {
        [SerializeField]private Image disabledImage;
        private Regex pattern = new Regex("^[a-zA-Z0-9]*$");
        private Text currentText;
        [SerializeField] private InputField disabledText;
        void Awake()
        {
            currentText = GetComponent<Text>();
        }

        void Update()
        {
            if (pattern.IsMatch(currentText.text))
            {
                disabledImage.color = Color.grey;
                disabledText.enabled = false;
                disabledText.text = currentText.text;
                disabledText.placeholder.transform.localScale = new Vector3(0, 0, 0);
            }
            else 
            {
                disabledImage.color = Color.white;
                disabledText.enabled = true;
                disabledText.placeholder.transform.localScale=new Vector3(1,1,1);
            }
        }

    }
}
