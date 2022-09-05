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
                //TODO: Convertion

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