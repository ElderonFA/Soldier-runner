using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPulled
{
    PulledType PulledType { get; }
    void Spawn(Vector3 position);
    void Despawn();
}
