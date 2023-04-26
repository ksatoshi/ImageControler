using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EasyImageProcessingResultChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private Mat image;
        private int id;
        private List<Mat> image_history;
        public Dictionary<int,string> Filter_List { get; set; }
        public ObservableCollection<FilterHistory> filter_history;

        private FileControl fc;

        public MainWindow()
        {

            fc = new FileControl();

            id = 0;
            image_history = new List<Mat>();

            Filter_List = new Dictionary<int, string>()
            {
                {0,"ガウシアンフィルタ" },
                {1,"メディアンフィルタ" },
                {2,"グレースケール" }
            };

            filter_history = new ObservableCollection<FilterHistory>();

            InitializeComponent();

            set_default_img();

            listview_history.ItemsSource = filter_history;
            DataContext = this;
        }

        /// <summary>
        /// UIの画像を更新するための関数
        /// </summary>
        private void rewrite_image()
        {
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.StreamSource = image.ToMemoryStream();
            bmpImage.EndInit();

            show_img.Source = bmpImage;
        }

        /// <summary>
        /// デフォルト画像を表示するための関数
        /// </summary>
        private void set_default_img()
        {
            //内部リソースから画像をbitmapとして読み込みUIにセット
            show_img.Source = BitmapFrame.Create(System.Reflection.Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("EasyImageProcessingResultChecker.no_image.png"));
        }

        
        private void obj_load_Click(object sender, RoutedEventArgs e)
        {
            image = fc.Image_Loading();
            Debug.Print(image.Channels().ToString());

            if (image != null)
            {
                filter_history.Clear();
                rewrite_image();
            }
            else
            {
                MessageBox.Show("画像を取得できませんでした", "画像取得エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void obj_del_Click(object sender, RoutedEventArgs e)
        {
            if (image == null && image_history.Count == 0)
            {
                MessageBox.Show("画像は読み込まれていません", "削除", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                image_history.Clear();
                id = 0;
                image = null;
                filter_history.Clear();
                set_default_img();
            }
        }

        private void obj_save_Click(object sender, RoutedEventArgs e)
        {
            if(image==null && image_history.Count == 0)
            {
                MessageBox.Show("画像は読み込まれていません", "保存", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                fc.Image_Saving(image);
            }
        }

        private void combo_filterSelector_DropDownClosed(object sender, System.EventArgs e)
        {

        }

        private void btn_processing_run_Click(object sender, RoutedEventArgs e)
        {   
            if(combo_filterSelector.SelectedValue != null)
            {
                int selected_filter_id = (int)combo_filterSelector.SelectedValue;
                
                if(selected_filter_id == 0)
                {

                    int kernel_size = Int32.Parse(textbox_kernel_size.Text);

                    /* kernel_sizeが奇数又は0のときのみ処理
                     * それ以外の場合はエラーウインドウを表示
                     * 参考)http://opencv.jp/opencv-2.1/cpp/image_filtering.html
                    */
                    if (kernel_size % 2 == 1 || kernel_size == 0)
                    {
                        Cv2.GaussianBlur(image, image, new OpenCvSharp.Size(kernel_size, kernel_size), 1);
                        filter_history.Add(new FilterHistory()
                        {
                            filter_name = "ガウシアンフィルタ",
                            kernel_size = kernel_size,
                            id = id
                        });
                    }
                    else
                    {
                        textbox_kernel_size.Text = '0'.ToString();
                        MessageBox.Show("kernel_sizeが不正な値です。\n0または奇数を入力してください。", "kernel_sizeエラー",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    

                }
                else if(selected_filter_id == 1)
                {

                    int kernel_size = Int32.Parse(textbox_kernel_size.Text);

                    /*
                     * kernel_sizeが1以上の奇数の場合のみ処理
                     * それ以外の場合はエラーウインドウを表示
                     */
                    if(kernel_size >= 1 && kernel_size % 2 == 1)
                    {
                        Cv2.MedianBlur(image, image, kernel_size);
                        filter_history.Add(new FilterHistory()
                        {
                            filter_name = "メディアンフィルタ",
                            kernel_size = kernel_size,
                            id = id
                        });
                    }
                    else
                    {
                        textbox_kernel_size.Text = '0'.ToString();
                        MessageBox.Show("kernel_sizeが不正な値です。\n1以上の奇数を入力してください。", "kernel_sizeエラー",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else if(selected_filter_id == 2)
                {
                    /*
                     *　画像のチャンネル数が3の場合のみ処理
                     *　それ以外の場合は処理せずエラーウインドウを表示
                     */
                    try
                    {
                        if(image.Channels() == 3)
                        {
                            image = image.CvtColor(ColorConversionCodes.RGB2GRAY);
                            filter_history.Add(new FilterHistory()
                            {
                                filter_name = "グレースケール",
                                id = id
                            });
                            Debug.Print(image.Channels().ToString());
                        }
                        else
                        {
                            MessageBox.Show("チャンネル数が不正な値です。\nチャンネル数が3である画像を使用してください。", "チャンネル数エラー",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }
                    catch(OpenCvSharp.OpenCVException)
                    {
                        MessageBox.Show("グレースケールにおいてエラーが発生しました", "グレースケールエラー",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                image_history.Add(image);
                id += 1;
                rewrite_image();
            }
        }

        /// <summary>
        /// TextBoxに数字のみを入力するように制限するための関数
        /// TextBoxのPreviewTextInputに設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox_isNumber_checker(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        /// <summary>
        /// フィルター履歴が選択されたとき履歴から画像を表示する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listview_history_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int selected_id = listview_history.SelectedIndex;
            Debug.Print("selected id:" + selected_id.ToString());

            image = image_history[selected_id];
            rewrite_image();
        }

        private void menue_verinfo_click(object sender, RoutedEventArgs e)
        {
            VersionWindow vw = new VersionWindow();
            vw.Show();
        }
    }
}
