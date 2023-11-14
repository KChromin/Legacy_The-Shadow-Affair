using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EFS.UI.Test
{
    public class Test_UISpeed : MonoBehaviour
    {
        private UIDocument _speedDocument;
        private Label _currentSpeedText;
        private Rigidbody _playerRigidbody;

        private void Awake()
        {
            _speedDocument = GetComponent<UIDocument>();
            _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            _currentSpeedText = _speedDocument.rootVisualElement.Q<Label>("SpeedText");
        }

        private void FixedUpdate()
        {
            _currentSpeedText.text = _playerRigidbody.velocity.magnitude.ToString("f2");
        }
    }
}