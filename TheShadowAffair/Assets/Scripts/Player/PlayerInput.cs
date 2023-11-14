using System;
using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.GameController;
using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player
{
    public class PlayerInput : MonoBehaviour
    {
        #region Variables

        private GameInputControls _input;
        private PlayerCases.GeneralCasesClass _generalCases;

        #region Input Data

        [field: Header("Input Data")]
        [field: SerializeField]
        public InputDataClass InputData { get; private set; }

        [Serializable]
        public class InputDataClass
        {
            [field: SerializeField]
            public Vector2 LookInput { get; set; }

            [field: SerializeField]
            public Vector2 MovementInput { get; set; }
        }

        #endregion Input Data

        #endregion Variables

        #region Setup

        private void Awake()
        {
            PlayerMain playerMain = GetComponent<PlayerMain>();

            _input = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<InputSystemHandler>().GameInput;

            _generalCases = playerMain.Data.Cases.GeneralCases;
        }

        #endregion Setup

        #region Public Methodes
        
        public void UpdateInputData()
        {
            InputData.LookInput = GetLookInput();
            InputData.MovementInput = GetMovementInput();
        }

        #endregion Public Methodes

        #region Methodes

        private Vector2 GetLookInput()
        {
            Vector2 input = _input.Default.Look.ReadValue<Vector2>();

            #region Cases

            _generalCases.IsLookingAround = input != Vector2.zero;

            #endregion Cases

            return input;
        }

        private Vector2 GetMovementInput()
        {
            Vector2 input = _input.Default.Movement.ReadValue<Vector2>();

            #region Cases

            _generalCases.IsGivingInput = input != Vector2.zero;

            _generalCases.IsMovingBackward = input.y < 0;

            #endregion Cases

            return input;
        }

        #endregion Methodes
    }
}