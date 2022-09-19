using MatthewEvans___BFM1___Software_I___C968.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MatthewEvans___BFM1___Software_I___C968
{
    public partial class partScreen : Form
    {
        //Initialize instance of Inventory class
        Inventory inventory = new Inventory();

        //Defualt Constructor
        public partScreen()
        {
            InitializeComponent();

            //partScreenSetup();

            idValue.Text = inventory.partIDGenerator().ToString();
        }

        //Constructor called when modifying a part
        public partScreen(Part part) // takes a part as input (Origin: mainScreen.partsModifyButton_Click)
        {
            partScreen modifyPartScreen = new partScreen(); //Inizializeing new Parts Screen

            modifyPartsScreenSetup(part, modifyPartScreen); //Function that sets up the Partscreen with data from the passed in part

            modifyPartScreen.Show(); //Displays the screen

        }

        

        ////////////////
        /// Buttons ///
        ///////////////
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            inventoryLogicTest();
        }

        //closes Add Part screen
        private void cancelButton_Click(object sender, EventArgs e)
    {
            this.Close();
        }







        ////////////////
        //// Events ////
        ///////////////

        /// <summary>
        /// Event that handels what to do when the inhouse radio button is checked
        /// </summary>
        private void inhouseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            machineIDLabel.Show(); //Shows the Machine ID Label and Value text boxes
            machineIDValue.Show();

            companyNameLabel.Hide(); //Hides the Company Name Label and Value text boxes
            companyNameValue.Hide();
        }

        /// <summary>
        /// Event that handels what to do when the outsourced radio button is checked
        /// </summary>
        private void outsourcedRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            //Hides Machine Lable and Value
            machineIDLabel.Hide(); //Hides the Machine ID Label and Value text boxes
            machineIDValue.Hide();

            companyNameLabel.Show(); //Shows the Company Name Label and Value text boxes
            companyNameValue.Show();
        }

        /// <summary>
        /// Event for conroling input to only Digits and Backspace
        /// </summary>
        private void keyPress_DigitBackspace(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar; //variable "ch" for storing keypress

            if (!Char.IsDigit(ch) && ch != 8) //first checks if "ch" is a digit, then checks that key press
            {                                //is equal to backspace enumeration
                e.Handled = true;
            }
        }

        /// <summary>
        /// WORK IN PROGRESS
        /// </summary>
        private void priceCostValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char ch = e.KeyChar; //variable "ch" for storing keypress
            //int i = 1;

            //if (!Char.IsDigit(ch) && ch != 8) //first checks if "ch" is a digit, then checks to see if the 
            //{                                //key press was the symbol '.' and finally it checks to see if
            //    e.Handled = true;           //the key pressed was equal to backspace's enumeration
            //}

            //while (i == 1)
            //{
            //    if (ch != 70)
            //    {
            //        e.Handled = true;
            //        i--;
            //    }
            //}
        }

        ////////////////
        //// Misc. ////
        ///////////////

        ///// <summary>
        ///// Inventory Logic Orginal
        ///// </summary>
        ///// <param name="part"></param>
        ///// <returns></returns>
        //private int inventoryLogic(Part part)
        //{
        //    if (inventory.checkExistence(part) == true)
        //    {
        //        return 1;
        //    }
        //    else if (part.InStock <= part.Max && part.InStock >= part.Min)
        //    {
        //        return 2;
        //    }
        //    else if (part.InStock < part.Min)
        //    {
        //        return 3;
        //    }
        //    else if (part.InStock > part.Max)
        //    {
        //        return 4;
        //    }
        //    else if (decimal.TryParse(priceCostValue.Text, out decimal parsedValue))
        //    {
        //        return 5;
        //    }
        //    else if (nameValue.Text == null)
        //    {
        //        return 99;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        private void inventoryLogicTest()
        {
            if (inhouseRadioButton.Checked == true)
            {

                Inhouse inhouse = new Inhouse(int.Parse(idValue.Text), nameValue.Text, decimal.Parse(priceCostValue.Text), Int32.Parse(inventoryValue.Text), Int32.Parse(minValue.Text), Int32.Parse(maxValue.Text), Int32.Parse(machineIDValue.Text));

                if (inventory.checkExistence(inhouse) == true)
                {
                    if (inventory.inventoryLogic(inhouse) == true)
                    {
                        inventory.updatePart(inhouse.PartID, inhouse);
                        this.Close();
                    }
                }
                else if (inventory.inventoryLogic(inhouse) == true)
                {
                    inventory.addPart(inhouse);
                    this.Close();
                }

            }
            else if (outsourcedRadioButton.Checked == true)
            {
                Outsourced outsourced = new Outsourced(int.Parse(idValue.Text), nameValue.Text, decimal.Parse(priceCostValue.Text), Int32.Parse(inventoryValue.Text), Int32.Parse(minValue.Text), Int32.Parse(maxValue.Text), companyNameValue.Text);

                if (inventory.checkExistence(outsourced) == true)
                {
                    if (inventory.inventoryLogic(outsourced) == true)
                    {
                        inventory.updatePart(outsourced.PartID, outsourced);
                        this.Close();
                    }
                }
                else if (inventory.inventoryLogic(outsourced) == true)
                {
                    inventory.addPart(outsourced);
                    this.Close();
                }
            }
        }


        /// <summary>
        /// Defualt page setup for Parts Screen
        /// </summary>
        private void partScreenSetup()
        {
            saveButton.Enabled = false;
        }

        /// <summary>
        /// Used to set up Parts Page when modifying an existing part
        /// </summary>
        /// <param name="part"> Part object to be modified </param>
        /// <param name="partScreen"> Screen object, used to interact with form elements </param>
        private void modifyPartsScreenSetup(Part part, partScreen modifyPartScreen)
        {
            if (part is Inhouse) //checks to see if the part is Inhouse
            {
                //sets Inhouse Radio Button to true
                modifyPartScreen.inhouseRadioButton.Checked = true;

                //shows Machine ID Label/Value and hides Company Name Label/Value
                modifyPartScreen.machineIDLabel.Show();
                modifyPartScreen.machineIDValue.Show();
                modifyPartScreen.modifyPartLabel.Show();
                modifyPartScreen.companyNameLabel.Hide();
                modifyPartScreen.companyNameValue.Hide();
                modifyPartScreen.addPartLabel.Hide();

                Inhouse inhouse = part as Inhouse; //sets passed in part as an Inhouse object
                modifyPartScreen.machineIDValue.Text = inhouse.MachineID.ToString(); //used that object to pass in MachineID
            }
            else if (part is Outsourced) //checks to see if the part is Outsourced
            {
                //sets Outsourced Radio Button to true
                modifyPartScreen.outsourcedRadioButton.Checked = true;

                //shows Machine ID Label/Value and hides Company Name Label/Value
                modifyPartScreen.companyNameLabel.Show();
                modifyPartScreen.companyNameValue.Show();
                modifyPartScreen.modifyPartLabel.Show();
                modifyPartScreen.machineIDLabel.Hide();
                modifyPartScreen.machineIDValue.Hide();
                modifyPartScreen.addPartLabel.Hide();

                Outsourced outsourced = part as Outsourced; //sets passed in part as an Outsourced object
                modifyPartScreen.companyNameValue.Text = outsourced.CompanyName.ToString(); //used that object to pass in company name value
            }

            //sets remaining data from the passed in part object
            modifyPartScreen.idValue.Text = part.PartID.ToString();
            modifyPartScreen.nameValue.Text = part.Name.ToString();
            modifyPartScreen.inventoryValue.Text = part.InStock.ToString();
            modifyPartScreen.priceCostValue.Text = part.Price.ToString();
            modifyPartScreen.maxValue.Text = part.Max.ToString();
            modifyPartScreen.minValue.Text = part.Min.ToString();
        }
    }
}
