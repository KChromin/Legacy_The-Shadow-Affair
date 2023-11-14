using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.Player.Movement;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementCrouchWalk : PlayerStateBase
    {
        public PlayerStateMovementCrouchWalk(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.CrouchState = PlayerMain.PlayerStates.CrouchStates.Walk;

#endif

            #endregion Update State Info
            
        }

        protected override void InitializeSubState()
        {
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.CrouchState = PlayerMain.PlayerStates.CrouchStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
        }

        #endregion State Methodes
    }
}