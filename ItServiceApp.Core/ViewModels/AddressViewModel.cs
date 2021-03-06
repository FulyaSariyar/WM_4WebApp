using ItServiceApp.Core.Entities;
using ItServiceApp.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Core.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string Line  { get; set; }
        public string PostCode { get; set; }
        public AddressTypes AddressTpes  { get; set; }
        public int StateId  { get; set; }
        public string UserId  { get; set; }
    }
}
