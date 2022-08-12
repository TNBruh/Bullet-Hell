using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class LayerCreator : MonoBehaviour
{
    internal int GetLayerInt(string layerName)
    {
        return LayerMask.GetMask(layerName);
    }

    internal int AddLayer(int[] layers)
    {
        int result = layers[0];
        for (int i = 1; i < layers.Length; i++)
        {
            result |= 1 << layers[i];
        }
        return result;
    }

    internal void ChangeObjLayer(GameObject obj, int chosenLayer)
    {
        obj.layer = chosenLayer;
    }

    internal void AddObjLayer(GameObject obj, int chosenLayer)
    {
        obj.layer |= 1 << chosenLayer;
    }
}
