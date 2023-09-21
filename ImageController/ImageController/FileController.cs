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
            // TODO:なんらかの例外処理
        }

        return null;
    }
}