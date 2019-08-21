using Xamarin.Essentials;

namespace ContactsApp.Helpers
{
    public class FileHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
