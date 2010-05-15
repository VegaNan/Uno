﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uno
{
    partial class StartupPlayerView : UserControl
    {
        private Player player;

        public StartupPlayerView()
        {
            InitializeComponent();

            // Create a new player object
            player = new Player();
        }


        /// <summary>
        ///  The player object 
        /// </summary>
        public Player Player
        {
            get { return player; }
        }

        /// <summary>
        /// Set the name of the player
        /// </summary>
        /// <param name="input"></param>
        public void SetPlayerName(string input)
        {
            name.Text = input;
        }



        private void name_TextChanged(object sender, EventArgs e)
        {
            player.Name = name.Text;
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (type.Text)
            {
                case "Human":
                    player.Type = Player.PlayerType.Human;
                    break;
                case "Computer":
                    player.Type = Player.PlayerType.Computer;
                    break;
                case "Smart Computer":
                    player.Type = Player.PlayerType.SmartComputer;
                    break;
            }

        }
    }
}
