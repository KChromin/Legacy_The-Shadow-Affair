using System;
using System.Collections;
using System.Collections.Generic;
using ExperimentalFox.GameController.Settings;
using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player.Rotation
{
    public class PlayerRotationDefault : MonoBehaviour
    {
        #region Cache Assignments

        private PlayerInput.InputDataClass _input;
        private SettingsControls.CurrentSettingsClass _controlSettings;
        private PlayerPossibilities.DefaultPossibilitiesClass _possibilities;
        private Transform _playerTransform;
        private Transform _orientation;
        private Transform _headPivot;

        #endregion Cache Assignments

        #region Variables

        [Header("Look Parameters")]
        [Header("Max Angles")]
        [SerializeField, Range(0, 90)]
        private float maximalLookAngleUp;

        [SerializeField, Range(0, 90)]
        private float maximalLookAngleDown;

        [Header("Smoothing Values")]
        [SerializeField, Range(0, 0.05f)]
        private float smoothingSpeed;

        private float _verticalRotation;

        private Vector2 _smoothedInput;
        private Vector2 _smoothedInputCalculation;

        #endregion Variables

        #region Setup

        private void Awake()
        {
            //Setup PlayerMain//
            PlayerMain playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();

            SetupTransforms(playerMain);

            //Game Input//
            _input = playerMain.Input.InputData;

            //Setup control settings//
            _controlSettings = playerMain.Controllers.ControlsSettings.CurrentSettings;

            // Current Possibilities/
            _possibilities = playerMain.Data.Possibilities.DefaultPossibilities;
        }

        private void SetupTransforms(PlayerMain playerMain)
        {
            _playerTransform = playerMain.Components.Transform;
            _orientation = playerMain.MainObjects.Orientation.transform;
            _headPivot = playerMain.MainObjects.HeadPivot.transform;
        }

        #endregion Setup

        #region Methodes

        public void ExecuteRotations()
        {
            if (!_possibilities.CanRotate) return;

            //Update input, account sensitivity, and smooth it//
            _smoothedInput = SmoothInput(ProcessInput());

            //Execute rotations//
            VerticalRotation();
            HorizontalRotation();
        }

        #region Calculations

        private Vector2 ProcessInput()
        {
            //Update Input//
            Vector2 input = _input.LookInput;

            //Set Sensitvity//
            input = _controlSettings.SeparateAxisSensitivity ? GetSensitivity(input, _controlSettings.LookSensitivityXAxis, _controlSettings.LookSensitivityYAxis) : GetSensitivity(input, _controlSettings.LookSensitivity);

            //Invert Y-Axis//
            if (_controlSettings.InvertYAxis)
            {
                input.y = -input.y;
            }

            return input;
        }

        private Vector2 GetSensitivity(Vector2 input, float sensitivity)
        {
            return input * sensitivity;
        }

        private Vector2 GetSensitivity(Vector2 input, float sensitivityX, float sensitivityY)
        {
            return new Vector2(input.x * sensitivityX, input.y * sensitivityY);
        }

        private Vector2 SmoothInput(Vector2 processedInput)
        {
            return Vector2.SmoothDamp(_smoothedInput, processedInput, ref _smoothedInputCalculation, smoothingSpeed);
        }

        #endregion Calculations

        #region Vertical Rotation

        //Camera//
        private void VerticalRotation()
        {
            //Add current delta//
            _verticalRotation -= _smoothedInput.y;

            //Clamp between max angles//
            _verticalRotation = Mathf.Clamp(_verticalRotation, -maximalLookAngleDown, maximalLookAngleUp);

            //Update head pivot rotation//
            _headPivot.localRotation = Quaternion.Euler(_playerTransform.right * _verticalRotation);
        }

        #endregion Vertical Rotation

        #region Horizontal Rotation

        //Player orientation//
        private void HorizontalRotation()
        {
            _orientation.Rotate(_smoothedInput.x * _playerTransform.up);
        }

        #endregion Horizontal Rotation

        #endregion Methodes
    }
}