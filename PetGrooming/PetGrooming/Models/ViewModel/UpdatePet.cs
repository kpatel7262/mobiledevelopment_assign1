using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGrooming.Models.ViewModel
{
    public class UpdatePet
    {
        //what info does update pet need
        //list of species
        
       public Pet pet { get; set; }
       public List<Species> species { get; set; }
    }
}