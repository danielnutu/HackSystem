﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public partial class DefaultProgramForm : Form
    {
        private Image _imageResource = DefaultProgramResource.HackSystemLogo;
        public Image ImageResource
        {
            get => _imageResource;
            set
            {
                if (value == null)
                    _imageResource = DefaultProgramResource.HackSystemLogo;
                else
                    _imageResource = value;

                MainPictureBox.Image = _imageResource;
                MainPictureBox.Size = _imageResource.Size;
            }
        }


        public DefaultProgramForm()
        {
            InitializeComponent();
            this.FormClosed += delegate {
                this.Dispose(true);
                MainPictureBox.Image?.Dispose();
                MainPictureBox.Image = null;
                GC.Collect();
            };
        }

        private void DefaultProgramForm_Load(object sender, EventArgs e)
        {
            //this.Text = this.GetHashCode().ToString("X");
        }

        private void DefaultProgramForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
