// Hp2Sample 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Newtonsoft.Json;

namespace Hp2SingleDateMod
{
    public class SingleDateStarter : IHp2BaseModStarter
    {
        public void Start(ModInterface gameDataModder)
        {
            MakeNoFocus(gameDataModder);

            //var newPairs = AddPairMods(gameDataModder);

            //AddEmptyGirl(gameDataModder, newPairs);

            //TEST
            gameDataModder.AddData(new GirlDataMod(1, InsertStyle.replace)
            {
                BaggageItemDefIDs = new List<int?>() { Constants.NoFocusBagadgeItemId, 94, 95 }
                //BaggageItemDefIDs = new List<int>() { 106, 94, 95 }
            });
        }

        private void AddEmptyGirl(ModInterface gameDataModder, List<int?> newPairs)
        {
            var emptySprite = new SpriteInfo(Constants.EmptyImagePath, true);

            var offscrenevector = new VectorInfo(-1000f, -1000f);

            var emptyPartList = new List<GirlPartInfo>();
            foreach (int i in Enum.GetValues(typeof(GirlPartType)))
            {
                var emptyPart = new GirlPartInfo()
                {
                    PartType = (GirlPartType)i,
                    PartName = "Empty Part",
                    SpriteInfo = emptySprite,
                    X = 0,
                    Y = 0,
                    MirroredPartIndex = 0,
                    AltPartIndex = 0
                };

                emptyPartList.Add(emptyPart);
            }

            var emptyExpression = new GirlExpressionSubDefinition()
            {
                expressionType = GirlExpressionType.NEUTRAL,
                partIndexEyebrows = (int)GirlPartType.EYEBROWS,
                partIndexEyes = (int)GirlPartType.EYES,
                partIndexEyesGlow = (int)GirlPartType.EYESGLOW,
                partIndexMouthClosed = (int)GirlPartType.MOUTH,
                partIndexMouthOpen = (int)GirlPartType.MOUTHOPEN,
                eyesClosed = false,
                mouthOpen = false,
                editorExpanded = false
            };

            var emptyHairstyle = new GirlHairstyleSubDefinition()
            {
                hairstyleName = "Nothing",
                partIndexFronthair = 0,
                partIndexBackhair = 0,
                pairOutfitIndex = 0,
                tightlyPaired = false,
                hideSpecials = false,
                editorExpanded = false
            };

            var emptyOutfit = new GirlOutfitSubDefinition()
            {
                outfitName = "Nothing",
                partIndexOutfit = -1,
                pairHairstyleIndex = 0,
                tightlyPaired = false,
                hideNipples = true,
                editorExpanded = false
            };

            int PHONEME_partid = (int)GirlPartType.PHONEMES;
            int PHONEMESTEETH_partid = (int)GirlPartType.PHONEMESTEETH;

            var EmptyGirl = new GirlDataMod(Constants.EmptyGirlId, InsertStyle.replace)
            {
                EditorTab = EditorGirlDefinitionTab.DEFAULT,
                GirlName = "Nobody",
                GirlNickName = string.Empty,
                GirlAge = 21,
                DialogTriggerTab = EditorDialogTriggerTab.DEFAULT,
                SpecialCharacter = true,
                BossCharacter = false,
                FavoriteAffectionType = PuzzleAffectionType.FLIRTATION,
                LeastFavoriteAffectionType = PuzzleAffectionType.FLIRTATION,
                VoiceVolume = 0f,
                SexVoiceVolume = 0f,
                CellphonePortrait = emptySprite,
                CellphonePortraitAlt = emptySprite,
                CellphoneHead = emptySprite,
                CellphoneHeadAlt = emptySprite,
                CellphoneMiniHead = emptySprite,
                CellphoneMiniHeadAlt = emptySprite,
                BreathEmitterPos = offscrenevector,
                UpsetEmitterPos = offscrenevector,
                SpecialEffectName = null,
                SpecialEffectOffset = offscrenevector,
                ShoesType = ItemShoesType.WINTER_BOOTS,// I don't see a way around this without larger modifications...
                ShoesAdj = string.Empty,
                UniqueType = ItemUniqueType.TAILORING,// Ditto
                UniqueAdj = string.Empty,
                BadFoodTypes = new List<ItemFoodType>(),
                GirlPairDefIDs = newPairs,
                BaggageItemDefIDs = new List<int?>() { Constants.NoFocusBagadgeItemId,
                                                      115,
                                                      106},
                UniqueItemDefIDs = new List<int?>(),
                ShoesItemDefIDs = new List<int?>(),
                HasAltStyles = false,
                AltStylesFlagName = string.Empty,
                AltStylesCodeDefinitionID = -1,
                UnlockStyleCodeDefinitionID = -1,
                PartIndexBody = 0,
                PartIndexNipples = 0,
                PartIndexBlushLight = 0,
                PartIndexBlushHeavy = 0,
                PartIndexBlink = 0,
                PartIndexMouthNeutral = 0,
                PartIndexesPhonemes = new List<int?>() { PHONEME_partid, PHONEME_partid, PHONEME_partid, PHONEME_partid, PHONEME_partid },
                PartIndexesPhonemesTeeth = new List<int?>() { PHONEMESTEETH_partid, PHONEMESTEETH_partid, PHONEMESTEETH_partid, PHONEMESTEETH_partid, PHONEMESTEETH_partid },
                Parts = emptyPartList,
                DefaultExpressionIndex = 0,
                FailureExpressionIndex = 0,
                DefaultHairstyleIndex = 0,
                DefaultOutfitIndex = 0,
                Expressions = new List<GirlExpressionSubDefinition> { emptyExpression },
                Hairstyles = new List<GirlHairstyleSubDefinition> { emptyHairstyle },
                Outfits = new List<GirlOutfitSubDefinition> { emptyOutfit },
                SpecialParts = new List<GirlSpecialPartSubDefinition>(),
                HerQuestions = new List<GirlQuestionSubDefinition>(),
                FavAnswers = new List<int>()
            };

            gameDataModder.AddData(EmptyGirl);
        }

