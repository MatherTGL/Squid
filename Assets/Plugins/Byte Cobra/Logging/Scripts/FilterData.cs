using System;
using System.Collections.Generic;
using UnityEngine;

namespace ByteCobra.Logging
{
    [CreateAssetMenu(fileName = "Filter", menuName = "Byte Cobra/Logging/Create Filter", order = 1)]
    [Serializable]
    public class FilterData : ScriptableObject
    {
        public FilterType Type = FilterType.And;

        [Serializable]
        public class FilterEntry
        {
            public string Value;
            public LogLevel Level;
        }

        public List<FilterEntry> Filters = new List<FilterEntry>
        {
            new FilterEntry { Value = "Test Value 1", Level = LogLevel.Debug },
            new FilterEntry { Value = "Test Value 2", Level = LogLevel.Warning }
        };
    }
}