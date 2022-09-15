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
        Product importedProduct = new Product();

        int addedParts = 0;


        public productScreen()
        {
            InitializeComponent();
            
            //sets up screen
            addProductSetup();

            idValue.Text = inventory.productIDGenerator().ToString();
        }

        public productScreen(Product product)
        {
            InitializeComponent();

            //sets up screen
            addProductSetup();

            importedProduct = product;

            //sets data from the passed in inhouse object
            idValue.Text = importedProduct.ProductID.ToString();
            nameValue.Text = importedProduct.Name.ToString();
            inventoryValue.Text = importedProduct.InStock.ToString();
            priceCostValue.Text = importedProduct.Price.ToString();
            maxValue.Text = importedProduct.Max.ToString();
            minValue.Text = importedProduct.Min.ToString();
            partsAssociatedDataGridView.DataSource = importedProduct.AssociatedParts;
        }

        ////////////////
        /// Buttons ///
        ///////////////

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

        //adds Part to AssociatedParts list
        private void allCandidateAddButton_Click(object sender, EventArgs e)
        {
            Part part = allCandidateDataGridView.CurrentRow.DataBoundItem as Part; //uses selection to create part object

            if (importedProduct.AssociatedParts == null)
            {
                myProduct.addAssociatedPart(part); //adds part object to Associated Parts List
                partsAssociatedDataGridView.DataSource = myProduct.AssociatedParts;
            }
            else
            {
                importedProduct.addAssociatedPart(part);
                partsAssociatedDataGridView.DataSource = importedProduct.AssociatedParts;
                addedParts++;
            } 
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
            if(addedParts > 0)
            {
                for (int i = 0; i < addedParts; i++)
                {
                    int x = importedProduct.AssociatedParts.Count;
                    int y = importedProduct.AssociatedParts[x - 1].PartID;

                    importedProduct.removeAssoicatedPart(y);
                }
                
            }
            this.Close();
        }
    }
}
