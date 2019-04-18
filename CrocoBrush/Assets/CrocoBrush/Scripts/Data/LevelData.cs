using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Scriptable object to store level data containing everything required to play a level.
    /// Contains: level name, audio, notes, space notes and delay.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelData", menuName = "CrocoBrush/LevelData", order = 1001)]
    public class LevelData : ScriptableObject
    {
        /// <summary>
        /// Level name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Level difficulty;
        /// </summary>
        public string Difficulty;

        /// <summary>
        /// Level Audio.
        /// Played when the level start.
        /// </summary>
        public AudioClip Audio;

        /// <summary>
        /// Notes for the audio clip.
        /// </summary>
        public SongNotes Notes;

        /// <summary>
        /// Spacebar notes for the audio clip.
        /// </summary>
        public SongNotes SpaceNotes;

        /// <summary>
        /// Delay between the notes and audio.
        /// </summary>
        public float Delay;
    }
}