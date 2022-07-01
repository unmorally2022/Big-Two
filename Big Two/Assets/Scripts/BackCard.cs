using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCard : MonoBehaviour
{
    private Vector3 NewPos;
    private Action AfterInPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveTo(Vector3 _NewPos, Action _AfterInPos) {
        NewPos = _NewPos;
        AfterInPos = _AfterInPos;
        StartCoroutine(MoveToPosition());
    }

    IEnumerator MoveToPosition()
    {
        float counter = 0;

        Vector3 _startPos = transform.position;

        //Vector3 endPosition = Camera.main.WorldToScreenPoint(CurrentDeck.transform.position);
        //Vector3 _endPos = Camera.main.ScreenToWorldPoint(endPosition);
        Vector3 _endPos = NewPos;

        while (counter < 2f)
        {
            //weapon.transform.position = Vector3.Lerp(weapon.transform.position, target, Time.deltaTime * speed);

            
            transform.position = Vector3.Lerp(_startPos, _endPos, counter);
            counter += Time.deltaTime / 0.75f;

            yield return null;
        }

        transform.position = _endPos;
        AfterInPos?.Invoke();

    }
}
