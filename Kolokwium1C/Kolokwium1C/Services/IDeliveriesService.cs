using Kolokwium1C.Models;
namespace Kolokwium1C.Services;

public interface IDeliveriesService
{
    Task<DeliveryDTO> GetDeliveryAsync(int id);
    Task AddDelivery(DeliveryDTO delivery);
}