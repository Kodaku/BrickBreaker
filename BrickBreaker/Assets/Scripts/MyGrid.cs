using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid
{
    private Cell[,] grid;
    private int m_rows;
    private int m_columns;

    public MyGrid(int rows, int columns)
    {
        m_rows = rows;
        m_columns = columns;
        PrepareGrid();
    }

    private void PrepareGrid()
    {
        grid = new Cell[m_rows, m_columns];
        for(int i = 0; i < m_rows; i++)
        {
            for(int j = 0; j < m_columns; j++)
            {
                grid[i, j] = new Cell(i, j);
            }
        }
    }

    public Cell CellAt(int row, int column)
    {
        if(row >= 0 && row < m_rows && column >= 0 && column < m_columns)
        {
            return grid[row, column];
        }
        return null;
    }

    public Cell WorldPointToCell(Vector3 position)
    {
        int row = Mathf.RoundToInt(position.y);
        int column = Mathf.RoundToInt(position.x);
        return grid[row, column];
    }

    public int rows
    {
        get { return m_rows; }
        set { m_rows = value; }
    }

    public int columns
    {
        get { return m_columns; }
        set { m_columns = value; }
    }
}
