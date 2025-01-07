using UnityEngine;

[CreateAssetMenu(menuName = "Board/Settings", fileName = "BoardSettings")]
public class BoardSettings : ScriptableObject
{
    public int rows = 16;
    public int cols = 16;
}
