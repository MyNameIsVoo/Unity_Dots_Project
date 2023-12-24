using Unity.Entities;
using UnityEngine;
using Unit.Component;

namespace Unit
{
    public class UnitAuthoring : MonoBehaviour
    {
        #region Attributes

        [SerializeField] private float movementSpeed;

        #endregion

        #region FUNCTIONS

        public float MovementSpeed
        {
            get => movementSpeed;
        }

        #endregion
    }

    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitBase());

            AddComponent(entity, new UnitMovements
            {
                Speed = authoring.MovementSpeed
            });
        }
    }
}
