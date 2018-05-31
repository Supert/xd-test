using System.Collections.Generic;

namespace XdTest.FSM
{
    //Stupid simple fsm.
    public class StateMachine
    {
        public States State { get; private set; }

        private Dictionary<States, State> states = new Dictionary<States, State>()
        {
            { States.Init, new InitState() },
            { States.Menu, new MenuState() },
            { States.Game, new GameState() },
            { States.EndGame, new EndGameState() },
        };

        public void Event(Events e)
        {
            States newState = states[State].Event(e);
            if (State != newState)
            {
                states[State].Exit();
                State = newState;
                states[State].Enter();
            }
        }

        public StateMachine()
        {
            //manually start fsm.
            State = States.Init;
            states[State].Enter();
        }
    }
}