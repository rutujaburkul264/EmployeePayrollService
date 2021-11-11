using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);
        public bool GetAllEmployee()
        {
            try
            {
                EmployeeModel employeemodel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                           // employeemodel.EmployeeID = dr.GetInt32(0);
                            employeemodel.EmployeeName = dr.GetString(1);
                            employeemodel.PhoneNumber = dr.GetString(2);
                            employeemodel.Address = dr.GetString(3);
                            employeemodel.Department = dr.GetString(4);
                            employeemodel.Gender = Convert.ToChar(dr.GetString(5));
                            employeemodel.BasicPay = dr.GetDecimal(6);
                            employeemodel.Deduction = dr.GetDecimal(7);
                            employeemodel.TaxablePay = dr.GetDecimal(8);
                            employeemodel.Tax = dr.GetDecimal(9);
                            employeemodel.NetPay = dr.GetDecimal(10);
                            employeemodel.startDate = dr.GetDateTime(11);


                            System.Console.WriteLine(employeemodel.EmployeeName + " " + employeemodel.BasicPay + " " + employeemodel.startDate + " " + employeemodel.Gender + " " + employeemodel.PhoneNumber + " " + employeemodel.Address + " " + employeemodel.Department + " " + employeemodel.Deduction + " " + employeemodel.TaxablePay + " " + employeemodel.Tax + " " + employeemodel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                //var qury=values()
                SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
               // command.Parameters.AddWithValue("@EmployeeId", model.EmployeeID);
                command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Department", model.Department);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                command.Parameters.AddWithValue("@Deduction", model.Deduction);
                command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                command.Parameters.AddWithValue("@Tax", model.Tax);
                command.Parameters.AddWithValue("@NetPay", model.NetPay);
                command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                // command.Parameters.AddWithValue("@City", model.City);
                //command.Parameters.AddWithValue("@Country", model.Country);
                this.connection.Open();
                var result = command.ExecuteNonQuery();
                this.connection.Close();
                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
    }
}
