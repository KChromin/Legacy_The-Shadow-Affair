using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.Player.Movement;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementGrounded : PlayerStateBase
    {
        public PlayerStateMovementGrounded(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GroundingState = PlayerMain.PlayerStates.GroundingStates.Grounded;

#endif

            #endregion Update State Info

            //Set drag//
            Ctx.Mechanics.DefaultMovement.ComponentsController.UpdateProperties(PlayerMovementComponentsController.DragModes.DefaultGrounded);
        }

        protected override void InitializeSubState()
        {
            SetSubState(Factory.Stand());
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            //Apply Gravity//
            Ctx.Mechanics.DefaultMovement.MovementExecution.ApplyGravityGrounded();
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GroundingState = PlayerMain.PlayerStates.GroundingStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Mechanics.DefaultMovement.MovementCheckers.CheckerCases.IsGrounded)
            {
                SwitchStates(Factory.InAir());
            }
        }

        #endregion State Methodes
    }
}