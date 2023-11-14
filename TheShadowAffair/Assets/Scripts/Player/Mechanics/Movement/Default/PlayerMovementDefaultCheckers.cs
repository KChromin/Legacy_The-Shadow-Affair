using System;
using ExperimentalFox.Player.Data;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementDefaultCheckers : MonoBehaviour
    {
        #region Variables

        private Transform _playerTransform;
        private Transform _checkersTransform;
        private Rigidbody _playerRigidbody;

        #region Ground Check

        [Header("Ground Check Properties")]
        [SerializeField]
        private GroundCheckStruct groundCheck;

        [Serializable]
        public struct GroundCheckStruct
        {
            [field: SerializeField]
            public float CheckDistance { get; set; }

            [field: SerializeField]
            public float CheckRadius { get; set; }

            public RaycastHit CheckHit;
        }

        #endregion Ground Check

        #region Slope Check

        [Header("Slope Check Properties")]
        [SerializeField]
        private SlopeCheckStruct slopeCheck;

        [Serializable]
        public struct SlopeCheckStruct
        {
            [field: Header("Min & Max Slope Angle")]
            [field: SerializeField, Range(0, 90)]
            public float MinSlopeAngle { get; set; }

            [field: SerializeField, Range(0, 90)]
            public float MaxSlopeAngle { get; set; }

            public RaycastHit CheckHit;
        }

        #endregion Slope Check

        #region Checker Cases

        [field: Header("Checker Cases")]
        [field: SerializeField]
        public CheckerCasesClass CheckerCases { get; set; }

        [Serializable]
        public class CheckerCasesClass
        {
            [field: Header("Grounding")]
            [field: SerializeField]
            public bool IsGrounded { get; set; }

            [field: Header("On Slope")]
            [field: SerializeField]
            public bool OnSlope { get; set; }

            [field: SerializeField]
            public bool SlopeIsTooSteep { get; set; }
        }

        #endregion Public Cases

        #region Gizmos

#if UNITY_EDITOR

        [Header("Gizmos")]
        [SerializeField]
        private DebugGizmosStruct debugGizmos;

        [Serializable]
        public struct DebugGizmosStruct
        {
            [field: SerializeField]
            public bool GroundCheck { get; set; }

            [field: SerializeField]
            public bool SlopeCheck { get; set; }

            [field: SerializeField]
            public bool CellingCheck { get; set; }
        }

#endif

        #endregion Gizmos

        [field: Space(10), SerializeField]
        private LayerMask GroundLayers { get; set; }

        private const float AdditionalSlopeCheckOffsetY = 0.05f;

        #endregion Variables

        #region Setup

        public void Setup(Transform playerTransform, Transform checkersTransform, Rigidbody playerRigidbody)
        {
            _playerTransform = playerTransform;
            _checkersTransform = checkersTransform;
            _playerRigidbody = playerRigidbody;
        }

        #endregion Setup

        #region Public Methodes

        public void UpdateCheckers()
        {
            CheckerCases.IsGrounded = GroundCheck();
            CheckerCases.OnSlope = IsOnSlopeCheck();
        }

        public Vector3 GetSlopeNormal()
        {
            return slopeCheck.CheckHit.normal;
        }

        public Vector3 GetGroundDirection()
        {
            return (slopeCheck.CheckHit.point - _checkersTransform.position).normalized;
        }

        #endregion Public Methodes

        #region Methodes

        #region Ground Check

        private bool GroundCheck()
        {
            return Physics.SphereCast(_checkersTransform.position, groundCheck.CheckRadius, -_playerTransform.up, out groundCheck.CheckHit, groundCheck.CheckDistance);
        }

        private bool IsOnSlopeCheck()
        {
            //If straight down ray not working, or not on slope, check ground hit slope//
            Vector3 origin = new Vector3(0, AdditionalSlopeCheckOffsetY, 0) + groundCheck.CheckHit.point;

            if (Physics.Raycast(origin, -_playerTransform.up, out slopeCheck.CheckHit, AdditionalSlopeCheckOffsetY + 0.04f, GroundLayers))
            {
                return InSlopeAngleRange(slopeCheck.CheckHit.normal);
            }

            return InSlopeAngleRange(groundCheck.CheckHit.normal);
        }


        private bool InSlopeAngleRange(Vector3 hitNormal)
        {
            float currentAngle = Vector3.Angle(hitNormal, _playerTransform.up);

            CheckerCases.SlopeIsTooSteep = slopeCheck.MaxSlopeAngle <= currentAngle;

            return slopeCheck.MinSlopeAngle <= currentAngle;
        }

        #endregion Ground Check

        #endregion Methodes

        #region Gizmos

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            DrawGizmoGroundCheck();
            DrawGizmoSlopeCheck();
        }

        private void DrawGizmoGroundCheck()
        {
            if (!debugGizmos.GroundCheck) return;

            Vector3 position = _checkersTransform.position;
            Vector3 down = -_playerTransform.up;

            if (CheckerCases.IsGrounded)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, groundCheck.CheckHit.point);
                Gizmos.DrawWireSphere(position + (down * groundCheck.CheckHit.distance), groundCheck.CheckRadius);
                return;
            }

            //When not grounded//
            Gizmos.color = Color.red;

            Gizmos.DrawLine(position, position + (down * groundCheck.CheckDistance));
            Gizmos.DrawWireSphere(position + (down * groundCheck.CheckDistance), groundCheck.CheckRadius);
        }

        private void DrawGizmoSlopeCheck()
        {
            if (!debugGizmos.SlopeCheck) return;

            Vector3 position = _checkersTransform.position;

            if (CheckerCases.OnSlope)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, slopeCheck.CheckHit.point);
            }
        }

#endif

        #endregion Gizmos
    }
}