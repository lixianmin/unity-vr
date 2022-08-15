using System;
using TMPro;
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
            input.text = "faint";
            Console.WriteLine("hello world");
        }

        public TMP_InputField input;
        public UIButton button;
    }
}