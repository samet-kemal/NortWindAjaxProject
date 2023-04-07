using Core.Models;
using Core.ViewModels;
using Dapper;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SupplierService : ISupplierService
    {
        DapperContext _dapperContext;
        public SupplierService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<CustomResponseViewModel<List<SupplierResponse>>> GetAllSuppliers()
        {
            var query = @"SELECT
                            SupplierID,
                              CompanyName,
		                      ContactName,
		                      Address,
		                      City,
		                      Country,
						      Phone
		                      FROM 
		                      Suppliers
		                      WITH(NOLOCK)";          
            var response = new CustomResponseViewModel<List<SupplierResponse>>();

            try
            {
                using (var connection = new SqlConnection(_dapperContext.CreateConnectionString()))
                {
                    var customers = await connection.QueryAsync<SupplierResponse>(query);

               
                     if (customers != null)
                     {
                         response.Data = customers.ToList();
                         response.Succes = true;
                     }
                     else
                     {
                         response.Succes = false;
                         response.Errors.Add("Failed");

                     }
                }
            }
            catch (Exception e)
            {

                response.Errors.Add(e.ToString());
            }
            return response;

        }
        public async Task<CustomResponseViewModel<SupplierResponse>> GetSupplierById(int supplierId)
        {
            var query = @"SELECT
                            SupplierID,
                            CompanyName,
		                    ContactName,
		                    Address,
		                    City,
		                    Country,
                            Phone
		                    FROM 
		                    Suppliers
		                    WITH(NOLOCK)
		                    WHERE 
		                    SupplierID=@SupplierID";
            var response=new CustomResponseViewModel<SupplierResponse>();
            var param =new {
                            SupplierID = supplierId
                            };
            try
            {
                using(var connection=new SqlConnection(_dapperContext.CreateConnectionString()))
                {
                    var result =await  connection.QuerySingleOrDefaultAsync<SupplierResponse>(query, param);
                    if (result !=null)
                    {
                        response.Data =  result;
                        response.Succes = true;
                    }
                    else
                    {
                        response.Succes = false;
                        response.Message = "Supplier is Null";
                    }
                }
            }
            catch (Exception e)
            {

                response.Errors.Add(e.ToString());
            }

            return response;

        }

        public async Task<CustomResponseViewModel<SupplierResponse>> AddSupplier(CreateSupplierViewModel supplier)
        {
            var query = @"  INSERT INTO Suppliers
                                          (CompanyName,
	                                      ContactName,
	                                      Address,
	                                      City,
	                                      Country,
	                                      Phone)
										  Output 
										  inserted.SupplierID,
										  Inserted.CompanyName,
	                                      Inserted.ContactName,
	                                      Inserted.Address,
	                                      Inserted.City,
	                                      Inserted.Country,
	                                      Inserted.Phone
	                                      VALUES(
                                          @CompanyName,
	                                      @ContactName,
	                                      @Address,
	                                      @City,
	                                      @Country,
	                                      @Phone
	                                    	)";
            var response= new CustomResponseViewModel<SupplierResponse>();
            var param = new
            {
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                Address = supplier.Address,
                City = supplier.City,
                Country = supplier.Country,
                Phone = supplier.Phone,
            };

            try
            {
            using(var connection=new SqlConnection(_dapperContext.CreateConnectionString()))
            {
                var result=await connection.QueryAsync<SupplierResponse>(query, param);
                if (result.Count() != 0)
                {
                    response.Data = result.FirstOrDefault();
                    response.Succes = true;
                }
                else
                {
                    response.Succes = false;
                    response.Message = "Failed To Add";
                }
            }

            }
            catch (Exception e)
            {

                response.Errors.Add(e.ToString());
            }
            return response;
        }


        public async Task<CustomResponseViewModel<int>> UpdateSupplier(SupplierResponse supplier)
        {
            var query = @"	UPDATE Suppliers
	                        SET
                                 CompanyName=@CompanyName,
	                        	 ContactName=@ContactName,
	                        	 Address=@Address,
	                        	 City=@City,
	                        	 Country=@Country,
                                 Phone=@Phone
	                        WHERE SupplierID=@SupplierID";
            var param = new
            {
                SupplierID=supplier.SupplierID,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                Address = supplier.Address,
                City = supplier.City,
                Country = supplier.Country,
                Phone = supplier.Phone
            };

            var response = new CustomResponseViewModel<int>();
            try
            {
                using (var connection = new SqlConnection(_dapperContext.CreateConnectionString()))
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        response.Data = result;
                        response.Succes = true;
                    }
                    else
                    {
                        response.Succes = false;
                        response.Message = "Failed To Update";
                    }
                }

            }
            catch (Exception e)
            {

                response.Errors.Add(e.ToString());
            }
            return response;
        }

        public async Task<CustomResponseViewModel<int>> DeleteSupplier(int supplierId)
        {
            var query = @"  DELETE 
                            FROM   
                            Suppliers
                            WHERE   
                            SupplierID=@SupplierID";
            CustomResponseViewModel<int> response = new CustomResponseViewModel<int>();
            var param = new
            {
                SupplierID = supplierId
            };
            try
            {
                using (var connection = new SqlConnection(_dapperContext.CreateConnectionString()))
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        response.Data = result;
                        response.Succes = true;
                    }
                    else
                    {
                        response.Succes = false;
                        response.Message = "Failed To Delete";
                    }
                }

            }
            catch (Exception e)
            {

                response.Errors.Add(e.ToString());
            }
            return response;
        }


    }
}
