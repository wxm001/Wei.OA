using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFWindowsForms
{
    using System.Activities;

    public partial class MainForm : Form
    {
        private WorkflowApplication WFApp { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
             WFApp=new WorkflowApplication(new StateMoneyActivity(),
                new Dictionary<string, object>()
                    {
                        { "InBookMarkName",this.txtBookMarkName.Text}
                    });
            WFApp.Idle += OnWfAppIdle;
            //wfApp.OnUnhandledException += OnWfAppException;
            WFApp.Run();
        }

        //private UnhandledExceptionAction OnWfAppException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        //{
        //    //arg.UnhandledException
        //    Console.WriteLine("出现异常"+arg.UnhandledException.ToString());
        //}

        private void OnWfAppIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流停下");
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //让工作流继续
            WFApp.ResumeBookmark(this.txtBookMarkName.Text, int.Parse(this.txtMoney.Text));
        }

        private void btnStateStart_Click(object sender, EventArgs e)
        {
            WorkflowApplication wfApp=new WorkflowApplication(new StateActivity());
            wfApp.Run();
        }
    }
}
