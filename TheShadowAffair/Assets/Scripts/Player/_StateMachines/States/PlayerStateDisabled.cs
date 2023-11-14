using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateDisabled : PlayerStateBase
    {
        public PlayerStateDisabled(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
            IsRootState = true;
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GeneralState = PlayerMain.PlayerStates.GeneralStates.Disabled;

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
           
        }

        protected override void CheckSwitchState()
        {
           
        }

        #endregion State Methodes
    }
}