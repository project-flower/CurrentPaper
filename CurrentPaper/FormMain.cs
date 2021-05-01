using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CurrentPaper
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void GetTranscodedImageCache()
        {
            listBoxPaths.Items.Clear();
            string[] currentWallpapers;

            try
            {
                currentWallpapers = MainEngine.GetCurrentWallpapers();
            }
            catch (Exception exception)
            {
                ShowErrorMessage(exception.Message);
                return;
            }

            listBoxPaths.Items.AddRange(currentWallpapers);
        }

        private void OpenItem(bool explore = false)
        {
            object selectedItem = listBoxPaths.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }

            try
            {
                if (explore)
                {
                    Process.Start("explorer.exe", string.Format("/select,\"{0}\"", selectedItem));
                }
                else
                {
                    Process.Start(selectedItem.ToString());
                }
            }
            catch (Exception exception)
            {
                ShowErrorMessage(exception.Message);
            }
        }

        private void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private DialogResult ShowMessage(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(this, text, Text, buttons, icon);
        }

        private void buttonExplore_Click(object sender, EventArgs e)
        {
            OpenItem(true);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenItem();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GetTranscodedImageCache();
        }

        private void listBoxPaths_DoubleClick(object sender, EventArgs e)
        {
            OpenItem();
        }

        private void load(object sender, EventArgs e)
        {
            GetTranscodedImageCache();
        }
    }
}
