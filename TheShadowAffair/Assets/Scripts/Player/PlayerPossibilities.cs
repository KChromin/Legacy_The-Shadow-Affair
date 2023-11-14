using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.Data
{
    public class PlayerPossibilities : MonoBehaviour
    {
        #region Default State Possibilities

        [field: Header("Default Possibilities")]
        [field: SerializeField]
        public DefaultPossibilitiesClass DefaultPossibilities { get; private set; }

        [Serializable]
        public class DefaultPossibilitiesClass
        {
            [field: Header("Rotation")]
            [field: SerializeField]
            public bool CanRotate { get; set; }

            [field: Header("Movement")]
            [field: SerializeField]
            public bool CanMove { get; set; }
        }

        #endregion Default State Possibilities
    }
}