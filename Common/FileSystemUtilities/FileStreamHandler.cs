using System;
using System.IO;
using Common.FileSystemUtilities.Interfaces;

namespace Common.FileSystemUtilities
{
    public class FileStreamHandler : IFileStreamHandler
    {
        public byte[] Read(string path, long offset, int length)
        {
            var data = new byte[length];

            using (var fs = new FileStream(path, FileMode.Open))
            {
                fs.Position = offset;
                var bytesRead = 0;
                while (bytesRead < length)
                {
                    var read = fs.Read(data, bytesRead, length - bytesRead);
                    if (read == 0)
                    {
                        throw new Exception("Could not read file");
                    }
                    bytesRead += read;
                }
            }

            return data;
        }

        public void Write(byte[] data, string fileName, bool firstPart)
        {
            if (File.Exists(fileName))
            {
                if (firstPart)
                {
                    File.Delete(fileName);
                    using (var fs = new FileStream(fileName, FileMode.Create))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                }
                else
                {
                    using (var fs = new FileStream(fileName, FileMode.Append))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                }
            }
            else
            {
                System.IO.FileInfo file = new System.IO.FileInfo(fileName);
                file.Directory.Create();
                using (var fs = new FileStream(fileName, FileMode.Create))
                {
                    fs.Write(data, 0, data.Length);
                }
            }
        }
    }
}
