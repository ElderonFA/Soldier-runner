using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
public class MoveAsphalt : MonoBehaviour
{
    [SerializeField] 
    private float stepMove;
    
    private Material asphaltMaterial;
    void Start()
    {
        asphaltMaterial = GetComponent<Renderer>().material;
        StartCoroutine(MoveAsphaltRoutine());
    }

    private IEnumerator MoveAsphaltRoutine()
    {
        var offsetY = 0f;
        while (true)
        {
            asphaltMaterial.mainTextureOffset = new Vector2(0f, offsetY);
            offsetY -= stepMove;
            if (offsetY <= -10f)
            {
                offsetY = 0f;
            }
            yield return null;
        }
    }
}
