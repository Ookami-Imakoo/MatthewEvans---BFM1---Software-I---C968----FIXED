﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatthewEvans___BFM1___Software_I___C968.model
{
    public class Inventory
    {
        //Initialization of Product
        //Product product = new Product();

        public static BindingList<Product> Products = new BindingList<Product>(); //Binding list for storeing Product objects
        public static BindingList<Part> AllParts = new BindingList<Part>(); //Binding list for storeing Part objects
        
        /// <summary>
        /// Method for adding products to Products BindingList.
        /// </summary>
        /// <param name="Product">Product to be added to BindingList</param>
        public void addProduct(Product product)
        {
            Products.Add(product);
        }

        /// <summary>
        /// Function for removing products.
        /// </summary>
        /// <param name="x"> Represents the product id to be deleted. </param>
        /// <returns> Returns true if the suplied product id is found, else returns false. </returns>
        public bool removeProduct(Product product)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (product == Products[i])
                {
                    Products.Remove(product);
                }
            }
            return false;
        }

        /// <summary>
        /// Represents a function that adds a Part to the All Parts list.
        /// </summary>
        /// <param name=""> Represents a part to be addded to the All Parts list. </param>
        public void addPart(Part myPart)
        {
            AllParts.Add(myPart);
        }

        /// <summary>
        /// Refers to the function that will iterate though the All Parts list and checks to make sure that
        /// the part entered as a parameter is not the same as the refrence form the list and if it dose match
        /// it will remove it.
        /// </summary>
        /// <param name=""> Represents the Part to be deleted</param>
        /// <returns> Returns true if part is deleted, else returns false. </returns>
        public bool deletePart(Part myPart)
        {
            foreach (var part in AllParts)
            {
                if (part != myPart)
                {
                    continue;
                }
                AllParts.Remove(part);
                return true;
            }
            return false;

            /*
            for (int i = 0 ; i < AllParts.Count ; i++)
            {
                if (myPart != AllParts[i])
                {
                    continue;
                }
                AllParts.RemoveAt(i);
                return true;

            }
            return false;
            */
        }

        /// <summary>
        /// Refers to the function that will look up a part based on input in the partSearchValue textbox
        /// </summary>
        /// <param name="x"> Refers input from partSearchValue textbox </param>
        public void lookupPart(int x)
        {
            int i = 0;

            while (i < AllParts.Count){
                if (i == AllParts.Count - 1)
                {
                    if (x == AllParts[i].PartID)
                    {
                        MessageBox.Show($"Part with id: {x} was found.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"No part with id: {x} was found.");
                        return;
                    }
                }
                else if (x == AllParts[i].PartID)
                {
                    MessageBox.Show($"Part with id: {x} was found.");
                    return;
                }
                else if (x != AllParts[i].PartID)
                {
                    i++;
                } 
            }
        }

        /// <summary>
        /// Refers to the function that will look up a part based on input in the partSearchValue textbox
        /// </summary>
        /// <param name="x"> Refers input from partSearchValue textbox </param>
        public void lookupPart(String x)
        {
            int i = 0;

            while (i < AllParts.Count)
            {
                if (i == AllParts.Count - 1)
                {
                    if (x == AllParts[i].Name)
                    {
                        MessageBox.Show($"Part with Name: {x} was found.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"No part with Name: {x} was found.");
                        return;
                    }
                }
                else if (x == AllParts[i].Name)
                {
                    MessageBox.Show($"Part with Name: {x} Name found.");
                    return;
                }
                else if (x != AllParts[i].Name)
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Refers to the function that will look up a part based on input in the partSearchValue textbox
        /// </summary>
        /// <param name="x"> Refers input from partSearchValue textbox </param>
        public void lookupProudct(int x)
        {
            int i = 0;

            while (i < Products.Count)
            {
                if (i == Products.Count - 1)
                {
                    if (x == Products[i].ProductID)
                    {
                        MessageBox.Show($"Product with ID: {x} was found.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"No Product with ID: {x} was found.");
                        return;
                    }
                }
                else if (x == Products[i].ProductID)
                {
                    MessageBox.Show($"Product with ID: {x} was found.");
                    return;
                }
                else if (x != Products[i].ProductID)
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Refers to the function that will look up a part based on input in the partSearchValue textbox
        /// </summary>
        /// <param name="x"> Refers input from partSearchValue textbox </param>
        public void lookupProudct(String x)
        {
            int i = 0;

            while (i < Products.Count)
            {
                if (i == Products.Count - 1)
                {
                    if (x == Products[i].Name)
                    {
                        MessageBox.Show($"Product with Name: {x} was found.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"No Product with Name: {x} was found.");
                        return;
                    }
                }
                else if (x == Products[i].Name)
                {
                    MessageBox.Show($"Product with Name: {x} was found.");
                    return;
                }
                else if (x != Products[i].Name)
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Refers to the function that will update a part, by first ittering though the binding list 
        /// and deleteing the part if found, and replaceing it with the Part provided. </summary>
        /// <param name="x"> Represents the part number wished to be replaced. </param>
        /// <param name="myPart"> Represents the part to be placed in the list after the removal of the old data. </param>
        public void updatePart(int x, Part myPart)
        {
            AllParts.RemoveAt(x - 1);
            AllParts.Add(myPart);
        }

        //private void addPartNumber(int i)
        //{
        //    PartNumbers.Add(AllParts[i].PartID);
        //}

        /// <summary>
        /// Function for generating and setting unique partID's
        /// </summary>
        /// <returns> Unique Part Number </returns>
        public int partIDGenerator()
        {
            int i = 0;
            int missingEntry = 0;

            do
            {
                if (AllParts.Count == 0)
                {
                    return 1;
                }
                else if (AllParts[0].PartID > 1)
                {
                    missingEntry = 1;
                    break;
                }
                else if ((i + 1) == AllParts[i].PartID)
                {
                    i++;
                    continue;
                }
                else
                {
                    missingEntry = (AllParts[i].PartID - 1);
                    break;
                }
            }
            while (i != AllParts.Count);

            do
            {
                if (i == AllParts.Count) 
                {
                    return (AllParts.Count + 1);
                }
                else if (missingEntry != AllParts[i].PartID)
                {
                    i++;
                    continue;
                }
                else if (missingEntry == AllParts[i].PartID)
                {
                    return (AllParts.Count + 1);
                }
            }
            while (i != AllParts.Count);    
            return missingEntry;
        }

        public int productIDGenerator()
        {
            int i = 0;
            int missingEntry = 0;

            do
            {
                if (Products.Count == 0)
                {
                    return 1;
                }
                else if (Products[0].ProductID > 1)
                {
                    missingEntry = 1;
                    break;
                }
                else if ((i + 1) == Products[i].ProductID)
                {
                    i++;
                    continue;
                }
                else
                {
                    missingEntry = (Products[i].ProductID - 1);
                    break;
                }
            }
            while (i != Products.Count);

            do
            {
                if (i == Products.Count)
                {
                    return (Products.Count + 1);
                }
                else if (missingEntry != Products[i].ProductID)
                {
                    i++;
                    continue;
                }
                else if (missingEntry == Products[i].ProductID)
                {
                    return (Products.Count + 1);
                }
            }
            while (i != Products.Count);
            return missingEntry;
        }

        public Part dataSetup(int x)
        {
            Part part = AllParts[x];
            return part;
        }

        public bool checkExistence(Part part)
        {
            for (int i = 0; i < AllParts.Count; i++){
                if (AllParts[i].PartID == part.PartID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}