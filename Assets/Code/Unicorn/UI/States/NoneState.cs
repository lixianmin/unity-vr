namespace Unicorn.UI.States
{
    internal class NoneState : StateBase
    {
        public void OnOpenWindow(UIWindowFetus fetus)
        {
            fetus.ChangeState(StateKind.Load);
        }

        public void Release()
        {
            
        }
    }
}