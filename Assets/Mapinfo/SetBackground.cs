using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mapinfo
{
    class SetBackground : MonoBehaviour
    {
        [SerializeField] private Image img;
        
        void Awake()
        {
            img = GetComponent<Image>();
        }
        
        void Start()
        {
            WWW www = new WWW("file:///"+GlobalValues.GlobalMap.path+GlobalValues.GlobalMap.Settings.EventsInfo.name_of_bg);
            img.sprite = Sprite.Create(www.texture, new Rect(0,0, www.texture.width, www.texture.height), new Vector2(0, 0));
            
        }
    }
}
