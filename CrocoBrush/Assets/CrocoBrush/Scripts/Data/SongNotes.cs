using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    /// <summary>
    /// Scriptable object to store music Notes.
    /// </summary>
    [CreateAssetMenu(fileName = "Notes", menuName = "CrocoBrush/SongNotes", order = 1001)]
    public class SongNotes : ScriptableObject
    {
        /*
         * Variables.
         */

        /// <summary>
        /// List of music notes.
        /// </summary>
        public List<SongNode> Nodes;

        /*
         * Functions.
         */

        /// <summary>
        /// Calculate delays between current and previous notes.
        /// </summary>
        public void CalculateDelays()
        {
            if(Nodes != null)
            {
                for(int i = 0; i < Nodes.Count; i++)
                {
                    var delay = (i == 0)
                        ? Nodes[i].Time
                        : Nodes[i].Time - Nodes[i - 1].Time;
                    Nodes[i].Delay = Mathf.Round(delay * 1000) / 1000;
                }
            }
        }
    }
}