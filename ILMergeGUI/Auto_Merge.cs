using System;
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
using System.Diagnostics;

namespace ILMergeGUI
{
    public partial class Auto_Merge : Form
    {
        private ILMerge merge;
        public Auto_Merge()
        {
            InitializeComponent();
            merge = new ILMerge();
        }

        private void Auto_Merge_Load(object sender, EventArgs e)
        {
            DirectoryInfo Dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\CommonLib");
            if (!Dir.Exists)
                Dir.Create();
            Dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\SFL");
            if (!Dir.Exists)
                Dir.Create();
            Dir = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\CHART");
            if (!Dir.Exists)
                Dir.Create();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.FileName = "explorer.exe";

            processStartInfo.Arguments = System.Windows.Forms.Application.StartupPath + @"\log";
            System.Diagnostics.Process.Start(processStartInfo);
        }

        private void btnMerge_commonlib_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = null;
                string targetDir = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf(@"\bin\Debug")); 
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "CopyCommonlib.bat";
                proc.StartInfo.Arguments = string.Format("10");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();

                string outPutfile = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\CNABS.Lib.dll");
                FileInfo f = new FileInfo(outPutfile);
                if (f.Exists)
                    f.Delete();

                DirectoryInfo Dir = new DirectoryInfo(Path.Combine(System.Windows.Forms.Application.StartupPath + @"\CommonLib"));
                var files = Dir.GetFiles();
                merge.Log = false;
                List<string> lstInputFiles = new List<string>();

                foreach (var item in files)
                {
                    if (item.Extension.Equals(".dll"))
                    {
                        if (item.FullName.Contains("lib") || item.FullName.Contains("QLNet"))
                        {
                            lstInputFiles.Add(item.FullName);
                        }
                    }
                }

                merge.SetInputAssemblies(lstInputFiles.ToArray());
                merge.OutputFile = outPutfile;
                string targetPlatform = "v4";
                merge.SetTargetPlatform(targetPlatform, string.Empty);
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
                        Loghelper.BugLog("btnMerge_commonlib_Click", ex.Message, ex.StackTrace);
                    }

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show((errors > 0 ? (" 过程中有" + errors + "处异常，请查看日志。") : "合并完成！"), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                });
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                Loghelper.BugLog("btnMerge_commonlib_Click", ex.Message, ex.StackTrace);
            }
        }

        private void btnMergeSFL_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = null;
                string targetDir = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf(@"\bin\Debug")); 
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "CopySFL.bat";
                proc.StartInfo.Arguments = string.Format("10");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();

                string outPutfile = Path.Combine(System.Windows.Forms.Application.StartupPath + @"\CNABS.SFL.dll");
                FileInfo f = new FileInfo(outPutfile);
                if (f.Exists)
                    f.Delete();

                DirectoryInfo Dir = new DirectoryInfo(Path.Combine(System.Windows.Forms.Application.StartupPath + @"\SFL"));
                var files = Dir.GetFiles();
                merge.Log = false;
                List<string> lstInputFiles = new List<string>();

                foreach (var item in files)
                {
                    if (item.Extension.Equals(".dll"))
                    {
                        if (item.FullName.Contains("CNABS"))
                        {
                            lstInputFiles.Add(item.FullName);
                        }
                    }
                }

                merge.SetInputAssemblies(lstInputFiles.ToArray());
                merge.OutputFile = outPutfile;
                string targetPlatform = "v4";
                merge.SetTargetPlatform(targetPlatform, string.Empty);
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
                        Loghelper.BugLog("btnMergeSFL_Click", ex.Message, ex.StackTrace);
                    }

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Visible = false;
                        MessageBox.Show((errors > 0 ? (" 过程中有" + errors + "处异常，请查看日志。") : "合并完成！"), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                });
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                Loghelper.BugLog("btnMergeSFL_Click", ex.Message, ex.StackTrace);
            }
        }
    }
}
