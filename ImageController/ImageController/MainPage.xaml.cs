using System.Collections;
using System.Net.Mime;

namespace ImageController
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.MainPageLayout();
        }
        
        private void MainPageLayout()
        {
            //Title = "MaiPageLayout";

            /*
             * ImageControllerResultCheckerと同様に
             * Row*4,
             * Column*2
             * を定義
             */
            var rootGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition(),
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    /*
                     * 第一列:画面の7割
                     * 第二列:画面の3割
                     */
                    new ColumnDefinition{Width = new GridLength(7,GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(3,GridUnitType.Star)}
                }
            };
            
            //テスト用のUIパーツ
            /*var dualRowBoxView = new BoxView { Color = Colors.Blue };
            Grid.SetRow(dualRowBoxView, 0);
            Grid.SetColumnSpan(dualRowBoxView,2);
            
            rootGrid.Add(dualRowBoxView);*/

            /*var menuBar = new MenuBar();
            menuBar.Add(new MenuBarItem{Text = "ファイル"});
            menuBar.Add(new MenuBarItem{Text = "ヒストリー"});
            menuBar.Add(new MenuBarItem{Text = "ヘルプ"});
            
            Grid.SetRow(menuBar,0);
            Grid.SetColumnSpan(menuBar,2);*/

            
            // コントロール用のボタン群を生成
            var buttonArrayStack = new StackLayout
            {
                Margin = new Thickness(20), Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                Spacing = 6
            };
            
            buttonArrayStack.Add(new Button{Text = "読み込み"});
            buttonArrayStack.Add(new Button{Text = "保存"});
            buttonArrayStack.Add(new Button{Text = "比較"});
            buttonArrayStack.Add(new Button{Text = "削除"});
            
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
            filterSettingsStack.Add(new Label{Text = "kernel_size"});
            filterSettingsStack.Add(new Entry{Placeholder = "kernel_size"});
            filterSettingsStack.Add(new Label{Text = "閾値1"});
            filterSettingsStack.Add(new Entry{Placeholder = "閾値1"});
            filterSettingsStack.Add(new Label{Text = "閾値2"});
            filterSettingsStack.Add(new Entry{Placeholder = "閾値2"});
            filterSettingsStack.Add(new Button{Text = "実行"});
            
            rootGrid.Add(buttonArrayStack,1,0);
            rootGrid.Add(filterSettingsStack,1,1);
            
            
            
            Content = rootGrid;
        }
        
    }
}