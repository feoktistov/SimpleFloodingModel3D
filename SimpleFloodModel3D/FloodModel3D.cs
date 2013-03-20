using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using FloodModel;

namespace SimpleFloodModel3D
{
    public partial class FloodModel3D : Form
    {
        public FloodModel3D()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            modelRenderPanel.KeyDown += new KeyEventHandler(glControl_KeyDown);
            modelRenderPanel.KeyUp += new KeyEventHandler(glControl_KeyUp);
            modelRenderPanel.Resize += new EventHandler(glControl_Resize);
            modelRenderPanel.Paint += new PaintEventHandler(glControl_Paint);

            this.MouseWheel += new MouseEventHandler(modelRenderPanel_MouseWheel);

            GL.ClearColor(Color.MidnightBlue);


            GL.Enable(EnableCap.DepthTest);
          
            Application.Idle += Application_Idle;

            // Ensure that the viewport and projection matrix are set correctly.
            glControl_Resize(modelRenderPanel, EventArgs.Empty);

            floodMap = new Map();
            floodMap.Init();
        }

        Map floodMap;

        #region GLControl event handlers

        float xAngle = 0;
        float yAngle = 0;
        float dAngle = 1f;

        void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                modelRenderPanel.GrabScreenshot().Save("screenshot.png");
            if (e.KeyCode == Keys.Up)
                xAngle += dAngle;
            if (e.KeyCode == Keys.Down)
                xAngle -= dAngle;
            if (e.KeyCode == Keys.Left)
                yAngle += dAngle;
            if (e.KeyCode == Keys.Right)
                yAngle -= dAngle;
            if (e.KeyCode == Keys.R)
                floodMap.Randomize(1);
        }


        void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        void glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        void glControl_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl c = sender as OpenTK.GLControl;

            if (c.ClientSize.Height == 0)
                c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

            GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }
        #endregion

        

        #region Application_Idle event

        void Application_Idle(object sender, EventArgs e)
        {
            while (modelRenderPanel.IsIdle)
            {
                Render();
            }
        }

        #endregion

        #region private void Render()

        private float distance = 10;
        private float dDistance = 0.01f;
        private void Render()
        {
            Matrix4 lookat = Matrix4.LookAt(0, 0, distance, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(xAngle, 0, 1.0f, 0.0f);
            GL.Rotate(yAngle, 1, 0, 0);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            floodMap.DrawMap();

            modelRenderPanel.SwapBuffers();
        }

        #endregion



        #region Mouse Events

        bool isMouseDown = false;
        Point mousePosition = new Point();

        private void modelRenderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mousePosition = new Point(e.X, e.Y);
        }

        private void modelRenderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                xAngle += dAngle * (e.X - mousePosition.X);
                yAngle += dAngle * (e.Y - mousePosition.Y);

                mousePosition = new Point(e.X, e.Y);
            }
        }

        private void modelRenderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void modelRenderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            distance += e.Delta * dDistance;
            if (distance < 2)
            {
                distance = 2;
            }
        }

        #endregion

        private void modelRenderPanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private bool isRunning;

        private void run()
        {
            floodMap.Step();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                buttonRun.Text = "Stop";
                while (isRunning)
                {
                    run();
                    Application.DoEvents();
                }
            }
            else
            {
                isRunning = false;
                buttonRun.Text = "Run";
            }
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            floodMap.Randomize(1);
        }

        private void buttonStep_Click(object sender, EventArgs e)
        {
            floodMap.Step();
        }
    }
}
