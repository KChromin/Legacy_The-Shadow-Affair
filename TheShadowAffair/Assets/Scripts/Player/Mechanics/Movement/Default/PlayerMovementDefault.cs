using System;
using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementDefault : MonoBehaviour
    {
        #region Variables

        private PlayerMovementDefaultCalculationHorizontal _horizontalCalculation;
        private PlayerMovementDefaultCalculationVertical _verticalCalculation;

        public PlayerMovementComponentsController ComponentsController { get; private set; }
        public PlayerMovementDefaultCheckers MovementCheckers { get; private set; }
        public PlayerMovementDefaultExecution MovementExecution { get; private set; }

        #endregion Variables

        #region Setup

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            //Find Main Controller//
            PlayerMain playerMain = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();

            //Get Components//
            ComponentsController = GetComponentInParent<PlayerMovementComponentsController>();
            MovementCheckers = GetComponent<PlayerMovementDefaultCheckers>();
            MovementExecution = GetComponent<PlayerMovementDefaultExecution>();
            _horizontalCalculation = GetComponent<PlayerMovementDefaultCalculationHorizontal>();
            _verticalCalculation = GetComponent<PlayerMovementDefaultCalculationVertical>();

            //Setup Scripts//
            ComponentsController.Setup(playerMain.Components.Collider, playerMain.Components.Rigidbody);
            MovementCheckers.Setup(playerMain.Components.Transform, playerMain.MainObjects.Checkers.transform, playerMain.Components.Rigidbody);
            MovementExecution.Setup(playerMain.Components.Rigidbody, _horizontalCalculation, _verticalCalculation);
            _horizontalCalculation.Setup(playerMain.Input.InputData, playerMain.Data.Cases, playerMain.Data.Modifiers, MovementCheckers, playerMain.MainObjects.Orientation.transform);
            _verticalCalculation.Setup(playerMain.Components.Transform, playerMain.Data.Modifiers, MovementCheckers);
        }

        #endregion Setup
    }
}