using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.Data
{
    public class PlayerCases : MonoBehaviour
    {
        #region General Cases

        [field: Header("General Cases")]
        [field: SerializeField]
        public GeneralCasesClass GeneralCases { get; private set; }

        [Serializable]
        public class GeneralCasesClass
        {
            [field: Header("General Cases")]
            [field: SerializeField]
            public bool IsGivingInput { get; set; }

            [field: SerializeField]
            public bool IsChangingPosition { get; set; }

            [field: SerializeField]
            public bool IsMovingBackward { get; set; }
            
            [field: SerializeField]
            public bool IsLookingAround { get; set; }
        }

        #endregion General Cases

        #region Default Movement

        [field: Header("Default Cases")]
        [field: SerializeField]
        public DefaultCasesClass DefaultCases { get; private set; }

        [Serializable]
        public class DefaultCasesClass
        {
            [field: Header("Is Running")]
            [field: SerializeField]
            public bool IsRunning { get; set; }

            [field: Header("Jumped")]
            [field: SerializeField]
            public bool Jumped { get; set; }
        }

        #endregion Default Movement
    }
}