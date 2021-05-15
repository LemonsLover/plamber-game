﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Plumber_Game
{
    public partial class LevelCreator : Form
    {
        static List<Image> TilesIcons = PlumberGame.TilesIcons;

        private int selectedTileId = 1;

        Character character = new Character();

        public LevelCreator()
        {
            InitializeComponent();
        }

        private void LevelCreator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            character.Hide();
            new Menu().Show();
            this.Hide();
        }

        private void FandeMouseWeel(object sender, MouseEventArgs e)
        {
            selectedTileId += e.Delta / Math.Abs(e.Delta);
            menuSwaper();
        }



        private void buttonUp_Click(object sender, EventArgs e)
        {

            selectedTileId += 1;
            menuSwaper();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            selectedTileId -= 1;
            menuSwaper();
        }

        private void menuSwaper()
        {
            if (selectedTileId < 0)
                selectedTileId = 17;
            else if (selectedTileId > 17)
                selectedTileId = 0;
            pictureBoxSelectetTile.BackgroundImage = TilesIcons[selectedTileId];

            if (selectedTileId == 0)
                labelTileId.Text = $"RND";
            else
                labelTileId.Text = $"Id: {selectedTileId}";

            if (selectedTileId == 17)
            {
                pictureBoxNextTile.BackgroundImage = TilesIcons[1];
                pictureBoxBeforeTile.BackgroundImage = TilesIcons[selectedTileId - 1];
            }
            else if (selectedTileId == 0)
            {
                pictureBoxNextTile.BackgroundImage = TilesIcons[selectedTileId + 1];
                pictureBoxBeforeTile.BackgroundImage = TilesIcons[17];
            }
            else
            {
                pictureBoxNextTile.BackgroundImage = TilesIcons[selectedTileId + 1];
                pictureBoxBeforeTile.BackgroundImage = TilesIcons[selectedTileId - 1];
            }

        }

        private void FillWithEmpty()
        {
            for (int i = 0; i < tableLayoutPanelPlayground.Controls.Count; i++)
            {

                PictureBox tile = (PictureBox)tableLayoutPanelPlayground.Controls[tableLayoutPanelPlayground.Controls.Count - 1 - i];


                tile.Image = TilesIcons[11];
            }
        }

        private void FillEmptyWithRandom()
        {
            for (int i = 0; i < tableLayoutPanelPlayground.Controls.Count; i++)
            {

                PictureBox tile = (PictureBox)tableLayoutPanelPlayground.Controls[tableLayoutPanelPlayground.Controls.Count - 1 - i];

                if (tile.Image == TilesIcons[11])
                    tile.Image = TilesIcons[0];
            }
        }

        private void ValidateLevel()
        {
            int exitsAmount = 0;
            int[] level = new int[tableLayoutPanelPlayground.Controls.Count];

            for (int i = 0; i < tableLayoutPanelPlayground.Controls.Count; i++)
            {

                PictureBox tile = (PictureBox)tableLayoutPanelPlayground.Controls[tableLayoutPanelPlayground.Controls.Count - 1 - i];

                for (byte j = 0; j < TilesIcons.Count; j++)
                {
                    if (tile.Image == TilesIcons[j])
                        level[i] = j;

                }

                if (tile.Image == TilesIcons[1] || tile.Image == TilesIcons[2] || tile.Image == TilesIcons[3] || tile.Image == TilesIcons[4])
                    exitsAmount++;

            }

            if (exitsAmount < 2)
                MessageBox.Show("Не достаточно выходов ! Их должно быть 2 !");
            else if (exitsAmount > 2)
                MessageBox.Show("Много выходов выходов ! Их должно быть 2 !");
            else
            {
                try
                {
                    Levels.AddLevel(new Level(textBoxLevelName.Text, level));
                    character.ChangeCharPosition(2);
                    MessageBox.Show($"Уровень был успешно сошранен !", "Успех !");
                    character.ChangeCharPosition(3);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    private void LevelCreator_Load(object sender, EventArgs e)
    {
        this.MouseWheel += FandeMouseWeel;
        FillWithEmpty();
        pictureBoxNextTile.BackgroundImage = TilesIcons[selectedTileId + 1];
        pictureBoxSelectetTile.BackgroundImage = TilesIcons[selectedTileId];
        pictureBoxBeforeTile.BackgroundImage = TilesIcons[17];

        character.Show();
        character.Attach(this.Location, this.Width);
        character.ChangeCharPosition(3);
    }


    private void buttonClearPlayground_Click(object sender, EventArgs e)
    {
        FillWithEmpty();
    }

    private void buttonSaveLevel_Click(object sender, EventArgs e)
    {
        ValidateLevel();
    }

    private void pictureBoxTile_Click(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            ((PictureBox)sender).Image = TilesIcons[selectedTileId];
        else if (e.Button == MouseButtons.Right)
            ((PictureBox)sender).Image = TilesIcons[11];
    }

    private void label6_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Спасибо Владу Мурсалову за подсказку !", "Пасибос !", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LevelCreator_LocationChanged(object sender, EventArgs e)
    {
        character.Attach(this.Location, this.Width);
    }

    private void buttonFillRandom_Click(object sender, EventArgs e)
    {
        FillEmptyWithRandom();
    }
}
}
