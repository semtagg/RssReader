﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RssReader
{
    public partial class RssUi : Form
    {
        private LinkLabel linkLabel1;
        private Button label2;
        private WebBrowser webBrowser1;

        public RssUi()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Text = "RssReader";
            InitializeComponent();
        }
    }
}
