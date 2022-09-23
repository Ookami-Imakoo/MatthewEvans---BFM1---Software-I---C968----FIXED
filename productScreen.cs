﻿using MatthewEvans___BFM1___Software_I___C968.model;
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
        //initializations//
        Inventory inventory = new Inventory();
        Product myProduct = new Product();
        //////////////////

        Product modifyProduct = new Product(); //used to store the product to be modifed

        /// <summary>
        /// Defult Constructor for Product Screen: Used when creating a new product
        /// </summary>
        public productScreen()
        {
            InitializeComponent();
            
            //sets up screen
            addProductSetup();

            idValue.Text = inventory.productIDGenerator().ToString(); //generates a unique idValue for new Products and sets it in textbox
        }

        /// <summary>
        /// Constructor used when modifying a product.
        /// </summary>
        /// <param name="product"> Product to be modifed -- passed in from MainScreen productsModifyButton_Click </param>
        public productScreen(Product product)
        {
            InitializeComponent();
            
            //sets up screen
            addProductSetup();

            modifyProduct = product; //setting modifyProduct to the product used in constructor


            //sets data from the passed in modifyProduct object
            idValue.Text = modifyProduct.ProductID.ToString();
            nameValue.Text = 
            inventoryValue.Text = modifyProduct.InStock.ToString();
            priceCostValue.Text = modifyProduct.Price.ToString();
            maxValue.Text = modifyProduct.Max.ToString();
            minValue.Text = modifyProduct.Min.ToString();
            partsAssociatedDataGridView.DataSource = modifyProduct.AssociatedParts; //Setting display to copy of ModifyProduct Associated Parts list.
        }
        
        ////////////////
        /// Buttons ///
        ///////////////

        // ADD - All Candidate Parts Button (Function: adds selected part to AssociatedParts list)
        private void allCandidateAddButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part; //uses selection to create part object
            Product modifyProductCopy = new Product(modifyProduct); //copy


            if (modifyProduct.AssociatedParts == null) //checks to see if we are NOT modifying an existing entry
            {
                myProduct.addAssociatedPart(part); //adds part object to Associated Parts List
                partsAssociatedDataGridView.DataSource = myProduct.AssociatedParts; //displays AssociatedParts list of new product
            }
            else
            {
                modifyProduct.addAssociatedPart(part); //adds part object to Associated Parts List of modifiedProduct
                partsAssociatedDataGridView.DataSource = modifyProductCopy.AssociatedParts; //displays AssociatedParts list of modified product
            }
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
                myProduct.removeAssoicatedPart(part.PartID); //removes part on the myProduct.AssociatedParts
            }
        }

        //WORK IN PROGRESS
        private void productSaveButton_Click(object sender, EventArgs e)
        {
            if (modifyProduct.ProductID == 0) //checks to make sure we are not modifying an existing entry
            {
                Product product = new Product
                {
                    ProductID = int.Parse(idValue.Text),
                    Name = nameValue.Text,
                    InStock = int.Parse(inventoryValue.Text),
                    Price = decimal.Parse(priceCostValue.Text),
                    Max = int.Parse(maxValue.Text),
                    Min = int.Parse(minValue.Text),
                    AssociatedParts = partsAssociatedDataGridView.DataSource as BindingList<Part>
                };

                if (inventoryLogic(product) == 1)
                {
                    inventory.addProduct(product);
                    this.Close();
                }
                else if (inventoryLogic(product) == 2)
                {
                    MessageBox.Show("Inventory Below Min Values");
                }
                else if (inventoryLogic(product) == 3)
                {
                    MessageBox.Show("Inventory Above Max Values");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                modifyProduct.ProductID = int.Parse(idValue.Text);
                modifyProduct.Name = nameValue.Text;
                modifyProduct.InStock = int.Parse(inventoryValue.Text);
                modifyProduct.Price = decimal.Parse(priceCostValue.Text);
                modifyProduct.Max = int.Parse(maxValue.Text);
                modifyProduct.Min = int.Parse(minValue.Text);
                
                this.Close();
            }


        }

        ///////////////
        //// Misc ////
        //////////////

        //Form settings
        private void addProductSetup()
        {
            allCandidateDataGridView.DataSource = Inventory.AllParts; //sets data for All Candidate Parts DataGrid
            
        }

 

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

        private int inventoryLogic(Product product)
        {
            if (product.InStock <= product.Max && product.InStock >= product.Min)
            {
                return 1;
            }
            else if (product.InStock < product.Min)
            {
                return 2;
            }
            else if (product.InStock > product.Max)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }

        //closes add product screen
        private void productCancelButton_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
