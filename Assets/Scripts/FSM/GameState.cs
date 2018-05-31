namespace XdTest.FSM
{
    public class GameState : State
    {
        public override States EnumValue { get { return States.Game; } }

        public override void Enter()
        {
            Core.Instance.Field.Initialize();
            base.Enter();
        }

        public override States Event(Events e)
        {
            if (e == Events.PuzzleSolved)
                return States.EndGame;

            return base.Event(e);
        }
    }
}