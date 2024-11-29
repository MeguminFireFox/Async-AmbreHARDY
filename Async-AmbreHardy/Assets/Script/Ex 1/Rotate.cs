using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private bool _isPlaying = false;

    public void OnClickStart()
    {
       _isPlaying = true;
    }

    public void OnclickStop() 
    {
        _isPlaying = false;
    }

    void Update()
    {
        if (!_isPlaying)
        {
            StopAllCoroutines();
            return;
        }

        StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        _object.transform.Rotate(0, 0, 1);
        yield return new WaitForSeconds(5);
        _isPlaying = false;
    }
        
}
