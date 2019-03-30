using UnityEngine;

namespace CrocoBrush
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "CrocoBrush/LevelData", order = 1001)]
    public class LevelData : ScriptableObject
    {
        public string Name;
        public AudioClip Audio;
        public SongNotes Notes;
        public SongNotes SpaceNotes;
        public float Delay;
    }
}