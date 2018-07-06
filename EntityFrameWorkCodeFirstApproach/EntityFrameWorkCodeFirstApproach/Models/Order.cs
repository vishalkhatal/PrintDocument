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
        public string OrderDescription { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        public DateTime DeliveryAddress { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public decimal PrintingCost { get; set; }
        public int TotalPages { get; set; }
        public DateTime DeliveryDate { get; set; }


    }
    public enum OrderStatus
    {
        New=0,
        Inprogress=1,
        OutForDelivery=2,
        Delivered=3
    }
}