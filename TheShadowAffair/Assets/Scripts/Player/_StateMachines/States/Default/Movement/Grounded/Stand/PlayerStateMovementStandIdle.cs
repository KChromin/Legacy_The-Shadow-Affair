using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.Player.Movement;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementStandIdle : PlayerStateBase
    {
        public PlayerStateMovementStandIdle(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.StandState = PlayerMain.PlayerStates.StandStates.Idle;

#endif

            #endregion Update State Info

            //Set physic material//
            Ctx.Mechanics.DefaultMovement.ComponentsController.UpdateProperties(PlayerMovementComponentsController.PhysicMaterialModes.DefaultIdle);

            //Set horizontal calculation idle//
            Ctx.Mechanics.DefaultMovement.MovementExecution.SetIdleStates();
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

            Ctx.PlayerStateInfo.StandState = PlayerMain.PlayerStates.StandStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Data.Cases.GeneralCases.IsGivingInput)
            {
                //Running or Walking//
                SwitchStates(Ctx.Data.Cases.DefaultCases.IsRunning ? Factory.StandRun() : Factory.StandWalk());
            }
        }

        #endregion State Methodes
    }
}