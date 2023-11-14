using System;
using UnityEngine;
using System.Collections;
using ExperimentalFox.GameController.Settings;
using ExperimentalFox.Player.StateMachine;
using ExperimentalFox.Player.Rotation;
using ExperimentalFox.Player.Movement;
using ExperimentalFox.Player.Data;


namespace ExperimentalFox.Player
{
    [SelectionBase]
    public class PlayerMain : MonoBehaviour
    {
        #region State Machine

        public PlayerStateBase CurrentState { get; set; }
        private PlayerStateFactory StateFactory { get; set; }

        private bool _stateMachineIsSetup = false;

        #region Current State Info

#if UNITY_EDITOR
        [field: Header("Current States")]
        [field: SerializeField]
        public PlayerStates PlayerStateInfo { get; set; }

        [Serializable]
        public class PlayerStates
        {
            [field: Header("General")]
            [field: SerializeField]
            public GeneralStates GeneralState { get; set; }

            public enum GeneralStates
            {
                Disabled,
                Default
            }

            #region Default

            [field: Header("Default State")]
            [field: SerializeField]
            public DefaultStates DefaultState { get; set; }

            public enum DefaultStates
            {
                Disabled,
                Movement
            }

            #region Movement State

            [field: Header("Grounding State")]
            [field: SerializeField]
            public GroundingStates GroundingState { get; set; }

            public enum GroundingStates
            {
                Disabled,
                Grounded,
                InAir
            }

            #region Grounded

            [field: Header("Ground State")]
            [field: SerializeField]
            public GroundStates GroundState { get; set; }

            public enum GroundStates
            {
                Disabled,
                Standing,
                Crouching
            }

            #region Standing

            [field: Header("Standing State")]
            [field: SerializeField]
            public StandStates StandState { get; set; }

            public enum StandStates
            {
                Disabled,
                Idle,
                Walk,
                Run
            }

            #endregion Standing

            #region Crouching

            [field: Header("Crouching State")]
            [field: SerializeField]
            public CrouchStates CrouchState { get; set; }

            public enum CrouchStates
            {
                Disabled,
                Idle,
                Walk
            }

            #endregion Crouching

            #endregion Grounded

            #region InAir

            [field: Header("InAir State")]
            [field: SerializeField]
            public InAirStates InAirState { get; set; }

            public enum InAirStates
            {
                Disabled,
                Rise,
                Fall
            }

            #endregion InAir

            #endregion Movement State

            #endregion Default
        }
#endif

        #endregion Current State Info

        #endregion State Machine

        #region Input

        //Input System//
        public PlayerInput Input { get; private set; }

        #endregion Input

        #region Player Main Objects

        //Player main objects for easier caching//
        [field: Header("Main Objects")]
        [field: SerializeField]
        public MainObjectsClass MainObjects { get; private set; }

        [Serializable]
        public class MainObjectsClass
        {
            [field: Header("Game Controllers")]
            [field: SerializeField]
            public GameObject GameControllers { get; set; }

            [field: Header("Camera")]
            [field: SerializeField]
            public GameObject CameraHolder { get; set; }

            [field: Header("Orientation")]
            [field: SerializeField]
            public GameObject Orientation { get; set; }

            [field: Header("Head - Pivot")]
            [field: SerializeField]
            public GameObject HeadPivot { get; set; }

            [field: Header("Checkers")]
            [field: SerializeField]
            public GameObject Checkers { get; set; }

            [field: Header("Mechanics")]
            [field: SerializeField]
            public GameObject MechanicsHolder { get; set; }
        }

        #endregion Player Main Objects

        #region Mechanic Controllers

        [field: Header("Mechanics")]
        [field: SerializeField]
        public MechanicsClass Mechanics { get; private set; }

        [Serializable]
        public class MechanicsClass
        {
            [field: Header("Default Rotation")]
            [field: SerializeField]
            public PlayerRotationDefault DefaultRotation { get; private set; }

            [field: Header("Default Movement")]
            [field: SerializeField]
            public PlayerMovementDefault DefaultMovement { get; private set; }
        }

        #endregion Mechanic Controllers

        #region Game Controllers

        [field: Header("Game Controllers")]
        [field: SerializeField]
        public GameControllersClass Controllers { get; private set; }

        [Serializable]
        public class GameControllersClass
        {
            [field: Header("Settings")]
            [field: SerializeField]
            public SettingsControls ControlsSettings { get; set; }
        }

        #endregion Game Controllers

        #region Components

        //Player main objects for easier caching//
        [field: Header("Components")]
        [field: SerializeField]
        public ComponentsClass Components { get; private set; }

        [Serializable]
        public class ComponentsClass
        {
            [field: Header("Transform")]
            [field: SerializeField]
            public Transform Transform { get; set; }

            [field: Header("Rigidbody")]
            [field: SerializeField]
            public Rigidbody Rigidbody { get; set; }

            [field: Header("Collider")]
            [field: SerializeField]
            public CapsuleCollider Collider { get; set; }
        }

        #endregion Components

        #region Data

        [field: Header("Data")]
        [field: SerializeField]
        public DataClass Data { get; private set; }

        [Serializable]
        public class DataClass
        {
            [field: Header("Possibilities")]
            [field: SerializeField]
            public PlayerPossibilities Possibilities { get; set; }

            [field: Header("Modifiers")]
            [field: SerializeField]
            public PlayerModifiers Modifiers { get; set; }

            [field: Header("Cases")]
            [field: SerializeField]
            public PlayerCases Cases { get; set; }
        }

        #endregion Data

        #region Methodes

        #region Setup

        private void Awake()
        {
            SetupInput();
            GameControllerSetups();
            MainObjectsSetups();
            ComponentSetups();
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            StateMachineSetup();
            _stateMachineIsSetup = true;
        }

        private void StateMachineSetup()
        {
            //Setup State Factory//
            StateFactory = new PlayerStateFactory(this);

            //Set Default State//
            CurrentState = StateFactory.Default();
            CurrentState.EnterStates();
        }

        void SetupInput()
        {
            Input = GetComponent<PlayerInput>();
        }

        private void GameControllerSetups()
        {
            MainObjects.GameControllers = GameObject.FindGameObjectWithTag("GameController");

            SetupGameControllers(MainObjects.GameControllers);
        }

        private void SetupGameControllers(GameObject gameControllerObject)
        {
            Controllers.ControlsSettings = gameControllerObject.GetComponentInChildren<SettingsControls>();
        }

        private void MainObjectsSetups()
        {
            //Setup Camera Holder//
            MainObjects.CameraHolder = GameObject.FindGameObjectWithTag("CameraHolder");
        }

        private void ComponentSetups()
        {
            //Player Components//
            Components.Transform = GetComponent<Transform>();
            Components.Rigidbody = GetComponent<Rigidbody>();
            Components.Collider = GetComponent<CapsuleCollider>();
        }

        #endregion Setup

        #region Updates

        private void Update()
        {
            if (!_stateMachineIsSetup) return;

            CurrentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            if (!_stateMachineIsSetup) return;

            CurrentState.FixedUpdateStates();
        }

        #endregion Updates

        #endregion Methodes
    }
}