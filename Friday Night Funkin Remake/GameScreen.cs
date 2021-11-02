using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Permissions;
using System.Media;

namespace Friday_Night_Funkin_Remake
{
    public partial class GameScreen : UserControl
    {

        #region global variables
        //arrow variables
        int X, Y, arrowSpace, SIZE = 75, SPEED = 8;
        string arrowNum, space = "1";

        //score variables
        int lifePoints = 80, gainedPoints = 0, counter = 0;

        //2D list - list of lists
        List<List<Arrow>> arrows = new List<List<Arrow>>();
        List<List<Rectangle>> arrowRec = new List<List<Rectangle>>();

        List<Arrow> greyArrows = new List<Arrow>();
        List<Rectangle> greyRec = new List<Rectangle>();

        //key press booleans
        Boolean upDown, downDown, rightDown, leftDown, wDown, aDown, sDown, dDown, escapeDown;

        

        Image lifeBar = Properties.Resources.bar0;
        #endregion

        public GameScreen()
        {
            InitializeComponent();
            Focus();

            ArrowInitialization();
        }
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Escape:
                    escapeDown = true;
                    break;
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upDown = false;
                    Console.WriteLine("KeyUp");
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
                case Keys.Escape:
                    escapeDown = false;
                    break;
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region arrow movement
            //move each arrow
            for (int i = 0; i < arrows.Count(); i++)
            {
                for (int j = 0; j < arrows[i].Count(); j++)
                {
                    arrows[i][j].y -= SPEED;
                }
            }

            //updating the rectangles
            for (int i = 0; i < arrows.Count(); i++)
            {
                arrowRec[i].Clear();

                for (int j = 0; j < arrows[i].Count(); j++)
                {
                    Rectangle tempRec = new Rectangle(arrows[i][j].x, arrows[i][j].y, SIZE, SIZE);
                    arrowRec[i].Add(tempRec);
                }

            }

            //removes arrows that go off the screen 
            for (int i = 0; i < arrows.Count(); i++)
            {
                if (arrows[i].Count() > 0 && arrows[i][0].y + SIZE < 0)
                {
                    arrows[i].RemoveAt(0);
                }
            }
            #endregion

            Collisions();

            //TODO - bring up pause screen *
            if (escapeDown == true)
            {
                
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {

            #region drawing grey arrows
            for (int i = 0; i < greyArrows.Count(); i++)
            {
                e.Graphics.DrawImage(greyArrows[i].getImage(), greyArrows[i].x, greyArrows[i].y, SIZE, SIZE);
            }
            #endregion

            #region drawing regular arrows
            for (int i = 0; i < arrows.Count(); i++)
            {
                for (int j = 0; j < arrows[i].Count(); j++)
                {
                    //drawing the arrows
                    e.Graphics.DrawImage(arrows[i][j].getImage(), arrows[i][j].x, arrows[i][j].y, SIZE, SIZE);
                }
            }
            #endregion

            //drawing life bar
            e.Graphics.DrawImage(lifeBar, 20, this.Height - 200, this.Width - 50, 250);

            #region drawing rectangles for testing
            Pen pen = new Pen(Color.Red);
            for (int i = 0; i < arrowRec.Count(); i++)
            {
                for (int j = 0; j < arrowRec[i].Count(); j++)
                {
                    e.Graphics.DrawRectangle(pen, arrowRec[i][j]);
                }
            }

            for (int i = 0; i < greyRec.Count(); i++)
            {
                e.Graphics.DrawRectangle(pen, greyRec[i]);
            }
            #endregion
        }

        public void ArrowInitialization()
        {
            //Arrow list initialization
            X = this.Width/2;
            Y = this.Height + SIZE;

            #region filling the moving arrow lists
            //makes 4 lists in my 2D arrow list and arrow rectangles(for the 4 arrow directions)
            for (int i = 3; i >= 0; i--)
            {
                arrows.Add(new List<Arrow>());
                arrowRec.Add(new List<Rectangle>());
            }

            int arrowCount = 1;
            //pulling the arrow information from an xml
            XmlReader reader = XmlReader.Create("Resources/arrows.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    //getting values from xml and setting them to variables
                    arrowNum = reader.ReadString();
                    reader.ReadToNextSibling("space");
                    space = reader.ReadString();

                    arrowSpace = Convert.ToInt32(space);

                    //assigning the different arrows to their specific lists
                    switch (arrowNum)
                    {
                        case "0":
                            arrows[0].Add(new Arrow(X-(SIZE*2+50), Y + (arrowCount*arrowSpace), 0, Properties.Resources.arrow0));
                            arrowCount++;
                            break;
                        case "1":
                            arrows[1].Add(new Arrow(X-(SIZE), Y + (arrowCount*arrowSpace), 1, Properties.Resources.arrow1));
                            arrowCount++;
                            break;
                        case "2":
                            arrows[2].Add(new Arrow(X+(SIZE), Y + (arrowCount*arrowSpace), 2, Properties.Resources.arrow2));
                            arrowCount++;
                            break;
                        case "3":
                            arrows[3].Add(new Arrow(X+(SIZE*2+50), Y + (arrowCount*arrowSpace), 3, Properties.Resources.arrow3));
                            arrowCount++;
                            break;
                    }
                }
            }
            #endregion

            #region grey arrows
            //filling the grey arrows list 
            greyArrows.Add(new Arrow(X-(SIZE*2+50), 10, 0, Properties.Resources.arrow0G));
            greyArrows.Add(new Arrow(X -(SIZE), 10, 0, Properties.Resources.arrow1G));
            greyArrows.Add(new Arrow(X + (SIZE), 10, SIZE, Properties.Resources.arrow2G));
            greyArrows.Add(new Arrow(X + (SIZE*2+50), 10, SIZE, Properties.Resources.arrow3G));

            //setting up the grey arrow rectangles
            for (int i = 0; i < greyArrows.Count(); i++)
            {
                greyRec.Add(new Rectangle(greyArrows[i].x, greyArrows[i].y, SIZE, SIZE));
            }
            #endregion
        }

