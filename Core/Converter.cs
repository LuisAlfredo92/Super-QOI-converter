using QoiSharp;
using QoiSharp.Codec;
using StbImageSharp;

namespace Core
{
    public class Converter : IOverwritingConfirmation
    {
        public static void StartNewConversion(string path)
        {
            //TODO: Receive string and start ConvertToQoi()
        }

        public static async void ConvertToQoi(string oldPath, bool copyData, bool deleteSource)
        {
            byte[] qoiData;
            var newPath = string.Concat(Path.GetDirectoryName(oldPath),
                Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(oldPath), ".qoi");

            await using (var stream = File.OpenRead(oldPath))
            {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                var qoiImage = new QoiImage(image.Data, image.Width, image.Height, (Channels)image.Comp);
                qoiData = QoiEncoder.Encode(qoiImage);
            }

            if (File.Exists(newPath) && !IOverwritingConfirmation.ConfirmateOverwrite(newPath)) return;

            await File.WriteAllBytesAsync(newPath, qoiData);
            if (copyData)
            {
                FileInfo oldInfo = new(oldPath);
                _ = new FileInfo(newPath)
                {
                    Attributes = oldInfo.Attributes,
                    CreationTime = oldInfo.CreationTime,
                    LastAccessTime = oldInfo.LastAccessTime,
                    LastWriteTime = oldInfo.LastWriteTime
                };
            }

            if (deleteSource)
                File.Delete(oldPath);

            /* TODO: Use this method to start a new thread for a conversion
                * StartNewConversion();
                */
        }
    }
}