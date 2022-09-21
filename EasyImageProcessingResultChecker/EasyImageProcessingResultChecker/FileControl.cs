using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using OpenCvSharp;

namespace EasyImageProcessingResultChecker
{
    public class FileControl
    {
        public Mat Image_Loading()
        {
            Mat image;

            OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "画像|*.jpg;*.jpeg;*.png;*.tif";

            //ファイル選択
            if (dialog.ShowDialog() != true)
            {
                return null;
            }
            else
            {
                image = new Mat(dialog.FileName);
                return image;
            }

        }

        public void Image_Saving(Mat img)
        {
            SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "画像|*.png";

            if(dialog.ShowDialog() != true)
            {
                MessageBox.Show("保存先を指定してください", "保存先指定エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                img.SaveImage(dialog.FileName);
            }
        }
    }
}
