using System;
using System.Data.SqlClient;

namespace DBapp
{
	public class DBConnect
	{
		private SqlConnectionStringBuilder builder;
		protected SqlConnection connection;


		//Constructor
		public DBConnect()
		{
			Initialize();
		}

		//Initialize values
		private void Initialize()
		{
			builder = new SqlConnectionStringBuilder();
			builder.DataSource = "d601f20.database.windows.net";
			builder.UserID = "admin123";
			builder.Password = "Demo1234";
			builder.InitialCatalog = "ProjectDB";

			connection = new SqlConnection(builder.ConnectionString);
		}

		//open connection to database
		public bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch (SqlException ex)
			{
				/*Two cases in case an error happens
				 * case 0: If there is not open connection to the server
				 * case 1045: if username or password is invalid
				 */
				switch (ex.Number)
				{
					case 0:
						Console.WriteLine("Cannot connect to server. Contact administrator");
						Console.ReadLine();
						break;
					case 1045:
						Console.WriteLine("Invalid username/password, please try again");
						Console.ReadLine();
						break;
				}
				return false;
			}
		}

		//Close connection to database
		public bool CloseConnection()
		{
			try
			{
				connection.Close();
				return true;
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
				return false;

			}
		}
	}
}