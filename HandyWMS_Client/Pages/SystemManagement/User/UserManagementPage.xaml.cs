using HandyControl.Controls;
using HandyWMS_Client.Constructs.Converts;
using HandyWMS_Client.Constructs.SystemEnums;
using HandyWMS_Client.Objects.Base;
using HandyWMS_Client.Objects.SystemManagement.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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

        }

        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Query_Click(object sender, RoutedEventArgs e)
        {
            
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
            // TODO
            // MessageBox.Show("ColumnReordered");
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
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory textFactory = new FrameworkElementFactory(typeof(TextBlock));

                Binding dataBinding = new Binding(row.Code);
                dataBinding.Converter = new SystemValueConvert();
                dataBinding.ConverterParameter = row.Type;

                textFactory.SetBinding(TextBlock.TextProperty, dataBinding);
                textFactory.SetValue(HorizontalAlignmentProperty, row.Align);
                textFactory.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
                ToolTip toolTip = new ToolTip();
                toolTip.SetBinding(ContentProperty, dataBinding);
                textFactory.SetValue(ToolTipProperty, toolTip);

                textFactory.AddHandler(MouseEnterEvent, new MouseEventHandler((sender, e) =>
                {
                    // 鼠标进入时显示 ToolTip
                    toolTip.Content = ((TextBlock)sender).Text;
                    toolTip.IsOpen = true;
                }));

                textFactory.AddHandler(MouseLeaveEvent, new MouseEventHandler((sender, e) =>
                {
                    // 鼠标离开时关闭 ToolTip
                    toolTip.IsOpen = false;
                }));
                
                textFactory.AddHandler(MouseRightButtonUpEvent, new MouseButtonEventHandler((sender, e) =>
                {
                    TextBlock textBlock = (TextBlock)sender;
                    if (textBlock.ContextMenu == null)
                    {
                        ContextMenu contextMenu = new ContextMenu();
                        MenuItem detailMenuItem = new MenuItem();
                        detailMenuItem.Header = "查看";
                        detailMenuItem.Click += (menuItem, cilck) =>
                        {

                        };
                        MenuItem copyMenuItem = new MenuItem();
                        copyMenuItem.Header = "复制";
                        copyMenuItem.Click += (menuItem, cilck) =>
                        {
                            Clipboard.SetText(textBlock.Text); 
                        };
                        contextMenu.Items.Add(detailMenuItem);
                        contextMenu.Items.Add(copyMenuItem);
                        textBlock.ContextMenu = contextMenu;
                    }
                }));

                dataTemplate.VisualTree = textFactory;

                column.CellTemplate = dataTemplate;
                DataGrid_User.Columns.Add(column);
                
            });
            List<UserDetail> userDetails = new List<UserDetail>();
            for (int i = 0; i < 11; i++)
            {
                userDetails.Add(new UserDetail(i, Resource.open_graph_logo, "名称" + i, i % 2 == 1, (i % 2).ToString(), "备注ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" + i, DateTime.Now.AddDays(i)));
            }
            DataGrid_User.ItemsSource = userDetails;
        }

        private void Column_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void Column_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
