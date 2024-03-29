
namespace API.Data.DTOs
{
    public class OrderWithCustomerDto
    {
        public int OrderId { get; set; }
        public string OrderTimeStamp { get; set; }
        public string OrderStatus { get; set; }
        
        public string DeliveryTimeStamp { get; set; }

        public int OrderedAnimalId { get; set; }

        public string OrderedAnimalName {get;set;}

        public decimal price { get; set; }


        public int customerId {get;set;}
        public string customerUserName { get; set; }
        public string customerName {get;set;}
        public string customerEmail {get;set;}
        public int customerPhoneNumber { get; set; }

        public string customerAddress {get;set;}
    }
}