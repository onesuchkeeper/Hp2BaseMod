// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// Wrapper to allow nullable int ids and impliment logging in gamedata
    /// </summary>
    public class GameDataProvider
    {
        private GameData _decorated;

        public GameDataProvider(GameData decorated)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public AbilityDefinition GetAbility(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Abilities.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(AbilityDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public DlcDefinition GetDlc(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Dlcs.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(DlcDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public PauseDefinition GetPause(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Pause.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(PauseDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public LocationDefinition GetLocation(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Locations.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(LocationDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public GirlDefinition GetGirl(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Girls.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(GirlDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public GirlPairDefinition GetGirlPair(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.GirlPairs.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(GirlPairDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public PhotoDefinition GetPhoto(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Photos.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(PhotoDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public ItemDefinition GetItem(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Items.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(ItemDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public TokenDefinition GetToken(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Tokens.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(TokenDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public AilmentDefinition GetAilment(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Ailments.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(AilmentDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public DialogTriggerDefinition GetDialogTrigger(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.DialogTriggers.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(DialogTriggerDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public QuestionDefinition GetQuestion(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Questions.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(QuestionDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public CutsceneDefinition GetCutscene(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Cutscenes.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(CutsceneDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public EnergyDefinition GetEnergy(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Energy.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(EnergyDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }

        public CodeDefinition GetCode(int? id)
        {
            if (id.HasValue && id.Value != -1)
            {
                var result = _decorated.Codes.Get(id.Value);

                ModInterface.Instance.LogLine($"{(result == null ? "Failed to find" : "Found")} {nameof(CodeDefinition)} {id} {result?.name}");

                return result;
            }

            return null;
        }
    }
}
