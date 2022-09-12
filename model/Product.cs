using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatthewEvans___BFM1___Software_I___C968.model
{
    public class Product
    {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public BindingList<Part> AssociatredParts { get; set; }

        public BindingList<Part> AssociatedParts = new BindingList<Part>();

        //default constructor
        public Product()
        {

        }

        public Product(int ProductID, String Name, decimal Price, int InStock, int Min, int Max)
        {
            this.ProductID = ProductID;
            this.Name = Name;
            this.Price = Price;
            this.InStock = InStock;
            this.Min = Min;
            this.Max = Max;
        }

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

        public void addAssociatedPart(Part myPart)
        {
            AssociatedParts.Add(myPart);

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
    }
}
