using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeadPhoto999
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();

            //必须把绘图函数写到Paint的响应函数里，不然图像绘制出来会一闪而过
            this.Paint += new PaintEventHandler(DrawHeadPhotos);
        }
        

        private void DrawHeadPhotos(object sender,PaintEventArgs e)
        {
            //创建新的线程，以达到非阻塞效果，不然程序没法退出，键盘鼠标事件写了也没用
            Thread thread = new Thread(DrawAsync);
            thread.Start();
        }

        private void DrawAsync()
        {
            Screen screen = Screen.PrimaryScreen;
            int width = screen.WorkingArea.Width;
            int height = screen.WorkingArea.Height;
            Random random = new Random();

            Graphics graphics = this.CreateGraphics();

            for (int i = 0; i < 10000; i++)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                Point[] points = { new Point(x, y), new Point(x + 50, y), new Point(x, y + 50) };
                int index = random.Next(0, imageList1.Images.Count);
                Image img = imageList1.Images[index];
                graphics.DrawImage(img, points);

                Thread.Sleep(100);

            }
            graphics.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /*switch (keyData)
            {
                case Keys.F1:
                    //Stuff
                    return true;
                case Keys.Alt | Keys.Down:
                    //Stuff
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);*/

            //this.Close();//这个只关闭窗体，程序并未退出
            //Application.Exit();//这个程序虽然会退出，但是会等待Msg响应完成之后才退出，显然不行
            System.Environment.Exit(0);//这个才是彻底退出
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void form1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
