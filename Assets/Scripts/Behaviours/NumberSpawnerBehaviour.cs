using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSpawnerBehaviour : MonoBehaviour {

    #region Inspector
    [Header("Prefab")]
    [SerializeField]
    private NumberBehaviour _numberPrefab;
    [Header("Positions")]
    [SerializeField]
    private Vector2 _actualNumberPosition;
    [SerializeField]
    private Vector2 _nextNumberPosition;
    #endregion

    private NumberBehaviour _actualNumber, _nextNumber;

    private NumberBehaviour[] _numbersPool = new NumberBehaviour[50];

#region Unity Methods

    // Use this for initialization
    void Start () {
        Setup();
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_actualNumberPosition, Vector2.one);
        Gizmos.DrawCube(_nextNumberPosition, Vector2.one * 0.5f);
    }

    #endregion
    public INumber GetNumber()
    {
        NumberBehaviour _number = _actualNumber;
        Next();
        return _number;
    }

    private void Next()
    {
        _actualNumber = _nextNumber;
        _actualNumber.transform.ScaleTo(Vector2.one,0.5f);
        _actualNumber.transform.position = _actualNumberPosition;
        _actualNumber.gameObject.SetActive(true);

        for (int i = 0; i < this._numbersPool.Length; i++)
        {
            if (!_numbersPool[i].gameObject.activeSelf)
            {
                _nextNumber = _numbersPool[i];
                break;
            }
        }

        _nextNumber.transform.position = _nextNumberPosition;
        _nextNumber.transform.localScale = Vector2.one * .5f;
        _nextNumber.gameObject.SetActive(true);        
    }

    private void Shuffer()
    {

        for (int i = 0; i < this._numbersPool.Length; i++)
        {
            int rand = Random.Range(0, _numbersPool.Length);

            NumberBehaviour tmp = _numbersPool[i];

            _numbersPool[i] = _numbersPool[rand];
            _numbersPool[rand] = tmp;

        }

    }

    private void Setup()
    {
        int index = 0;
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _numbersPool[index] = Instantiate<NumberBehaviour>(this._numberPrefab);
                _numbersPool[index].Value = i;
                _numbersPool[index].gameObject.SetActive(false);
                index++;
            }

        }

        Shuffer();
        
        _nextNumber = this._numbersPool[0];
        _nextNumber.transform.position = _nextNumberPosition;
        _nextNumber.gameObject.SetActive(true);

        Next();

    }
	
}