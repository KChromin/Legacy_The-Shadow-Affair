using System.Collections.Generic;

namespace ExperimentalFox.Player.StateMachine
{
    /*      How to Add State?
    0. Create new state script
    1. Add a new state to enum
    2. Assign it to directory
    3. Create new State under
    */

    public class PlayerStateFactory
    {
        #region Cache

        private readonly Dictionary<PlayerStates, PlayerStateBase> _states = new Dictionary<PlayerStates, PlayerStateBase>();

        public PlayerStateFactory(PlayerMain context)
        {
            _states[PlayerStates.Disabled] = new PlayerStateDisabled(context, this);

            #region Default

            _states[PlayerStates.Default] = new PlayerStateDefault(context, this);

            #region Movement

            _states[PlayerStates.Movement] = new PlayerStateMovement(context, this);
            _states[PlayerStates.Grounded] = new PlayerStateMovementGrounded(context, this);
            _states[PlayerStates.Stand] = new PlayerStateMovementStand(context, this);
            _states[PlayerStates.StandIdle] = new PlayerStateMovementStandIdle(context, this);
            _states[PlayerStates.StandWalk] = new PlayerStateMovementStandWalk(context, this);
            _states[PlayerStates.StandRun] = new PlayerStateMovementStandRun(context, this);
            _states[PlayerStates.Crouch] = new PlayerStateMovementCrouch(context, this);
            _states[PlayerStates.CrouchIdle] = new PlayerStateMovementCrouchIdle(context, this);
            _states[PlayerStates.CrouchWalk] = new PlayerStateMovementCrouchWalk(context, this);
            _states[PlayerStates.InAir] = new PlayerStateMovementInAir(context, this);
            _states[PlayerStates.InAirFall] = new PlayerStateMovementInAirFall(context, this);
            _states[PlayerStates.InAirRise] = new PlayerStateMovementInAirRise(context, this);

            #endregion Movement

            #endregion Default
        }

        #endregion Cache

        #region State Change Methodes

        public PlayerStateBase Disabled()
        {
            return _states[PlayerStates.Disabled];
        }

        #region Default

        public PlayerStateBase Default()
        {
            return _states[PlayerStates.Default];
        }

        #region Movement

        public PlayerStateBase Movement()
        {
            return _states[PlayerStates.Movement];
        }

        public PlayerStateBase Grounded()
        {
            return _states[PlayerStates.Grounded];
        }

        public PlayerStateBase Stand()
        {
            return _states[PlayerStates.Stand];
        }

        public PlayerStateBase StandIdle()
        {
            return _states[PlayerStates.StandIdle];
        }

        public PlayerStateBase StandWalk()
        {
            return _states[PlayerStates.StandWalk];
        }

        public PlayerStateBase StandRun()
        {
            return _states[PlayerStates.StandRun];
        }

        public PlayerStateBase Crouch()
        {
            return _states[PlayerStates.Crouch];
        }

        public PlayerStateBase CrouchIdle()
        {
            return _states[PlayerStates.CrouchIdle];
        }

        public PlayerStateBase CrouchWalk()
        {
            return _states[PlayerStates.CrouchWalk];
        }

        public PlayerStateBase InAir()
        {
            return _states[PlayerStates.InAir];
        }

        public PlayerStateBase InAirFall()
        {
            return _states[PlayerStates.InAirFall];
        }

        public PlayerStateBase InAirRise()
        {
            return _states[PlayerStates.InAirRise];
        }

        #endregion Movement

        #endregion Default

        #endregion State Change Methodes
    }

    public enum PlayerStates
    {
        Disabled,

        #region Default

        Default,

        #region Movement

        Movement,
        Grounded,
        Stand,
        StandIdle,
        StandWalk,
        StandRun,
        Crouch,
        CrouchIdle,
        CrouchWalk,
        InAir,
        InAirFall,
        InAirRise

        #endregion Movement

        #endregion Default
    }
}