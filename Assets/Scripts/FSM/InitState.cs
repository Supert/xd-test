namespace XdTest.FSM
{
    public class InitState : State
    {
        public override States EnumValue { get { return States.Init; } }

        public override States Event(Events e)
        {
            if (e == Events.InitializationFinished)
                return States.Menu;
            return base.Event(e);
        }
    }
}