using Hp2BaseMod.GameDataInfo;
using System.Collections.Generic;

namespace Hp2BaseMod
{
    /// <summary>
    /// Handles metadata about mods
    /// </summary>
    public class ModData
    {
        /// <summary>
        /// Maps a pair's id to its style info
        /// </summary>
        private readonly static Dictionary<RelativeId, PairStyleInfo> _pairIdToPairStyleInfo = new Dictionary<RelativeId, PairStyleInfo>();

        /// <summary>
        /// Maps a location's id to its style info
        /// </summary>
        private readonly static Dictionary<RelativeId, Dictionary<RelativeId, GirlStyleInfo>> _locationIdToLocationStyleInfo = new Dictionary<RelativeId, Dictionary<RelativeId, GirlStyleInfo>>();

        /// <summary>
        /// Maps a girl's id to its dialogtrigger index
        /// </summary>
        private readonly static Dictionary<RelativeId, int> _girlIdToDialogTriggerIndex = new Dictionary<RelativeId, int>();
        private readonly static Dictionary<RelativeId, Dictionary<RelativeId, Dictionary<RelativeId, int>>> _dtIdToGirlIdToLineIndexLookup = new Dictionary<RelativeId, Dictionary<RelativeId, Dictionary<RelativeId, int>>>();
        private readonly static Dictionary<RelativeId, Dictionary<RelativeId, Dictionary<int, RelativeId>>> _dtIdToGirlIdToLineIdLookup = new Dictionary<RelativeId, Dictionary<RelativeId, Dictionary<int, RelativeId>>>();

        /// <summary>
        /// Maps a girls id to a lookup from a <see cref="RelativeId"/> to an index of the girl's outfits
        /// </summary>
        private readonly Dictionary<RelativeId, Dictionary<RelativeId, int>> _girlIdToOutfitIndexLookup = new Dictionary<RelativeId, Dictionary<RelativeId, int>>();
        private readonly Dictionary<RelativeId, Dictionary<int, RelativeId>> _girlIdToOutfitIdLookup = new Dictionary<RelativeId, Dictionary<int, RelativeId>>();

        /// <summary>
        /// Maps a girls id to a lookup from a <see cref="RelativeId"/> to an index of the girl's hairstyles
        /// </summary>
        private readonly Dictionary<RelativeId, Dictionary<RelativeId, int>> _girlIdToHairstyleIndexLookup = new Dictionary<RelativeId, Dictionary<RelativeId, int>>();
        private readonly Dictionary<RelativeId, Dictionary<int, RelativeId>> _girlIdToHairstyleIdLookup = new Dictionary<RelativeId, Dictionary<int, RelativeId>>();

        /// <summary>
        /// Maps a girls id to a lookup from a <see cref="RelativeId"/> to an index of the girl's parts
        /// </summary>
        private readonly Dictionary<RelativeId, Dictionary<RelativeId, int>> _girlIdToPartIndexLookup = new Dictionary<RelativeId, Dictionary<RelativeId, int>>();
        private readonly Dictionary<RelativeId, Dictionary<int, RelativeId>> _girlIdToPartIdLookup = new Dictionary<RelativeId, Dictionary<int, RelativeId>>();

