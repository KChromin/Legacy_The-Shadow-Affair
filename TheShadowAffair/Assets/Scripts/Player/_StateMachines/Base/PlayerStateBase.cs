namespace ExperimentalFox.Player.StateMachine
{
    public abstract class PlayerStateBase
    {
        protected bool IsRootState { get; set; }
        private PlayerStateBase CurrentSubState { get; set; }
        private PlayerStateBase CurrentSuperState { get; set; }

        protected PlayerMain Ctx { get; set; }
        protected PlayerStateFactory Factory { get; set; }

        protected PlayerStateBase(PlayerMain currentContext, PlayerStateFactory stateFactory)
        {
            Ctx = currentContext;
            Factory = stateFactory;
        }

        //When Entering State//
        protected abstract void EnterState();

        //Install Sub-States//
        protected abstract void InitializeSubState();

        //Update Function//
        protected abstract void UpdateState();

        //Fixed Update Function//
        protected abstract void FixedUpdateState();

        //When exiting state//
        protected abstract void ExitState();

        //Check for state switching//
        protected abstract void CheckSwitchState();


        //Enter State, and Initialize SubState//
        public void EnterStates()
        {
            EnterState();

            InitializeSubState();
        }

        //Updates main and sub states//
        public void UpdateStates()
        {
            //Check for switch//
            CheckSwitchStates();

            //Update Functions//
            UpdateState();

            //Update All Sub States//
            CurrentSubState?.UpdateStates();
        }

        //Like Update but in fixed update//
        public void FixedUpdateStates()
        {
            //Update Functions//
            FixedUpdateState();

            //Update All Sub States//
            CurrentSubState?.FixedUpdateStates();
        }

        //Switches main and sub states//
        private void CheckSwitchStates()
        {
            //Check for switch//
            CheckSwitchState();

            CurrentSubState?.CheckSwitchStates();
        }

        //Exits main and sub states//
        private void ExitStates()
        {
            //Call exit state//
            ExitState();

            //Exit State in sub-states//
            CurrentSubState?.ExitStates();
        }

        protected void SwitchStates(PlayerStateBase newState)
        {
            //Exit actual state//
            ExitStates();

            if (IsRootState)
            {
                //Set new root//
                Ctx.CurrentState = newState;
                //Enter new state//
                Ctx.CurrentState.EnterStates();
            }
            else
            {
                CurrentSuperState?.SetSubState(newState);
            }
        }

        protected void SetSuperState(PlayerStateBase newSuperState)
        {
            CurrentSuperState = newSuperState;
        }

        protected void SetSubState(PlayerStateBase newSubState)
        {
            CurrentSubState = newSubState;
            CurrentSubState.SetSuperState(this);
            CurrentSubState.EnterStates();
        }
    }
}