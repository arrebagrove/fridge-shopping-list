﻿using FridgeShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace FridgeShoppingList.ViewModels.ControlViewModels
{
    public class ShoppingListEntryViewModel : BindableBase
    {
        public GroceryEntry Entry { get; set; }

        public ShoppingListEntryViewModel(GroceryEntry entry)
        {
            Entry = entry;
        }
    }
}
