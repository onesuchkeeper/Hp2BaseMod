using System;
using System.Collections.Generic;

namespace Hp2BaseMod.EnumExpansion
{
    public struct PairStyleInfo
    {
        public StyleInfo MeetingGirlOne;
        public StyleInfo MeetingGirlTwo;
        public StyleInfo SexGirlOne;
        public StyleInfo SexGirlTwo;
    }

    public struct StyleInfo
    {
        public string OutfitName;
        public string HairstyleName;
    }

    public static class EnumLookups
    {
        private static Dictionary<int, PairStyleInfo> PairIDToPairStyleInfo = new Dictionary<int, PairStyleInfo>();
        private static Dictionary<int, Dictionary<int, StyleInfo>> LocationIDToLocationStyleInfo = new Dictionary<int, Dictionary<int, StyleInfo>>();
        private static Dictionary<int, int> GirlIdToDialogTriggerIndex = new Dictionary<int, int>();

        public static int GetGirlDialogTriggerIndex(int girlId) => GirlIdToDialogTriggerIndex[girlId];

        public static PairStyleInfo GetPairStyleInfo(int pairId) => PairIDToPairStyleInfo[pairId];

        public static Dictionary<int, StyleInfo> GetLocationStyleInfo(int locationId) => LocationIDToLocationStyleInfo[locationId];

        public static StyleInfo GetLocationStyleInfo(int locationId, int girlId) => LocationIDToLocationStyleInfo[locationId][girlId];

        public static void AddGirlDialogTrigger(int girlId, int dialogTriggerIndex)
        { 
            if (GirlIdToDialogTriggerIndex.ContainsKey(girlId))
            {
                GirlIdToDialogTriggerIndex[girlId] = dialogTriggerIndex;
            }
            else
            {
                GirlIdToDialogTriggerIndex.Add(girlId, dialogTriggerIndex);
            }
        }

        public static void AddPairInfo(GirlPairDefinition def)
        {
            var pairInfo = new PairStyleInfo();

            pairInfo.MeetingGirlOne.OutfitName = def.girlDefinitionOne.outfits[(int)def.meetingStyleTypeOne].outfitName;
            pairInfo.MeetingGirlOne.HairstyleName = def.girlDefinitionOne.hairstyles[(int)def.meetingStyleTypeOne].hairstyleName;

            pairInfo.MeetingGirlTwo.OutfitName = def.girlDefinitionTwo.outfits[(int)def.meetingStyleTypeTwo].outfitName;
            pairInfo.MeetingGirlTwo.HairstyleName = def.girlDefinitionTwo.hairstyles[(int)def.meetingStyleTypeTwo].hairstyleName;

            pairInfo.SexGirlOne.OutfitName = def.girlDefinitionOne.outfits[(int)def.sexStyleTypeOne].outfitName;
            pairInfo.SexGirlOne.HairstyleName = def.girlDefinitionOne.hairstyles[(int)def.sexStyleTypeOne].hairstyleName;

            pairInfo.SexGirlTwo.OutfitName = def.girlDefinitionTwo.outfits[(int)def.sexStyleTypeTwo].outfitName;
            pairInfo.SexGirlTwo.HairstyleName = def.girlDefinitionTwo.hairstyles[(int)def.sexStyleTypeTwo].hairstyleName;

            AddPairInfo(def.id, pairInfo);
        }

        public static void AddPairInfo(int id, PairStyleInfo pairInfo)
        {
            if (PairIDToPairStyleInfo.ContainsKey(id))
            {
                PairIDToPairStyleInfo[id] = pairInfo;
            }
            else
            {
                PairIDToPairStyleInfo.Add(id, pairInfo);
            }
        }

        public static void AddLocationInfo(LocationDefinition def, IEnumerable<GirlDefinition> girlDefs)
        {
            var girlIdToStyleInfo = new Dictionary<int, StyleInfo>();

            var styleIndex = (int)def.dateGirlStyleType;

            try
            {
                foreach (var girl in girlDefs)
                {
                    var styleInfo = new StyleInfo();

                    var effectiveIndex = styleIndex > (girl.hairstyles.Count-1) ? girl.hairstyles.Count-1 : styleIndex;

                    styleInfo.HairstyleName = girl.hairstyles[effectiveIndex].hairstyleName;
                    styleInfo.OutfitName = girl.outfits[effectiveIndex].outfitName;

                    if (girlIdToStyleInfo.ContainsKey(girl.id))
                    {
                        girlIdToStyleInfo[girl.id] = styleInfo;
                    }
                    else
                    {
                        girlIdToStyleInfo.Add(girl.id, styleInfo);
                    }
                }

                AddLocationInfo(def.id, girlIdToStyleInfo);
            }
            catch (Exception ex)
            {
                ModInterface.Instance.LogLine(ex.Message);
            }
        }

        public static void AddLocationInfo(int id, Dictionary<int, StyleInfo> girlIdToStyleInfo)
        {
            if (LocationIDToLocationStyleInfo.ContainsKey(id))
            {
                foreach(var girl in girlIdToStyleInfo)
                {
                    if (LocationIDToLocationStyleInfo[id].ContainsKey(girl.Key))
                    {
                        LocationIDToLocationStyleInfo[id][girl.Key] = girl.Value;
                    }
                    else
                    {
                        LocationIDToLocationStyleInfo[id].Add(girl.Key, girl.Value);
                    }
                }
            }
            else
            {
                LocationIDToLocationStyleInfo.Add(id, girlIdToStyleInfo);
            }
        }
    }
}
