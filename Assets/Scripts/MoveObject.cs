using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveObject : MonoBehaviour
{
    [SerializeField] 
    private float speed = 4f;

    public void RandomizeSpeed()
    {
        speed = Random.Range(4f, 7f);
    }

    private void Start()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, transform.position.z - speed, Time.deltaTime));
            yield return null;
        }
    }
}
