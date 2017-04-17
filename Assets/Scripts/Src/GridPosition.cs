using UnityEngine;
namespace Grid
{
    class GridPosition : IGridPosition
    {
        #region Properties    
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
        }

        private INumber _number;

        public INumber Number
        {
            get { return _number; }
            internal set { _number = value; }
        }
        #endregion

        #region Contructors

        public GridPosition() { }

        public GridPosition(Vector2 position, INumber number)
        {
            _position = position;
            _number = number;
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Return true if this position is free
        /// </summary>
        /// <returns></returns>
        public bool IsFree()
        {
            return (_number == null);
        }

        /// <summary>
        /// Clean the position
        /// </summary>
        public void Clear()
        {
            
            _number = null;
        }

        #endregion
        

    }

}