using System;
using UnityEngine;

namespace ExperimentalFox.Player.Movement
{
    public class PlayerMovementComponentsController : MonoBehaviour
    {
        #region Variables

        private CapsuleCollider _capsuleCollider;
        private Rigidbody _rigidbody;

        #region Drag

        [Header("Drag")]
        [SerializeField]
        DragValuesStruct dragValues;

        [Serializable]
        public struct DragValuesStruct
        {
            [field: Header("Default")]
            [field: SerializeField]
            public float DefaultGrounded { get; set; }

            [field: SerializeField]
            public float DefaultInAir { get; set; }
        }

        #endregion Drag

        #region Physic Materials

        [Header("Physic Materials")]
        [SerializeField]
        private PhysicMaterialsStruct physicMaterials;

        [Serializable]
        public struct PhysicMaterialsStruct
        {
            [field: Header("Default", order = 1)]
            [field: SerializeField]
            public PhysicMaterial DefaultIdle { get; set; }

            [field: SerializeField]
            public PhysicMaterial DefaultMove { get; set; }

            [field: SerializeField]
            public PhysicMaterial DefaultMoveSlope { get; set; }
        }

        #endregion Physic Materials

        #region External modes to set

        //for external methods to set local variable as new value// 

        public enum DragModes
        {
            DefaultGrounded,
            DefaultInAir
        }

        public enum PhysicMaterialModes
        {
            DefaultIdle,
            DefaultMove,
            DefaultMoveSlope
        }

        #endregion External Modes to Set

        #endregion Variables

        #region Setup

        public void Setup(CapsuleCollider colliderComponent, Rigidbody rigidbodyComponent)
        {
            _capsuleCollider = colliderComponent;
            _rigidbody = rigidbodyComponent;
        }

        #endregion Setup

        #region Public methodes

        public void UpdateProperties(DragModes dragMode)
        {
            AssignValueDrag(dragMode);
        }

        public void UpdateProperties(PhysicMaterialModes physicMaterialMode)
        {
            AssignValuePhysicMaterial(physicMaterialMode);
        }


        public void UpdateProperties(DragModes dragMode, PhysicMaterialModes physicMaterialMode)
        {
            AssignValueDrag(dragMode);
            AssignValuePhysicMaterial(physicMaterialMode);
        }

        #endregion Public methodes

        #region Set Drag

        private void AssignValueDrag(DragModes dragMode)
        {
            switch (dragMode)
            {
                case DragModes.DefaultGrounded:
                    SetDrag(dragValues.DefaultGrounded);
                    break;
                case DragModes.DefaultInAir:
                    SetDrag(dragValues.DefaultInAir);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dragMode), dragMode, null);
            }
        }

        private void SetDrag(float newDrag)
        {
            _rigidbody.drag = newDrag;
        }

        #endregion Set Drag

        #region Set PhysicMaterial

        private void AssignValuePhysicMaterial(PhysicMaterialModes physicMaterialMode)
        {
            switch (physicMaterialMode)
            {
                case PhysicMaterialModes.DefaultIdle:
                    SetPhysicMaterial(physicMaterials.DefaultIdle);
                    break;
                case PhysicMaterialModes.DefaultMove:
                    SetPhysicMaterial(physicMaterials.DefaultMove);
                    break;
                case PhysicMaterialModes.DefaultMoveSlope:
                    SetPhysicMaterial(physicMaterials.DefaultMoveSlope);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(physicMaterialMode), physicMaterialMode, null);
            }
        }

        private void SetPhysicMaterial(PhysicMaterial newMaterial)
        {
            if (_capsuleCollider.material != newMaterial)
            {
                _capsuleCollider.material = newMaterial;
            }
        }

        #endregion Set PhysicMaterial
    }
}