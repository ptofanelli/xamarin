using System;
using System.Collections.Generic;
using System.Linq;
using XamarinMarathon.model;
using SQLite;
using System.Threading.Tasks;

namespace XamarinMarathon.model
{
	public class PersonRepository
	{
		public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

		public PersonRepository(string dbPath)
		{
            // Initialize a new SQLiteConnection
            // Create the Person table
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>().Wait();
        }

		public async Task AddNewPerson(string name)
		{
			var result = 0;
			try
			{
				//basic validation to ensure a name was entered
				if (string.IsNullOrEmpty(name))
					throw new Exception("Valid name required");

                // insert a new person into the Person table
                result = await conn.InsertAsync(new Person { Name = name });

                await Task.Delay(3000);

				StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
			}

		}

		public async Task<List<Person>> GetAllPeople()
		{
            // return a list of people saved to the Person table in the database
            return await conn.Table<Person>().ToListAsync();
        }
	}
}