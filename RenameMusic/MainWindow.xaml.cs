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
        private static int count = 0;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(m_Dir);
                foreach (var item in directoryInfo.GetFiles())
                {
                    var oldName = item.Name;
                    String newName = Head.Text + "(" + count + ")" + oldName+End.Text;
                    String newPath = System.IO.Path.Combine(m_Dir, newName);
                    item.MoveTo(newPath);
                    count++;
                }
                System.Windows.MessageBox.Show("转换成功");
            }
            catch (System.ArgumentNullException)
            {
                System.Windows.MessageBox.Show("路径无效");
            }
        }
    }
}
