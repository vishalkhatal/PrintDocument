using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameWorkCodeFirstApproach.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Order Description")]
        public string OrderDescription { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        [Display(Name = "Upload File")]
        public string FileName { get; set; }
        [Display(Name = "Printing Cost")]
        public decimal PrintingCost { get; set; }
        [Display(Name = "Total Pages")]
        public int TotalPages { get; set; }
        public DateTime? DeliveryDate { get; set; }


    }
    public enum OrderStatus
    {
        New=0,
        Inprogress=1,
        OutForDelivery=2,
        Delivered=3
    }
}