using UnityEngine;

namespace Grid
{
    public interface IGridPosition
    {
        INumber Number { get; }
        Vector2 Position { get; }

        bool IsFree();
        void Clear();
    }
}