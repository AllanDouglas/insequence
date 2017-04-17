using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Grid
    {
        #region Variables
        private GridPosition[,] positions;

        private int columns = 5, lines = 5;
        #endregion

        #region Constructors
        /// <summary>
        /// A new instace of Grid with 5x5
        /// </summary>
        public Grid()
        {
            positions = new GridPosition[columns, lines];
        }

        /// <summary>
        /// A new instance of Grid
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="lines"></param>
        public Grid(Vector2[,] positions)
        {
            this.columns = positions.GetLength(0);
            this.lines = positions.GetLength(1);

            Initialize(positions);

        }
        #endregion

        #region Interface Methods

        /// <summary>
        /// Init the grid
        /// </summary>
        /// <param name="positions"></param>
        public void Initialize(Vector2[,] positions)
        {
            this.positions = new GridPosition[this.columns, this.lines];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < lines; y++)
                {
                    this.positions[x, y] = new GridPosition(positions[x, y], null);
                }
            }

        }

        /// <summary>
        /// Get the first position free on the column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IGridPosition GetFreePosition(int column)
        {
            for (int i = 0; i < lines; i++)
            {
                if (positions[column, i].IsFree())
                {
                    return positions[column, i];
                }
            }

            return null;
        }

        /// <summary>
        /// Get a gridposition 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public IGridPosition GetPosition(int column, int line)
        {
            return positions[column, line];
        }

        /// <summary>
        /// Change the number position
        /// </summary>
        public void ChangePosition(IGridPosition from, IGridPosition to)
        {
            GridPosition _to = to as GridPosition;
            GridPosition _from = from as GridPosition;

            INumber tmp = to.Number;

            _to.Number = from.Number;
            _from.Number = tmp;
            
        }
        
        /// <summary>
        /// set the Number to the Position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="number"></param>
        public void SetNumber(IGridPosition position, INumber number)
        {
            GridPosition gridPosition = position as GridPosition;
            gridPosition.Number = number;
        }
        /// <summary>
        /// Search for match to the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Return the amount matched</returns>
        public ICollection<IGridPosition> Match(IGridPosition position)
        {
            int itemColumn = -1;
            int itemLine = -1;
            // search the position in the grid
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < lines; y++)
                {
                    if (position == positions[x, y])
                    {
                        itemColumn = x;
                        itemLine = y;
                        break;
                    }
                }

                if (itemColumn > -1) break;

            }

            // search in line
            HashSet<IGridPosition> selectedsNumberLines = new HashSet<IGridPosition>();
            int sequenceStart = -1;
            for (int x = 0; x < columns; x++)
            {
                IGridPosition _position = this.positions[x, itemLine];
                if (_position.Number == null)
                {

                    if (selectedsNumberLines.Count < 3)
                    {
                        selectedsNumberLines.Clear();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (
                    selectedsNumberLines.Count == 0 ||
                    Mathf.Abs(sequenceStart - _position.Number.Value) == 1
                    )
                {
                    selectedsNumberLines.Add(_position);
                }
                else
                {
                    if (selectedsNumberLines.Count >= 3)
                    {
                        break;
                    }
                    else
                    {
                        selectedsNumberLines.Clear();
                        selectedsNumberLines.Add(_position);
                    }
                }

                sequenceStart = _position.Number.Value;

            }

            if (selectedsNumberLines.Count < 3)
            {
                selectedsNumberLines.Clear();
            }

            // search in column
            sequenceStart = -1;
            HashSet<IGridPosition> selectedsNumberColumn = new HashSet<IGridPosition>();
            for (int y = 0; y < lines; y++)
            {
                IGridPosition _position = this.positions[itemColumn, y];
                if (_position.Number == null)
                {
                    if (selectedsNumberColumn.Count < 3)
                    {
                        selectedsNumberColumn.Clear();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (
                    selectedsNumberColumn.Count == 0 ||
                    Mathf.Abs(sequenceStart - _position.Number.Value) == 1
                    )
                {
                    selectedsNumberColumn.Add(_position);
                }
                else
                {
                    if (selectedsNumberColumn.Count >= 3)
                    {
                        break;
                    }
                    else
                    {
                        selectedsNumberColumn.Clear();
                        selectedsNumberColumn.Add(_position);
                    }
                }

                sequenceStart = _position.Number.Value;

            }

            if (selectedsNumberColumn.Count < 3)
            {
                selectedsNumberColumn.Clear();
            }

            selectedsNumberLines.UnionWith(selectedsNumberColumn);

            if (!selectedsNumberLines.Contains(position))
            {
                return null;
            }

            return selectedsNumberLines;
        }

        #endregion
    }

}