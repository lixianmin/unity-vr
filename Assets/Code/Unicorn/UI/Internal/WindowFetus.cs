/********************************************************************
created:    2022-08-15
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/


using Unicorn.UI.States;
using UnityEngine;

namespace Unicorn.UI.Internal
{
    internal class WindowFetus
    {
        public WindowFetus(UIWindowBase master)
        {
            this.master = master;
        }
        
        public void Dispose()
        {
            _CloseWindow();

            parent = null;
            serializer = null;
            argument = null;

            if (gameObject is not null)
            {
                Object.Destroy(gameObject);
                gameObject = null;
            }
        }
        
        public void ChangeState(StateKind kind, object arg1=null)
        {
            state?.OnExit(this, arg1);
            state = StateBase.Create(kind);
            state?.OnEnter(this, arg1);
        }

        public void OnLoadGameObject(GameObject goCloned)
        {
            gameObject = goCloned;
            transform = goCloned.transform;
            serializer = goCloned.GetComponent(typeof(UISerializer)) as UISerializer;
            if (serializer is null)
            {
                Console.Error.WriteLine("serializer is null, gameObject={0}", gameObject.ToString());
                return;
            }
            
            master._SetTransform(transform);
            if (parent is not null)
            {
                transform.SetParent(parent, false);
            }
            
            master._FillWidgets(serializer);
        }

        public void _OpenWindow(object arg1)
        {
            this.argument = arg1;
            state.OnOpenWindow(this);
        }

        public void _CloseWindow()
        {
            state.OnCloseWindow(this);
        }

        public StateBase state = StateBase.Create(StateKind.None);
        public readonly UIWindowBase master;
        public object argument;

        public GameObject gameObject;
        public Transform transform;
        public Transform parent = UIManager.GetUIRoot();
        public UISerializer serializer;
        
        public float activateTime = 0;
        public bool isWindowCached;
        public bool isLoaded;
        public bool isOpened;
        public bool isDelayedOpenWindow;
        public bool isDelayedCloseWindow;
    }
}