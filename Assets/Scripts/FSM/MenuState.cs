namespace XdTest.FSM
{
    public class MenuState : State
    {
        public override States EnumValue { get { return States.Menu; } }

        public override void Enter()
        {
            Core.Instance.Menu.SetActive(true);
            base.Enter();
        }

        public override States Event(Events e)
        {
            if (e == Events.StartGame)
            {
                return States.Game;
            }

            return base.Event(e);
        }

        public override void Exit()
        {
            Core.Instance.Menu.SetActive(false);
            base.Exit();
        }
    }
}