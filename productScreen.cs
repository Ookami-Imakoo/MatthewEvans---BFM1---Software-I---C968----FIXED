using MatthewEvans___BFM1___Software_I___C968.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatthewEvans___BFM1___Software_I___C968
{
    public partial class productScreen : Form
    {
        ////////////////////////
        /// Global Veriables ///
        ///////////////////////
        
        Inventory inventory = new Inventory(); //initialzation of inventory object
        
        Product modifyProduct = new Product(); //used to store the product to be modifed

        public BindingList<Part> AssociatedPartsCopy = new BindingList<Part>(); //listed used to store associated parts list durring modification

        ////////////////////////
        //// Constoructors ////
        ///////////////////////

        /// <summary>
        /// Defult Constructor for Product Screen: Used when creating a new product
        /// </summary>
        public productScreen()
        {
            InitializeComponent();
            
            productScreenSetup(); //sets up screen

            idValue.Text = inventory.productIDGenerator().ToString(); //generates a unique idValue for new Products and sets it in textbox
        }

        /// <summary>
        /// Constructor used when modifying a product.
        /// </summary>
        /// <param name="product"> Product to be modifed -- passed in from MainScreen productsModifyButton_Click </param>
        public productScreen(Product product)
        {
            InitializeComponent();

            productScreenSetup(); //sets up screen

            modifyProductScreenSetup(product); //sets data useing product as an argument
        }
        
        ////////////////
        /// Buttons ///
        ///////////////

        // ADD - All Candidate Parts Button (Function: adds selected part to AssociatedParts list)
        private void allCandidateAddButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part; //uses selection to create part object

            modifyProduct.addAssociatedPart(part); //adds part object to Associated Parts List
            partsAssociatedDataGridView.DataSource = modifyProduct.AssociatedParts; //displays AssociatedParts list of new product
        }

        // Delete - AssociatedParts Button (Function: removes parts from the AssociatedParts list, but not from the AllParts list)
        private void partsAssociatedDeleteButton_Click(object sender, EventArgs e)
        {
            Product modifyProductCopy = new Product(modifyProduct); //copy

            if (partsAssociatedDataGridView.CurrentRow == null || !partsAssociatedDataGridView.CurrentRow.Selected) //checks if the associated parts list is null or if no row is currently selected
            {
                MessageBox.Show("No Part Selected, please select a part to be deleted."); //returns message if the above logic is true
                return;
            }
            Part part = partsAssociatedDataGridView.CurrentRow.DataBoundItem as Part; //sets part highlighted in AssociatedParts list as part object

            if (modifyProduct.AssociatedParts != null) //checks to see if the part is a modifed part or not
            {
                modifyProductCopy.removeAssoicatedPart(part.PartID); //removes part on the modifyPart.AssociatedParts
            }
            else
            {
                modifyProduct.removeAssoicatedPart(part.PartID); //removes part on the myProduct.AssociatedParts
            }
        }

        /// <summary>
        /// function used to save (new and modified) products 
        /// </summary>
        private void productSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (modifyProduct.ProductID == 0) //checks to make sure we are creating a new or existing entry (ture == new product)
                {
                    Product product = new Product
                    {
                        ProductID = int.Parse(idValue.Text),
                        Name = nameValue.Text,
                        InStock = int.Parse(inventoryValue.Text),         //creates new prodcut useing the data from forms textboxes
                        Price = decimal.Parse(priceCostValue.Text),
                        Max = int.Parse(maxValue.Text),
                        Min = int.Parse(minValue.Text),
                        AssociatedParts = partsAssociatedDataGridView.DataSource as BindingList<Part>
                    };

                    product.productValidation(product); //runs product validation to confirm product has good data

                }
                else
                {
                    modifyProduct.ProductID = int.Parse(idValue.Text);
                    modifyProduct.Name = nameValue.Text;
                    modifyProduct.InStock = int.Parse(inventoryValue.Text);    //updates preexisting product record
                    modifyProduct.Price = decimal.Parse(priceCostValue.Text);
                    modifyProduct.Max = int.Parse(maxValue.Text);
                    modifyProduct.Min = int.Parse(minValue.Text);

                    this.Close();
                }
            }
            catch (Exception) //exception captured incase bad data is able to be passed into a product creation, this error message should only tigger in a last case senerio
            {

                MessageBox.Show("An Error has occured, please check your data and try again.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void productCancelButton_Click(object sender, EventArgs e)
        {
            modifyProduct.AssociatedParts = AssociatedPartsCopy; //returns a copy of the Associated Parts list that was created when the parts screen was created (so pre any modifications by the end user)
            this.Close();
        }

        ///////////////
        //// Misc ////
        //////////////
 
        //clears inital selection on allCadidateDataGridView
        private void allCandidateDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            allCandidateDataGridView.ClearSelection();
        }

        //clears inital selection on partsAssociatedDataGridView
        private void partsAssociatedDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            partsAssociatedDataGridView.ClearSelection();
        }

        /// <summary>
        /// Method used to set up productScreen (defualt)
        /// </summary>
        private void productScreenSetup()
        {
            allCandidateDataGridView.DataSource = Inventory.AllParts; //sets data for All Candidate Parts DataGrid
        }

        /// <summary>
        /// Method used to setup productScreen (for modfiying existing prodcuts)
        /// </summary>
        /// <param name="prodcut"></param>
        private void modifyProductScreenSetup(Product prodcut)
        {
            modifyProduct = prodcut; //setting modifyProduct to the product used in constructor

            //sets data from the passed in modifyProduct object
            idValue.Text = modifyProduct.ProductID.ToString();
            nameValue.Text =
            inventoryValue.Text = modifyProduct.InStock.ToString();
            priceCostValue.Text = modifyProduct.Price.ToString();
            maxValue.Text = modifyProduct.Max.ToString();
            minValue.Text = modifyProduct.Min.ToString();
            partsAssociatedDataGridView.DataSource = modifyProduct.AssociatedParts; //Setting display to copy of ModifyProduct Associated Parts list

            if (prodcut.AssociatedParts != null) //checks to make sure the prodcut Associated Parts list is not empty
            {
                for (int i = 0; i < prodcut.AssociatedParts.Count; i++) //loops though Associated parts, and adds all parts to another list to be used if the product screen is closed before saving
                {
                    AssociatedPartsCopy.Add(prodcut.AssociatedParts[i]);
                }
            }
 
            addProductLabel.Text = "Modify Product"; //changes the screen lable text to indicate the product is being modified
        }
    }
}
