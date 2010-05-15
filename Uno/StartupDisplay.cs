﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Projectplace.Gui;

namespace Uno
{
    public partial class StartupDisplay : Form
    {
        private List<StartupPlayerView> startupPlayerViews = new List<StartupPlayerView>(Game.MAXPLAYERS);
        private GameOptionsView optionsView = new GameOptionsView();

        // Aids in aniating height, without having to disable the number of players control while animating
        private bool animating = false;
        private int heightToAnimate = -1;


        public StartupDisplay()
        {
            InitializeComponent();

            // Populate the startup player views list
            startupPlayerViews.Add(startupPlayerView1);
            startupPlayerViews.Add(startupPlayerView2);
            startupPlayerViews.Add(startupPlayerView3);
            startupPlayerViews.Add(startupPlayerView4);

            Height = 200;

            numberOfPlayers.Maximum = Game.MAXPLAYERS;
            numberOfPlayers.Value = 2;



            this.FormClosed += new FormClosedEventHandler(StartupDisplay_FormClosed);



            // Only show the debug buton when in debug mode in VisualC#
            quickDebugGameButton.Visible = false;

            #if DEBUG
            quickDebugGameButton.Visible = true;
            #endif

            
        }


        void StartupDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.CloseWindow();
        }


        private void numberOfPlayers_ValueChanged(object sender, EventArgs e)
        {
            // Set the number of players
            changeNumberOfPlayers((int) numberOfPlayers.Value);
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>(Game.MAXPLAYERS);

            // Add the players from the form into the list
            for (int i = 0; i < numberOfPlayers.Value; i++)
            {
                players.Add(startupPlayerViews[i].Player);

                // Add a name if one isn't provided
                if (players[i].Name == null) players[i].Name = GetPlayerNameForInt(i);
            }

            // Create the new game in a new form
            Program.NewGame(players, optionsView.Options);


            // Close this form
            Close();

        }

        private void gameOptionsButton_Click(object sender, EventArgs e)
        {
            optionsView.ShowDialog();
        }


        private void quickDebugGameButton_Click(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>(Game.MAXPLAYERS);
            
            // Set options for a quick game to be able to test the end of game window
            GameOptions options = new GameOptions();
            options.CardsForEachPlayer = 25;
            options.ComputerPlayerDelay = 1;
            options.UseAnimation = false;

            // Add the players from the form into the list
            for (int i = 0; i < numberOfPlayers.Value; i++)
                players.Add(new Player(GetPlayerNameForInt(i), Player.PlayerType.Computer));

            // Create the new game in a new form
            Program.NewGame(players, options);


            // Close this form
            Close();
        }


        private void changeNumberOfPlayers(int count)
        {
            // Hide/Show the forms for different players depending on the number selecred
            for (int i = 0; i < Game.MAXPLAYERS; i++)
            {
                startupPlayerViews[i].Visible = i < count ? true : false;
                if (startupPlayerViews[i].Player.Name == "") startupPlayerViews[i].SetPlayerName(GetPlayerNameForInt(i));
            }

            int height = count * 100 + 202;


            if (animating)
                heightToAnimate = height;
            else
                animateToHeight(height);

            
        }

        private void resizeCompleted()
        {
            animating = false;

            if (heightToAnimate >= 0 && heightToAnimate != Height)
                animateToHeight(heightToAnimate);
            else
                heightToAnimate = -1;
        }

        private void animateToHeight(int input)
        {
            // Animate changing the height
            TweenPairs p = new TweenPairs();
            p.Add("TheFormHeight", input);
            Tweener t = new Tweener(this, p, Tweener.easeOutElastic, 30, 0);

            //animating = true;

            //t.setOnComplete(new Tweener.onCompleteFunction(resizeCompleted));
            Tweener.add(t);
            
            
        }



        


        public Single TheFormHeight
        {
            get { return (Single)Height; }
            set { Height = (int)value; }
        }


        static public string GetPlayerNameForInt(int input)
        {
            return "Player " + (input + 1).ToString();
        }


    }
}