        public void Collisions()
        {
            SoundPlayer errorSound = new SoundPlayer(Properties.Resources.FNFerrorSound);


            if (leftDown == true||aDown == true)
            {
                for (int i = 0; i < arrows[0].Count(); i++)
                {
                    if (arrowRec[0][i].IntersectsWith(greyRec[0]))
                    {
                        //add points, remove arrow, reset counter
                        gainedPoints += 100;
                        arrows[0][i].setImage(Properties.Resources.arrow0W);
                        arrows[0].RemoveAt(i);
                        
                    }
                    else if (counter >= 10)
                    {
                        //lose life, reset counter to 0
                        lifePoints -= 5;
                        counter = 0;
                        errorSound.Play();
                        LifeBarDecision();
                    }
                    else
                    {

                    }
                }
            }
            if (upDown == true||wDown == true)
            {
                for (int i = 0; i < arrows[1].Count(); i++)
                {
                    int test = 0;
                    if (arrowRec[1][i].IntersectsWith(greyRec[1]))
                    {
                        gainedPoints += 100;
                        arrows[1][i].setImage(Properties.Resources.arrow1W);
                        arrows[1].RemoveAt(i);
                    }
                    else if (counter >= 10)
                    {
                        lifePoints -= 5;
                        errorSound.Play();
                        LifeBarDecision();
                        counter = 0;
                    }
                }
            }
            if (downDown == true|| sDown == true)
            {
                for (int i = 0; i < arrows[2].Count(); i++)
                {
                    if (arrowRec[2][i].IntersectsWith(greyRec[2]))
                    {
                        gainedPoints += 100;
                        arrows[2][i].setImage(Properties.Resources.arrow2W);
                        arrows[2].RemoveAt(i);
                    }
                    else if (counter >= 10)
                    {
                        lifePoints -= 5;
                        counter = 0;
                        errorSound.Play();
                        LifeBarDecision();
                    }
                }
            }
            if (rightDown == true||dDown == true)
            {
                for (int i = 0; i < arrows[3].Count(); i++)
                {
                    if (arrowRec[3][i].IntersectsWith(greyRec[3]))
                    {
                        gainedPoints += 100;
                        arrows[3][i].setImage(Properties.Resources.arrow3W);
                        arrows[3].RemoveAt(i);
                    }
                    else if (counter >= 10)
                    {
                        lifePoints -= 5;
                        counter = 0;
                        errorSound.Play();
                        LifeBarDecision();
                        
                    }
                }
            }
            counter++;
            //shows points for testing purposes
            testLabel.Text = "G:" + Convert.ToString(gainedPoints) + "       L:" + Convert.ToString(lifePoints);
        }

        public void LifeBarDecision()
        {
            //chaging the life bar image based on how many points you have/gameover
            switch (lifePoints)
            {
                case 70:
                    lifeBar = Properties.Resources.bar1;
                    break;
                case 60:
                    lifeBar = Properties.Resources.bar2;
                    break;
                case 50:
                    lifeBar = Properties.Resources.bar3;
                    break;
                case 40:
                    lifeBar = Properties.Resources.bar4;
                    break;
                case 30:
                    lifeBar = Properties.Resources.bar5;
                    break;
                case 20:
                    lifeBar = Properties.Resources.bar6;
                    break;
                case 10:
                    lifeBar = Properties.Resources.bar7;
                    break;
                case 0:
                    GameOverScreen gos = new GameOverScreen();
                    GameScreen gs = new GameScreen();
                    Form form = this.FindForm();
                    form.Controls.Add(gos);
                    form.Controls.Remove(this);
                    form.Controls.Remove(gs);
                    gos.Focus();
                    break;
                default:
                    break;
            }
        }
    }
}
