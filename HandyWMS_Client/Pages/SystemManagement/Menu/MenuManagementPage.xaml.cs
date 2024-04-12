using HandyControl.Controls;
using HandyWMS_Client.Objects.SystemManagement.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HandyWMS_Client.Pages.SystemManagement.Menu
{
    /// <summary>
    /// MenuManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class MenuManagementPage : Page
    {
        public MenuManagementPage()
        {
            InitializeComponent();
        }

        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {
            StartEdit();
        }

        private void Button_Cancle_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认取消编辑？", "取消");
            if(messageBoxResult == MessageBoxResult.OK)
            {
                EndEdit();
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            StartEdit();
            // 详情界面清空
            foreach (var item in Panel_Properties.Children)
            {
                if (item.GetType() == typeof(HandyControl.Controls.TextBox))
                {
                    ((HandyControl.Controls.TextBox)item).Text = "";
                }
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认删除？", "删除");
            if (messageBoxResult == MessageBoxResult.OK)
            {
                RefreshMenuList();
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            EndEdit();
            RefreshMenuList();
        }

        private void StartEdit()
        {
            // 按键组可见性调整
            Panel_Edit.Visibility = Visibility.Visible;
            Panel_Normal.Visibility = Visibility.Collapsed;
            // 详情界面只读调整
            Panel_Properties.IsEnabled = true;
        }

        private void EndEdit()
        {
            // 按键组可见性调整
            Panel_Edit.Visibility = Visibility.Collapsed;
            Panel_Normal.Visibility = Visibility.Visible;
            // 详情界面只读调整
            Panel_Properties.IsEnabled = false;
        }

        private void RefreshMenuList()
        {
            List<TreeViewItem> menuItems = new List<TreeViewItem>();
            TreeViewItem menu1 = new TreeViewItem();
            menu1.Header = "菜单1";
            TreeViewItem menu1_1 = new TreeViewItem();
            menu1_1.Header = "菜单1_1";
            menu1_1.Name = "menu1_1";
            menu1.Items.Add(menu1_1);
            TreeViewItem menu1_2 = new TreeViewItem();
            menu1_2.Header = "菜单1_2";
            menu1_2.Name = "menu1_2";
            menu1.Items.Add(menu1_2);
            menuItems.Add(menu1);
            TreeViewItem menu2 = new TreeViewItem();
            menu2.Header = "菜单2";
            TreeViewItem menu2_1 = new TreeViewItem();
            menu2_1.Header = "菜单2_1";
            menu2_1.Name = "menu2_1";
            menu2.Items.Add(menu2_1);
            TreeViewItem menu2_2 = new TreeViewItem();
            menu2_2.Header = "菜单2_2";
            menu2_2.Name = "menu2_2";
            menu2.Items.Add(menu2_2);
            menuItems.Add(menu2);
            Tree_Menu.Items.Clear();
            for (int i = 0; i < menuItems.Count; i++)
            {
                Tree_Menu.Items.Add(menuItems[i]);
            }
        }
    }
}
