using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacement2D : MonoBehaviour
{
    [SerializeField] private GridManager2D gridManager;
    [SerializeField] private Tilemap buildableTilemap;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject towerPrefab;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    private void TryPlaceTower()
    {
        if (mainCamera == null || gridManager == null
            || buildableTilemap == null
            || towerPrefab == null) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell = gridManager.WorldToCell(new Vector3(mouseWorld.x, mouseWorld.y, 0));

        Debug.Log(cell);

        if (gridManager.IsOccupied(cell) || !buildableTilemap.HasTile(cell))
            return;

        if(gridManager.TryOccupyCell(cell))
        {
            Vector3 position = gridManager.GetCellCenter(cell);
            Instantiate(towerPrefab, position, Quaternion.identity);
        }
    }
}
