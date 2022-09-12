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
        //initialization of Inventory and Product
        Inventory inventory = new Inventory();
        Product myProduct = new Product();

        public productScreen()
        {
            InitializeComponent();
            
            //sets up screen
            addProductSetup();

            idValue.Text = inventory.productIDGenerator().ToString();
        }

        public productScreen(Product selectedProduct)
        {
            productScreen modifyProduct = new productScreen();

            //sets data from the passed in inhouse object
            modifyProduct.idValue.Text = selectedProduct.ProductID.ToString();
            modifyProduct.nameValue.Text = selectedProduct.Name.ToString();
            modifyProduct.inventoryValue.Text = selectedProduct.InStock.ToString();
            modifyProduct.priceCostValue.Text = selectedProduct.Price.ToString();
            modifyProduct.maxValue.Text = selectedProduct.Max.ToString();
            modifyProduct.minValue.Text = selectedProduct.Min.ToString();

            modifyProduct.Show();
        }

        ////////////////
        /// Buttons ///
        ///////////////

        //created a product adding it to the 
        private void productSaveButton_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                ProductID = int.Parse(idValue.Text),
                Name = nameValue.Text,
                InStock = int.Parse(inventoryValue.Text),
                Price = decimal.Parse(priceCostValue.Text),
                Max = int.Parse(maxValue.Text),
                Min = int.Parse(minValue.Text),
                AssociatredParts = myProduct.AssociatredParts
            };

            //invetoryLogicSwitch(product);

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

        //adds Part to AssociatedParts list
        private void allCandidateAddButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part; //uses selection to create part object 
            myProduct.AssociatedParts.Add(part); //adds part object to Associated Parts List
        }

        //Form settings
        private void addProductSetup()
        {
            allCandidateDataGridView.DataSource = Inventory.AllParts; //sets data for All Candidate Parts DataGrid
            partsAssociatedDataGridView.DataSource = myProduct.AssociatedParts; //sets data for Parts Associated DataGrid

            
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
            this.Close();
        }
    }
}