        private List<int> AddPairMods(ModInterface gameDataModder)
        {
            var newPairIds = new List<int>();

            var configs = new List<SingleDateConfig>();
            foreach (var path in Directory.GetFiles("mods/Hp2SingleDateMod" + "/configs"))
            {
                var config = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(path), typeof(SingleDateConfig)) as SingleDateConfig;

                if (config == null)
                {
                    configs.Add(config);
                }
            }

            //configs.ForEach(x => newPairIds.Add(x.AddMods(gameDataModder)));

            return newPairIds;
        }

        private void MakeNoFocus(ModInterface gameDataModder)
        {
            var NoFocusBagadgeItem = new ItemDataMod(Constants.NoFocusBagadgeItemId, InsertStyle.replace)
            {
                itemName = "Unfocusable",
                itemType = ItemType.BAGGAGE,
                itemDescription = "There's no one here! This side of the puzzle cannot be focused.",
                energyDefinitionID = -1,
                ItemSpriteInfo = new SpriteInfo("item_baggage_attention_whore", false), //make custom image pls thx
                affectionType = PuzzleAffectionType.TALENT,
                tooltipColorIndex = 0,
                giveConditionType = 0,
                girlDefinitionID = -1,
                difficultyExclusive = false,
                difficulty = SettingDifficulty.EASY,
                storeSectionPreference = false,
                storeCost = 0,
                foodType = ItemFoodType.JAPANESE,
                noStaminaCost = false,
                shoesType = ItemShoesType.WINTER_BOOTS,
                uniqueType = ItemUniqueType.TAILORING,
                dateGiftType = ItemDateGiftType.FLOWERS,
                dateGiftAilment = false,
                abilityDefinitionID = -1,
                useCost = 0,
                baggageGirl = EditorDialogTriggerTab.SARAH, // does this matter? I hope not
                cutsceneDefinitionID = 132, // See if this can have no cutscene
                ailmentDefinitionID = 14,// Constants.NoFocusBagadgeAilmentId,
                categoryDescription = string.Empty,
                costDescription = string.Empty,
                notifierHeaderIndex = 0
            };

            var NoFocusBagadgeAilment = new AilmentDataMod(Constants.NoFocusBagadgeAilmentId, InsertStyle.replace)
            {
                ItemDefinitionID = Constants.NoFocusBagadgeItemId,
                PersistentFlags = false,
                EnableType = AilmentEnableType.NONE,
                EnableTriggerIndex = -1,
                EnableTokenDefID = -1,
                EnableIntVal = 0,
                EnableFloatVal = 0.0f,
                EnableBoolVal = false,
                EnableStringVal = string.Empty,
                EnableAbilityDefID = -1,
                DisableAbilityDefID = -1,
                Hints = new List<AilmentHintSubDefinition>(),
                Triggers = new List<AilmentTriggerInfo>()
                {
                    new AilmentTriggerInfo()
                    {
                        TriggerType = AilmentTriggerType.PRE_MOVE,
                        StepsProcessType = AilmentTriggerStepsProcessType.BAIL_ON_FAIL,
                        PerentChance = 1.0f,
                        ThresholdValue = 0,
                        ExecuteLimit = 3,
                        VerbalizedIndex = -1,
                        FocusMatters = true,
                        OnUnfocused = true,
                        ExhaustionMatters = true,
                        OnExhausted = false,
                        UpsetMatters = false,
                        OnUpset = false,
                        ThresholdPersistent = false,
                        DefaultDisabled = false,
                        Audibalized = true,
                        Verbalized = true,
                        StepInfos = new List<AilmentStepInfo>()
                        {
                            new AilmentStepInfo()
                            {
                                StepType = AilmentStepType.CHECK_MOVE,
                                FlagType = AilmentFlagType.HARD,
                                ComparisonType = NumberComparisonType.EQUAL_TO,
                                StringValue = string.Empty,
                                IntValue = 0,
                                AbilityDefinitionID = -1,
                                BoolValue = false,
                                MoveModifier = new MoveModifier()
                                {
                                    blockMoveCost = false,
                                    blockStaminaCost = false,
                                    blockStaminaRecover = false,
                                    postSwitchGirlFocus = false
                                },
                                MatchModifierInfo = new MatchModifierInfo()
                                {
                                    PointsOperation = NumberCombineOperation.MULTIPLY,
                                    PointsOperation2 = NumberCombineOperation.MULTIPLY,
                                    PointsFactor = 1.0f,
                                    PointsFactor2 = 1.0f,
                                    TokenDefinitionID = -1,
                                    ReplaceDefinitionID = -1,
                                    Absorb = false,
                                    AbsorbAltGirl = false,
                                    ReplacePriority = false,
                                    SkipMostFavFactor = false,
                                    SkipLeastFavFactor = false,
                                    PointsOp = false,
                                    PointsOp2 = false
                                },
                                GiftModifier = new GiftModifier()
                                {
                                    blockGift = false
                                },
                                GiftConditionInfos = new List<GiftConditionInfo>(),
                                MoveConditionInfos = new List<MoveConditionInfo>()
                                {
                                    new MoveConditionInfo()
                                    {
                                        Type = MoveConditionType.HAS_TOKEN,
                                        TokenType = MoveConditionTokenType.DEFINITION,
                                        ResourceType = PuzzleResourceType.AFFECTION,
                                        Comparison = NumberComparisonType.EQUAL_TO,
                                        TokenDefinitionID = 6,
                                        Val = 0,
                                        BoolValue = false,
                                        Inverse = true
                                    }
                                },
                                MatchConditionInfos = new List<MatchConditionInfo>(),
                                GirlConditionInfos = new List<GirlConditionInfo>()
                            },
                            new AilmentStepInfo()
                            {
                                StepType = AilmentStepType.MODIFY_MOVE,
                                FlagType = AilmentFlagType.HARD,
                                ComparisonType = NumberComparisonType.EQUAL_TO,
                                StringValue = string.Empty,
                                IntValue = 0,
                                AbilityDefinitionID = -1,
                                BoolValue = false,
                                MoveModifier = new MoveModifier()
                                {
                                    blockMoveCost = false,
                                    blockStaminaCost = false,
                                    blockStaminaRecover = false,
                                    postSwitchGirlFocus = true
                                },
                                MatchModifierInfo = new MatchModifierInfo()
                                {
                                    PointsOperation = NumberCombineOperation.MULTIPLY,
                                    PointsOperation2 = NumberCombineOperation.MULTIPLY,
                                    PointsFactor = 1.0f,
                                    PointsFactor2 = 1.0f,
                                    TokenDefinitionID = -1,
                                    ReplaceDefinitionID = -1,
                                    Absorb = false,
                                    AbsorbAltGirl = false,
                                    ReplacePriority = false,
                                    SkipMostFavFactor = false,
                                    SkipLeastFavFactor = false,
                                    PointsOp = false,
                                    PointsOp2 = false
                                },
                                GiftModifier = new GiftModifier()
                                {
                                    blockGift = false
                                },
                                GirlConditionInfos = new List<GirlConditionInfo>(),
                                MoveConditionInfos = new List<MoveConditionInfo>(),
                                MatchConditionInfos = new List<MatchConditionInfo>(),
                                GiftConditionInfos = new List<GiftConditionInfo>()
                            },
                            new AilmentStepInfo()
                            {
                                StepType = AilmentStepType.ENABLE_TRIGGER,
                                FlagType = AilmentFlagType.HARD,
                                ComparisonType = 0,
                                StringValue = string.Empty,
                                IntValue = 1,
                                AbilityDefinitionID = -1,
                                BoolValue = false,
                                MoveModifier = new MoveModifier()
                                {
                                    blockMoveCost = false,
                                    blockStaminaCost = false,
                                    blockStaminaRecover = false,
                                    postSwitchGirlFocus = false
                                },
                                MatchModifierInfo = new MatchModifierInfo()
                                {
                                    PointsOperation = NumberCombineOperation.MULTIPLY,
                                    PointsOperation2 = NumberCombineOperation.MULTIPLY,
                                    PointsFactor = 1.0f,
                                    PointsFactor2 = 1.0f,
                                    TokenDefinitionID = -1,
                                    ReplaceDefinitionID = -1,
                                    Absorb = false,
                                    AbsorbAltGirl = false,
                                    ReplacePriority = false,
                                    SkipMostFavFactor = false,
                                    SkipLeastFavFactor = false,
                                    PointsOp = false,
                                    PointsOp2 = false
                                },
                                GiftModifier = new GiftModifier()
                                {
                                    blockGift = false
                                },
                                GirlConditionInfos = new List<GirlConditionInfo>(),
                                MoveConditionInfos = new List<MoveConditionInfo>(),
                                MatchConditionInfos = new List<MatchConditionInfo>(),
                                GiftConditionInfos = new List<GiftConditionInfo>()
                            },
                            new AilmentStepInfo()
                            {
                                StepType = AilmentStepType.ENABLE_TRIGGER,
                                FlagType = 0,
                                ComparisonType = 0,
                                StringValue = string.Empty,
                                IntValue = 2,
                                AbilityDefinitionID = -1,
                                BoolValue = false,
                                MoveModifier = new MoveModifier()
                                {
                                    blockMoveCost = false,
                                    blockStaminaCost = false,
                                    blockStaminaRecover = false,
                                    postSwitchGirlFocus = false
                                },
                                MatchModifierInfo = new MatchModifierInfo()
                                {
                                    PointsOperation = NumberCombineOperation.MULTIPLY,
                                    PointsOperation2 = NumberCombineOperation.MULTIPLY,
                                    PointsFactor = 1.0f,
                                    PointsFactor2 = 1.0f,
                                    TokenDefinitionID = -1,
                                    ReplaceDefinitionID = -1,
                                    Absorb = false,
                                    AbsorbAltGirl = false,
                                    ReplacePriority = false,
                                    SkipMostFavFactor = false,
                                    SkipLeastFavFactor = false,
                                    PointsOp = false,
                                    PointsOp2 = false
                                },
                                GiftModifier = new GiftModifier()
                                {
                                    blockGift = false
                                },
                                GirlConditionInfos = new List<GirlConditionInfo>(),
                                MoveConditionInfos = new List<MoveConditionInfo>(),
                                MatchConditionInfos = new List<MatchConditionInfo>(),
                                GiftConditionInfos = new List<GiftConditionInfo>()
                            }
                        }
                    }
                }
            };

            gameDataModder.AddData(NoFocusBagadgeItem);
            gameDataModder.AddData(NoFocusBagadgeAilment);
        }
    }
}
