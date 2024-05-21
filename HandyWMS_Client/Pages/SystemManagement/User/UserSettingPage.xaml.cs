using HandyControl.Tools.Extension;
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
using System.Windows.Threading;

namespace HandyWMS_Client.Pages.SystemManagement.User
{
    /// <summary>
    /// UserSettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserSettingPage : Page
    {

        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserSettingPage));

        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set
            {
                SetValue(ReadOnlyProperty, value);
                if (ReadOnly)
                {
                    Button_Save.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Button_Save.Visibility = Visibility.Visible;
                }
            }
        }

        public static readonly DependencyProperty OperateTitleProperty =
            DependencyProperty.Register("OperateTitle", typeof(string), typeof(UserSettingPage));

        public string OperateTitle
        {
            get { return (string)GetValue(OperateTitleProperty); }
            set
            {
                SetValue(OperateTitleProperty, value);
                TextBlock_OperateTitle.Text = value;
            }
        }

        public UserSettingPage()
        {
            InitializeComponent();
            ReadOnly = true;
            OperateTitle = "详情";
        }
    }
}
