/********************************************************************
created:    2022-08-16
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using Unicorn.Collections;

namespace Unicorn.UI.States
{
    internal class StateBase
    {
        public static StateBase Create(StateKind kind)
        {
            if (_states.TryGetValue(kind, out var last))
            {
                return last;
            }

            switch (kind)
            {
                case StateKind.None:
                    last = new NoneState();
                    break;
                case StateKind.Load:
                    break;
                case StateKind.OpenAnimation:
                    break;
                case StateKind.Opened:
                    break;
                case StateKind.Unload:
                    break;
                case StateKind.CloseAnimation:
                    break;
                case StateKind.Closed:
                    break;
                case StateKind.Failure:
                    break;
                default:
                    Console.Error.WriteLine("invalid state kind={0}", kind);
                    break;
            }

            _states[kind] = last;
            return last;
        }

        public virtual void OnEnter(UIWindowFetus fetus, object arg1)
        {
            
        }
                
        public virtual void OnExit(UIWindowFetus fetus, object arg1)
        {
            
        }

        private static readonly SortedTable<StateKind, StateBase> _states = new(8);
        protected static readonly UILoadingMask _loadWindowMask = new(0.5f);
        protected static readonly UILoadingMask _playAnimationMask = new(0);
    }
}