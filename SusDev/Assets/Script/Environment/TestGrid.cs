using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TestGrid
{
    public int _x;
    public int _y;
    public int _width;
    public int _height;
    public Vector3 _startPos;
    public float _cellSize;
    public int[,] _grid;
    public List<int[]> _vacant;
    public int _buildingType = 0;
    public TestGrid(int x, int y, Vector3 pos, int w, int h, float size)
    {
        _x = x;
        _y = y;
        _startPos = pos;
        _width = w;
        _height = h;
        _cellSize = size;
        _grid = new int[w, h];
        _vacant = new List<int[]>();
        //how many cells in each grid: if i=j=2, there should be 4 cells
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                /*              CreateText(_grid[i, j].ToString(), null, GetWorldPos(i, j));*/
                /*DrawQuadrant(new Vector3(pos.x + i * size, pos.y, pos.z + j * size), size, Color.red);*/
                Add2List(i, j);
            }
        }
        ShuffleList();
    }
    public void SetBuildingType(int input)
    {
        _buildingType = input;
    }
    public int GetBuildingType()
    {
        return _buildingType;
    }
    public void Add2List(int i, int j)
    {
        _vacant.Add(new int[] { i, j });
    }
    public void RemoveFromList(int i, int j)
    {
        int[] r = new int[] { i, j };
        foreach (var value in _vacant.ToList())
        {
            if (Enumerable.SequenceEqual(value, r))
            {
                _vacant.Remove(value);
            }
        }
    }
    public void ShuffleList()
    {
        for (int i = 0; i < _vacant.Count; i++)
        {
            var temp = _vacant[i];
            int index = Random.Range(i, _vacant.Count);
            _vacant[i] = _vacant[index];
            _vacant[index] = temp;
        }
    }
    public void DrawQuadrant(Vector3 pos, float size, Color c)
    {
        //(0,0) to (1,0)
        Debug.DrawLine(pos, pos + new Vector3(size, 0, 0), c, 200f);
        //(1,0) to (1,1)
        Debug.DrawLine(pos + new Vector3(size, 0, 0), pos + new Vector3(size, 0, size), c, 200f);
        //(0,1) to (1,1)
        Debug.DrawLine(pos + new Vector3(0, 0, size), pos + new Vector3(size, 0, size), c, 200f);
        //(0,0) to (0,1)
        Debug.DrawLine(pos, pos + new Vector3(0, 0, size), c, 200f);
    }
    public Vector3 GetWorldPos(int x, int z)
    {
        return new Vector3(_startPos.x + x * _cellSize, _startPos.y, _startPos.z + z * _cellSize);
    }
    private Vector2Int GetCordinate(Vector3 worldPos)
    {
        Vector2Int vector = new Vector2Int(Mathf.FloorToInt(worldPos.x / _cellSize), Mathf.FloorToInt(worldPos.z / _cellSize));
        return vector;
    }
    public void SetValue(Vector3 worldPos, int value)
    {
        int x = GetCordinate(worldPos).x;
        int z = GetCordinate(worldPos).y;
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            _grid[x, z] = value;
            RemoveFromList(x, z);
/*            Debug.Log(_vacant.Count);
            Debug.Log(x + " " + z + " value is " + _grid[x, z]);*/
        }
    }
}
