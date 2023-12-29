using DI;
using Unit.Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Unit
{
    public class UnitMarker : MonoBehaviour
    {
        private InputSystem inputSystem;
        private Entity targetEntity;

        private void Start()
        {
            Debug.Log($"Unit Marker => {inputSystem.testText}");
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                targetEntity = GetRandomeEntity();

            if (targetEntity != Entity.Null)
            {
                Vector3 position = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorld>(targetEntity).Position;
                transform.position = position;
            }
        }

        private Entity GetRandomeEntity()
        {
            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(UnitTag));
            NativeArray<Entity> entityNativeArray = entityQuery.ToEntityArray(Allocator.Temp);
            if (entityNativeArray.Length > 0)
                return entityNativeArray[Random.Range(0, entityNativeArray.Length)];
            return Entity.Null;
        }

        [Inject]
        private void Inject(InputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }

    }
}
