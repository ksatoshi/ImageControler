using System.Diagnostics;
using System.Formats.Asn1;

namespace ImageController;


public class FileController
{
    protected PickOptions Options;


    public async Task<FileResult> FileSelect()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(Options);
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
        try
        {
            var cacheDir = FileSystem.Current.CacheDirectory;
            return System.IO.Path.Combine(cacheDir,fileName);
        }catch (Exception ex)
        {
                Debug.Write(ex.Message);
        }

        return null;
    }

    // 一時ファイルディレクトリ内にあるファイルを削除する関数
    public void DeleteTempFiles()
    {
        var cacheDir = FileSystem.Current.CacheDirectory;
        var fileList = Directory.GetFiles(cacheDir);

        foreach (var file in fileList)
        {
            Debug.WriteLine("Delete:" + file);
            File.Delete(file);
        }
    }
}