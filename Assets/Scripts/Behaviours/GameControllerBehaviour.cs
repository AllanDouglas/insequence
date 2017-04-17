using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class GameControllerBehaviour : MonoBehaviour
{
    #region Inspector
    [Header("Externals")]
    [SerializeField]
    private NumberSpawnerBehaviour _numberSpawner;
    [Header("Grid preparation")]
    [SerializeField]
    private float margin;
    #endregion

    #region Variables
    private Grid.Grid _grid;
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        Setup();
    }

    private void OnDrawGizmos()
    {
        Vector2[,] gridPositions = new Vector2[5, 5];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                gridPositions[x, y] = new Vector2(x * margin, y * margin);

                Gizmos.DrawCube(gridPositions[x, y], Vector2.one);

            }
        }
    }

    #endregion

    private void Setup()
    {
        // create the grid
        Vector2[,] gridPositions = new Vector2[5, 5];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                gridPositions[x, y] = new Vector2(x * margin, y * margin);
            }
        }

        this._grid = new Grid.Grid(gridPositions);

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int column = (int)position.x;
            int line = (int)position.y;

            if (column < 0) column = 0;
            if (column > 4) column = 4;

            if (line < 0) line = 0;
            if (line > 4) line = 4;

            // tenta fazer um match

            IGridPosition touchedPosition = _grid.GetPosition(column, line);

            if (touchedPosition.Number != null)
            {
                ICollection<IGridPosition> matchs = _grid.Match(touchedPosition);
                if (matchs != null && matchs.Count > 0)
                {
                    foreach (var item in matchs)
                    {
                        item.Number.Release();
                        item.Clear();

                    }
                    Repositionate();
                    return;
                }
                
            }

            IGridPosition freePosition = this._grid.GetFreePosition(column);

            if (freePosition == null) return;

            NumberBehaviour number = this._numberSpawner.GetNumber() as NumberBehaviour;

            this._grid.SetNumber(freePosition, number);

            number.transform.MoveTo(freePosition.Position, 1f);

            Debug.Log("posicao encontrada " + column);
        }
    }

    /// <summary>
    /// Repositionate the positions
    /// </summary>
    public void Repositionate()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                IGridPosition position = _grid.GetPosition(i, j);

                if (position.IsFree()) continue;

                IGridPosition freePosition = _grid.GetFreePosition(i);

                if (freePosition != null)
                {
                    if (freePosition.Position.y > position.Position.y) continue;

                    NumberBehaviour number = position.Number as NumberBehaviour;

                    number.transform.MoveTo(freePosition.Position, .1f);

                    _grid.ChangePosition(position, freePosition);
                }
            }
        }


    }

}
