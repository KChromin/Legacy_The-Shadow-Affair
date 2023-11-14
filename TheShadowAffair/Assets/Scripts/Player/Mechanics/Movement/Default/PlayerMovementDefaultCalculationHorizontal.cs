using System;
using ExperimentalFox.Player.Data;
using UnityEditor.Experimental;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementDefaultCalculationHorizontal : MonoBehaviour
    {
        #region Variables

        private PlayerInput.InputDataClass _input;
        private PlayerCases _cases;
        private PlayerModifiers _modifiers;
        private PlayerMovementDefaultCheckers _checkers;
        private Transform _orientation;

        #region Parameters

        [field: Header("Parameters")]
        [field: SerializeField]
        public ParametersStruct Parameters { get; set; }

        [Serializable]
        public struct ParametersStruct
        {
            [field: Header("Walk")]
            [field: SerializeField]
            public float WalkMaxSpeed { get; set; }

            [field: SerializeField]
            public float WalkAccelerationSpeed { get; set; }

            [field: Space, Header("Run")]
            [field: SerializeField]
            public float RunMaxSpeed { get; set; }

            [field: SerializeField]
            public float RunAccelerationSpeed { get; set; }

            [field: Space, Header("Crouch Walk")]
            [field: SerializeField]
            public float CrouchWalkMaxSpeed { get; set; }

            [field: SerializeField]
            public float CrouchWalkAccelerationSpeed { get; set; }
        }

        #endregion Speed Parameters

        #region Factors

        [field: Header("Factors")]
        [field: SerializeField]
        public FactorsStruct Factors { get; set; }

        [Serializable]
        public struct FactorsStruct
        {
            [field: Header("Backwards")]
            [field: SerializeField, Range(0, 1)]
            public float MoveBackwards { get; set; }
        }

        #endregion Factors

        public enum MoveModes
        {
            Walk,
            Run,
            CrouchWalk
        }

        private const float CounterDragFactor = 8.2f;

        [SerializeField]
        private float _currentSpeed;

        private float _currentSpeedCalculation;

        private bool _wasIdle = true;

        #endregion Variables

        #region Setup

        public void Setup(PlayerInput.InputDataClass input, PlayerCases cases, PlayerModifiers modifiers, PlayerMovementDefaultCheckers checkers, Transform orientation)
        {
            _input = input;
            _cases = cases;
            _modifiers = modifiers;
            _checkers = checkers;
            _orientation = orientation;
        }

        #endregion Setup

        #region Methodes

        //Takes input and returns it in direction//
        private Vector3 GetCurrentDirection()
        {
            Vector2 input = _input.MovementInput;
            Vector3 input3D = new(input.x, 0, input.y);

            return (_orientation.localRotation * input3D).normalized;
        }

        private Vector3 GetFinalDirection()
        {
            Vector3 currentDirection = GetCurrentDirection();

            #region Apply plane projection, for slopes

            if (_checkers.CheckerCases.OnSlope && !_checkers.CheckerCases.SlopeIsTooSteep)
            {
                Vector3 planeProjection = Vector3.ProjectOnPlane(currentDirection, _checkers.GetSlopeNormal());

                //Lower the vectors to counter jumping from edges//
                //When going up//
                if (planeProjection.y > 0)
                {
                    return ((planeProjection + currentDirection * 2) / 3).normalized;
                }

                return planeProjection.normalized;
            }

            #endregion Apply plane projection, for slopes

            return currentDirection.normalized;
        }

        private float GetCurrentSpeed(MoveModes moveMode, float currentVelocityMagnitude)
        {
            //Max Speed, Max Force//
            float[] currentSpeedValues = GetCurrentSpeedValues(moveMode);

            #region Apply Modifiers

            //Modifiers//
            currentSpeedValues[0] *= _modifiers.MovementDefault.MaximalSpeedMultiplier;
            currentSpeedValues[1] *= _modifiers.MovementDefault.AccelerationForceMultiplier;

            //Factor moving backwards//
            if (_cases.GeneralCases.IsMovingBackward)
            {
                currentSpeedValues[0] *= Factors.MoveBackwards;
            }

            #endregion Apply Modifiers

            float currentSpeed = GetCurrentSpeedForceFactor(currentSpeedValues[0], currentSpeedValues[1], currentVelocityMagnitude);

            //Apply counter drag factor//
            currentSpeed *= CounterDragFactor;

            return currentSpeed;
        }

        private float[] GetCurrentSpeedValues(MoveModes moveMode)
        {
            //Set Speeds//
            return moveMode switch
            {
                MoveModes.Walk => new[] { Parameters.WalkMaxSpeed, Parameters.WalkAccelerationSpeed },
                MoveModes.Run => new[] { Parameters.RunMaxSpeed, Parameters.RunAccelerationSpeed },
                MoveModes.CrouchWalk => new[] { Parameters.CrouchWalkMaxSpeed, Parameters.CrouchWalkAccelerationSpeed },
                _ => throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null)
            };
        }

        private float GetCurrentSpeedForceFactor(float maxSpeed, float accelerationTime, float currentVelocityMagnitude)
        {
            //If was in idle state, then reset set current speed//
            if (_wasIdle)
            {
                _wasIdle = false;
                _currentSpeed = currentVelocityMagnitude;
                _currentSpeedCalculation = 0;
            }

            return _currentSpeed = Mathf.SmoothDamp(_currentSpeed, maxSpeed, ref _currentSpeedCalculation, accelerationTime, Mathf.Infinity, Time.fixedDeltaTime);
        }

        #endregion Methodes

        #region Public Methodes

        public Vector3 GetHorizontalMoveVector(MoveModes speedMode, float currentVelocityMagnitude)
        {
            return GetFinalDirection() * GetCurrentSpeed(speedMode, currentVelocityMagnitude);
        }

        public void OnIdleState()
        {
            _wasIdle = true;
        }

        #endregion Public Methodes
    }
}