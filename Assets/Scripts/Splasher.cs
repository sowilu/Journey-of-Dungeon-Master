using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Splasher : MonoBehaviour
{
    public static Splasher instance;

    private ParticleSystem particleSystem;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        particleSystem = GetComponent<ParticleSystem>();
    }

    public void SpawnSplash(Vector3 pos)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos;

        particleSystem.Emit(emitParams, 1);
    }
}
