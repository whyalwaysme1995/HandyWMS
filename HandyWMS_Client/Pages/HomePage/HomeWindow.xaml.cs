using HandyControl.Controls;
using HandyWMS_Client.Objects.SystemManagement.Menus;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HandyWMS_Client.Pages.HomePage
{
    /// <summary>
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : System.Windows.Window
    {
        public HomeWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            // SystemParameters.MaximizedPrimaryScreenHeight;
            InitializeComponent();
            Dispatcher.Invoke(new Action(() => 
            {
                LoadResource();
            }), DispatcherPriority.Loaded);
        }

        private void LoadResource()
        {
            // System Info
            // TODO system name
            Label_System.Content = "HandyWMS";
            // TODO logo
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.CacheOption = BitmapCacheOption.OnLoad;
            logo.StreamSource = new MemoryStream(Resource.open_graph_logo);
            logo.EndInit();
            logo.Freeze();
            Image_System.Source = logo;
            // TODO system version
            Label_System_Version.Content = "v0.0.1";
            Label_System_Version.Width = Window_Main.Width - 310 - Label_System.Width;

            // TODO menu
            List<WMSMenuItem> menuItems = new List<WMSMenuItem>();
            WMSMenuItem menu1 = new WMSMenuItem();
            menu1.Header = "菜单1";
            WMSMenuItem menu1_1 = new WMSMenuItem();
            menu1_1.Header = "菜单1_1";
            menu1_1.Name = "menu1_1";
            menu1_1.Page = "HandyWMS_Client.Pages.SystemManagement.User.UserManagementPage";
            menu1.Items.Add(menu1_1);
            WMSMenuItem menu1_2 = new WMSMenuItem();
            menu1_2.Header = "菜单1_2";
            menu1_2.Name = "menu1_2";
            menu1_2.Page = "HandyWMS_Client.Pages.SystemManagement.Menu.MenuManagementPage";
            menu1.Items.Add(menu1_2);
            menuItems.Add(menu1);
            WMSMenuItem menu2 = new WMSMenuItem();
            menu2.Header = "菜单2";
            WMSMenuItem menu2_1 = new WMSMenuItem();
            menu2_1.Header = "菜单2_1";
            menu2_1.Name = "menu2_1";
            menu2_1.Page = "HandyWMS_Client.Pages.SystemManagement.Menu.MenuSettingPage";
            menu2.Items.Add(menu2_1);
            WMSMenuItem menu2_2 = new WMSMenuItem();
            menu2_2.Header = "菜单2_2";
            menu2_2.Name = "menu2_2";
            menu2_2.Page = "HandyWMS_Client.Pages.SystemManagement.User.UserSettingPage";
            menu2.Items.Add(menu2_2);
            menuItems.Add(menu2);
            SideMenu_Main.Items.Clear();
            for (int i = 0; i < menuItems.Count; i++)
            {
                SideMenu_Main.Items.Add(menuItems[i]);
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show("确认关闭系统？", "关闭提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_Max_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized) {
                this.WindowState = WindowState.Normal;
                Button_Max.BorderBrush = Brushes.CornflowerBlue;
                Button_Max.Background = Brushes.CornflowerBlue;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                Button_Max.BorderBrush = Brushes.BurlyWood;
                Button_Max.Background = Brushes.BurlyWood;
            }
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void SideMenu_Main_Selection_Changed(object sender, RoutedEventArgs e)
        {
            WMSMenuItem info = (WMSMenuItem)((HandyControl.Data.FunctionEventArgs<object>)e).Info;
            ItemCollection items = TabControl_Page.Items;
            foreach (var item in items)
            {
                if (item is HandyControl.Controls.TabItem)
                {
                    var tab = (HandyControl.Controls.TabItem)item;
                    if (tab.Name.Equals(info.Name))
                    {
                        tab.IsSelected = true;
                        return;
                    }
                }
            }
            HandyControl.Controls.TabItem tabItem = new HandyControl.Controls.TabItem();
            tabItem.Header = info.Header;
            tabItem.Name = info.Name;
            tabItem.IsSelected = true;
            Frame frame = new Frame();
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            dynamic? obj = assembly.CreateInstance(info.Page!); // 创建类的实例，返回为 object 类型，需要强制类型转换
            frame.Content = obj;
            tabItem.Content = frame;
            TabControl_Page.Items.Add(tabItem);
        }

        private void StackPanel_Header_SystemInfo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
