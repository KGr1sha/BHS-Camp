using UnityEngine;

[CreateAssetMenu(fileName = "LevelPreviewData", menuName = "Levels/Preview")]
public class LevelPreviewData : ScriptableObject
{
    public Sprite LevelPreview;
    public string LevelName;
    public int CorrespondingSceneIndex;
    public bool IsAccesible;
}
