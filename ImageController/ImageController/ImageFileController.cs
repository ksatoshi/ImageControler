using OpenCvSharp;
using System.Diagnostics;

namespace ImageController
{
    public class ImageFileController : FileController
    {
        public ImageFileController()
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                {DevicePlatform.WinUI, new[] {"bmp","dib","jpeg","jpg","jpeg","png","pbm","pgm","ppm","sr","ras","tiff","tif"}},
                {DevicePlatform.macOS, new[]{"png","jpeg","tiff","bmp"} }
            });

            Options = new()
            {
                PickerTitle = "画像ファイルを選択してください",
                FileTypes = customFileType
            };
        }

        // 画像を一時ファイルディレクトリに書き出す関数
        public void ImageFileWriteTmp(Mat image,String fileName)
        {
            var cacheDir = FileSystem.Current.CacheDirectory;

            try
            {
                string tmpFIlePath = System.IO.Path.Combine(cacheDir, fileName);

                Cv2.ImWrite(tmpFIlePath,image);
                Debug.WriteLine("Write:" +tmpFIlePath);
            }catch(Exception ex) {
                Debug.WriteLine(ex.Message);
            }    
        }

        // 画像を読み込むための関数
        public Mat ImageLoader(string fileName)
        {
            try
            {
                Mat image = Cv2.ImRead(fileName);
                return image;
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
