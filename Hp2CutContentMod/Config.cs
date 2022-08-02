namespace Hp2CutContentMod
{
    public class Config
    {
        public string Hunipop_2_Double_Date_OST_WAV_Path;
        public string Digital_Art_Collection_Path;

        public void ApplyMissingDefaults()
        {
            if (Hunipop_2_Double_Date_OST_WAV_Path == null)
            {
                Hunipop_2_Double_Date_OST_WAV_Path = @"../../music/HuniePop 2 - Double Date OST/WAV";
            }

            if (Digital_Art_Collection_Path == null)
            {
                Digital_Art_Collection_Path = @"Digital Art Collection";
            }
        }
    }
}
