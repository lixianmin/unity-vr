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
        public override string GetResourcePath()
        {
            return "Assets/res/prefabs/uiadjustnumber.prefab";
        }

        private readonly UIWidget<UIButton> _btnIncr = new( "btn_increment");
        private readonly UIWidget<UIButton> _btnDecr= new(  "btn_decrement");
        private readonly UIWidget<UIText> _title = new( "text_number");
        public override void OnLoaded()
        {
            _btnIncr.UI.onClick.AddListener(() =>
            {
                _title.UI.text = (int.Parse(_title.UI.text) + 1).ToString();
            });
            
            var btnDecr = _btnDecr.GetWidget(this);
            var title = _title.GetWidget(this);
            
            // btnIncr.onClick.AddListener(()=>{
            //     title.text = (int.Parse(title.text) + 1).ToString();
            // });
            
            btnDecr.onClick.AddListener(()=>{
                title.text = (int.Parse(title.text) - 1).ToString();
            });
            
            Console.WriteLine("uiadjustnumber is OnLoaded");
        }

        public override void OnUnloading()
        {
            Console.WriteLine("uiadjustnumber is OnUnloading");
        }
    }
}