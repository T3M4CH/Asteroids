using System.Collections.Generic;
using Game.Settings.Enums;
using UnityEngine;
using Game.Utils;

public class Pricelist : MonoBehaviour
{
    [SerializeField] private List<SerializablePair<EInputScheme, int>> input = new();
}
