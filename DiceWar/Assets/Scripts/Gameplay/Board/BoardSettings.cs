using UnityEngine;

[CreateAssetMenu(menuName = "Board/Settings", fileName = "BoardSettings")]
public class MatchSettings : ScriptableObject
{
    public int rows = 16;
    public int cols = 16;
}
