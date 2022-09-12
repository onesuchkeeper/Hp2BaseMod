using System;
using System.Collections.Generic;
using System.IO;

namespace Hp2BaseMod.Utility
{
    public static class FileUtility
    {
        public static IEnumerable<byte[]> SplitFile(string SourceFile)
        {
            var result = new List<byte[]>();

            try
            {
                var fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);

                var intBuffer = new byte[sizeof(int)];

                while (true)
                {
                    if (fs.Read(intBuffer, 0, intBuffer.Length) == 0)
                    {
                        //reached end of the file
                        break;
                    }
                    else
                    {
                        var size = BitConverter.ToInt32(intBuffer, 0);

                        var fileBuffer = new byte[size];

                        fs.Read(fileBuffer, 0, size);
                        result.Add(fileBuffer);
                    }
                }

                fs.Close();
            }
            catch (Exception Ex)
            {
                throw new ArgumentException(Ex.Message);
            }

            return result;
        }

        public static void CombineFiles(string resultPath, IEnumerable<string> filePaths)
        {
            var resultFs = new FileStream(resultPath, FileMode.Create, FileAccess.Write);
            var streams = new List<FileStream>();

            foreach(var path in filePaths)
            {
                var bytes = File.ReadAllBytes(path);

                resultFs.Write(BitConverter.GetBytes(((int)bytes.Length)), 0, sizeof(int));
                resultFs.Write(bytes, 0, bytes.Length);
            }

            resultFs.Close();
        }
    }
}
