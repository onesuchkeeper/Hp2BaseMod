// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using Hp2BaseMod.GameDataMods;

namespace Hp2BaseMod
{
    /// <summary>
    /// Interface class to add in data mods
    /// </summary>
    public class GameDataModder
    {
        //These unfortunatly need to be public to allow for json serialization
        public List<AbilityDataMod> AbilityDataMods = new List<AbilityDataMod>();
        public List<AilmentDataMod> AilmentDataMods = new List<AilmentDataMod>();
        public List<CodeDataMod> CodeDataMods = new List<CodeDataMod>();
        public List<CutsceneDataMod> CutsceneDataMods = new List<CutsceneDataMod>();
        public List<DialogTriggerDataMod> DialogTriggerDataMods = new List<DialogTriggerDataMod>();
        public List<DlcDataMod> DlcDataMods = new List<DlcDataMod>();
        public List<EnergyDataMod> EnergyDataMods = new List<EnergyDataMod>();
        public List<GirlDataMod> GirlDataMods = new List<GirlDataMod>();
        public List<GirlPairDataMod> GirlPairDataMods = new List<GirlPairDataMod>();
        public List<ItemDataMod> ItemDataMods = new List<ItemDataMod>();
        public List<LocationDataMod> LocationDataMods = new List<LocationDataMod>();
        public List<PhotoDataMod> PhotoDataMods = new List<PhotoDataMod>();
        public List<QuestionDataMod> QuestionDataMods = new List<QuestionDataMod>();
        public List<TokenDataMod> TokenDataMods = new List<TokenDataMod>();

        public void AddData(AbilityDataMod data) { AbilityDataMods.Add(data); }
        public void AddData(AilmentDataMod data) { AilmentDataMods.Add(data); }
        public void AddData(CutsceneDataMod data) { CutsceneDataMods.Add(data); }
        public void AddData(CodeDataMod data) { CodeDataMods.Add(data); }
        public void AddData(DialogTriggerDataMod data) { DialogTriggerDataMods.Add(data); }
        public void AddData(DlcDataMod data) { DlcDataMods.Add(data); }
        public void AddData(EnergyDataMod data) { EnergyDataMods.Add(data); }
        public void AddData(GirlDataMod data) { GirlDataMods.Add(data); }
        public void AddData(GirlPairDataMod data) { GirlPairDataMods.Add(data); }
        public void AddData(ItemDataMod data) { ItemDataMods.Add(data); }
        public void AddData(LocationDataMod data) { LocationDataMods.Add(data); }
        public void AddData(PhotoDataMod data) { PhotoDataMods.Add(data); }
        public void AddData(QuestionDataMod data) { QuestionDataMods.Add(data); }
        public void AddData(TokenDataMod data) { TokenDataMods.Add(data); }

        public IEnumerable<T> GetMods<T>()
        {
            //switch to dict? maybe?
            if (typeof(T) == typeof(AbilityDataMod))
            {
                return AbilityDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(AilmentDataMod))
            {
                return AilmentDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(CutsceneDataMod))
            {
                return CutsceneDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(CodeDataMod))
            {
                return CodeDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(DialogTriggerDataMod))
            {
                return DialogTriggerDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(DlcDataMod))
            {
                return DlcDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(EnergyDataMod))
            {
                return EnergyDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(GirlDataMod))
            {
                return GirlDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(GirlPairDataMod))
            {
                return GirlPairDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(ItemDataMod))
            {
                return ItemDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(LocationDataMod))
            {
                return LocationDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(PhotoDataMod))
            {
                return PhotoDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(QuestionDataMod))
            {
                return QuestionDataMods as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(TokenDataMod))
            {
                return TokenDataMods as IEnumerable<T>;
            }
            else
            {
                throw new Exception($"Yo that type isn't handled: {nameof(T)}");
            }
        }
    }
}