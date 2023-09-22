using System.Diagnostics;
using System.Formats.Asn1;

namespace ImageController;


public class FileController
{
    protected PickOptions options;


    public async Task<FileResult> FileSelect()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null;
    }

    // 一時ファイルディレクトリ内にあるファイルのパスを取得する関数
    public string GetTmpFilePath(string fileName)
    {
        var cacheDir = FileSystem.Current.CacheDirectory;
        return cacheDir + fileName;
    }
}