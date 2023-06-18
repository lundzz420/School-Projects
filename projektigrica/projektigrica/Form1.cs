using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektigrica
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;
        int horizontalSpeed = 5;
        int verticalSpeed = 3;
        int enemy1Speed = 5;
        int enemy2Speed = 3;
        int enemy3Speed = 4;
        public Form1()
        {
            InitializeComponent();
        }
        private void MainTimerEvent(object sender, EventArgs e)
        {
            Scoretxt.Text = "Score: " + score;
            player1.Top += jumpSpeed;
            if (goLeft == true)
            {
                player1.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player1.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player1.Top = x.Top - player1.Height;
                            if ((string)x.Name == "HorizontalPlatform" && goLeft == false || (string)x.Name == "HorizontalPlatform" && goRight == false)
                            {
                                player1.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameTimer.Stop();
                            isGameOver = true;
                            Scoretxt.Text = "Score: " + score + Environment.NewLine + "YOU DIED !";
                        }
                    }
                }
            }
            HorizontalPlatform.Left -= horizontalSpeed;
            if (HorizontalPlatform.Left < 0 || HorizontalPlatform.Left + HorizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            VerticalPlatform.Top += verticalSpeed;
            if (VerticalPlatform.Top < 455 || VerticalPlatform.Top > 361)
            {
                verticalSpeed = -verticalSpeed;
            }
            enemy1.Left -= enemy1Speed;
            if (enemy1.Left < pictureBox5.Left || enemy1.Left + enemy1.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemy1Speed = -enemy1Speed;
            }
            enemy2.Left += enemy2Speed;
            if (enemy2.Left < pictureBox2.Left || enemy2.Left + enemy2.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemy2Speed = -enemy2Speed;
            }
            enemy3.Left += enemy3Speed;
            if (enemy3.Left < pictureBox4.Left || enemy3.Left + enemy3.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemy3Speed = -enemy3Speed;
            }
            if (player1.Top + player1.Height > this.ClientSize.Height + 50)
            {
                GameTimer.Stop();
                isGameOver = true;
                Scoretxt.Text = "Score: " + score + Environment.NewLine + "You have fallen to death !";
            }
            if (player1.Bounds.IntersectsWith(Finish.Bounds) && score == 26)
            {
                GameTimer.Stop();
                isGameOver = true;
                Scoretxt.Text = "Score: " + score + Environment.NewLine + "You have completed your quest !";
            }
            else
            {
                Scoretxt.Text = "Score: " + score + Environment.NewLine + "Collect the bread";
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            Scoretxt.Text = "Score: " + score;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            // reset the position of player, platform and enemies
            player1.Left = 34;
            player1.Top = 611;
            enemy1.Left = 592;
            enemy2.Left = 170;
            enemy3.Left = 192;
            HorizontalPlatform.Left = 402;
            VerticalPlatform.Top = 131;
            GameTimer.Start();
        }
    }

}
