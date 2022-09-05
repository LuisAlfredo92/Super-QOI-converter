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

        public static async void ConvertToQoi(string oldPath, bool copyData, bool deleteSource)
        {
            try
            {
                byte[] qoiData;
                string newPath = Path.GetDirectoryName(oldPath) + Path.GetFileNameWithoutExtension(oldPath)
                    + ".qoi";
                await using (var stream = File.OpenRead(oldpath))
                {
                    var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    var qoiImage = new QoiImage(image.Data, image.Width, image.Height, (Channels)image.Comp);
                    qoiData = QoiEncoder.Encode(qoiImage);
                }

                await File.WriteAllBytesAsync(@"C:\Users\luisa\Desktop\Dibujo.qoi", qoiData);

                if (copyData)
                {
                    //TODO: Using FileInfo class, copy attributes and dates from oldPath
                }

                if (deleteSource)
                {
                    //TODO: With File class, delete the oldPath
                }

                /* TODO: Use this method to start a new thread for a conversion
                * StartNewConversion();
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}