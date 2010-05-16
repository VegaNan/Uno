﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uno
{
    partial class GameOptionsView : Form
    {
        private GameOptions options = new GameOptions();

        public GameOptionsView()
        {
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(GameOptionsView_FormClosing);
        }


        /// <summary>
        /// Selected game options
        /// </summary>
        public GameOptions Options
        {
            get
            {
                // Update options before returning
                options.UseAnimation = animationCheckbox.Checked;
                options.CardsForEachPlayer = (int)cardsPerPlayerUpDown.Value;
                options.ComputerPlayerDelay = (int)computerDelayUpDown.Value;

                return options;
            }
        }


        void GameOptionsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
