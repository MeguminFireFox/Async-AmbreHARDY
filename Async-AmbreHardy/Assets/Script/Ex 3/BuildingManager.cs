using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;

    private NativeArray<Building.Data> _buildingDataArray;
    private BuildingUpdateJob _job;
    private JobHandle _jobHandle;

    private void Awake()
    {
        _buildingDataArray = new(buildings.Count, Allocator.Persistent);

        for (int i = 0; i < buildings.Count; i++)
        {
            _buildingDataArray[i] = new Building.Data(buildings[i]);
        }

        _job = new BuildingUpdateJob()
        {
            BuildingDataArray = _buildingDataArray
        };
    }

    void Update()
    {
        _jobHandle = _job.Schedule(buildings.Count, 1);
    }

    void LateUpdate()
    {
        _jobHandle.Complete();
    }

    void OnDestroy()
    {
        _buildingDataArray.Dispose();
    }
}