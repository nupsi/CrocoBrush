using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    [CreateAssetMenu(fileName = "GameSave", menuName = "CrocoBrush/GameSave", order = 1010)]
    public class GameSave : ScriptableObject
    {
        /// <summary>
        /// List of stored level stat.
        /// </summary>
        public List<SongStats> Stats;

        /// <summary>
        /// Dictionary for faster access to level data.
        /// Cannot be serialized by unity, use Load() to conver Scores to Data.
        /// </summary>
        private Dictionary<string, List<SongStats>> m_data;

        /// <summary>
        /// Convert serialized list to dictionary.
        /// </summary>
        public void Load()
        {
            m_data = new Dictionary<string, List<SongStats>>();
            foreach(var stat in Stats)
            {
                if(m_data[stat.Name] != null)
                {
                    m_data[stat.Name].Add(stat);
                }
                else
                {
                    m_data.Add(stat.Name, new List<SongStats> { stat });
                }
            }
        }

        /// <summary>
        /// Get all stored stats for a given level.
        /// </summary>
        /// <param name="level">Level name.</param>
        /// <returns>Level Stats.</returns>
        public List<SongStats> GetLevelStats(string level)
        {
            if(m_data == null)
            {
                Load();
            }

            return m_data[level] ?? new List<SongStats>();
        }

        /// <summary>
        /// Add new level stats to saved stats.
        /// Clears data dictionary to force update on next stat request.
        /// </summary>
        /// <param name="stats">Stored song stats.</param>
        public void AddStat(SongStats stats)
        {
            Stats.Add(stats);
            m_data = null;
        }
    }
}