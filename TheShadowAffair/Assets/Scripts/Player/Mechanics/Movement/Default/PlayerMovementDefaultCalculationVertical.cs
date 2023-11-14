using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementDefaultCalculationVertical : MonoBehaviour
    {
        #region Variables

        private Transform _playerTransform;
        private PlayerModifiers _modifiers;
        private PlayerMovementDefaultCheckers _checkers;

        #endregion Variables

        #region Setup

        public void Setup(Transform playerTransform, PlayerModifiers modifiers, PlayerMovementDefaultCheckers checkers)
        {
            _playerTransform = playerTransform;
            _modifiers = modifiers;
            _checkers = checkers;
        }

        #endregion Setup

        #region Public Methodes

        public Vector3 GetGravityForceDefault()
        {
            return -_playerTransform.up * (-Physics.gravity.y * _modifiers.MovementDefault.GravityForceMultiplier);
        }

        public Vector3 GetGravityForceGrounded()
        {
            if (!_checkers.CheckerCases.OnSlope) return GetGravityForceDefault();
            
            if (_checkers.CheckerCases.SlopeIsTooSteep)
            {
                return GetGravityForceDefault() * _modifiers.MovementDefault.GravitySteepSlopeForceMultiplier;
            }
                
            return _checkers.GetGroundDirection() * (-Physics.gravity.y * _modifiers.MovementDefault.GravityGroundingForceMultiplier);
        }

        #endregion Public Methodes
    }
}