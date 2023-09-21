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

   
}