using CSScriptLibrary;
using IronPython.Hosting;
using NLua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsCallScriptDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dicCodeDemo.Add("Lua", "function add(left, right)\n" +
                   "    return (left + right)& 0xFFFFFF;\n" +
                   "end\n");
            dicCodeDemo.Add("C#",
                "int add(int left, int right)\n" +
                "{\n" +
                "    return (left + right)& 0xFFFFFF;\n" +
                "}\n");
            dicCodeDemo.Add("Python",
                 "def add(left, right):\n" +
                 "  return (left + right)& 0xFFFFFF\n" +
                 "\n");

            cmbScriptType.Items.Add("C#");
            cmbScriptType.Items.Add("Lua");
            cmbScriptType.Items.Add("Python");
            cmbScriptType.SelectedItem = "C#";

            nudLeft.Value = 1;
            nudRight.Value = 2;
            nudLoop.Value = 1000;
        }
        Dictionary<string, string> dicCodeDemo = new Dictionary<string, string>();
        private void btnExec_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (cmbScriptType.SelectedItem)
            {
                case "C#":
                    res = RunCSScript();
                    break;
                case "Lua":
                    res = RunLuaScript();
                    break;
                case "Python":
                    res = RunPythonScript();
                    break;
                default:
                    break;
            }
            stopwatch.Stop();

            rtbResult.Text = res;
            rtbTime.Text = stopwatch.ElapsedMilliseconds.ToString();
        }
        string RunLuaScript()
        {
            Lua state = new Lua();

            state.DoString(rtbScript.Text);
            var scriptFunc = state["add"] as LuaFunction;
            // var res = scriptFunc.Call(3, 5)[0];
            var res = (long)(scriptFunc.Call((int)nudLeft.Value, (int)nudRight.Value).First());

            return res.ToString();
        }
        string RunCSScript()
        {
            var adder = CSScript.CreateFunc<int>(rtbScript.Text);
            int res = adder((int)nudLeft.Value, (int)nudRight.Value);

            return res.ToString();
        }
        string RunPythonScript()
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var source = engine.CreateScriptSourceFromString(rtbScript.Text);
            source.Execute(scope);
            var adder = scope.GetVariable<Func<object, object, object>>("add");

            return adder((int)nudLeft.Value, (int)nudRight.Value).ToString();
        }
        private void cmbScriptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbScript.Text = dicCodeDemo[(string)cmbScriptType.SelectedItem];
            rtbResult.Text = "";
            rtbTime.Text = "";
        }

        private void btnExecLoop_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            string selectItem = (string)cmbScriptType.SelectedItem;
            int ct = (int)nudLoop.Value;

            stopwatch.Start();
            for (int i = 0; i < ct; i++)
            {
                switch (selectItem)
                {
                    case "C#":
                        RunCSScript();
                        break;
                    case "Lua":
                        RunLuaScript();
                        break;
                    case "Python":
                        RunPythonScript();
                        break;
                    default:
                        break;
                }
            }
            stopwatch.Stop();
            rtbResult.Text = $"循环执行成功, 脚本:{selectItem}, 运行次数:{ct}, 运行时间: {stopwatch.ElapsedMilliseconds}";
            rtbTime.Text = stopwatch.ElapsedMilliseconds.ToString();
            //Task.Run(() =>
            //{
            //    stopwatch.Start();
            //    for (int i = 0; i < ct; i++)
            //    {
            //        switch (selectItem)
            //        {
            //            case "C#":
            //                RunCSScript();
            //                break;
            //            case "Lua":
            //                RunLuaScript();
            //                break;
            //            case "Python":
            //                RunPythonScript();
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    stopwatch.Stop();
            //}).ContinueWith((task)=> {
            //    rtbResult.Invoke(new Action(()=> {
            //        rtbResult.Text = "";
            //        rtbTime.Text = stopwatch.ElapsedMilliseconds.ToString();
            //    }));
            //});
        }
    }
}
