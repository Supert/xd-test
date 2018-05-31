using UnityEngine;
using XdTest.FSM;

namespace XdTest
{
    public class Core : MonoBehaviour
    {        //Fixed size for the sake of implementation speed.
        public const int SIZE = 16;
        public const int SIDE_SIZE = 4;

        public static Core Instance { get; private set; }

        [SerializeField]
        private GameObject menu;
        public GameObject Menu { get { return menu; } }

        [SerializeField]
        private GameObject endGameMenu; 
        public GameObject EndGameMenu { get { return endGameMenu; } }

        [SerializeField]
        private Field field;
        public Field Field { get { return field; } }

        public IPuzzleGenerator PuzzleGenerator { get; private set; }

        public StateMachine FSM { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Another instance of Core already exists.", this);
                return;
            }

            DontDestroyOnLoad(this);

            Instance = this;
            
            PuzzleGenerator = new MockPuzzleGenerator();
            
            FSM = new StateMachine();
            FSM.Event(Events.InitializationFinished);
        }

        public void StartGame()
        {
            FSM.Event(Events.StartGame);
        }

        public void BackToMenu()
        {
            FSM.Event(Events.BackToMenu);
        }
    }
}