using OpenCvSharp;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ImageController;

public partial class MainPage : ContentPage
{
    private ImageFileController imageFileController;

    public MainPage()
    {
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Light; // ライトモードに固定
        imageFileController = new ImageFileController();
    }

    private async void Load_Button_Clicked(object sender, EventArgs e)
    {
        // ファイルパスを取得
        var path =await imageFileController.FileSelect();
        if (path != null)
        {
            Debug.WriteLine("path:" + path.FullPath);
            Guid fileUuid = Guid.NewGuid();
            string tmpFileName = $"{fileUuid.ToString()}.png";

            // 画像を読み込み一時ファイルディレクトリに書き出す
            Mat image = imageFileController.ImageLoader(path.FullPath);
            imageFileController.ImageFileWriteTmp(image, tmpFileName);

            // 一時ファイルディレクトリからロードし表示する
            var tmpImagePath = imageFileController.GetTmpFilePath(tmpFileName);
            image_view.Source = tmpImagePath;
        }
        else
        {
            await DisplayAlert("File Loading Error!!", "ファイルの読み込みに失敗しました。\n再度実行してください", "OK");
        }
    }

    private void Clear_Button_Clicked(object sender, EventArgs e)
    {
        image_view.Source = "no_image.png";
        imageFileController.DeleteTempFiles();
    }
}