using System.Collections.Generic;
using System.Windows.Forms;
using static SherlockCluedo.Form1.Direction;

namespace SherlockCluedo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        // Start the show!
        private void button1_Click(object sender, System.EventArgs e)
        {
            FindTheBadGuy();
        }

        // Outline the Detectives. Yes the dog is a detective.
        public class Detective
        {
            public int CurrentSuspectIndex;

            public void Move(Direction direction)
            {
                if (direction == Clockwise)
                {
                    CurrentSuspectIndex++;
                    if (CurrentSuspectIndex == 6)
                        CurrentSuspectIndex = 0;
                }
                else
                {
                    CurrentSuspectIndex--;
                    if (CurrentSuspectIndex == -1)
                        CurrentSuspectIndex = 5;
                }
            }
        }

        // Where the magic (predominantly) happens.
        public void FindTheBadGuy()
        {
            // Set the stage..
            var suspects = new List<string>()
            {
                "Mustard",
                "Plum",
                "Green",
                "Peacock",
                "Scarlett",
                "White"
            };

            var badGuyIdentified = false;
            var timeElapsed = 0;

            var sherlock = new Detective()
            {
                CurrentSuspectIndex = 0  // Mustard
            };

            var watson = new Detective()
            {
                CurrentSuspectIndex  = 5 // White
            };

            var wellington = new Detective()
            {
                CurrentSuspectIndex  = 0 // Mustard
            };

            // AND GO!
            while (!badGuyIdentified)
            {
                timeElapsed += 5;

                // Assuming Sherlock takes precendent here,
                // Because Benedit Cumberbatch has way more screen
                // presence that Martin Freeman

                if (timeElapsed % 15 == 0)
                {
                    sherlock.Move(Clockwise);

                    // See if Sherlock has walked into the room with Watson and Wellington
                    if (sherlock.CurrentSuspectIndex == watson.CurrentSuspectIndex && watson.CurrentSuspectIndex == wellington.CurrentSuspectIndex)
                    {
                        badGuyIdentified = true;
                    }

                    // If it's just Watson then, move along!
                    else if (sherlock.CurrentSuspectIndex == watson.CurrentSuspectIndex)
                    {
                        sherlock.Move(Clockwise);
                    }
                }

                if (timeElapsed % 20 == 0)
                {
                    watson.Move(Anticlockwise);

                    // Do not interrupt Benedict Cumberbatch!
                    if (watson.CurrentSuspectIndex == sherlock.CurrentSuspectIndex)
                    {
                        watson.Move(Anticlockwise);
                    }
                }

                if (timeElapsed % 30 == 0)
                {
                    wellington.Move(Anticlockwise);
                }

                if (!badGuyIdentified) continue;

                // If we get here, that means the bad guy has been identified.
                var name = suspects[sherlock.CurrentSuspectIndex];
                var message = "Sherlock deduced that " + name + " was the perpetrator after " + timeElapsed +
                              " minutes, the clever sausage.";

                // The big reveal!
                MessageBox.Show(message);
            }
            
        }

        // Used an enumerator for the sake of readability. Some people like it, some people don't, I'm easy
        // either way!
        public enum Direction
        {
            Clockwise,
            Anticlockwise
        }

    }
}
