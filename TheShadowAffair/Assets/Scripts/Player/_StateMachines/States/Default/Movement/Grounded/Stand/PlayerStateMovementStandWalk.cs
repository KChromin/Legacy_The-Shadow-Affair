using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.Player.Movement;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementStandWalk : PlayerStateBase
    {
        public PlayerStateMovementStandWalk(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.StandState = PlayerMain.PlayerStates.StandStates.Walk;

#endif

            #endregion Update State Info
        }

        protected override void InitializeSubState()
        {
        }

        protected override void UpdateState()
        {
            //Set proper physic material//
            SetMovePhysicMaterial();
        }

        protected override void FixedUpdateState()
        {
            Ctx.Mechanics.DefaultMovement.MovementExecution.MoveDefault(PlayerMovementDefaultCalculationHorizontal.MoveModes.Walk);
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.StandState = PlayerMain.PlayerStates.StandStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
            //When no input, then idle//
            if (!Ctx.Data.Cases.GeneralCases.IsGivingInput)
            {
                SwitchStates(Factory.StandIdle());
            }
            //When running, then walk//
            else if (Ctx.Data.Cases.DefaultCases.IsRunning)
            {
                SwitchStates(Factory.StandRun());
            }
        }

        #endregion State Methodes

        private void SetMovePhysicMaterial()
        {
            if (Ctx.Mechanics.DefaultMovement.MovementCheckers.CheckerCases.OnSlope)
            {
                Ctx.Mechanics.DefaultMovement.ComponentsController.UpdateProperties(PlayerMovementComponentsController.PhysicMaterialModes.DefaultMoveSlope);
            }
            else
            {
                Ctx.Mechanics.DefaultMovement.ComponentsController.UpdateProperties(PlayerMovementComponentsController.PhysicMaterialModes.DefaultMove);
            }
        }
    }
}