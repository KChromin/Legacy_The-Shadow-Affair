using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExperimentalFox.Player.Data
{
    public class PlayerModifiers : MonoBehaviour
    {
        #region Movement

        [field: Header("Movement", order = 0)]

        #region Default

        [field: Header("Default", order = 1)]
        [field: SerializeField]
        public MovementDefaultClass MovementDefault { get; private set; }

        [Serializable]
        public class MovementDefaultClass
        {
            [field: Header("Horizontal", order = 0)]
            [field: Header("Max Speed", order = 1)]
            [field: SerializeField, Range(0, 10)]
            public float MaximalSpeedMultiplier { get; set; }

            [field: Header("Acceleration Speed")]
            [field: SerializeField, Range(0, 10)]
            public float AccelerationForceMultiplier { get; set; }

            [field: Space, Header("Vertical", order = 0)]
            [field: Header("Gravity", order = 1)]
            [field: SerializeField, Range(0, 10)]
            public float GravityForceMultiplier { get; set; }

            [field: SerializeField, Range(0, 10)]
            public float GravityGroundingForceMultiplier { get; set; }
            
            [field: SerializeField, Range(0, 10)]
            public float GravitySteepSlopeForceMultiplier { get; set; }
        }

        #endregion Default

        #endregion Movement
    }
}