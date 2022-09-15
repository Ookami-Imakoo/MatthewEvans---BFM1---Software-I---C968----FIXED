using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatthewEvans___BFM1___Software_I___C968.model
{
    public class Product
    {
        /*
         *  Product Defualt Properties
         */
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int addedPartsCounter { get; set; }
        public BindingList<Part> AssociatedParts { get; set; }


        /// <summary>
        /// Defualt Constructor for Prodcut Object
        /// </summary>
        public Product()
        {

        }

        /// <summary>
        /// Constroctor for Product Object that has no AssociatedParts list
        /// </summary>
        public Product(int ProductID, String Name, decimal Price, int InStock, int Min, int Max)
        {
            this.ProductID = ProductID;
            this.Name = Name;
            this.Price = Price;
            this.InStock = InStock;
            this.Min = Min;
            this.Max = Max;
        }

        /// <summary>
        /// Full Constructor for Product Object
        /// </summary>
        public Product(int ProductID, String Name, decimal Price, int InStock, int Min, int Max, BindingList<Part> AssociatedParts)
        {
            this.ProductID = ProductID;
            this.Name = Name;
            this.Price = Price;
            this.InStock = InStock;
            this.Min = Min;
            this.Max = Max;
            this.AssociatedParts = AssociatedParts;
        }

        /// <summary>
        /// Adds Associated Part to the Associated Parts List
        /// </summary>
        /// <param name="myPart"> Part to be added to the List </param>
        public void addAssociatedPart(Part myPart)
        {
            if (AssociatedParts == null && checkExistence(myPart) == false) //checks to see if Associated Parts list exists and checks if the part already exists in that list.
            {
                AssociatedParts = new BindingList<Part>(); //initializes AssociatedParts list
                AssociatedParts.Add(myPart); //adds part to AssociatedParts list
            }
            else if (checkExistence(myPart) == false) //if an Associated Parts list already exists, and the part has not already been added
            {
                AssociatedParts.Add(myPart); //adds part to AssociatedParts list
                addedPartsCounter++; //increments counter used in cancel button logic
            }
            else
            {
                MessageBox.Show("You can not add duplicate parts."); // messaged returned if the part the user is adding already exists in the list
            }

        }

        public bool removeAssoicatedPart(int x)
        {
           for(int i = 0; i < AssociatedParts.Count; i++)
            {
                if (x == AssociatedParts[i].PartID)
                {
                    AssociatedParts.RemoveAt(i);
                return true;
                }
            }
            return false;
        
        }

        public Part lookupAssoicatedPart(int x)
        {
            for (int i = 0; i < AssociatedParts.Count; i++)
            {
                if (x == ProductID)
                {
                    return AssociatedParts[i];
                }
                continue;
            }
            return null;
        }

        public bool checkExistence(Part part)
        {
            if(AssociatedParts != null)
            {
                for (int i = 0; i < AssociatedParts.Count; i++)
                {
                    if (AssociatedParts[i].PartID == part.PartID)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
