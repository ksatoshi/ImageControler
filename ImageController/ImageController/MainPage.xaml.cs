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
                    new RowDefinition{Height = new GridLength(20)}, //メニューバーが配置されるRow高さは仮で0
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
            
            

            Content = rootGrid;
        }

        /*private MenuBarItem DesktopMenubar()
        {
            var menuBar = new MenuBarItem
            {
                
            };
        }*/
    }
}