using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageController
{
    public class ImageFileController : FileController
    {
        public ImageFileController()
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                {DevicePlatform.WinUI, new[] {"bmp","dib","jpeg","jpg","jpe","png","pbm","pgm","ppm","sr","ras","tiff","tif"}},
                {DevicePlatform.macOS, new[]{"png","jpeg","tiff","bmp"} }
            });

            options = new()
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
                Cv2.ImWrite(cacheDir + fileName, image);
                Debug.WriteLine("Write:" + cacheDir + "." + fileName);
            }catch(Exception ex) {
                Debug.WriteLine(ex.Message);
            }    
        }
    }
}
