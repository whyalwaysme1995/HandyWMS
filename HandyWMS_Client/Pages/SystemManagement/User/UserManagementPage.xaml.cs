using HandyControl.Controls;
using HandyWMS_Client.Constructs.Converts;
using HandyWMS_Client.Constructs.SystemEnums;
using HandyWMS_Client.Objects.Base;
using HandyWMS_Client.Objects.SystemManagement.Users;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace HandyWMS_Client.Pages.SystemManagement.User
{
    /// <summary>
    /// UserManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
            Dispatcher.Invoke(new Action(InitDataGrid), DispatcherPriority.Loaded);
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            // TODO 打开详情界面
            AddUser();
        }
        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {
            // TODO 获取id
            ModifyUser(1);
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            // TODO 获取id
            DeleteUser(1);
        }
        private void Button_Query_Click(object sender, RoutedEventArgs e)
        {
            QueryUserList(1);
        }
        private void Button_More_Click(object sender, RoutedEventArgs e)
        {
            if (Grid_Query.MaxHeight == double.PositiveInfinity)
            {
                Grid_Query.MaxHeight = 70;
                Grid_Query.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                Grid_Query.MaxHeight = double.PositiveInfinity;
            }
        }
        private void DataGrid_User_ColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            // TODO 列序号记录
            // MessageBox.Show("ColumnReordered");
        }
        private void Pagination_User_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            // HandyControl.Controls.MessageBox.Show(((Pagination)sender).PageIndex + "");
            // HandyControl.Controls.MessageBox.Show(e.Info + "");
            QueryUserList(e.Info);
        }
        private void ComboBox_PageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QueryUserList(1);
        }
        private void Drawer_Detail_Closed(object sender, RoutedEventArgs e)
        {
            UserSettingPage content = (UserSettingPage)Frame_Detail.Content;
            if (!content.ReadOnly)
            {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认关闭？", "关闭");
                if (messageBoxResult != MessageBoxResult.OK)
                {
                    // TODO 查询
                    QueryUserList(1);
                    Drawer_Detail.IsOpen = true;
                }
            }
        }
        private void Drawer_Detail_Opened(object sender, RoutedEventArgs e)
        {
            
        }

        private void InitDataGrid()
        {
            // TODO 获取表单列定义
            List<WMSTableRow> tableRows = new List<WMSTableRow>();
            WMSTableRow tableRow1 = new WMSTableRow
            {
                Index = 1,
                Code = "Index",
                Description = "序号",
                Width = 50,
                Type = ContentTypeEnum.DOUBLE,
                Align = HorizontalAlignment.Center
            };
            tableRows.Add(tableRow1);
            WMSTableRow tableRow2 = new WMSTableRow()
            {
                Index = 2,
                Code = "ImgPath",
                Description = "头像",
                Width = 80,
                Type = ContentTypeEnum.FILE,
                Align = HorizontalAlignment.Center
            };
            tableRows.Add(tableRow2);
            WMSTableRow tableRow3 = new WMSTableRow()
            {
                Index = 3,
                Code = "Name",
                Description = "名称",
                Width = 150,
                Type = ContentTypeEnum.STRING,
                Align = HorizontalAlignment.Center
            };
            tableRows.Add(tableRow3);
            WMSTableRow tableRow4 = new WMSTableRow()
            {
                Index = 4,
                Code = "IsSelected",
                Description = "有效性",
                Width = 100,
                Type = ContentTypeEnum.BOOL,
                Align = HorizontalAlignment.Center
            };
            tableRows.Add(tableRow4);
            WMSTableRow tableRow5 = new WMSTableRow()
            {
                Index = 5,
                Code = "Type",
                Description = "角色",
                Width = 200,
                Type = ContentTypeEnum.SELECT_MANY,
                Align = HorizontalAlignment.Center
            };
            tableRows.Add(tableRow5);
            WMSTableRow tableRow6 = new WMSTableRow()
            {
                Index = 6,
                Code = "Remark",
                Description = "备注",
                Width = 300,
                Type = ContentTypeEnum.STRING,
                Align = HorizontalAlignment.Left,
                TextTrimming = TextTrimming.CharacterEllipsis
            };
            tableRows.Add(tableRow6);
            WMSTableRow tableRow7 = new WMSTableRow()
            {
                Index = 7,
                Code = "CreateTime",
                Description = "创建日期",
                Width = 300,
                Type = ContentTypeEnum.DATE,
                Align = HorizontalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis
            };
            tableRows.Add(tableRow7);

            tableRows.ForEach(row =>
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn()
                {
                    Header = row.Description,
                    Width = row.Width,
                    IsReadOnly = true,
                    CanUserSort = true
                };
                Binding dataBinding = new Binding(row.Code);
                dataBinding.Converter = new SystemValueConvert();
                dataBinding.ConverterParameter = row.Type;

                DataTemplate dataTemplate = new DataTemplate();

                FrameworkElementFactory dataFactory;
                switch (row.Type)
                {
                    case ContentTypeEnum.FILE:
                        dataFactory = new FrameworkElementFactory(typeof(Image));
                        dataFactory.SetBinding(Image.SourceProperty, dataBinding);
                        break;
                    default: 
                        dataFactory = new FrameworkElementFactory(typeof(TextBlock));
                        dataFactory.SetBinding(TextBlock.TextProperty, dataBinding);
                        ToolTip toolTip = new ToolTip();
                        toolTip.SetBinding(ContentProperty, dataBinding);
                        dataFactory.SetValue(ToolTipProperty, toolTip);
                        dataFactory.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
                        dataFactory.AddHandler(MouseEnterEvent, new MouseEventHandler((sender, e) =>
                        {
                            toolTip.Content = ((TextBlock)sender).Text;
                            toolTip.IsOpen = true;
                        }));
                        dataFactory.AddHandler(MouseLeaveEvent, new MouseEventHandler((sender, e) =>
                        {
                            toolTip.IsOpen = false;
                        }));
                        dataFactory.AddHandler(MouseRightButtonUpEvent, new MouseButtonEventHandler((sender, e) =>
                        {
                            TextBlock textBlock = (TextBlock)sender;
                            if (textBlock.ContextMenu == null)
                            {
                                ContextMenu contextMenu = new ContextMenu();
                                MenuItem detailMenuItem = new MenuItem();
                                detailMenuItem.Header = "查看";
                                detailMenuItem.Click += (menuItem, cilck) =>
                                {
                                    // TODO 获取id
                                    DetailUser(1);
                                };
                                contextMenu.Items.Add(detailMenuItem);
                                MenuItem copyMenuItem = new MenuItem();
                                copyMenuItem.Header = "复制";
                                copyMenuItem.Click += (menuItem, cilck) =>
                                {
                                    Clipboard.SetText(textBlock.Text);
                                };
                                contextMenu.Items.Add(copyMenuItem);
                                MenuItem modifyMenuItem = new MenuItem();
                                modifyMenuItem.Header = "修改";
                                modifyMenuItem.Click += (menuItem, cilck) =>
                                {
                                    // TODO 获取id
                                    ModifyUser(1);
                                };
                                contextMenu.Items.Add(modifyMenuItem);
                                MenuItem deleteMenuItem = new MenuItem();
                                deleteMenuItem.Header = "删除";
                                deleteMenuItem.Click += (menuItem, cilck) =>
                                {
                                    // TODO 获取id
                                    DeleteUser(1);
                                };
                                contextMenu.Items.Add(deleteMenuItem);
                                textBlock.ContextMenu = contextMenu;
                            }
                        }));
                        break;

                }
                dataFactory.SetValue(HorizontalAlignmentProperty, row.Align);
                dataTemplate.VisualTree = dataFactory;
                column.CellTemplate = dataTemplate;
                DataGrid_User.Columns.Add(column);
                
            });
            QueryUserList(1);
        }

        private void QueryUserList(int pageNum)
        {
            int pageSize = string.IsNullOrWhiteSpace(ComboBox_PageSize.Text) ? 10 : int.Parse(ComboBox_PageSize.Text);
            // TODO 获取用户列表
            List<UserDetail> userDetails = new List<UserDetail>();
            for (int i = 0; i < 11; i++)
            {
                userDetails.Add(new UserDetail(i, Resource.open_graph_logo, "名称" + i, i % 2 == 1, (i % 2).ToString(), "备注ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" + i, DateTime.Now.AddDays(i)));
            }
            DataGrid_User.ItemsSource = userDetails;
        }

        private void DetailUser(int id)
        {
            // TODO 查看详情界面
            UserSettingPage userSettingPage = new UserSettingPage();
            userSettingPage.ReadOnly = true;
            userSettingPage.Button_Close.Click += (sender, e) => {
                Drawer_Detail.IsOpen = false;
            };
            Frame_Detail.Content = userSettingPage;
            Drawer_Detail.IsOpen = true;
        }

        private void AddUser()
        {
            // TODO 新增详情界面
            UserSettingPage userSettingPage = new UserSettingPage();
            userSettingPage.ReadOnly = false;
            userSettingPage.Button_Save.Click += (sender, e) => {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认保存？", "保存");
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    // TODO 保存
                    userSettingPage.ReadOnly = true;
                    QueryUserList(1);
                    Drawer_Detail.IsOpen = false;
                }
            };
            userSettingPage.Button_Close.Click += (sender, e) => {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认取消？", "取消");
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    userSettingPage.ReadOnly = true;
                    Drawer_Detail.IsOpen = false;
                }
            };
            Frame_Detail.Content = userSettingPage;
            Drawer_Detail.IsOpen = true;
        }

        private void ModifyUser(int id)
        {
            // TODO 修改详情界面
            UserSettingPage userSettingPage = new UserSettingPage();
            userSettingPage.ReadOnly = false;
            userSettingPage.Button_Save.Click += (sender, e) => {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认保存？", "保存");
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    // TODO 保存
                    userSettingPage.ReadOnly = true;
                    QueryUserList(1);
                    Drawer_Detail.IsOpen = false;
                }
            };
            userSettingPage.Button_Close.Click += (sender, e) => {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认取消？", "取消");
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    userSettingPage.ReadOnly = true;
                    Drawer_Detail.IsOpen = false;
                }
            };
            Frame_Detail.Content = userSettingPage;
            Drawer_Detail.IsOpen = true;
        }

        private void DeleteUser(int id)
        {
            MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask("确认删除？", "删除");
            if (messageBoxResult == MessageBoxResult.OK)
            {
                // TODO 删除
                QueryUserList(1);
            }
        }
    }
}
