using OpenCvSharp;
using System.Diagnostics;

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
        Debug.WriteLine("path:" +path.FullPath);

        // 画像を読み込み一時ファイルディレクトリに書き出す
        Mat image = imageFileController.ImageLoader(path.FullPath);
        imageFileController.ImageFileWriteTmp(image, "tmp.png");

        // 一時ファイルディレクトリからロードし表示する
        var tmpImagePath = imageFileController.GetTmpFilePath("tmp.png");
        image_view.Source = tmpImagePath;
    }
}