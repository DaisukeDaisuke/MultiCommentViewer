using System;
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
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using Common.Wpf;
namespace MultiCommentViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _applyingColumnOrder;

        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<SetAddingCommentDirection>(this, message =>
            {
                _addingCommentToTop = message.IsTop;
            });
            Messenger.Default.Register<SetPostCommentPanel>(this, message =>
            {
                PostCommentPanelPlaceHolder.Children.Clear();

                var newPanel = message.Panel;
                if (newPanel == null)
                {
                    PostCommentPanelPlaceHolder.IsEnabled = false;
                }
                else
                {
                    PostCommentPanelPlaceHolder.IsEnabled = true;
                    newPanel.Margin = new Thickness(0);
                    newPanel.VerticalAlignment = VerticalAlignment.Stretch;
                    newPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                    newPanel.Width = double.NaN;
                    newPanel.Height = double.NaN;
                    PostCommentPanelPlaceHolder.Children.Add(newPanel);
                }
            });
            Messenger.Default.Register<ShowOptionsViewMessage>(this, message =>
            {
                try
                {
                    var optionsView = new OptionsView();
                    foreach (var tab in message.Tabs)
                    {
                        optionsView.AddTabPage(tab);
                    }
                    optionsView.Owner = this;
                    optionsView.ShowDialog();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
            });
            Messenger.Default.Register<ShowUserViewMessage>(this, message =>
            {
                try
                {
                    var uvm = message.Uvm;
                    var userView = new UserView
                    {
                        DataContext = uvm
                    };
                    userView.Show();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            Messenger.Default.Register<ShowUserListViewMessage>(this, message =>
            {
                try
                {
                    var uvms = message.UserViewModels;
                    var mainVm = message.MainVm;
                    var options = message.Options;
                    var vm = new ViewModels.UserListViewModel(uvms, mainVm, options);
                    var userView = new View.UserListView
                    {
                        DataContext = vm,
                    };
                    userView.Show();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }

        private void ConnectionsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is MainViewModel vm))
                return;

            ApplyColumnOrder(ConnectionsDataGrid, new[]
            {
                vm.ConnectionsViewSelectionDisplayIndex,
                vm.ConnectionsViewSiteDisplayIndex,
                vm.ConnectionsViewConnectionNameDisplayIndex,
                vm.ConnectionsViewInputDisplayIndex,
                vm.ConnectionsViewBrowserDisplayIndex,
                vm.ConnectionsViewConnectionDisplayIndex,
                vm.ConnectionsViewDisconnectionDisplayIndex,
                vm.ConnectionsViewSaveDisplayIndex,
                vm.ConnectionsViewLoggedinUsernameDisplayIndex,
                vm.ConnectionsViewConnectionBackgroundDisplayIndex,
                vm.ConnectionsViewConnectionForegroundDisplayIndex,
            });
        }

        private void ConnectionsDataGrid_ColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            if (_applyingColumnOrder || !(DataContext is MainViewModel vm))
                return;

            vm.ConnectionsViewSelectionDisplayIndex = ConnectionsDataGrid.Columns[0].DisplayIndex;
            vm.ConnectionsViewSiteDisplayIndex = ConnectionsDataGrid.Columns[1].DisplayIndex;
            vm.ConnectionsViewConnectionNameDisplayIndex = ConnectionsDataGrid.Columns[2].DisplayIndex;
            vm.ConnectionsViewInputDisplayIndex = ConnectionsDataGrid.Columns[3].DisplayIndex;
            vm.ConnectionsViewBrowserDisplayIndex = ConnectionsDataGrid.Columns[4].DisplayIndex;
            vm.ConnectionsViewConnectionDisplayIndex = ConnectionsDataGrid.Columns[5].DisplayIndex;
            vm.ConnectionsViewDisconnectionDisplayIndex = ConnectionsDataGrid.Columns[6].DisplayIndex;
            vm.ConnectionsViewSaveDisplayIndex = ConnectionsDataGrid.Columns[7].DisplayIndex;
            vm.ConnectionsViewLoggedinUsernameDisplayIndex = ConnectionsDataGrid.Columns[8].DisplayIndex;
            vm.ConnectionsViewConnectionBackgroundDisplayIndex = ConnectionsDataGrid.Columns[9].DisplayIndex;
            vm.ConnectionsViewConnectionForegroundDisplayIndex = ConnectionsDataGrid.Columns[10].DisplayIndex;
        }

        private void MetadataDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is MainViewModel vm))
                return;

            ApplyColumnOrder(MetadataDataGrid, new[]
            {
                vm.MetadataViewConnectionNameDisplayIndex,
                vm.MetadataViewTitleDisplayIndex,
                vm.MetadataViewElapsedDisplayIndex,
                vm.MetadataViewCurrentViewersDisplayIndex,
                vm.MetadataViewTotalViewersDisplayIndex,
                vm.MetadataViewActiveDisplayIndex,
                vm.MetadataViewOthersDisplayIndex,
            });
        }

        private void MetadataDataGrid_ColumnReordered(object sender, DataGridColumnEventArgs e)
        {
            if (_applyingColumnOrder || !(DataContext is MainViewModel vm))
                return;

            vm.MetadataViewConnectionNameDisplayIndex = MetadataDataGrid.Columns[0].DisplayIndex;
            vm.MetadataViewTitleDisplayIndex = MetadataDataGrid.Columns[1].DisplayIndex;
            vm.MetadataViewElapsedDisplayIndex = MetadataDataGrid.Columns[2].DisplayIndex;
            vm.MetadataViewCurrentViewersDisplayIndex = MetadataDataGrid.Columns[3].DisplayIndex;
            vm.MetadataViewTotalViewersDisplayIndex = MetadataDataGrid.Columns[4].DisplayIndex;
            vm.MetadataViewActiveDisplayIndex = MetadataDataGrid.Columns[5].DisplayIndex;
            vm.MetadataViewOthersDisplayIndex = MetadataDataGrid.Columns[6].DisplayIndex;
        }

        private void ApplyColumnOrder(DataGrid dataGrid, int[] displayIndexes)
        {
            if (displayIndexes.Length != dataGrid.Columns.Count)
                return;

            var orderedColumns = displayIndexes
                .Select((displayIndex, columnIndex) => new { displayIndex, columnIndex })
                .OrderBy(x => x.displayIndex)
                .ThenBy(x => x.columnIndex)
                .ToArray();

            _applyingColumnOrder = true;
            try
            {
                for (var displayIndex = 0; displayIndex < orderedColumns.Length; displayIndex++)
                {
                    dataGrid.Columns[orderedColumns[displayIndex].columnIndex].DisplayIndex = displayIndex;
                }
            }
            finally
            {
                _applyingColumnOrder = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/63fa1e10-1050-4448-a2bc-62dfe0836f25/selecting-datagrid-row-when-right-mouse-button-is-pressed?forum=wpf
        private void DataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            //depがRunだと、VisualTreeHelper.GetParent()で下記の例外が投げられてしまう。
            //'System.Windows.Documents.Run' is not a Visual or Visual3D' InvalidOperationException
            if (e.OriginalSource is Run run)
            {
                dep = run.Parent;
            }
            while ((dep != null) && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null) return;

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                cell.Focus();

                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                DataGridRow row = dep as DataGridRow;
                //dataGrid.SelectedItem = row.DataContext;
            }
        }

        private bool _addingCommentToTop;
        private bool _bottom = true;
        //private bool neverTouch = true;
        private void DataGridScrollChanged(object sender, RoutedEventArgs e)
        {
            if (_addingCommentToTop)
                return;
            if (sender == null)
                return;
            ScrollViewer scrollViewer;
            if (sender is DataGrid dataGrid)
            {
                scrollViewer = dataGrid.GetScrollViewer();
            }
            else if (sender is ScrollViewer)
            {
                scrollViewer = sender as ScrollViewer;
            }
            else
            {
                return;
            }
            var a = e as ScrollChangedEventArgs;


            //2017/09/11
            //ExtentHeightは表示されていない部分も含めた全てのコンテントの高さ。
            //ScrollChangedが呼び出されたのにExtentHeightChangeが0ということはアイテムが追加されていないのにも関わらずスクロールがあった。
            //それはユーザが手動でスクロールした場合のみ起こること。
            if (a.ExtentHeightChange == 0)
            {
                //ユーザが手動でスクロールした
                _bottom = scrollViewer.IsBottom();
                //neverTouch = false;
            }

            //2017/09/11全体の高さが表示部に収まる間はスクロールがBottomにあるとみなすと、表示部に収まらなくなった瞬間にもBottomにあると判定されて、最初のスクロールが上手くいくかも。

            //if (bottom && a.ExtentHeightChange != 0)
            if (_bottom && Test(a))
            {
                scrollViewer.ScrollToBottom();
            }
        }
        private bool Test(ScrollChangedEventArgs e)
        {
            return e.ViewportHeightChange > 0 || e.ExtentHeightChange > 0 || e.ViewportHeightChange < 0 || e.ExtentHeightChange < 0;
        }
    }
    public static class DataGridBehavior
    {
        public static ScrollViewer GetScrollViewer(this DataGrid dataGrid)
        {
            return dataGrid.Template.FindName("DG_ScrollViewer", dataGrid) as ScrollViewer;
        }
    }
    public static class ScrollViewerBehavior
    {
        public static bool IsBottom(this ScrollViewer sv)
        {
            //var b = (sv.VerticalOffset * 1.01) > sv.ScrollableHeight;
            var b = (sv.VerticalOffset >= sv.ScrollableHeight
                || sv.ExtentHeight < sv.ViewportHeight);
            return b;
        }
    }
}