        /// <summary>
        /// Maps a source's local ids to the ids used at runtime.
        /// </summary>
        private readonly Dictionary<GameDataType, Dictionary<RelativeId, int>> _relativeIdToRuntimeId = new Dictionary<GameDataType, Dictionary<RelativeId, int>>()
        {
            { GameDataType.Ability, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Ailment, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Code, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Cutscene, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.DialogTrigger, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Dlc, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Energy, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Girl, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.GirlPair, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Item, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Location, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Photo, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Question, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } },
            { GameDataType.Token, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1}, { RelativeId.Zero, 0 } } }
        };
        private readonly Dictionary<GameDataType, Dictionary<int, RelativeId>> _runtimeIdToRelativeId = new Dictionary<GameDataType, Dictionary<int, RelativeId>>()
        {
            { GameDataType.Ability, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Ailment, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Code, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Cutscene, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.DialogTrigger, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Dlc, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Energy, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Girl, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.GirlPair, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Item, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Location, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Photo, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Question, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } },
            { GameDataType.Token, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default }, { 0, RelativeId.Zero } } }
        };

        /// <summary>
        /// Keeps track of the next id to assign to new data defs of each type.
        /// Starts at 1,000,000 just for security in not conflicting with the base game ever. Hopefully.
        /// Overkill but cheap.
        /// </summary>
        private readonly Dictionary<GameDataType, int> _runtimeIdSource = new Dictionary<GameDataType, int>()
        {
            { GameDataType.Ability, 1000000},
            { GameDataType.Ailment, 1000000},
            { GameDataType.Code, 1000000},
            { GameDataType.Cutscene, 1000000},
            { GameDataType.DialogTrigger, 1000000},
            { GameDataType.Dlc, 1000000},
            { GameDataType.Energy, 1000000},
            { GameDataType.Girl, 1000000},
            { GameDataType.GirlPair, 1000000},
            { GameDataType.Item, 1000000},
            { GameDataType.Location, 1000000},
            { GameDataType.Photo, 1000000},
            { GameDataType.Question, 1000000},
            { GameDataType.Token, 1000000}
        };

        /// <summary>
        /// Keeps track of the ids in use for each <see cref="GameDataType"/>
        /// </summary>
        private readonly Dictionary<GameDataType, HashSet<RelativeId>> _dataIds = new Dictionary<GameDataType, HashSet<RelativeId>>()
        {
            { GameDataType.Ability, new HashSet<RelativeId>() },
            { GameDataType.Ailment, new HashSet<RelativeId>() },
            { GameDataType.Code, new HashSet<RelativeId>() },
            { GameDataType.Cutscene, new HashSet<RelativeId>() },
            { GameDataType.DialogTrigger, new HashSet<RelativeId>() },
            { GameDataType.Dlc, new HashSet<RelativeId>() },
            { GameDataType.Energy, new HashSet<RelativeId>() },
            { GameDataType.Girl, new HashSet<RelativeId>() },
            { GameDataType.GirlPair, new HashSet<RelativeId>() },
            { GameDataType.Item, new HashSet<RelativeId>() },
            { GameDataType.Location, new HashSet<RelativeId>() },
            { GameDataType.Photo, new HashSet<RelativeId>() },
            { GameDataType.Question, new HashSet<RelativeId>() },
            { GameDataType.Token, new HashSet<RelativeId>() }
        };

        #region registration

        internal void RegisterLocationStyles(RelativeId locationId, Dictionary<RelativeId, GirlStyleInfo> girlIdToStyleInfo)
        {
            if (_locationIdToLocationStyleInfo.ContainsKey(locationId))
            {
                foreach (var girl in girlIdToStyleInfo)
                {
                    if (_locationIdToLocationStyleInfo[locationId].ContainsKey(girl.Key))
                    {
                        var styleInfo = _locationIdToLocationStyleInfo[locationId][girl.Key];
                        girl.Value.SetData(ref styleInfo);
                    }
                    else
                    {
                        _locationIdToLocationStyleInfo[locationId].Add(girl.Key, girl.Value);
                    }
                }
            }
            else
            {
                _locationIdToLocationStyleInfo.Add(locationId, girlIdToStyleInfo);
            }
        }

        internal void RegisterPairStyle(RelativeId pairId, PairStyleInfo pairStyle)
        {
            if (pairStyle != null)
            {
                if (_pairIdToPairStyleInfo.ContainsKey(pairId))
                {
                    var currentStyle = _pairIdToPairStyleInfo[pairId];
                    pairStyle.SetData(ref currentStyle);
                }
                else
                {
                    _pairIdToPairStyleInfo.Add(pairId, pairStyle);
                }
            }
        }

        internal void RegisterDefaultData(GameDataType type, int localId)
        {
            var id = new RelativeId(-1, localId);
            _relativeIdToRuntimeId[type].Add(id, localId);
            _runtimeIdToRelativeId[type].Add(localId, id);
            _dataIds[type].Add(id);
        }

        internal bool TryRegisterData(GameDataType type, RelativeId id)
        {
            if (!_relativeIdToRuntimeId[type].ContainsKey(id))
            {
                var runtimeId = _runtimeIdSource[type]++;
                _relativeIdToRuntimeId[type].Add(id, runtimeId);
                _runtimeIdToRelativeId[type].Add(runtimeId, id);
                _dataIds[type].Add(id);

                ModInterface.Log.LogLine($"{type} {id.LocalId} from source with id {id.SourceId} assigned to {runtimeId} at runtime");
                return true;
            }

            return false;
        }

        internal bool TryRegisterGirlDialogTrigger(RelativeId girlId, int index)
        {
            if (!_girlIdToDialogTriggerIndex.ContainsKey(girlId))
            {
                _girlIdToDialogTriggerIndex.Add(girlId, index);
                //_log.LogLine($"Girl with id {girlId} assigned dialog trigger index {index}");
                return true;
            }
            return false;
        }

        internal bool TryRegisterOutfit(RelativeId girlId, int index, RelativeId outfitId)
        {
            if (!_girlIdToOutfitIndexLookup.ContainsKey(girlId))
            {
                _girlIdToOutfitIndexLookup.Add(girlId, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1 } });
                _girlIdToOutfitIdLookup.Add(girlId, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default } });
            }

            if (!_girlIdToOutfitIndexLookup[girlId].ContainsKey(outfitId))
            {
                _girlIdToOutfitIndexLookup[girlId].Add(outfitId, index);
                _girlIdToOutfitIdLookup[girlId].Add(index, outfitId);
                //_log.LogLine($"Girl with id {girlId} had oufit with id {outfitId} assigned to index {index}");
                return true;
            }

            return false;
        }

        internal bool TryRegisterHairstyle(RelativeId girlId, int index, RelativeId hairstyleId)
        {
            if (!_girlIdToHairstyleIndexLookup.ContainsKey(girlId))
            {
                _girlIdToHairstyleIndexLookup.Add(girlId, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1 } });
                _girlIdToHairstyleIdLookup.Add(girlId, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default } });
            }

            if (!_girlIdToHairstyleIndexLookup[girlId].ContainsKey(hairstyleId))
            {
                _girlIdToHairstyleIndexLookup[girlId].Add(hairstyleId, index);
                _girlIdToHairstyleIdLookup[girlId].Add(index, hairstyleId);
                //_log.LogLine($"Girl with id {girlId} had hairstyle with id {hairstyleId} assigned to index {index}");
                return true;
            }

            return false;
        }

        internal bool TryRegisterPart(RelativeId girlId, int index, RelativeId partId)
        {
            if (!_girlIdToPartIndexLookup.ContainsKey(girlId))
            {
                _girlIdToPartIndexLookup.Add(girlId, new Dictionary<RelativeId, int>() { { RelativeId.Default, -1 } });
                _girlIdToPartIdLookup.Add(girlId, new Dictionary<int, RelativeId>() { { -1, RelativeId.Default } });
            }

            if (!_girlIdToPartIndexLookup[girlId].ContainsKey(partId))
            {
                _girlIdToPartIndexLookup[girlId].Add(partId, index);
                _girlIdToPartIdLookup[girlId].Add(index, partId);
                //_log.LogLine($"Girl with id {girlId} had part with id {partId} assigned to index {index}");
                return true;
            }
            return false;
        }

        internal bool TryRegisterLine(RelativeId dialogTriggerId, RelativeId girlId, int index, RelativeId lineId)
        {
            if (!_dtIdToGirlIdToLineIndexLookup.ContainsKey(dialogTriggerId))
            {
                _dtIdToGirlIdToLineIndexLookup.Add(dialogTriggerId, new Dictionary<RelativeId, Dictionary<RelativeId, int>>());
                _dtIdToGirlIdToLineIdLookup.Add(dialogTriggerId, new Dictionary<RelativeId, Dictionary<int, RelativeId>>());
            }

            if (!_dtIdToGirlIdToLineIndexLookup[dialogTriggerId].ContainsKey(girlId))
            {
                _dtIdToGirlIdToLineIndexLookup[dialogTriggerId].Add(girlId, new Dictionary<RelativeId, int>());
                _dtIdToGirlIdToLineIdLookup[dialogTriggerId].Add(girlId, new Dictionary<int, RelativeId>());
            }

            if (!_dtIdToGirlIdToLineIndexLookup[dialogTriggerId][girlId].ContainsKey(lineId))
            {
                _dtIdToGirlIdToLineIndexLookup[dialogTriggerId][girlId].Add(lineId, index);
                _dtIdToGirlIdToLineIdLookup[dialogTriggerId][girlId].Add(index, lineId);
                //_log.LogLine($"Girl with id {girlId} had line with id {lineId} assigned to index {index} of dialog trigger with id {dialogTriggerId}");
                return true;
            }

            return false;
        }

        #endregion

        #region lookup

        public IEnumerable<RelativeId> GetIds(GameDataType type) => _dataIds[type];

        public int GetLineIndex(RelativeId dialogTriggerId, RelativeId girlId, RelativeId lineId) => _dtIdToGirlIdToLineIndexLookup[dialogTriggerId][girlId][lineId];
        public RelativeId GetLineId(RelativeId dialogTriggerId, RelativeId girlId, int lineIndex) => _dtIdToGirlIdToLineIdLookup[dialogTriggerId][girlId][lineIndex];

        public GirlStyleInfo GetLocationStyleInfo(RelativeId locationId, RelativeId girlId) => _locationIdToLocationStyleInfo[locationId][girlId];
        public GirlStyleInfo TryGetLocationStyleInfo(RelativeId locationId, RelativeId girlId)
        {
            if (_locationIdToLocationStyleInfo.ContainsKey(locationId)
                && _locationIdToLocationStyleInfo[locationId].ContainsKey(girlId))
            {
                return _locationIdToLocationStyleInfo[locationId][girlId];
            }

            return null;
        }

        public PairStyleInfo GetPairStyleInfo(RelativeId pairId) => _pairIdToPairStyleInfo[pairId];
        public PairStyleInfo GetPairStyleInfo(int pairRuntimeId) => _pairIdToPairStyleInfo[_runtimeIdToRelativeId[GameDataType.GirlPair][pairRuntimeId]];

        public int GetGirlDialogTriggerIndex(RelativeId girlId) => _girlIdToDialogTriggerIndex[girlId];

        public int GetRuntimeDataId(GameDataType dataModType, RelativeId id) => _relativeIdToRuntimeId[dataModType][id];

        public int? GetRuntimeDataId(GameDataType dataModType, RelativeId? id) => id.HasValue
                                                                                    ? (int?)GetRuntimeDataId(dataModType, id.Value)
                                                                                    : null;

        public RelativeId GetDataId(GameDataType dataModType, int runtimeId) => _runtimeIdToRelativeId[dataModType][runtimeId];

        public int? GetOutfitIndex(RelativeId? girlId, RelativeId? id) => girlId.HasValue ? GetOutfitIndex(girlId.Value, id) : null;
        public int? GetOutfitIndex(RelativeId girlId, RelativeId? id) => id.HasValue ? (int?)GetOutfitIndex(girlId, id.Value) : null;
        public int GetOutfitIndex(RelativeId girlId, RelativeId id) => _girlIdToOutfitIndexLookup[girlId][id];
        public RelativeId GetOutfitId(RelativeId girlId, int index) => _girlIdToOutfitIdLookup[girlId][index];

        public int? GetHairstyleIndex(RelativeId? girlId, RelativeId? id) => girlId.HasValue ? GetHairstyleIndex(girlId.Value, id) : null;
        public int? GetHairstyleIndex(RelativeId girlId, RelativeId? id) => id.HasValue ? (int?)GetHairstyleIndex(girlId, id.Value) : null;
        public int GetHairstyleIndex(RelativeId girlId, RelativeId id) => _girlIdToHairstyleIndexLookup[girlId][id];
        public RelativeId GetHairstyleId(RelativeId girlId, int index) => _girlIdToHairstyleIdLookup[girlId][index];

        public int? GetPartIndex(RelativeId girlId, RelativeId? id) => id.HasValue ? (int?)GetPartIndex(girlId, id.Value) : null;
        public int GetPartIndex(RelativeId girlId, RelativeId id) => _girlIdToPartIndexLookup[girlId][id];
        public RelativeId GetPartId(RelativeId girlId, int index) => _girlIdToPartIdLookup[girlId][index];

        #endregion
    }
}
