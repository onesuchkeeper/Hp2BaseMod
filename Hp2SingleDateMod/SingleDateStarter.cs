// Hp2Sample 2021, By OneSuchKeeper

using System.Collections.Generic;
using System.IO;
using Hp2BaseMod;
using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.GameDataMods;

namespace Hp2SampleMod
{
    public class SingleDateStarter : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
			var LolaSoloPhoto = new PhotoDataMod();

			var LolaSoloRelationshipCutscene = new CutsceneDataMod();

            var EmptyGirlID = 10001;

			var emptySprite = new SpriteInfo("empty.png", true);

            var offscrenevector = new VectorInfo(-1000f,-1000f);

			var emptyPart = new GirlPartInfo()
			{
				PartType = GirlPartType.EYEBROWS,
				PartName = "Empty Part",
				SpriteInfo = emptySprite,
				X = 0,
				Y = 0,
				MirroredPartIndex = -1,
				AltPartIndex = -1
			};

			var emptyExpression = new GirlExpressionSubDefinition()
			{
				expressionType = GirlExpressionType.NEUTRAL,
				partIndexEyebrows = -1,
				partIndexEyes = -1,
				partIndexEyesGlow = -1,
				partIndexMouthClosed = -1,
				partIndexMouthOpen = -1,
				eyesClosed = false,
				mouthOpen = false,
				editorExpanded = false
			};

			var emptyHairstyle = new GirlHairstyleSubDefinition()
			{
				hairstyleName = "Nothing",
				partIndexFronthair = -1,
				partIndexBackhair = -1,
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

			var LolaPair = new GirlPairDataMod()
			{
				Id = EmptyGirlID,
				IsAdditive = false,

				FavQuestions = new List<GirlPairFavQuestionInfo>(),
				GirlDefinitionOneID = EmptyGirlID,
				GirlDefinitionTwoID = 1,
				HasMeetingStyleOne = false,
				HasMeetingStyleTwo = false,
				IntroductionPair = false,
				IntroSidesFlipped = false,
				MeetingLocationDefinitionID = 5,
				MeetingStyleTypeOne = GirlStyleType.RELAXING,
				MeetingStyleTypeTwo = GirlStyleType.RELAXING,
				PhotoDefinitionID = LolaSoloPhoto.Id,
				RelationshipCutsceneDefinitionIDs = new List<int>() { LolaSoloRelationshipCutscene.Id },
				SexDayTime = ClockDaytimeType.MORNING,
				SexLocationDefinitionID = 11,
				SexStyleTypeOne = GirlStyleType.RELAXING,
				SexStyleTypeTwo = GirlStyleType.SEXY,
				SpecialPair = false
			};

            var NoFocusBagadgeItem = new ItemDataMod()
			{ 
			
			};

			var NoStaminaDropBagadgeItem = new ItemDataMod()
			{ 
			
			};

			var NoStaminaSpendBagadgeItem = new ItemDataMod()
			{ 
			
			};

			var EmptyGirl = new GirlDataMod()
            {
                Id = EmptyGirlID,
                IsAdditive = false,

				EditorTab = EditorGirlDefinitionTab.DEFAULT,
				GirlName = "No one.",
				GirlNickName = string.Empty,
				GirlAge = 0,
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
				ShoesType = ItemShoesType.WINTER_BOOTS,
				ShoesAdj = string.Empty,
				UniqueType = ItemUniqueType.TAILORING,
				UniqueAdj = string.Empty,
				BadFoodTypes = new List<ItemFoodType>(),
				GirlPairDefIDs = new List<int>() { LolaPair.Id },
				BaggageItemDefIDs = new List<int>() { NoFocusBagadgeItem.Id, NoStaminaDropBagadgeItem.Id, NoStaminaSpendBagadgeItem.Id },
				UniqueItemDefIDs = new List<int>(),
				ShoesItemDefIDs = new List<int>(),
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
				PartIndexesPhonemes = new List<int>() { 0, 0, 0, 0, 0 },
				PartIndexesPhonemesTeeth = new List<int>() { 0, 0, 0, 0, 0 },
				Parts = new List<GirlPartInfo>() { emptyPart },
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

			gameDataMod.AddData(EmptyGirl);
        }

        private static void foo()
        {
            // Make pairs between all girls and empty girl
        }
    }
}
