using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]

[SerializeField]
public class SaveData: ScriptableObject
{
    [SerializeField]
    public int recordPoints;
    [SerializeField]
    public int currentPoints;

    public SaveData()
    {
        recordPoints = 0;
        currentPoints = 0;
    }
}