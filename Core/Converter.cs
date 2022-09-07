using QoiSharp;
using QoiSharp.Codec;
using StbImageSharp;

namespace Core
{
    /// <summary>
    /// The converter class to convert images to QOI format
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Converts an image from png, jpg or bmp to qoi format.
        /// </summary>
        /// <param name="father">The class that will use this function.
        /// It must implement IOptionsConfirmation interface to work properly</param>
        /// <param name="oldPath">path of the original file, the one that will be converted
        /// into qoi format.</param>
        public static void ConvertToQoi(IOptionsConfirmation father, string oldPath)
        {
            /* Checks if the path is a directory, if so, will skip it and call the
             * ManageDirectory function since it won't be possible to convert
             */
            if (File.GetAttributes(oldPath).HasFlag(FileAttributes.Directory))
            {
                father.ManageDirectory(oldPath);
                return;
            }

            // Starts conversion and creates the new path
            byte[] qoiData;
            var newPath = string.Concat(Path.GetDirectoryName(oldPath),
                Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(oldPath), ".qoi");

            using (var stream = File.OpenRead(oldPath))
            {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                var qoiImage = new QoiImage(image.Data, image.Width, image.Height, (Channels)image.Comp);
                qoiData = QoiEncoder.Encode(qoiImage);
            }

            /* If the file exists, the ConfirmOverwrite will be called to handle it, and
             * if the user doesn't want to overwrite, the file will be skipped
             */
            if (File.Exists(newPath) && !father.ConfirmOverwrite(newPath)) return;

            File.WriteAllBytesAsync(newPath, qoiData);

            // Copies info from original file if the user accepts
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

            // Deletes original file if the user accepts
            if (father.ConfirmDeletion(oldPath))
                File.Delete(oldPath);
        }
    }
}