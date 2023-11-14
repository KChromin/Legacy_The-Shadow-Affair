using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.StateMachine
{
    public class PlayerStateDefault : PlayerStateBase
    {
        public PlayerStateDefault(PlayerMain currentContext, PlayerStateFactory stateFactory) : base(currentContext, stateFactory)
        {
            IsRootState = true;
        }

        #region State Methodes

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GeneralState = PlayerMain.PlayerStates.GeneralStates.Default;

#endif

            #endregion Update State Info
        }

        protected override void InitializeSubState()
        {
            SetSubState(Factory.Movement());
        }

        protected override void UpdateState()
        {
            //Update Input//
            Ctx.Input.UpdateInputData();
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR

            Ctx.PlayerStateInfo.GeneralState = PlayerMain.PlayerStates.GeneralStates.Disabled;

#endif

            #endregion Update State Info
        }

        protected override void CheckSwitchState()
        {
            //SwitchStates(Factory.Default());
        }

        #endregion State Methodes
    }
}