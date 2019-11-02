using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class ECSManager : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    private EntityManager entityManager;
    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.Active.EntityManager;
        Entity entity = entityManager.CreateEntity(
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(Rotation),
            typeof(Scale)
        );

        entityManager.SetSharedComponentData(entity, new RenderMesh{
            mesh = mesh,
            material = material
        });
    }
}

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        // 위로 움직이는 코드
        Entities.ForEach((ref Translation Translation) =>{
            float moveSpeed = 1f;
            Translation.Value.y += moveSpeed * Time.deltaTime;
        });
    }
}

public class RotatorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation) =>
        {
            rotation.Value = Quaternion.Euler(0, 0, math.PI * Time.realtimeSinceStartup);
        });
    }
}

public class ScalerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Scale scale) =>
        {
            scale.Value += 1f * Time.deltaTime;
        });
    }
}