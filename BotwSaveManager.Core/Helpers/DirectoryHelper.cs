namespace BotwSaveManager.Core.Helpers
{
    public class DirectoryHelper
    {
        public static void Copy(string src, string dst, bool overwrite = false, string searchPattern = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Parallel.ForEach(Directory.EnumerateFiles(src, searchPattern, searchOption), (srcFile) => {
                string dstFile = $"{dst}/{Path.GetRelativePath(src, srcFile)}";
                Directory.CreateDirectory(Path.GetDirectoryName(dstFile) ?? "");
                File.Copy(srcFile, dstFile, overwrite);
            });
        }
    }
}
