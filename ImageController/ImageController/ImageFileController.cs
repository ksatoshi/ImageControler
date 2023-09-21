using System;
using System.Collections.Generic;
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
                //TODO: macOS対応のUUtypeによる記述を追加
            });

            options = new()
            {
                PickerTitle = "画像ファイルを選択してください",
                FileTypes = customFileType
            };
        }
    }
}
