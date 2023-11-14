using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateMovementCrouch : PlayerStateBase
    {
        public PlayerStateMovementCrouch(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        #region State Methods

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GroundState = PlayerMain.PlayerStates.GroundStates.Crouching;

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

            Ctx.PlayerStateInfo.GroundState = PlayerMain.PlayerStates.GroundStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
         
        }

        #endregion State Methods
    }
}