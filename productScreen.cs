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
        /// <param name="product"> Product to be modifed -- passed in from MainScreen productsModifyButton_Click</param>
        public productScreen(Product product)
        {
            InitializeComponent();

            //sets up screen
            addProductSetup();

            modifyProduct = product; //setting modifyProduct to the product used in constructor

            //sets data from the passed in modifyProduct object
            idValue.Text = modifyProduct.ProductID.ToString();
            nameValue.Text = modifyProduct.Name.ToString();
            inventoryValue.Text = modifyProduct.InStock.ToString();
            priceCostValue.Text = modifyProduct.Price.ToString();
            maxValue.Text = modifyProduct.Max.ToString();
            minValue.Text = modifyProduct.Min.ToString();
            partsAssociatedDataGridView.DataSource = modifyProduct.AssociatedParts;
        }

        ////////////////
        /// Buttons ///
        ///////////////

        
        //

        private void allCandidateAddButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part; //uses selection to create part object

            if (modifyProduct.AssociatedParts == null) //checks to see if we are NOT modifying an existing entry
            {
                myProduct.addAssociatedPart(part); //adds part object to Associated Parts List
                partsAssociatedDataGridView.DataSource = myProduct.AssociatedParts; //displays AssociatedParts list of new product
            }
            else
            {
                modifyProduct.addAssociatedPart(part); //adds part object to Associated Parts List of modifiedProduct
                partsAssociatedDataGridView.DataSource = modifyProduct.AssociatedParts; //displays AssociatedParts list of modified product
            }
        }

        //created a product adding it to the 
        private void productSaveButton_Click(object sender, EventArgs e)
        {
            //Product product = new Product
            //{
            //    ProductID = int.Parse(idValue.Text),
            //    Name = nameValue.Text,
            //    InStock = int.Parse(inventoryValue.Text),
            //    Price = decimal.Parse(priceCostValue.Text),
            //    Max = int.Parse(maxValue.Text),
            //    Min = int.Parse(minValue.Text),
            //    AssociatedParts = partsAssociatedDataGridView.DataSource as BindingList<Part>
            //};

            ////invetoryLogicSwitch(product);

            //if (inventoryLogic(product) == 1)
            //{
            //    inventory.addProduct(product);
            //    this.Close();
            //}
            //else if (inventoryLogic(product) == 2)
            //{
            //    MessageBox.Show("Inventory Below Min Values");
            //}
            //else if (inventoryLogic(product) == 3)
            //{
            //    MessageBox.Show("Inventory Above Max Values");
            //}
            //else
            //{
            //    MessageBox.Show("Error");
            //}
        }

        

        //Form settings
        private void addProductSetup()
        {
            allCandidateDataGridView.DataSource = Inventory.AllParts; //sets data for All Candidate Parts DataGrid
            //partsAssociatedDataGridView.DataSource = Product.AssociatedParts; //sets data for Parts Associated DataGrid

            
        }

        //removes parts form the Associated Parts list, but not form the all Candidate list
        private void partsAssociatedDeleteButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part;
            int i = part.PartID;
            myProduct.removeAssoicatedPart(i);

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

        //private bool inventoryMinMax(Product product)
        //{
        //    int inventoryAmount = int.Parse(inventoryValue.Text);
        //    int maxAmount = int.Parse(maxValue.Text);
        //    int minAmount = int.Parse(minValue.Text);

        //    if (inventoryAmount <= maxAmount && inventoryAmount >= minAmount)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

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
            if (myProduct.addedPartsCounter > 0)
            {
                for(int i = 0; i < myProduct.addedPartsCounter; i++)
                myProduct.removeAssoicatedPart(myProduct.AssociatedParts.Count - 1);
            }
            this.Close();
        }
    }
}
