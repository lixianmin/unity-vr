using System;
using Unicorn.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class UIMainPanel : MonoBehaviour
    {
        private void Awake()
        {
            button.onClick.AddListener(onClickButton);
        }

        private void onClickButton()
        {
            Console.WriteLine("hello world");
        }

        public UIButton button;
    }
}