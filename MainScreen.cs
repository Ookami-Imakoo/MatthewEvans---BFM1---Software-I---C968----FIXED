using MatthewEvans___BFM1___Software_I___C968.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatthewEvans___BFM1___Software_I___C968
{
    public partial class mainScreen : Form
    {
        //Initialize instance of Inventory class
        Inventory inventory = new Inventory();
        Product product = new Product();

        //Value used to setup data only once
        int setupData = 0; // 0: Setup Test Data || 1: Dont Setup Test Data

        public mainScreen()
        {
            InitializeComponent();

            //sets properties for MainScreen datagridviews
            MainScreenSetup();

            //sets up sampel data for debug
            SetupSampelData();
        }

        //clears the autoselection of rows
        private void partsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            partsDataGridView.ClearSelection();
            productsDataGridView.ClearSelection();
        }

        ////////////////
        /// Buttons ///
        ///////////////

        //Parts - ADD Button
        //opens a blank add part screen
        private void partsAddButton_Click(object sender, EventArgs e)
        {
            partScreen addPartScreen = new partScreen();
            addPartScreen.ShowDialog();
        }

        //Parts - MODIFY Button
        //opens and populates modify parts screen
        private void partsModifyButton_Click(object sender, EventArgs e)
        {
            if (partsDataGridView.CurrentRow.DataBoundItem is Inhouse) //checks to see if highlighted part is Inhouse
            {
                Inhouse selectedInhouse = partsDataGridView.CurrentRow.DataBoundItem as Inhouse; //stores selection in Inhouse object
                partScreen modifyPartScreen = new partScreen(selectedInhouse); //opens the partScreen useing the data from selectedInhouse to populate the screen
            }
            else
            {
                Outsourced selectedOutsourced = partsDataGridView.CurrentRow.DataBoundItem as Outsourced; //stores selection in Outsourced object
                partScreen modifyPartScreen = new partScreen(selectedOutsourced); //opens the partScreen useing the data from selectedOutsourced to populate the screen
            }
        }

        //Parts - DELETE Button
        //button used to delete selected part
        public void partsDeleteButton_Click(object sender, EventArgs e)
        {
            if (partsDataGridView.CurrentRow == null || !partsDataGridView.CurrentRow.Selected) //checks if the current row is empty or if there is no selection
            {
                MessageBox.Show("Nothing Selected!", "Please Make A Slection");
                return;
            }

            Part deletepart = partsDataGridView.CurrentRow.DataBoundItem as Part; //storeing selection in Part veriable
            inventory.deletePart(deletepart); //deleteing selecting from bindinglist
        }

        //Products - ADD Button
        //opens a blank add product screen
        private void productsAddButton_Click(object sender, EventArgs e)
        {
            productScreen addProduct = new productScreen();
            addProduct.Show();
        }

        //Products - MODIFY Button
        //opens and populates productScreen
        private void productsModifyButton_Click(object sender, EventArgs e)
        {
            if (productsDataGridView.CurrentRow == null || !productsDataGridView.CurrentRow.Selected)
            {
                MessageBox.Show("Nothing Selected!", "Please Make A Selection");
                return;
            }

            Product selectedProduct = productsDataGridView.CurrentRow.DataBoundItem as Product;
            productScreen modifyProduct = new productScreen(selectedProduct);
        }

        //Products - DELETE Button
        //button used to delete selected product
        private void productsDeleteButton_Click(object sender, EventArgs e)
        {
            if (productsDataGridView.CurrentRow == null || !productsDataGridView.CurrentRow.Selected)
            {
                MessageBox.Show("Nothing Selected!", "Please Make A Selection");
                return;
            }

            Product deleteproduct = productsDataGridView.CurrentRow.DataBoundItem as Product;
            inventory.removeProduct(deleteproduct);

        }

        //Parts - Search Button
        //takes input from search text box and returns a message if found or not
        private void partsSearchButton_Click(object sender, EventArgs e)
        {
            String searchInput = partsSearchValue.Text;

            if(Regex.IsMatch(searchInput, @"^\d+$"))
            {
                inventory.lookupPart(Int32.Parse(searchInput));
            }
            else
            {
                inventory.lookupPart(searchInput);
            }

        }

        //Product - Search Button
        //takes input from search text box and returns a message if found or not
        private void productsSearchButton_Click(object sender, EventArgs e)
        {
            String searchInput = productsSearchValue.Text;

            if (Regex.IsMatch(searchInput, @"^\d+$"))
            {
                inventory.lookupProudct(Int32.Parse(searchInput));
            }
            else
            {
                inventory.lookupProudct(searchInput);
            }
        }

        //MainScreen - EXIT Button
        //closes application
        private void exitButton_Click(object sender, EventArgs e)
    {
            this.Close();
        }


        ///////////////
        //// Misc ////
        //////////////

        //Settings for main screen form
        private void MainScreenSetup()
        {
            //set data source for Parts datagrid
            partsDataGridView.DataSource = Inventory.AllParts;
            productsDataGridView.DataSource = Inventory.Products;

            //Remove bottom row from datagrids
            partsDataGridView.AllowUserToAddRows = false;
            productsDataGridView.AllowUserToAddRows = false;
        }

        //Sample Data
        private void SetupSampelData()
        {
            if (setupData < 1)
            {
                //Part Sample Data
                inventory.addPart(new Inhouse(1, "Rear Weight Bracket", 67.41m, 1, 1, 20, 8675309));
                inventory.addPart(new Inhouse(2, "Magnetic Hitch Pin", 18.99m, 5, 1, 10, 8675309));
                inventory.addPart(new Inhouse(3, "Strorage Compartment Cover", 7.46m, 10, 10, 99, 8675309));
                inventory.addPart(new Inhouse(4, "3/4 in. Push-to-Connect Brass Ball Valve", 26.97m, 22, 5, 100, 8675309));
                inventory.addPart(new Inhouse(5, "1/2 in. FIP x MHT Bras Flanged Sillcock Valve", 7.78m, 11, 5, 100, 8675309));
                inventory.addPart(new Outsourced(6, "Rear Weight Bracket", 67.41m, 1, 1, 20, "Sanford and Sons"));
                inventory.addPart(new Outsourced(7, "Magnetic Hitch Pin", 18.99m, 5, 1, 10, "John Deer"));
                inventory.addPart(new Outsourced(8, "Strorage Compartment Cover", 7.46m, 10, 10, 99, "John Deer"));
                inventory.addPart(new Outsourced(9, "3/4 in. Push-to-Connect Brass Ball Valve", 26.97m, 22, 5, 100, "Yamaha"));
                inventory.addPart(new Outsourced(10, "1/2 in. FIP x MHT Bras Flanged Sillcock Valve", 7.78m, 11, 5, 100, "Yamaha"));

                //Product Sample Data
                inventory.addProduct(new Product(1, "Riding Lawnmower", 6.99m, 2, 1, 10));
                inventory.addProduct(new Product(2, "40 Gal. 36,000 BTU Tank Water Heater", 519.00m, 2, 0, 2));
                inventory.addProduct(new Product(3, "Colorado 5-Light Black Modern Farmhouse Rectangular Chandelier", 353.00m, 0, 0, 5));

                setupData++;
            }
            
        }
    }
}
