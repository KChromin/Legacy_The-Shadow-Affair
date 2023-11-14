using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovement : PlayerStateBase
    {
        public PlayerStateMovement(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.DefaultState = PlayerMain.PlayerStates.DefaultStates.Movement;

#endif

            #endregion Update State Info
        }

        protected override void InitializeSubState()
        {
            SetSubState(!Ctx.Mechanics.DefaultMovement.MovementCheckers.CheckerCases.IsGrounded ? Factory.InAir() : Factory.Grounded());
        }

        protected override void UpdateState()
        {
            //Update Checkers//
            Ctx.Mechanics.DefaultMovement.MovementCheckers.UpdateCheckers();
            
            //Rotation//
            Ctx.Mechanics.DefaultRotation.ExecuteRotations();
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.DefaultState = PlayerMain.PlayerStates.DefaultStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
        }

        #endregion State Methodes
    }
}