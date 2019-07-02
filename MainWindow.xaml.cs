using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace IP查询
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //WebRequest request = WebRequest.Create("http://www.baidu.com/");

            String ipaddress = TB1.Text;
            String url = "http://localhost:2202/test?ip=" + ipaddress;

            WebRequest request = WebRequest.Create(url);

            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0";


            WebResponse response = request.GetResponse();
            String arr = ((HttpWebResponse) response).StatusDescription;


            if (arr == "OK")
            {
                Console.WriteLine("响应状态码为"+arr);
                Stream dataStream = response.GetResponseStream();

                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(dataStream))
                    {
                        tb2.Text = "";

                        string line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            //Console.WriteLine(line);
                            tb2.Text = tb2.Text + line+"\n";

                        }
                    }
                }
                catch (Exception ee)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ee.Message);
                }
            }
            else
            {
                Console.WriteLine("网络异常,请稍后重试.");
            }
            response.Close();
        }
    }

 
}
