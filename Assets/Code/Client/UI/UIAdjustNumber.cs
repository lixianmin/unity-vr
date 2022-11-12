/********************************************************************
created:    2022-08-24
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.UI;

namespace Client.UI
{
    public class UIAdjustNumber: UIWindowBase
    {
        public override string GetAssetPath()
        {
            return "uiadjustnumber";
        }
        
        protected override void OnLoaded()
        {
            _isLoaded = true;
            _btnIncr.UI.onClick.AddListener(() =>
            {
                _title.UI.text = (int.Parse(_title.UI.text) + 1).ToString();
                Console.WriteLine("increment");
            });
            
            _btnDecr.UI.onClick.AddListener(()=>{
                _title.UI.text = (int.Parse(_title.UI.text) - 1).ToString();
                Console.WriteLine("decrement");
            });
            
            Console.WriteLine("uiadjustnumber is OnLoaded");
        }

        protected override void OnUnloading()
        {
            Console.WriteLine("uiadjustnumber is OnUnloading");
            _isLoaded = false;
        }

        public void SetDebugText(string text)
        {
            if (_isLoaded)
            {
                _debugText.UI.text = text;
            }
        }

        private bool _isLoaded;
        private readonly UIWidget<UIButton> _btnIncr = new( "btn_increment");
        private readonly UIWidget<UIButton> _btnDecr= new(  "btn_decrement");
        private readonly UIWidget<UIText> _title = new( "text_number");
        private readonly UIWidget<UIText> _debugText = new("text_debug");
    }
}