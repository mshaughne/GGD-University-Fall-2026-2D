using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager2D : MonoBehaviour
{
	[SerializeField] private Grid grid;
	[SerializeField] private int width = 18;
	[SerializeField] private int height = 10;
	[SerializeField] private Vector3Int originPoint;

	private bool[,] occupiedCells;

	private void Awake()
	{
		if (grid != null)
		{
			grid = GetComponent<Grid>();
		}

		occupiedCells = new bool[width, height];
	}

	public Vector3Int WorldToCell(Vector3 worldPos)
	{
		return grid.WorldToCell(worldPos);
	}

	public Vector3 GetCellCenter(Vector3Int cell)
	{
		Vector3 corner = grid.CellToWorld(cell);

		return new Vector3(corner.x + grid.cellSize.x * 0.5f,
			corner.y + grid.cellSize.y * 0.5f,
			grid.transform.position.z);
	}

	public bool IsInsideGrid(Vector3Int cell)
	{
		return cell.x >= originPoint.x && cell.x < width + originPoint.x && cell.y >= originPoint.y && cell.y < height + originPoint.y;
	}

	public bool IsOccupied(Vector3Int cell)
	{
		if (!IsInsideGrid(cell))
			return true;

		Vector3Int index = CellToIndex(cell);
		return occupiedCells[index.x, index.y];
	}

	public bool TryOccupyCell(Vector3Int cell)
	{
		if (IsOccupied(cell))
			return false;

        Vector3Int index = CellToIndex(cell);
        occupiedCells[index.x, index.y] = true;
		return true;
	}

	public void FreeCell(Vector3Int cell)
	{
		if(IsInsideGrid(cell))
		{
            Vector3Int index = CellToIndex(cell);
            occupiedCells[index.x, index.y] = false;
		}
	}

	private Vector3Int CellToIndex(Vector3Int cell)
	{
		return new Vector3Int(cell.x - originPoint.x, cell.y - originPoint.y, cell.z - originPoint.z);
	}
}
