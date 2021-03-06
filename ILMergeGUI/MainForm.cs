﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ILMerging;
using System.IO;
using System.Threading;
namespace ILMergeGUI
{
    public partial class MainForm : Form
    {
        private ILMerge merge;
        public MainForm()
        {
            InitializeComponent();
            merge = new ILMerge();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void btnView_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "(*.exe,*.dll)|*.exe;*.dll";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtMainAsm.Text = openDlg.FileName;
            }
        }

        /// <summary>
        /// modify by qjl --2018-03-08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "(*.exe,*.dll)|*.exe;*.dll";
            openDlg.Multiselect = true;
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                //add Multiselect
                for (int fi = 0; fi < openDlg.FileNames.Length; fi++)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.FullFileName = openDlg.FileNames[fi];
                    item.DisplayName = System.IO.Path.GetFileName(openDlg.FileNames[fi]);
                    this.listBox.Items.Add(item);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.listBox.SelectedItem != null)
            {
                this.listBox.Items.Remove(this.listBox.SelectedItem);
            }
        }

        private void btnViewOutPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择和并后的导出路径";
            dialog.ShowNewFolderButton = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutPath.Text = dialog.SelectedPath;
            }

        }

        private bool IsValidateInput()
        {
            if (string.IsNullOrEmpty(txtMainAsm.Text))
            {
                MessageBox.Show("主文件必填！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (listBox.Items.Count == 0)
            {
                MessageBox.Show("请选择引用程序集文件！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtOutPath.Text))
            {
                MessageBox.Show("输出文件路径必填！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtOutFileName.Text))
            {
                MessageBox.Show("输出文件必填！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (!IsValidateInput())
            {
                return;
            }

            merge.Log = this.chkLog.Checked;
            merge.LogFile = Path.Combine(Application.StartupPath, "log.txt");
            List<string> lstInputFiles = new List<string>();
            string primaryInputFile = this.txtMainAsm.Text;
            lstInputFiles.Add(primaryInputFile);//The first element of the array is considered to be the primary assembly.

            foreach (ListBoxItem item in this.listBox.Items)
            {
                lstInputFiles.Add(item.FullFileName);
            }

            merge.SetInputAssemblies(lstInputFiles.ToArray());

            string outPutfile = Path.Combine(txtOutPath.Text, txtOutFileName.Text);
            FileInfo fiPrimeray = new FileInfo(primaryInputFile);
            string ext = fiPrimeray.Extension;
            FileInfo fi = new FileInfo(outPutfile);
            if (string.IsNullOrEmpty(fi.Extension))
            {
                outPutfile += ext;
                fi = new FileInfo(outPutfile);
            }

            merge.OutputFile = outPutfile;

            string targetPlatform = "v4";
            foreach (Control ctrl in groupBox2.Controls)
            {
                RadioButton rdo = ctrl as RadioButton;
                if (rdo != null)
                {
                    targetPlatform = rdo.Text;
                    break;
                }
            }
            merge.SetTargetPlatform(targetPlatform, string.Empty);////
            merge.TargetKind = ILMerge.Kind.SameAsPrimaryAssembly;

            var errors = 0;
            Thread t = new Thread(() =>
                {
                    this.BeginInvoke(new Action(() =>
                        {
                            progressBar1.Visible = true;

                        }));
                    try
                    {
                        merge.Merge();
                    }
                    catch (Exception ex)
                    {
                        errors++;
                        Loghelper.BugLog(this.GetType().ToString(), ex.Message, ex.StackTrace);
                    }

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show("合并完成！" + (errors > 0 ? (" 过程中有" + errors + "处异常，请查看日志。") : ""), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                });
            t.IsBackground = true;
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < this.listBox.Items.Count; i++)
            {
                this.listBox.Items.Remove(this.listBox.Items[i]);
                i--;
            }
        }
    }
}
