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

            idValue.Text = inventory.partIDGenerator().ToString();
        }

        //Constructor called when modifying a part
        public partScreen(Part part) // takes a part as input (Origin: mainScreen.partsModifyButton_Click)
        {
            partScreen modifyPartScreen = new partScreen(); //Inizializeing new Parts Screen

            partsPageSetup(part, modifyPartScreen); //Function that sets up the Partscreen with data from the passed in part

            modifyPartScreen.Show(); //Displays the screen

        }

        private void inhouseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            machineIDLabel.Show();
            machineIDValue.Show();

            companyNameLabel.Hide();
            companyNameValue.Hide();
        }

        private void outsourcedRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            //Hides Machine Lable and Value
            machineIDLabel.Hide();
            machineIDValue.Hide();

            companyNameLabel.Show();
            companyNameValue.Show();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (inhouseRadioButton.Checked == true) 
            {

                Inhouse inhouse = new Inhouse(int.Parse(idValue.Text), nameValue.Text, decimal.Parse(priceCostValue.Text), Int32.Parse(inventoryValue.Text), Int32.Parse(minValue.Text), Int32.Parse(maxValue.Text), Int32.Parse(machineIDValue.Text));

                if (inventoryLogic(inhouse) == 1)
                {
                    inventory.updatePart(inhouse.PartID, inhouse);
                    this.Close();
                }
                else if (inventoryLogic(inhouse) == 2) 
                { 
                inventory.addPart(inhouse);
                this.Close();
                }
                else if (inventoryLogic(inhouse) == 3)
                {
                    MessageBox.Show("Inventory Below Min Values");
                }
                else if (inventoryLogic(inhouse) == 4)
                {
                    MessageBox.Show("Inventory Above Max Values");
                }
                else if (inventoryLogic(inhouse) == 5)
                {
                    MessageBox.Show("Check Price");
                }
                else if (inventoryLogic(inhouse) == 99)
                {
                    MessageBox.Show("Fields Blank");
                }
                else
                {
                    MessageBox.Show("Error");
                }

            }
            else if (outsourcedRadioButton.Checked == true)
            {
                Outsourced outsourced = new Outsourced(int.Parse(idValue.Text), nameValue.Text, decimal.Parse(priceCostValue.Text), Int32.Parse(inventoryValue.Text), Int32.Parse(minValue.Text), Int32.Parse(maxValue.Text), companyNameValue.Text);

                if (inventoryLogic(outsourced) == 1)
                {
                    inventory.updatePart(outsourced.PartID, outsourced);
                    this.Close();
                }
                else if (inventoryLogic(outsourced) == 2)
                {
                    inventory.addPart(outsourced);
                    this.Close();
                }
                else if (inventoryLogic(outsourced) == 3)
                {
                    MessageBox.Show("Inventory Below Min Values");
                }
                else if (inventoryLogic(outsourced) == 4)
                {
                    MessageBox.Show("Inventory Above Max Values");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            
        }

        //closes Add Part screen
        private void cancelButton_Click(object sender, EventArgs e)
    {
            this.Close();
        }

        private int inventoryLogic(Part part)
        {
            if (inventory.checkExistence(part) == true)
            {
                return 1;
            }
            else if (part.InStock <= part.Max && part.InStock >= part.Min)
            {
                return 2;
            }
            else if (part.InStock < part.Min)
            {
                return 3;
            }
            else if (part.InStock > part.Max)
            {
                return 4;
            }
            else if (decimal.TryParse(priceCostValue.Text, out decimal parsedValue)){
                return 5;
            }
            else if (nameValue.Text == null)
            {
                return 99;
            }
            else
            {
                return 0;
            }
        }

        //function for restricting key presses to digits and backspace
        private void keyPress_DigitBackspace(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar; //variable "ch" for storing keypress

            if (!Char.IsDigit(ch) && ch != 8) //first checks if "ch" is a digit, then checks that key press
            {                                //is equal to backspace enumeration
                e.Handled = true;
            }
        }

        //restrics input to numbers and backspace
        private void inventoryValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress_DigitBackspace(sender, e);
        }

        //restrics input to numbers and backspace
        private void machineIDValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress_DigitBackspace(sender, e);
        }

        //restrics input to numbers and backspace
        private void maxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress_DigitBackspace(sender, e);
        }

        //restrics input to numbers and backspace
        private void minValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress_DigitBackspace(sender, e);
        }

        //restrics input to numbers, "." and backspace
        private void priceCostValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar; //variable "ch" for storing keypress
            int i = 1;

            if (!Char.IsDigit(ch) && ch != 8) //first checks if "ch" is a digit, then checks to see ifthe 
            {                                //key press was the symbol '.' and finally it checks to see if
                e.Handled = true;           //the key pressed was equal to backspace's enumeration
            }
            
            while (i == 1)
            {
                if (Char.IsSymbol(ch))
                {
                    e.Handled = true;
                    i--;
                }
            }


        }


        /// <summary>
        /// Used to set up Parts Page when modifying an existing part
        /// </summary>
        /// <param name="part"> Part object to be modified </param>
        /// <param name="partScreen"> Screen object, used to interact with form elements </param>
        private void partsPageSetup(Part part, partScreen partScreen)
        {
            if (part is Inhouse) //checks to see if the part is Inhouse
            {
                //sets Inhouse Radio Button to true
                partScreen.inhouseRadioButton.Checked = true;

                //shows Machine ID Label/Value and hides Company Name Label/Value
                partScreen.machineIDLabel.Show();
                partScreen.machineIDValue.Show();
                partScreen.modifyPartLabel.Show();
                partScreen.companyNameLabel.Hide();
                partScreen.companyNameValue.Hide();
                partScreen.addPartLabel.Hide();

                Inhouse inhouse = part as Inhouse; //sets passed in part as an Inhouse object
                partScreen.machineIDValue.Text = inhouse.MachineID.ToString(); //used that object to pass in MachineID
            }
            else if (part is Outsourced) //checks to see if the part is Outsourced
            {
                //sets Outsourced Radio Button to true
                partScreen.outsourcedRadioButton.Checked = true;

                //shows Machine ID Label/Value and hides Company Name Label/Value
                partScreen.companyNameLabel.Show();
                partScreen.companyNameValue.Show();
                partScreen.modifyPartLabel.Show();
                partScreen.machineIDLabel.Hide();
                partScreen.machineIDValue.Hide();
                partScreen.addPartLabel.Hide();

                Outsourced outsourced = part as Outsourced; //sets passed in part as an Outsourced object
                partScreen.companyNameValue.Text = outsourced.CompanyName.ToString(); //used that object to pass in company name value
            }

            //sets remaining data from the passed in part object
            partScreen.idValue.Text = part.PartID.ToString();
            partScreen.nameValue.Text = part.Name.ToString();
            partScreen.inventoryValue.Text = part.InStock.ToString();
            partScreen.priceCostValue.Text = part.Price.ToString();
            partScreen.maxValue.Text = part.Max.ToString();
            partScreen.minValue.Text = part.Min.ToString();
        }
    }
    }
