using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    public class SpriteInfoBytes : IGameDefinitionInfo<Sprite>
    {
        public byte[] Data;

        public void SetData(ref Sprite def, GameDefinitionProvider gameData, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (Data == null)
            {
                if (insertStyle == InsertStyle.assignNull)
                {
                    def = null;
                }
            }
            else
            {
                var texture = TextureUtility.LoadFromBytes(Data);
                def = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                ModInterface.Log.LogLine($"{(def == null ? "Failed to load" : "Loaded")} byte {nameof(Sprite)}");
            }

            Data = null;
        }
    }
}
