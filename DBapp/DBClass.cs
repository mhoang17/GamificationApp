using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DBapp
{
	public class DatabaseManagement : DBConnect {

	}


	public class UserInfo : DBConnect
	{
		public void UserInsert(string userName, string age, string primaryTransportStart, string primaryTransportCurrent, string totalXP)
		{

			string query = "INSERT INTO dbo.userInfo (userName, age, primaryTransportStart, primaryTransportCurrent, totalXP) VALUES ('" + userName + "', '" + age + "', '" + primaryTransportStart + "', '" + primaryTransportCurrent + "', '" + totalXP + "')";

			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				SqlCommand cmd = new SqlCommand(query, connection);

				//execute command
				int executed = cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public void UserDelete(string userID)
		{
			string query = "DELETE FROM dbo.userInfo WHERE userID = " + userID;
			if (this.OpenConnection() == true)
			{
				SqlCommand cmd = new SqlCommand(query, connection);
				cmd.ExecuteNonQuery();
				this.CloseConnection();
			}
		}

		public void UserUpdate(string column, string newValue, string userID)
		{
			string query = "UPDATE dbo.userInfo SET " + column + "=@" + column + " WHERE userID=" + userID;

			//Open connection
			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				cmd.Parameters.AddWithValue("@" + column, newValue);

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public string UserSelect(string column, string condition) {

			string result = "";

			string query = "SELECT " + column + " FROM dbo.userInfo WHERE "+ condition;

			if (!condition.Contains("=")) {

				return "False Query";
			}
			
			//Open connection
			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				using (SqlDataReader reader = cmd.ExecuteReader()) {

					if (reader.Read()) {
						result = reader[column].ToString();
					}
				}

				//close connection
				this.CloseConnection();
			}

			return result;
		}
	}

	public class CarInfo : DBConnect
	{
		public void CarInsert(string carType, string carName, string KMPerL, string userID)
		{
			string query = "INSERT INTO dbo.carInfo (carType, carName, KMPerL, userID) VALUES ('" + carType + "', '" + carName + "', '" + KMPerL + "', '" + userID + "')";

			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				SqlCommand cmd = new SqlCommand(query, connection);

				//execute command
				int executed = cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public void CarDelete(string carID)
		{
			string query = "DELETE FROM dbo.carInfo WHERE carID = " + carID;
			
			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				SqlCommand cmd = new SqlCommand(query, connection);

				//execute command
				int executed = cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public void CarUpdate(string column, string newValue, string carID)
		{
			string query = "UPDATE dbo.carInfo SET " + column + "=@" + column + " WHERE carID=" + carID;


			//Open connection
			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				cmd.Parameters.AddWithValue("@" + column, newValue);

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public List<string> CarSelect(string column, string condition)
		{
			string query = "SELECT " + column + " FROM dbo.carInfo WHERE " + condition;

			List<string> result = new List<string>();

			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							result.Add(reader[column].ToString());
						}
					}
				}

				//close connection
				this.CloseConnection();
			}

			return result;
		}
	}

	public class TripInfo : DBConnect
	{
		public void TripInsert(string distance, string timeStamp, string userID, string transport)
		{

			string query = "";

			if (transport.Equals("Bike") || transport.Equals("Bus") || transport.Equals("Walk"))
			{
				query = "INSERT INTO tripInfo (distance, timeStamp, userID, otherTransport) VALUES ('" + distance + "','" + timeStamp +"','" + userID + "','" + transport + "')";
			}
			else {

				query = "INSERT INTO tripInfo (distance, timeStamp, userID, carID) VALUES ('" + distance + "','" + timeStamp + "','" + userID + "','" + transport + "')";
			}
			
			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				SqlCommand cmd = new SqlCommand(query, connection);

				//execute command
				int executed = cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public void TripDelete(string tripID)
		{
			string query = "DELETE FROM dbo.tripInfo WHERE tripID = " + tripID;

			//open connection
			if (this.OpenConnection() == true)
			{
				//create command and assign the query and connection from the constructor
				SqlCommand cmd = new SqlCommand(query, connection);

				//execute command
				int executed = cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public void TripUpdate(string column, string newValue, string tripID)
		{
			string query = "UPDATE dbo.tripInfo SET " + column + "=@" + column + " WHERE tripID=" + tripID;


			//Open connection
			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				cmd.Parameters.AddWithValue("@" + column, newValue);

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				cmd.ExecuteNonQuery();

				//close connection
				this.CloseConnection();
			}
		}

		public string TripSelect(string column, string condition)
		{
			string query = "SELECT " + column + " FROM dbo.tripInfo WHERE " + condition;

			string result = "";

			if (!condition.Contains("="))
			{
				return "False Query";
			}

			if (this.OpenConnection() == true)
			{
				//create MySql command
				SqlCommand cmd = new SqlCommand();

				//assign the query using CommandText
				cmd.CommandText = query;
				//Assign the connection using Connection
				cmd.Connection = connection;

				//execute the query 
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						result = reader[column].ToString();
					}
				}

				//close connection
				this.CloseConnection();
			}

			return result;
		}
	}
}