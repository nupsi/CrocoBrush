using System.Collections.Generic;
using UnityEngine;

namespace CrocoBrush
{
    [CreateAssetMenu(fileName = "Notes", menuName = "CrocoBrush/SongNotes", order = 1001)]
    public class SongNotes : ScriptableObject
    {
        public List<SongNode> Nodes;
    }
}