using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Order
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton;

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public Order()
        {
            // get the mysqlconnection singleton
            connectionSingleton = ConnectionSingleton.GetSingleton();
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns a dictionary containing all the purchase data. when user not found, method returns null
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public DataTable getAllPurchases()
        {
            try
            {
                myCommand = new MySqlCommand("getAllPurchases", mysqlConnection);
                // input - There is no input needed.
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@ACCOUNT_ID", MySqlDbType.Int32);
                myCommand.Parameters["@ACCOUNT_ID"].Direction = ParameterDirection.Output;
                //myCommand.Parameters.Add("@Date", MySqlDbType.DateTime);
                //myCommand.Parameters["@Date"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Status", MySqlDbType.VarChar);
                myCommand.Parameters["@Status"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Shipping", MySqlDbType.VarChar);
                myCommand.Parameters["@Shipping"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return datatable
                return dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }

        /// <summary>
        /// this method updates the purchases' status to Paid if the status is 'Not Paid'.
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public void updatePurchaseStatus(int purchaseID, string paidStatus, string shippingStatus)
        {
            try
            {
                myCommand = new MySqlCommand("changeOrderStatus", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_purchase_ID", MySqlDbType.Int32).Value = purchaseID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_newPaidStatus", MySqlDbType.VarChar).Value = paidStatus;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_newShippingStatus", MySqlDbType.VarChar).Value = shippingStatus;
                // output - There is no output.
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
        }


        public int getPrice(int product_ID, int photo_ID)
        {
            try
            {
                myCommand = new MySqlCommand("getPrice", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_Product_ID", MySqlDbType.Int32).Value = product_ID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_Photo_ID", MySqlDbType.Int32).Value = photo_ID;

                myCommand.Parameters.Add("@Price", MySqlDbType.Int32);
                myCommand.Parameters["@Price"].Direction = ParameterDirection.Output;
                // execute query
                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return price int
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return 0;
        }


        /// <summary>
        /// this method inserts an order into the databse and updates the stock
        /// </summary>
        /// <param name="accountID">the account id</param>
        /// <param name="date">the date of the order</param>
        /// <param name="status"> the status eg paid, not paid</param>
        /// <param name="productID">the product id</param>
        /// <param name="photoID">the photo id</param>
        /// <param name="filterType">the filter type</param>
        /// <param name="paymentType">the payment type</param>
        /// <param name="productType">the product type</param>
        /// <param name="price">the product price</param>
        /// <param name="quantity">the product quantity</param>
        /// <returns> the order ID</returns>
        public int inserOrder(int accountID, string date, string status, int productID, int photoID, string filterType, string paymentType, string productType, string iban, double price, int quantity)
        {
            // insert purchase
            int purchaseID = insertPurchase(accountID, date, status);
           
            if(purchaseID != 0)
            {
                // insert purchase_product
                insertPurchaseProduct(purchaseID, productID, photoID, filterType, quantity);
                // insert payment
                int paymentID = insertPayment(purchaseID, date, paymentType, iban, price);
                // insert printorder
                int printOrderID = insertPrintOrder(paymentID, date);
                // update stock
                updateStock(productType, quantity);
                // return the order id
                return printOrderID;
            }
            return 0;
        }


        /// <summary>
        /// insert the purchase and return it's id
        /// </summary>
        /// <param name="accountID"> the id of the account placing the order</param>
        /// <param name="date"> the date of the order</param>
        /// <param name="status"> the status of the order eg paid , not paid</param>
        /// <returns> the id of the purchase</returns>
        public int insertPurchase(int accountID, string date, string status)
        {
            try
            {
                myCommand = new MySqlCommand("insertPurchase", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_account_ID", MySqlDbType.Int32).Value = accountID;               
                myCommand.Parameters.Add("@p_date", MySqlDbType.Date).Value = date;
                myCommand.Parameters.Add("@p_status", MySqlDbType.VarChar).Value = status;                

                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return purchade id
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return 0;
        }

        /// <summary>
        /// insert into the purchase_product table
        /// </summary>
        /// <param name="purchaseID"> the id of the purchase</param>
        /// <param name="productID"> the id of the product</param>
        /// <param name="photoID"> the id of the photo</param>
        /// <param name="filterType"> the type of the filter</param>
        /// <param name="quantity"> the quantity</param>
        public void insertPurchaseProduct(int purchaseID, int productID, int photoID, string filterType, int quantity)
        {
            try
            {
                myCommand = new MySqlCommand("insertPurchaseProduct", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_purchase_ID", MySqlDbType.Int32).Value = purchaseID;
                myCommand.Parameters.Add("@p_product_ID", MySqlDbType.Int32).Value = productID;
                myCommand.Parameters.Add("@p_photo_ID", MySqlDbType.Int32).Value = photoID;
                myCommand.Parameters.Add("@p_filterType", MySqlDbType.VarChar).Value = filterType;
                myCommand.Parameters.Add("@p_quantity", MySqlDbType.Int32).Value = quantity;

                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
        }

        /// <summary>
        /// insert the payment
        /// </summary>
        /// <param name="purchaseID">the purchase id</param>
        /// <param name="date">the date</param>
        /// <param name="type">the type</param>
        /// <param name="iban">the iban number</param>
        /// <param name="price">the price</param>
        /// <returns>return the payment id</returns>
        public int insertPayment(int purchaseID, string date, string paymentType, string iban, double price)
        {
            try
            {
                myCommand = new MySqlCommand("insertPayment", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_purchase_ID", MySqlDbType.Int32).Value = purchaseID;
                myCommand.Parameters.Add("@p_date", MySqlDbType.Date).Value = date;
                myCommand.Parameters.Add("@p_type", MySqlDbType.VarChar).Value = paymentType;
                myCommand.Parameters.Add("@p_iban", MySqlDbType.VarChar).Value = iban;
                myCommand.Parameters.Add("@p_price", MySqlDbType.Decimal).Value = price;


                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return purchade id
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return 0;
        }

        /// <summary>
        /// insert the print order
        /// </summary>
        /// <param name="paymentID"> the payment id</param>
        /// <param name="date"> the date</param>
        /// <returns>return the order id</returns>
        public int insertPrintOrder(int paymentID, string date)
        {
            try
            {
                myCommand = new MySqlCommand("insertPrintOrder", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_payment_ID", MySqlDbType.Int32).Value = paymentID;
                myCommand.Parameters.Add("@p_date", MySqlDbType.Date).Value = date;

                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return purchade id
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return 0;
        }

  

        /// <summary>
        /// Update the stock so 
        /// </summary>
        /// <param name="type"> the producttype</param>
        /// <param name="quantity"> the quantity of the ordered product</param>
        public void updateStock(string productType, int quantity)
        {
            try
            {
                myCommand = new MySqlCommand("updateStock", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_type", MySqlDbType.VarChar).Value = productType;
                myCommand.Parameters.Add("@p_quantity", MySqlDbType.Int32).Value = quantity;
                
                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
        }

        public DataTable getAllOrders(int accountID)
        {
            try
            {
                myCommand = new MySqlCommand("getAllPaymentsByAccount", mysqlConnection);
                // input - There is no input needed.
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_account_ID", MySqlDbType.Int32).Value = accountID;
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Date", MySqlDbType.DateTime);
                myCommand.Parameters["@Date"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Type", MySqlDbType.VarChar);
                myCommand.Parameters["@Type"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@IBAN", MySqlDbType.VarChar);
                myCommand.Parameters["@IBAN"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@PRICE", MySqlDbType.Double);
                myCommand.Parameters["@PRICE"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return datatable
                return dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }

    }
}