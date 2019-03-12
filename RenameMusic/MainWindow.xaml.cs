using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RenameMusic
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string m_Dir;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            m_Dir = m_Dialog.SelectedPath.Trim();
            this.filepath.Text = m_Dir;
        }

        //点击重命名按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = 0;
                DirectoryInfo directoryInfo = new DirectoryInfo(m_Dir);
                foreach (var item in directoryInfo.GetFiles())
                {
                    var oldName = item.Name;
                    string format = "";
                    string oldNameWithoutFormat = oldName;
                    if (oldName.Contains("."))
                    {
                        int pointIndex = oldName.LastIndexOf(".");
                        format = oldName.Substring(pointIndex);
                        oldNameWithoutFormat = oldName.Substring(0, pointIndex);
                    }
                    StringBuilder newName = new StringBuilder();
                    newName.Append(Head.Text);
                    if (isOldName.IsChecked == true)
                    {
                        newName.Append(oldNameWithoutFormat);
                    }
                    if (isIndexNum.IsChecked == true)
                    {
                        newName.Append("(" + count + ")");
                    }
                    if (string.IsNullOrWhiteSpace(End.Text))
                    {
                        newName.Append(format);
                    }
                    else
                    {
                        newName.Append(End.Text);
                    }
                    string newPath = System.IO.Path.Combine(m_Dir, newName.ToString());
                    item.MoveTo(newPath);
                    count++;
                }
                System.Windows.MessageBox.Show("转换成功");
            }
            catch (ArgumentNullException)
            {
                System.Windows.MessageBox.Show("路径无效");
            }
        }

        private void IsOldName_OnClick(object sender, RoutedEventArgs e)
        {
            if (isOldName.IsChecked == false && isIndexNum.IsChecked == false)
            {
                isIndexNum.IsChecked = true;
            }
        }

        private void IsIndexNum_OnClick(object sender, RoutedEventArgs e)
        {
            if (isOldName.IsChecked == false && isIndexNum.IsChecked == false)
            {
                isOldName.IsChecked = true;
            }
        }
    }
}
