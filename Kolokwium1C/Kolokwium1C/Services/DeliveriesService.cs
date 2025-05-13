using System.Data.Common;
using Kolokwium1C.Models;
using Microsoft.Data.SqlClient;

namespace Kolokwium1C.Services;

public class DeliveriesService : IDeliveriesService
{
    private readonly String _connectionString;

    public DeliveriesService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<DeliveryDTO> GetDeliveryAsync(int deliveryId)
    {

        string sqlQuery = @"Select D.date as date, C.first_name as clientname, C.last_name as clientlastname, C.date_of_birth as clientdateofbirth, 
       Dr.first_name as drivername, Dr.last_name as driverlastname, Dr.licence_number as licencenumber, 
       P.name as productname, P.price as productprice, Pd.amount as productamount 
       from Delivery D join Customer C on D.customer_id = C.customer_id 
       join Driver Dr on D.driver_id = Dr.driver_id 
       join Product_Delivery Pd on D.delivery_id = Pd.delivery_id 
       join Product P on Pd.product_id = P.product_id
       where D.delivery_id = @deliveryId;";

        try
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;
            command.CommandText = sqlQuery;

            command.Parameters.AddWithValue("@deliveryId", deliveryId);

            await sqlConnection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            DeliveryDTO? delivery = null;
            while (await reader.ReadAsync())
            {
                if (delivery is null)
                {
                    delivery = new DeliveryDTO
                    {
                        date = reader.GetDateTime(reader.GetOrdinal("date")),
                        customer = new Customer
                        {
                            firstName = reader.GetString(reader.GetOrdinal("clientname")),
                            lastName = reader.GetString(reader.GetOrdinal("clientlastname")),
                            dateOfBirth = reader.GetDateTime(reader.GetOrdinal("clientdateofbirth")),
                        },
                        driver = new Driver
                        {
                            firstName = reader.GetString(reader.GetOrdinal("drivername")),
                            lastName = reader.GetString(reader.GetOrdinal("driverlastname")),
                            licenseNumber = reader.GetString(reader.GetOrdinal("licencenumber")),
                        }

                    };

                    delivery.products.Add(new Product
                    {
                        name = reader.GetString(reader.GetOrdinal("productname")),
                        price = reader.GetDecimal(reader.GetOrdinal("productprice")),
                        amount = reader.GetInt32(reader.GetOrdinal("productamount"))
                    });

                }

            }

            if (delivery is null)
            {
                throw new ArgumentException($"Delivery {deliveryId} not found");
            }

            return delivery;
        }
        catch (SqlException sqlEx)
        {
            throw new ApplicationException(sqlEx.Message);
        }
    }

    public async Task AddDelivery(DeliveryDTO delivery)
    {
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        await connection.OpenAsync();
        
        DbTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction as SqlTransaction;

        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}