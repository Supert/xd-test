namespace XdTest.FSM
{
    public class EndGameState : State
    {
        public override States EnumValue { get { return States.EndGame; } }

        public override void Enter()
        {
            Core.Instance.EndGameMenu.SetActive(true);
            base.Enter();
        }

        public override States Event(Events e)
        {
            if (e == Events.BackToMenu)
                return States.Menu;
            return base.Event(e);
        }

        public override void Exit()
        {
            Core.Instance.EndGameMenu.SetActive(false);
            base.Exit();
        }
    }
}