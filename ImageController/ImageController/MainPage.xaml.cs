namespace ImageController;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        MainPageLayout();
        Application.Current.UserAppTheme = AppTheme.Light; // ライトモードに固定
    }

    private void MainPageLayout()
    {
        Image nonLoadingPlaceHolder;

        try
        {
            //画像未読み込み時用のプレースホルダーをロード
            nonLoadingPlaceHolder = new Image { Source = ImageSource.FromFile("no_image.png"), Aspect = Aspect.Center };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        /*
         * ImageControllerResultCheckerと同様に
         * Row*2,
         * Column*2
         * を定義
         */
        var rootGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition()
            },
            ColumnDefinitions =
            {
                /*
                 * 第一列:画面の7割
                 * 第二列:画面の3割
                 */
                new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
            }
        };

        // コントロール用のボタン群を生成
        var buttonArrayStack = new StackLayout
        {
            Margin = new Thickness(20), Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Start,
            Spacing = 6
        };

        buttonArrayStack.Add(new Button { Text = "読み込み"});
        buttonArrayStack.Add(new Button { Text = "保存" });
        buttonArrayStack.Add(new Button { Text = "比較" });
        buttonArrayStack.Add(new Button { Text = "削除" });

        // 各種パラメーター入力用テキストボックス群
        var filterSettingsStack = new StackLayout
        {
            Margin = new Thickness(20),
            Spacing = 6
        };

        var filterList = new List<string>();
        filterList.Add("ガウシアンフィルタ");
        filterList.Add("メディアンフィルタ");
        filterList.Add("グレースケール");

        var filterPicker = new Picker { Title = "フィルターセレクター" };
        filterPicker.ItemsSource = filterList;

        filterSettingsStack.Add(filterPicker);
        filterSettingsStack.Add(new Label { Text = "kernel_size" });
        filterSettingsStack.Add(new Entry { Placeholder = "kernel_size" });
        filterSettingsStack.Add(new Label { Text = "閾値1" });
        filterSettingsStack.Add(new Entry { Placeholder = "閾値1" });
        filterSettingsStack.Add(new Label { Text = "閾値2" });
        filterSettingsStack.Add(new Entry { Placeholder = "閾値2" });
        filterSettingsStack.Add(new Button { Text = "実行" });

        rootGrid.Add(buttonArrayStack, 1); // column:1,row:0
        rootGrid.Add(filterSettingsStack, 1, 1); // column:1,row:1

        Grid.SetColumn(nonLoadingPlaceHolder, 0);
        Grid.SetRowSpan(nonLoadingPlaceHolder, 2);
        rootGrid.Add(nonLoadingPlaceHolder, 0); // column:0,row:0

        Content = rootGrid;
    }
}