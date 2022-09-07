using QoiSharp;
using QoiSharp.Codec;
using StbImageSharp;

namespace Core
{
    public class Converter
    {
        public static void StartNewConversion(string path)
        {
            //TODO: Receive string and start ConvertToQoi()
        }

        public static void ConvertToQoi(IOptionsConfirmation father, string oldPath)
        {
            if (File.GetAttributes(oldPath).HasFlag(FileAttributes.Directory))
            {
                father.ManageDirectory(oldPath);
                return;
            }

            byte[] qoiData;
            var newPath = string.Concat(Path.GetDirectoryName(oldPath),
                Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(oldPath), ".qoi");

            using (var stream = File.OpenRead(oldPath))
            {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                var qoiImage = new QoiImage(image.Data, image.Width, image.Height, (Channels)image.Comp);
                qoiData = QoiEncoder.Encode(qoiImage);
            }

            if (File.Exists(newPath) && !father.ConfirmOverwrite(newPath)) return;

            File.WriteAllBytesAsync(newPath, qoiData);
            if (father.ConfirmCopy(oldPath))
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

            if (father.ConfirmDeletion(oldPath))
                File.Delete(oldPath);

            /* TODO: Use this method to start a new thread for a conversion
                * StartNewConversion();
                */
        }
    }
}