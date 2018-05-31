namespace XdTest.FSM
{
    public abstract class State
    {
        public abstract States EnumValue { get; }

        public virtual States Event(Events e)
        {
            return EnumValue;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }
    }
}