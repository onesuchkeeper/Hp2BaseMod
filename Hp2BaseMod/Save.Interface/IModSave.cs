namespace Hp2BaseMod.Save.Interface
{
    public interface IModSave<T>
    {
        /// <summary>
        /// Copies and removes the modded save data from the game save
        /// </summary>
        /// <param name="save"></param>
        void Strip(T save);

        /// <summary>
        /// Applies the mod save to the game save
        /// </summary>
        /// <param name="save"></param>
        void SetData(T save);
    }
}
