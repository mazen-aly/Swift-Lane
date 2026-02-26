
namespace Entities
{
    public class ShipmentCarrier : BaseEntity
    {
        public string CarrierName { get; set; } = null!;

        public ICollection<ShipmentStatus> ShipmentStatuses { get; set; } = new List<ShipmentStatus>();
    }

}

