using UnityEngine;
using System;

namespace Hp2BaseMod.Utility
{
    public static class WAV
    {
        public static AudioClip LoadAudioClip(byte[] fileBytes, int offsetSamples = 0, string name = "wav")
        {
            //string riff = Encoding.ASCII.GetString (fileBytes, 0, 4);
            //string wave = Encoding.ASCII.GetString (fileBytes, 8, 4);
            int subchunk1 = BitConverter.ToInt32(fileBytes, 16);
            UInt16 audioFormat = BitConverter.ToUInt16(fileBytes, 20);

            // NB: Only uncompressed PCM wav files are supported.
            string formatCode = FormatCode(audioFormat);
            Debug.AssertFormat(audioFormat == 1 || audioFormat == 65534, "Detected format code '{0}' {1}, but only PCM and WaveFormatExtensable uncompressed formats are currently supported.", audioFormat, formatCode);

            UInt16 channels = BitConverter.ToUInt16(fileBytes, 22);
            int sampleRate = BitConverter.ToInt32(fileBytes, 24);
            //int byteRate = BitConverter.ToInt32 (fileBytes, 28);
            //UInt16 blockAlign = BitConverter.ToUInt16 (fileBytes, 32);
            UInt16 bitDepth = BitConverter.ToUInt16(fileBytes, 34);

            int headerOffset = 16 + 4 + subchunk1 + 4;
            int subchunk2 = BitConverter.ToInt32(fileBytes, headerOffset);
            //Debug.LogFormat ("riff={0} wave={1} subchunk1={2} format={3} channels={4} sampleRate={5} byteRate={6} blockAlign={7} bitDepth={8} headerOffset={9} subchunk2={10} filesize={11}", riff, wave, subchunk1, formatCode, channels, sampleRate, byteRate, blockAlign, bitDepth, headerOffset, subchunk2, fileBytes.Length);

            float[] data;
            switch (bitDepth)
            {
                case 8:
                    data = Convert8BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                    break;
                case 16:
                    data = Convert16BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                    break;
                case 24:
                    data = Convert24BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                    break;
                case 32:
                    data = Convert32BitByteArrayToAudioClipData(fileBytes, headerOffset, subchunk2);
                    break;
                default:
                    throw new Exception(bitDepth + " bit depth is not supported.");
            }

            AudioClip audioClip = AudioClip.Create(name, data.Length, (int)channels, sampleRate, false);
            audioClip.SetData(data, 0);
            return audioClip;
        }

        #region wav file bytes to Unity AudioClip conversion methods

        private static float[] Convert8BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
        {
            int wavSize = BitConverter.ToInt32(source, headerOffset);
            headerOffset += sizeof(int);
            Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 8-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

            float[] data = new float[wavSize];

            sbyte maxValue = sbyte.MaxValue;

            int i = 0;
            while (i < wavSize)
            {
                data[i] = (float)source[i] / maxValue;
                ++i;
            }

            return data;
        }

        private static float[] Convert16BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
        {
            int wavSize = BitConverter.ToInt32(source, headerOffset);
            headerOffset += sizeof(int);
            Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 16-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

            int x = sizeof(Int16); // block size = 2
            int convertedSize = wavSize / x;

            float[] data = new float[convertedSize];

            Int16 maxValue = Int16.MaxValue;

            int offset = 0;
            int i = 0;
            while (i < convertedSize)
            {
                offset = i * x + headerOffset;
                data[i] = (float)BitConverter.ToInt16(source, offset) / maxValue;
                ++i;
            }

            Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

            return data;
        }

        private static float[] Convert24BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
        {
            int wavSize = BitConverter.ToInt32(source, headerOffset);
            headerOffset += sizeof(int);
            Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 24-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

            int x = 3; // block size = 3
            int convertedSize = wavSize / x;

            int maxValue = Int32.MaxValue;

            float[] data = new float[convertedSize];

            byte[] block = new byte[sizeof(int)]; // using a 4 byte block for copying 3 bytes, then copy bytes with 1 offset

            int offset = 0;
            int i = 0;
            while (i < convertedSize)
            {
                offset = i * x + headerOffset;
                Buffer.BlockCopy(source, offset, block, 1, x);
                data[i] = (float)BitConverter.ToInt32(block, 0) / maxValue;
                ++i;
            }

            Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

            return data;
        }

        private static float[] Convert32BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
        {
            int wavSize = BitConverter.ToInt32(source, headerOffset);
            headerOffset += sizeof(int);
            Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 32-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

            int x = sizeof(float); //  block size = 4
            int convertedSize = wavSize / x;

            Int32 maxValue = Int32.MaxValue;

            float[] data = new float[convertedSize];

            int offset = 0;
            int i = 0;
            while (i < convertedSize)
            {
                offset = i * x + headerOffset;
                data[i] = (float)BitConverter.ToInt32(source, offset) / maxValue;
                ++i;
            }

            Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

            return data;
        }

        #endregion

        private static string FormatCode(UInt16 code)
        {
            switch (code)
            {
                case 1:
                    return "PCM";
                case 2:
                    return "ADPCM";
                case 3:
                    return "IEEE";
                case 7:
                    return "μ-law";
                case 65534:
                    return "WaveFormatExtensable";
                default:
                    Debug.LogWarning("Unknown wav code format:" + code);
                    return "";
            }
        }

    }
}
