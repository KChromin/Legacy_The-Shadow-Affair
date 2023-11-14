using System;
using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementDefaultExecution : MonoBehaviour
    {
        #region Variables

        private Rigidbody _rigidbody;
        private PlayerMovementDefaultCalculationHorizontal _horizontalCalculation;
        private PlayerMovementDefaultCalculationVertical _verticalCalculation;

        private const float PlayerMass = 30;

        #endregion Variables

        #region Setup

        public void Setup(Rigidbody playerRigidbody, PlayerMovementDefaultCalculationHorizontal horizontalCalculation, PlayerMovementDefaultCalculationVertical verticalCalculation)
        {
            _rigidbody = playerRigidbody;
            _horizontalCalculation = horizontalCalculation;
            _verticalCalculation = verticalCalculation;
        }

        #endregion Setup

        #region Public Methodes

        #region Horizontal

        public void SetIdleStates()
        {
            _horizontalCalculation.OnIdleState();
        }

        public void MoveDefault(PlayerMovementDefaultCalculationHorizontal.MoveModes moveMode)
        {
            _rigidbody.AddForce(_horizontalCalculation.GetHorizontalMoveVector(moveMode, _rigidbody.velocity.magnitude) * PlayerMass, ForceMode.Force);
        }

        #endregion Horizontal

        #region Vertical

        public void ApplyGravityDefault()
        {
            _rigidbody.AddForce(_verticalCalculation.GetGravityForceDefault(), ForceMode.Acceleration);
        }

        public void ApplyGravityGrounded()
        {
            _rigidbody.AddForce(_verticalCalculation.GetGravityForceGrounded(), ForceMode.Acceleration);
        }

        #endregion Vertical

        #endregion Public Methodes
    }
}