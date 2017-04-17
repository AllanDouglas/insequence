using System;
using UnityEngine;

public class NumberBehaviour : MonoBehaviour, INumber
{
    #region Inspector
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int _number;
    [SerializeField]
    private TextMesh _textDisplay;
    [SerializeField]
    private Color[] _colors = new Color[10];
    #endregion

    #region Variables
    /// <summary>
    /// The number
    /// </summary>
    public int Value
    {
        get
        {
            return _number;
        }
        set
        {
            _number = value;
            Setup();
            this._textDisplay.text = _number.ToString();
        }
    }

    #endregion

    #region  Methods

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        _spriteRenderer.color = _colors[_number - 1];
        _textDisplay.color = _colors[_number - 1];
    }
    #endregion

    #region Interface
    public void Release()
    {
        
        Debug.Log("Release the Number");
        gameObject.SetActive(false);
    }
    #endregion
}
