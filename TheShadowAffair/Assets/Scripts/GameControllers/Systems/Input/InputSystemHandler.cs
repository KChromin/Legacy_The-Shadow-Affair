using UnityEngine;

namespace ExperimentalFox.GameController
{
    public class InputSystemHandler : MonoBehaviour
    {
        public GameInputControls GameInput { get; private set; }

        private void Awake()
        {
            GameInput = new GameInputControls();
        }

        private void OnEnable()
        {
            GameInput.Enable();
        }

        private void OnDisable()
        {
            GameInput.Disable();
        }
    }
}