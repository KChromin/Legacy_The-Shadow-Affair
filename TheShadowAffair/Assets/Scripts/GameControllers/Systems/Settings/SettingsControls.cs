using System;
using UnityEngine;

namespace ExperimentalFox.GameController.Settings
{
    public class SettingsControls : MonoBehaviour
    {
        [field: Header("Current Settings")]
        [field: SerializeField]
        public CurrentSettingsClass CurrentSettings { get; private set; }

        [Serializable]
        public class CurrentSettingsClass
        {
            [field: Header("Look")]
            [field: SerializeField]
            public float LookSensitivity { get; private set; }

            [field: Space]
            [field: SerializeField]
            public bool SeparateAxisSensitivity { get; private set; }

            [field: SerializeField]
            public float LookSensitivityXAxis { get; private set; }

            [field: SerializeField]
            public float LookSensitivityYAxis { get; private set; }

            [field: Space]
            [field: SerializeField]
            public bool InvertYAxis { get; private set; }
        }
    }
}