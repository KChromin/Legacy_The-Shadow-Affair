using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementStand : PlayerStateBase
    {
        public PlayerStateMovementStand(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GroundState = PlayerMain.PlayerStates.GroundStates.Standing;

#endif

            #endregion Update State Info
        }

        protected override void InitializeSubState()
        {
            SetSubState(Factory.StandIdle());
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

            Ctx.PlayerStateInfo.GroundState = PlayerMain.PlayerStates.GroundStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
        }

        #endregion State Methodes
    }
}