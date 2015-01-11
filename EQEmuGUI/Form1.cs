using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EQEmuGUI
{
    public partial class Form1 : Form
    {
        public ProcessManager proc_LoginServer;
        public ProcessManager proc_shared_memory;
        public ProcessManager proc_WorldServer;
        public ProcessManager proc_queryserv;
        public ProcessManager proc_UCSServer;
        public ProcessManager proc_ZoneServer1;
        public ProcessManager proc_ZoneServer2;
        public ProcessManager proc_ZoneServer3;
        public ProcessManager proc_ZoneServer4;
        public ProcessManager proc_ZoneServer5;
        public ProcessManager proc_ZoneServer6;
        public ProcessManager proc_ZoneServer7;
        public ProcessManager proc_ZoneServer8;
        public ProcessManager proc_CMD;
        public delegate void AppendText(string Text);

        public Form1()
        {
            InitializeComponent();
            tabsRemove();
            if (!File.Exists("loginserver.exe") | !File.Exists("shared_memory.exe") | !File.Exists("world.exe") | !File.Exists("queryserv.exe") | !File.Exists("ucs.exe") | !File.Exists("zone.exe"))
            {
                MessageBox.Show("Please include the files loginserver.exe, shared_memory.exe, world.exe, queryserv.exe, ucs.exe, and zone.exe and their respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            this.Closing += new CancelEventHandler(Form1_Closing);
        }

        protected void Form1_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you REALLY want to close the server?",
                 "Closing event!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                foreach (Process p in Process.GetProcessesByName("loginserver"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                foreach (Process p in Process.GetProcessesByName("shared_memory"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                foreach (Process p in Process.GetProcessesByName("world"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                foreach (Process p in Process.GetProcessesByName("queryserv"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                foreach (Process p in Process.GetProcessesByName("ucs"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                foreach (Process p in Process.GetProcessesByName("zone"))
                {
                    p.Kill();
                    p.WaitForExit();
                }
                System.Environment.Exit(0);
                e.Cancel = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtInputCMD.KeyPress += new KeyPressEventHandler(txtInputCMD_KeyPress);
            tabControl1.Selected += new TabControlEventHandler(tabControl1_Selected);
            UpdateTabVisibility();
            this.stopRadio.Checked.Equals(true);
            radioStatus();
        }

        private void Stop()
        {
            if (proc_LoginServer != null)
            {                
                loginText.AppendText("Shutting down LoginServer. CPU time used: " + proc_LoginServer.TotalProcessorTime.ToString() + "\r\n");
                proc_LoginServer.StopProcess();
                proc_LoginServer = null;
            }
            if (proc_shared_memory != null)
            {
                shared_memoryText.AppendText("Shutting down shared_memory. CPU time used: " + proc_shared_memory.TotalProcessorTime.ToString() + "\r\n");
                proc_shared_memory.StopProcess();
                proc_shared_memory = null;
            }
            if (proc_WorldServer != null)
            {
                worldText.AppendText("Shutting down WorldServer. CPU time used: " + proc_WorldServer.TotalProcessorTime.ToString() + "\r\n");
                proc_WorldServer.StopProcess();
                proc_WorldServer = null;
            }
            if (proc_queryserv != null)
            {
                queryservText.AppendText("Shutting down queryserv. CPU time used: " + proc_queryserv.TotalProcessorTime.ToString() + "\r\n");
                proc_queryserv.StopProcess();
                proc_queryserv = null;
            }
            if (proc_UCSServer != null)
            {
                worldText.AppendText("Shutting down UCSServer. CPU time used: " + proc_UCSServer.TotalProcessorTime.ToString() + "\r\n");
                proc_UCSServer.StopProcess();
                proc_UCSServer = null;
            }
            if (proc_ZoneServer1 != null)
            {
                zone1Text.AppendText("Shutting down ZoneServer 1. CPU time used: " + proc_ZoneServer1.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer1.StopProcess();
                proc_ZoneServer1 = null;
            }
            if (proc_ZoneServer2 != null)
            {
                zone2Text.AppendText("Shutting down ZoneServer 2. CPU time used: " + proc_ZoneServer2.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer2.StopProcess();
                proc_ZoneServer2 = null;
            }
            if (proc_ZoneServer3 != null)
            {
                zone3Text.AppendText("Shutting down ZoneServer 3. CPU time used: " + proc_ZoneServer3.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer3.StopProcess();
                proc_ZoneServer3 = null;
            }
            if (proc_ZoneServer4 != null)
            {
                zone4Text.AppendText("Shutting down ZoneServer 4. CPU time used: " + proc_ZoneServer4.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer4.StopProcess();
                proc_ZoneServer4 = null;
            }
            if (proc_ZoneServer5 != null)
            {
                zone5Text.AppendText("Shutting down ZoneServer 5. CPU time used: " + proc_ZoneServer5.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer5.StopProcess();
                proc_ZoneServer5 = null;
            }
            if (proc_ZoneServer6 != null)
            {
                zone6Text.AppendText("Shutting down ZoneServer 6. CPU time used: " + proc_ZoneServer6.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer6.StopProcess();
                proc_ZoneServer6 = null;
            }
            if (proc_ZoneServer7 != null)
            {
                zone7Text.AppendText("Shutting down ZoneServer 7. CPU time used: " + proc_ZoneServer7.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer7.StopProcess();
                proc_ZoneServer7 = null;
            }
            if (proc_ZoneServer8 != null)
            {
                zone8Text.AppendText("Shutting down ZoneServer 8. CPU time used: " + proc_ZoneServer8.TotalProcessorTime.ToString() + "\r\n");
                proc_ZoneServer8.StopProcess();
                proc_ZoneServer8 = null;
            }
            if (proc_CMD != null)
            {
                txtCMD.AppendText("Closing CMD Shell. CPU time used: " + proc_CMD.TotalProcessorTime.ToString() + "\r\n");
                proc_CMD.StopProcess();
                proc_CMD = null;
                UpdateTabVisibility();
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void UpdateTabVisibility()
        {
            if (proc_LoginServer == null)
            {
                if (tabControl1.TabPages.Contains(loginTab1))
                    tabControl1.TabPages.Remove(loginTab1);
            }
            else if (proc_LoginServer != null)
            {
                if (!tabControl1.TabPages.Contains(loginTab1))
                    tabControl1.TabPages.Add(loginTab1);
            }
            if (proc_shared_memory == null)
            {
                if (tabControl1.TabPages.Contains(shared_memoryTab))
                    tabControl1.TabPages.Remove(shared_memoryTab);
            }
            else if (proc_shared_memory != null)
            {
                if (!tabControl1.TabPages.Contains(shared_memoryTab))
                    tabControl1.TabPages.Add(shared_memoryTab);
            }
            if (proc_WorldServer == null)
            {
                if (tabControl1.TabPages.Contains(worldTab))
                    tabControl1.TabPages.Remove(worldTab);
            }
            else if (proc_WorldServer != null)
            {
                if (!tabControl1.TabPages.Contains(worldTab))
                    tabControl1.TabPages.Add(worldTab);
            }
            if (proc_queryserv == null)
            {
                if (tabControl1.TabPages.Contains(queryservTab))
                    tabControl1.TabPages.Remove(queryservTab);
            }
            else if (proc_queryserv != null)
            {
                if (!tabControl1.TabPages.Contains(queryservTab))
                    tabControl1.TabPages.Add(queryservTab);
            }
            if (proc_UCSServer == null)
            {
                if (tabControl1.TabPages.Contains(ucsTab))
                    tabControl1.TabPages.Remove(ucsTab);
            }
            else if (proc_UCSServer != null)
            {
                if (!tabControl1.TabPages.Contains(ucsTab))
                    tabControl1.TabPages.Add(ucsTab);
            }
            if (proc_ZoneServer1 == null)
            {
                if (tabControl1.TabPages.Contains(zone1Tab))
                    tabControl1.TabPages.Remove(zone1Tab);
            }
            else if (proc_ZoneServer1 != null)
            {
                if (!tabControl1.TabPages.Contains(zone1Tab))
                    tabControl1.TabPages.Add(zone1Tab);
            }
            if (proc_ZoneServer2 == null)
            {
                if (tabControl1.TabPages.Contains(zone2Tab))
                    tabControl1.TabPages.Remove(zone2Tab);
            }
            else if (proc_ZoneServer2 != null)
            {
                if (!tabControl1.TabPages.Contains(zone2Tab))
                    tabControl1.TabPages.Add(zone2Tab);
            }
            if (proc_ZoneServer3 == null)
            {
                if (tabControl1.TabPages.Contains(zone3Tab))
                    tabControl1.TabPages.Remove(zone3Tab);
            }
            else if (proc_ZoneServer3 != null)
            {
                if (!tabControl1.TabPages.Contains(zone3Tab))
                    tabControl1.TabPages.Add(zone3Tab);
            }
            if (proc_ZoneServer4 == null)
            {
                if (tabControl1.TabPages.Contains(zone4Tab))
                    tabControl1.TabPages.Remove(zone4Tab);
            }
            else if (proc_ZoneServer4 != null)
            {
                if (!tabControl1.TabPages.Contains(zone4Tab))
                    tabControl1.TabPages.Add(zone4Tab);
            }
            if (proc_ZoneServer5 == null)
            {
                if (tabControl1.TabPages.Contains(zone5Tab))
                    tabControl1.TabPages.Remove(zone5Tab);
            }
            else if (proc_ZoneServer5 != null)
            {
                if (!tabControl1.TabPages.Contains(zone5Tab))
                    tabControl1.TabPages.Add(zone5Tab);
            }
            if (proc_ZoneServer6 == null)
            {
                if (tabControl1.TabPages.Contains(zone6Tab))
                    tabControl1.TabPages.Remove(zone6Tab);
            }
            else if (proc_ZoneServer6 != null)
            {
                if (!tabControl1.TabPages.Contains(zone6Tab))
                    tabControl1.TabPages.Add(zone6Tab);
            }
            if (proc_ZoneServer7 == null)
            {
                if (tabControl1.TabPages.Contains(zone7Tab))
                    tabControl1.TabPages.Remove(zone7Tab);
            }
            else if (proc_ZoneServer7 != null)
            {
                if (!tabControl1.TabPages.Contains(zone7Tab))
                    tabControl1.TabPages.Add(zone7Tab);
            }
            if (proc_ZoneServer8 == null)
            {
                if (tabControl1.TabPages.Contains(zone8Tab))
                    tabControl1.TabPages.Remove(zone8Tab);
            }
            else if (proc_ZoneServer8 != null)
            {
                if (!tabControl1.TabPages.Contains(zone8Tab))
                    tabControl1.TabPages.Add(zone8Tab);
            }
            if (proc_CMD == null)
            {
                if (tabControl1.TabPages.Contains(tabCMD)) 
                tabControl1.TabPages.Remove(tabCMD);
            }
            else if (proc_CMD != null)
            {
                if (!tabControl1.TabPages.Contains(tabCMD))
                    tabControl1.TabPages.Add(tabCMD);
            }
        }

        private void tabsAdd()
        {
            if (!tabControl1.TabPages.Contains(loginTab1))
                tabControl1.TabPages.Add(loginTab1);
            if (!tabControl1.TabPages.Contains(shared_memoryTab))
                tabControl1.TabPages.Add(shared_memoryTab);
            if (!tabControl1.TabPages.Contains(worldTab))
                tabControl1.TabPages.Add(worldTab);
            if (!tabControl1.TabPages.Contains(queryservTab))
                tabControl1.TabPages.Add(queryservTab);
            if (!tabControl1.TabPages.Contains(ucsTab))
                tabControl1.TabPages.Add(ucsTab);
            if (!tabControl1.TabPages.Contains(zone1Tab))
                tabControl1.TabPages.Add(zone1Tab);
            if (!tabControl1.TabPages.Contains(zone2Tab))
                tabControl1.TabPages.Add(zone2Tab);
            if (!tabControl1.TabPages.Contains(zone3Tab))
                tabControl1.TabPages.Add(zone3Tab);
            if (!tabControl1.TabPages.Contains(zone4Tab))
                tabControl1.TabPages.Add(zone4Tab);
            if (!tabControl1.TabPages.Contains(zone5Tab))
                tabControl1.TabPages.Add(zone5Tab);
            if (!tabControl1.TabPages.Contains(zone6Tab))
                tabControl1.TabPages.Add(zone6Tab);
            if (!tabControl1.TabPages.Contains(zone7Tab))
                tabControl1.TabPages.Add(zone7Tab);
            if (!tabControl1.TabPages.Contains(zone8Tab))
                tabControl1.TabPages.Add(zone8Tab);
            if (!tabControl1.TabPages.Contains(tabCMD))
                tabControl1.TabPages.Add(tabCMD);
            radioStatus();
        }

        private void tabsRemove()
        {
            if (tabControl1.TabPages.Contains(loginTab1))
                tabControl1.TabPages.Remove(loginTab1);
            if (tabControl1.TabPages.Contains(shared_memoryTab))
                tabControl1.TabPages.Remove(shared_memoryTab);
            if (tabControl1.TabPages.Contains(worldTab))
                tabControl1.TabPages.Remove(worldTab);
            if (tabControl1.TabPages.Contains(queryservTab))
                tabControl1.TabPages.Remove(queryservTab);
            if (tabControl1.TabPages.Contains(ucsTab))
                tabControl1.TabPages.Remove(ucsTab);
            if (tabControl1.TabPages.Contains(zone1Tab))
                tabControl1.TabPages.Remove(zone1Tab);
            if (tabControl1.TabPages.Contains(zone2Tab))
                tabControl1.TabPages.Remove(zone2Tab);
            if (tabControl1.TabPages.Contains(zone3Tab))
                tabControl1.TabPages.Remove(zone3Tab);
            if (tabControl1.TabPages.Contains(zone4Tab))
                tabControl1.TabPages.Remove(zone4Tab);
            if (tabControl1.TabPages.Contains(zone5Tab))
                tabControl1.TabPages.Remove(zone5Tab);
            if (tabControl1.TabPages.Contains(zone6Tab))
                tabControl1.TabPages.Remove(zone6Tab);
            if (tabControl1.TabPages.Contains(zone7Tab))
                tabControl1.TabPages.Remove(zone7Tab);
            if (tabControl1.TabPages.Contains(zone8Tab))
                tabControl1.TabPages.Remove(zone8Tab);
            if (tabControl1.TabPages.Contains(tabCMD))
                tabControl1.TabPages.Remove(tabCMD);
            radioStatus();
        }

        private void StartLogin()//Starts Login Server
        {
            string Lrunning = "Login Server already running." + "\r\n";
            string kill = "Either wait for it to end or kill process." + "\r\n";

            if (!File.Exists("loginserver.exe"))
            {
                MessageBox.Show("Please include the file loginserver.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                this.completeButton.Enabled = false;
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_LoginServer != null)
                    {
                        loginText.AppendText(Lrunning); loginText.AppendText(kill);
                    }
                    else
                    {
                        // Start LoginServer
                        this.loginButton.Enabled = false;
                        loginText.AppendText("Starting: Login server" + "\r\n");
                        proc_LoginServer = new ProcessManager("loginserver.exe", "");
                        proc_LoginServer.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_LoginServer_DataReceived);
                        proc_LoginServer.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_LoginServer_DataReceived);
                        proc_LoginServer.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_LoginServer != null)
                    {
                        loginText.AppendText("Shutting down LoginServer. CPU time used: " + proc_LoginServer.TotalProcessorTime.ToString() + "\r\n");
                        proc_LoginServer.StopProcess();
                        proc_LoginServer = null;
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void Startshared_memory()
        {
            if (!File.Exists("shared_memory.exe"))
            {
                MessageBox.Show("Please include the file shared_memory.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                this.shared_memoryButton.Enabled = false;
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_shared_memory != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_shared_memory = new ProcessManager("shared_memory.exe", "");
                        shared_memoryText.AppendText("Starting: shared_memory server" + "\r\n");
                        proc_shared_memory.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_shared_memory_DataReceived);
                        proc_shared_memory.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_shared_memory_DataReceived);
                        proc_shared_memory.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_shared_memory != null)
                    {
                        shared_memoryText.AppendText("Shutting down shared_memory. CPU time used: " + proc_shared_memory.TotalProcessorTime.ToString() + "\r\n");
                        proc_shared_memory.StopProcess();
                        proc_shared_memory = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartWorld()//Starts World Server
        {
            string Wrunning = "World Server already running." + "\r\n";
            string kill = "Either wait for it to end or kill process." + "\r\n";

            if (!File.Exists("World.exe"))
            {
                MessageBox.Show("Please include the file World.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                this.completeButton.Enabled = false;
                this.worldButton.Enabled = false;
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_WorldServer != null)
                    {
                        worldText.AppendText(Wrunning); worldText.AppendText(kill);
                    }
                    else
                    {
                        // Start WorldServer
                        proc_WorldServer = new ProcessManager("World.exe", "");
                        worldText.AppendText("Starting: World server" + "\r\n");
                        proc_WorldServer.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_WorldServer_DataReceived);
                        proc_WorldServer.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_WorldServer_DataReceived);
                        proc_WorldServer.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_WorldServer != null)
                    {
                        worldText.AppendText("Shutting down WorldServer. CPU time used: " + proc_WorldServer.TotalProcessorTime.ToString() + "\r\n");
                        proc_WorldServer.StopProcess();
                        proc_WorldServer = null;
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void Startqueryserv()
        {
            if (!File.Exists("queryserv.exe"))
            {
                MessageBox.Show("Please include the file queryserv.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                this.queryservButton.Enabled = false;
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_queryserv != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_queryserv = new ProcessManager("queryserv.exe", "");
                        queryservText.AppendText("Starting: queryserv server" + "\r\n");
                        proc_queryserv.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_queryserv_DataReceived);
                        proc_queryserv.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_queryserv_DataReceived);
                        proc_queryserv.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_queryserv != null)
                    {
                        queryservText.AppendText("Shutting down queryserv Server. CPU time used: " + proc_queryserv.TotalProcessorTime.ToString() + "\r\n");
                        proc_queryserv.StopProcess();
                        proc_queryserv = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartUCS()
        {
            if (!File.Exists("ucs.exe"))
            {
                MessageBox.Show("Please include the file ucs.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                this.ucsButton.Enabled = false;
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_UCSServer != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_UCSServer = new ProcessManager("ucs.exe", "");
                        UCSText.AppendText("Starting: UCS server" + "\r\n");
                        proc_UCSServer.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_UCSServer_DataReceived);
                        proc_UCSServer.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_UCSServer_DataReceived);
                        proc_UCSServer.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_UCSServer != null)
                    {
                        UCSText.AppendText("Shutting down UCS Server. CPU time used: " + proc_UCSServer.TotalProcessorTime.ToString() + "\r\n");
                        proc_UCSServer.StopProcess();
                        proc_UCSServer = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZones()//Starts zones based on number selected
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                //string number = numericUpDown1.ToString();
                int num = Convert.ToInt32(numericUpDown1.Value);
                //num = number;
                for (int i = 1; i <= num; i++)
                {
                    if (num >= 1) StartZone1();
                    if (num >= 2) StartZone2();
                    if (num >= 3) StartZone3();
                    if (num >= 4) StartZone4();
                    if (num >= 5) StartZone5();
                    if (num >= 6) StartZone6();
                    if (num >= 7) StartZone7();
                    if (num >= 8) StartZone8();
                }
            }
        }

        private void StartZone1()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer1 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer1 = new ProcessManager("Zone.exe", "");
                        zone1Text.AppendText("Starting: Zone server 1" + "\r\n");
                        proc_ZoneServer1.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer1_DataReceived);
                        proc_ZoneServer1.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer1_DataReceived);
                        proc_ZoneServer1.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer1 != null)
                    {
                        zone1Text.AppendText("Shutting down ZoneServer 1. CPU time used: " + proc_ZoneServer1.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer1.StopProcess();
                        proc_ZoneServer1 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone2()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer2 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer2 = new ProcessManager("Zone.exe", "");
                        zone2Text.AppendText("Starting: Zone server 2" + "\r\n");
                        proc_ZoneServer2.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer2_DataReceived);
                        proc_ZoneServer2.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer2_DataReceived);
                        proc_ZoneServer2.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer2 != null)
                    {
                        zone2Text.AppendText("Shutting down ZoneServer 2. CPU time used: " + proc_ZoneServer2.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer2.StopProcess();
                        proc_ZoneServer2 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone3()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer3 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer3 = new ProcessManager("Zone.exe", "");
                        zone3Text.AppendText("Starting: Zone server 3" + "\r\n");
                        proc_ZoneServer3.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer3_DataReceived);
                        proc_ZoneServer3.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer3_DataReceived);
                        proc_ZoneServer3.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer3 != null)
                    {
                        zone3Text.AppendText("Shutting down ZoneServer 3. CPU time used: " + proc_ZoneServer3.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer3.StopProcess();
                        proc_ZoneServer3 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone4()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer4 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer4 = new ProcessManager("Zone.exe", "");
                        zone4Text.AppendText("Starting: Zone server 4" + "\r\n");
                        proc_ZoneServer4.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer4_DataReceived);
                        proc_ZoneServer4.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer4_DataReceived);
                        proc_ZoneServer4.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer4 != null)
                    {
                        zone4Text.AppendText("Shutting down ZoneServer 4. CPU time used: " + proc_ZoneServer4.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer4.StopProcess();
                        proc_ZoneServer4 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone5()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer5 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer5 = new ProcessManager("Zone.exe", "");
                        zone5Text.AppendText("Starting: Zone server 5" + "\r\n");
                        proc_ZoneServer5.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer5_DataReceived);
                        proc_ZoneServer5.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer5_DataReceived);
                        proc_ZoneServer5.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer5 != null)
                    {
                        zone5Text.AppendText("Shutting down ZoneServer 5. CPU time used: " + proc_ZoneServer5.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer5.StopProcess();
                        proc_ZoneServer5 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone6()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer6 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer6 = new ProcessManager("Zone.exe", "");
                        zone6Text.AppendText("Starting: Zone server 6" + "\r\n");
                        proc_ZoneServer6.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer6_DataReceived);
                        proc_ZoneServer6.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer6_DataReceived);
                        proc_ZoneServer6.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer6 != null)
                    {
                        zone6Text.AppendText("Shutting down ZoneServer 6. CPU time used: " + proc_ZoneServer6.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer6.StopProcess();
                        proc_ZoneServer6 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone7()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer7 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer7 = new ProcessManager("Zone.exe", "");
                        zone7Text.AppendText("Starting: Zone server 7" + "\r\n");
                        proc_ZoneServer7.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer7_DataReceived);
                        proc_ZoneServer7.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer7_DataReceived);
                        proc_ZoneServer7.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer7 != null)
                    {
                        zone7Text.AppendText("Shutting down ZoneServer 7. CPU time used: " + proc_ZoneServer7.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer7.StopProcess();
                        proc_ZoneServer7 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void StartZone8()
        {
            if (!File.Exists("Zone.exe"))
            {
                MessageBox.Show("Please include the file Zone.exe and it's respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer8 != null)
                    {
                        return;
                    }
                    else
                    {
                        proc_ZoneServer8 = new ProcessManager("Zone.exe", "");
                        zone8Text.AppendText("Starting: Zone server 8" + "\r\n");
                        proc_ZoneServer8.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer8_DataReceived);
                        proc_ZoneServer8.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_ZoneServer8_DataReceived);
                        proc_ZoneServer8.StartProcess();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    if (proc_ZoneServer8 != null)
                    {
                        zone8Text.AppendText("Shutting down ZoneServer 8. CPU time used: " + proc_ZoneServer8.TotalProcessorTime.ToString() + "\r\n");
                        proc_ZoneServer8.StopProcess();
                        proc_ZoneServer8 = null;
                    }
                    else
                    {
                    }
                }
            }
            UpdateTabVisibility();
            radioStatus();
        }

        void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabCMD)
                txtInputCMD.Focus();
        }

        void txtInputCMD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (proc_CMD != null)
            {
                if (e.KeyChar == 13)
                {
                    // We got a command
                    e.Handled = true;
                    proc_CMD.StandardInput.WriteLine(txtInputCMD.Text + "\r\n");
                    txtInputCMD.Text = "";
                }
            }
            else
            {
                txtCMD.AppendText("CMD: CMD Shell is not running can't send commands to it." + "\r\n");
            }
        }

        void proc_LoginServer_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(loginText.AppendText), new object[] { "LoginServer: " + e.Data + "\r\n" });
        }

        void proc_shared_memory_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(shared_memoryText.AppendText), new object[] { "shared_memory: " + e.Data + "\r\n" });
        }

        void proc_WorldServer_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(worldText.AppendText), new object[] { "WorldServer: " + e.Data + "\r\n" });
        }

        void proc_queryserv_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(queryservText.AppendText), new object[] { "queryserv Server: " + e.Data + "\r\n" });
        }

        void proc_UCSServer_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(UCSText.AppendText), new object[] { "UCS Server: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer1_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone1Text.AppendText), new object[] { "ZoneServer1: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer2_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone2Text.AppendText), new object[] { "ZoneServer2: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer3_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone3Text.AppendText), new object[] { "ZoneServer3: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer4_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone4Text.AppendText), new object[] { "ZoneServer4: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer5_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone5Text.AppendText), new object[] { "ZoneServer5: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer6_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone6Text.AppendText), new object[] { "ZoneServer6: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer7_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone7Text.AppendText), new object[] { "ZoneServer7: " + e.Data + "\r\n" });
        }

        void proc_ZoneServer8_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(zone8Text.AppendText), new object[] { "ZoneServer8: " + e.Data + "\r\n" });
        }

        void proc_CMD_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            this.Invoke(new AppendText(txtCMD.AppendText), new object[] { "CMD: " + e.Data + "\r\n" });
        }

        private void radioStatus()//Checks stop start setting
        {
            //string rStatus;
            if (startRadio.Checked.Equals(true) | stopRadio.Checked.Equals(false))//start
            {
                if (!tabControl1.TabPages.Contains(loginTab1))
                {
                    this.button29.Enabled = true;
                }
                else if (tabControl1.TabPages.Contains(loginTab1))
                {
                    this.button29.Enabled = false;
                }
                ///////////////////////////////////
                if (proc_LoginServer != null)
                {
                    this.loginButton.Enabled = false;
                    this.completeButton.Enabled = false;
                }
                else if (proc_LoginServer == null)
                {
                    this.loginButton.Enabled = true;
                    this.completeButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_shared_memory != null)
                {
                    this.shared_memoryButton.Enabled = false;
                }
                else if (proc_shared_memory == null)
                {
                    this.shared_memoryButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_WorldServer != null)
                {
                    this.worldButton.Enabled = false;
                }
                else if (proc_WorldServer == null)
                {
                    this.worldButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_queryserv != null)
                {
                    this.queryservButton.Enabled = false;
                }
                else if (proc_queryserv == null)
                {
                    this.queryservButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_UCSServer != null)
                {
                    this.ucsButton.Enabled = false;
                }
                else if (proc_UCSServer == null)
                {
                    this.ucsButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer1 != null)
                {
                    this.zone1Button.Enabled = false;
                    this.zonesButton.Enabled = false;
                }
                else if (proc_ZoneServer1 == null)
                {
                    this.zone1Button.Enabled = true;
                    this.zonesButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer2 != null)
                {
                    this.zone2Button.Enabled = false;
                }
                else if (proc_ZoneServer2 == null)
                {
                    this.zone2Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer3 != null)
                {
                    this.zone3Button.Enabled = false;
                }
                else if (proc_ZoneServer3 == null)
                {
                    this.zone3Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer4 != null)
                {
                    this.zone4Button.Enabled = false;
                }
                else if (proc_ZoneServer4 == null)
                {
                    this.zone4Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer5 != null)
                {
                    this.zone5Button.Enabled = false;
                }
                else if (proc_ZoneServer5 == null)
                {
                    this.zone5Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer6 != null)
                {
                    this.zone6Button.Enabled = false;
                }
                else if (proc_ZoneServer6 == null)
                {
                    this.zone6Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer7 != null)
                {
                    this.zone7Button.Enabled = false;
                }
                else if (proc_ZoneServer7 == null)
                {
                    this.zone7Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer8 != null)
                {
                    this.zone8Button.Enabled = false;
                }
                else if (proc_ZoneServer8 == null)
                {
                    this.zone8Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_CMD != null)
                {
                    this.button14.Enabled = false;
                }
                else if (proc_CMD == null)
                {
                    this.button14.Enabled = true;
                }

                this.completeButton.ForeColor = System.Drawing.Color.White;
                this.completeButton.BackColor = System.Drawing.Color.Green;
                this.button29.ForeColor = System.Drawing.Color.White;
                this.button29.BackColor = System.Drawing.Color.Green;
                this.loginButton.ForeColor = System.Drawing.Color.White;
                this.loginButton.BackColor = System.Drawing.Color.Green;
                this.shared_memoryButton.ForeColor = System.Drawing.Color.White;
                this.shared_memoryButton.BackColor = System.Drawing.Color.Green;
                this.worldButton.ForeColor = System.Drawing.Color.White;
                this.worldButton.BackColor = System.Drawing.Color.Green;
                this.queryservButton.ForeColor = System.Drawing.Color.White;
                this.queryservButton.BackColor = System.Drawing.Color.Green;
                this.ucsButton.ForeColor = System.Drawing.Color.White;
                this.ucsButton.BackColor = System.Drawing.Color.Green;
                this.zonesButton.ForeColor = System.Drawing.Color.White;
                this.zonesButton.BackColor = System.Drawing.Color.Green;
                this.zone1Button.ForeColor = System.Drawing.Color.White;
                this.zone1Button.BackColor = System.Drawing.Color.Green;
                this.zone2Button.ForeColor = System.Drawing.Color.White;
                this.zone2Button.BackColor = System.Drawing.Color.Green;
                this.zone3Button.ForeColor = System.Drawing.Color.White;
                this.zone3Button.BackColor = System.Drawing.Color.Green;
                this.zone4Button.ForeColor = System.Drawing.Color.White;
                this.zone4Button.BackColor = System.Drawing.Color.Green;
                this.zone5Button.ForeColor = System.Drawing.Color.White;
                this.zone5Button.BackColor = System.Drawing.Color.Green;
                this.zone6Button.ForeColor = System.Drawing.Color.White;
                this.zone6Button.BackColor = System.Drawing.Color.Green;
                this.zone7Button.ForeColor = System.Drawing.Color.White;
                this.zone7Button.BackColor = System.Drawing.Color.Green;
                this.zone8Button.ForeColor = System.Drawing.Color.White;
                this.zone8Button.BackColor = System.Drawing.Color.Green;
                this.button14.ForeColor = System.Drawing.Color.White;
                this.button14.BackColor = System.Drawing.Color.Green;
            }
            else//stop
            {
                if (!tabControl1.TabPages.Contains(loginTab1))
                {
                    this.button29.Enabled = false;
                }
                else if (tabControl1.TabPages.Contains(loginTab1))
                {
                    this.button29.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_LoginServer == null)
                {
                    this.completeButton.Enabled = false;
                    this.loginButton.Enabled = false;
                }
                else if (proc_LoginServer != null)
                {
                    this.completeButton.Enabled = true;
                    this.loginButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_shared_memory == null)
                {
                    this.shared_memoryButton.Enabled = false;
                }
                else if (proc_shared_memory != null)
                {
                    this.shared_memoryButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_WorldServer == null)
                {
                    this.worldButton.Enabled = false;
                }
                else if (proc_WorldServer != null)
                {
                    this.worldButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_queryserv == null)
                {
                    this.queryservButton.Enabled = false;
                }
                else if (proc_queryserv != null)
                {
                    this.queryservButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_UCSServer == null)
                {
                    this.ucsButton.Enabled = false;
                }
                else if (proc_UCSServer != null)
                {
                    this.ucsButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer1 == null)
                {
                    this.zone1Button.Enabled = false;
                    this.zonesButton.Enabled = false;
                }
                else if (proc_ZoneServer1 != null)
                {
                    this.zone1Button.Enabled = true;
                    this.zonesButton.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer2 == null)
                {
                    this.zone2Button.Enabled = false;
                }
                else if (proc_ZoneServer2 != null)
                {
                    this.zone2Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer3 == null)
                {
                    this.zone3Button.Enabled = false;
                }
                else if (proc_ZoneServer3 != null)
                {
                    this.zone3Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer4 == null)
                {
                    this.zone4Button.Enabled = false;
                }
                else if (proc_ZoneServer4 != null)
                {
                    this.zone4Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer5 == null)
                {
                    this.zone5Button.Enabled = false;
                }
                else if (proc_ZoneServer5 != null)
                {
                    this.zone5Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer6 == null)
                {
                    this.zone6Button.Enabled = false;
                }
                else if (proc_ZoneServer6 != null)
                {
                    this.zone6Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer7 == null)
                {
                    this.zone7Button.Enabled = false;
                }
                else if (proc_ZoneServer7 != null)
                {
                    this.zone7Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_ZoneServer8 == null)
                {
                    this.zone8Button.Enabled = false;
                }
                else if (proc_ZoneServer8 != null)
                {
                    this.zone8Button.Enabled = true;
                }
                ///////////////////////////////////
                if (proc_CMD == null)
                {
                    this.button14.Enabled = false;
                }
                else if (proc_CMD != null)
                {
                    this.button14.Enabled = true;
                }

                this.completeButton.ForeColor = System.Drawing.Color.White;
                this.completeButton.BackColor = System.Drawing.Color.Red;
                this.button29.ForeColor = System.Drawing.Color.White;
                this.button29.BackColor = System.Drawing.Color.Red;
                this.loginButton.ForeColor = System.Drawing.Color.White;
                this.loginButton.BackColor = System.Drawing.Color.Red;
                this.shared_memoryButton.ForeColor = System.Drawing.Color.White;
                this.shared_memoryButton.BackColor = System.Drawing.Color.Red;
                this.worldButton.ForeColor = System.Drawing.Color.White;
                this.worldButton.BackColor = System.Drawing.Color.Red;
                this.queryservButton.ForeColor = System.Drawing.Color.White;
                this.queryservButton.BackColor = System.Drawing.Color.Red;
                this.ucsButton.ForeColor = System.Drawing.Color.White;
                this.ucsButton.BackColor = System.Drawing.Color.Red;
                this.zonesButton.ForeColor = System.Drawing.Color.White;
                this.zonesButton.BackColor = System.Drawing.Color.Red;
                this.zone1Button.ForeColor = System.Drawing.Color.White;
                this.zone1Button.BackColor = System.Drawing.Color.Red;
                this.zone2Button.ForeColor = System.Drawing.Color.White;
                this.zone2Button.BackColor = System.Drawing.Color.Red;
                this.zone3Button.ForeColor = System.Drawing.Color.White;
                this.zone3Button.BackColor = System.Drawing.Color.Red;
                this.zone4Button.ForeColor = System.Drawing.Color.White;
                this.zone4Button.BackColor = System.Drawing.Color.Red;
                this.zone5Button.ForeColor = System.Drawing.Color.White;
                this.zone5Button.BackColor = System.Drawing.Color.Red;
                this.zone6Button.ForeColor = System.Drawing.Color.White;
                this.zone6Button.BackColor = System.Drawing.Color.Red;
                this.zone7Button.ForeColor = System.Drawing.Color.White;
                this.zone7Button.BackColor = System.Drawing.Color.Red;
                this.zone8Button.ForeColor = System.Drawing.Color.White;
                this.zone8Button.BackColor = System.Drawing.Color.Red;
                this.button14.ForeColor = System.Drawing.Color.White;
                this.button14.BackColor = System.Drawing.Color.Red;
            }
        }

        private void startRadio_CheckedChanged(object sender, EventArgs e)//Start Radio Selector
        {
            radioStatus();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//Stop Radio selector
        {
            radioStatus();
        }

        private void completeButton_Click(object sender, EventArgs e)//Start ALL
        {
            if (!File.Exists("loginserver.exe") | !File.Exists("shared_memory.exe") | !File.Exists("world.exe") | !File.Exists("queryserv.exe") | !File.Exists("ucs.exe") | !File.Exists("zone.exe"))
            {
                MessageBox.Show("Please include the files loginserver.exe, shared_memory.exe, world.exe, queryserv.exe, ucs.exe, and zone.exe and their respective dependancies including config files for this program to work correctly.",
                    "FILE MISSING WARNING!");
            }
            else
            {
                if (startRadio.Checked.Equals(true))
                {
                    this.zonesButton.Enabled = false;
                    StartLogin();
                    Startshared_memory();
                    StartWorld();
                    Startqueryserv();
                    StartUCS();
                    StartZones();
                }
                else if (stopRadio.Checked.Equals(true))
                {
                    Stop();
                }
            }
        }

        private void loginButton_Click(object sender, EventArgs e)//Login start
        {
            StartLogin();
        }

        private void shared_memoryButton_Click(object sender, EventArgs e)
        {
            Startshared_memory();
        }

        private void worldButton_Click(object sender, EventArgs e)//World Start
        {
            StartWorld();
        }

        private void queryservButton_Click(object sender, EventArgs e)
        {
            Startqueryserv();
        }

        private void ucsButton_Click(object sender, EventArgs e)
        {
            StartUCS();
        }

        private void zonesButton_Click(object sender, EventArgs e)//Start zones based on number selected
        {
            StartZones();
        }

        private void zone1Button_Click(object sender, EventArgs e)//Start Zone 1
        {
            StartZone1();
        }

        private void zone2Button_Click(object sender, EventArgs e)//Start Zone 2
        {
            StartZone2();
        }

        private void zone3Button_Click(object sender, EventArgs e)//Start Zone 3
        {
            StartZone3();
        }

        private void zone4Button_Click(object sender, EventArgs e)//Start Zone 4
        {
            StartZone4();
        }

        private void zone5Button_Click(object sender, EventArgs e)//Start Zone 5
        {
            StartZone5();
        }

        private void zone6Button_Click(object sender, EventArgs e)//Start Zone 6
        {
            StartZone6();
        }

        private void zone7Button_Click(object sender, EventArgs e)//Start Zone 7
        {
            StartZone7();
        }

        private void zone8Button_Click(object sender, EventArgs e)//Start Zone 8
        {
            StartZone8();
        }

        private void button14_Click(object sender, EventArgs e)//Start or Stop shell
        {
            if (startRadio.Checked.Equals(true))
            {
                if (proc_CMD != null)
                {
                    string Crunning = "Command Shell already running." + "\r\n";
                    loginText.AppendText(Crunning); worldText.AppendText(Crunning); zone1Text.AppendText(Crunning);
                    zone2Text.AppendText(Crunning); zone3Text.AppendText(Crunning); zone4Text.AppendText(Crunning);
                    zone5Text.AppendText(Crunning); zone6Text.AppendText(Crunning); zone7Text.AppendText(Crunning);
                    zone8Text.AppendText(Crunning); txtCMD.AppendText(Crunning);
                }
                else
                {
                    proc_CMD = new ProcessManager("cmd.exe", "");
                    proc_CMD.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_CMD_DataReceived);
                    proc_CMD.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_CMD_DataReceived);
                    proc_CMD.StartProcess();
                }
            }
            else if (stopRadio.Checked.Equals(true))
            {
                if (proc_CMD != null)
                {
                    txtCMD.AppendText("Closing CMD Shell. CPU time used: " + proc_CMD.TotalProcessorTime.ToString() + "\r\n");
                    proc_CMD.StopProcess();
                    proc_CMD = null;
                }
                else
                {
                }
            }
            else
            {
            }
            UpdateTabVisibility();
            radioStatus();
        }

        private void button29_Click(object sender, EventArgs e)//Tabs
        {
            if (startRadio.Checked.Equals(true))
            {
                tabsAdd();
            }
            else if (stopRadio.Checked.Equals(true))
            {
                tabsRemove();
            }
        }

        private void button3_Click(object sender, EventArgs e)//Clear ALL
        {
            loginText.Clear(); shared_memoryText.Clear(); worldText.Clear(); queryservText.Clear();
            UCSText.Clear(); zone1Text.Clear(); zone2Text.Clear(); zone3Text.Clear(); zone4Text.Clear();
            zone5Text.Clear(); zone6Text.Clear(); zone7Text.Clear(); zone8Text.Clear(); txtCMD.Clear();
        }

        private void button4_Click(object sender, EventArgs e)//Clear World
        {
            worldText.Clear();
        }

        private void button5_Click(object sender, EventArgs e)//Clear All Zones
        {
            zone1Text.Clear(); zone2Text.Clear(); zone3Text.Clear(); zone4Text.Clear();
            zone5Text.Clear(); zone6Text.Clear(); zone7Text.Clear(); zone8Text.Clear();
        }

        private void button6_Click(object sender, EventArgs e)//Clear Zone 1
        {
            zone1Text.Clear();
        }

        private void button7_Click(object sender, EventArgs e)//Clear Zone 2
        {
            zone2Text.Clear();
        }

        private void button8_Click(object sender, EventArgs e)//Clear Zone 3
        {
            zone3Text.Clear();
        }

        private void button9_Click(object sender, EventArgs e)//Clear Zone 4
        {
            zone4Text.Clear();
        }

        private void button10_Click(object sender, EventArgs e)//Clear Zone 5
        {
            zone5Text.Clear();
        }

        private void button11_Click(object sender, EventArgs e)//Clear Zone 6
        {
            zone6Text.Clear();
        }

        private void button12_Click(object sender, EventArgs e)//Clear Zone 7
        {
            zone7Text.Clear();
        }

        private void button13_Click(object sender, EventArgs e)//Clear Zone 8
        {
            zone8Text.Clear();
        }

        private void button15_Click(object sender, EventArgs e)//Clear Shell
        {
            txtCMD.Clear();
        }

        private void button16_Click(object sender, EventArgs e)//Load Default Settings
        {
            //MessageBox.Show(FindXmlData(selectedIndex));
            //need to figure out how to read and write xml and ini files
        }

        private void button2_Click(object sender, EventArgs e)//Force ALL STOP
        {
            Stop();
        }
    }
}
