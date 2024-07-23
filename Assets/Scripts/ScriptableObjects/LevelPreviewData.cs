using UnityEngine;

namespace BHSCamp.UI
{
    [CreateAssetMenu(fileName = "LevelPreviewData", menuName = "Levels/Preview")]
    public class LevelPreviewData : ScriptableObject
    {
        public Sprite Preview;
        public string Name;
        public int SceneIndex;
        public bool IsAccesible;
        public int  GemsAmount;
    }
}