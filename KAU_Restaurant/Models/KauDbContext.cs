using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace KAU_Restaurant.Models
{
    // Satisfies: CRUD Operations - Database context using SQLite
    public class KauDbContext
    {
        private string connectionString;

        public KauDbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["KauRestaurantDB"].ConnectionString;
        }

        // ============================================
        // Satisfies: Forms & Read - RETRIEVE student by ID
        // ============================================
        public Student GetStudentByID(string studentID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                string query = "SELECT StudentID, FullName, Password FROM Students WHERE StudentID = @ID";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", studentID);

                conn.Open();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = reader["StudentID"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // ============================================
        // Satisfies: Update - UPDATE password
        // ============================================
        public bool UpdatePassword(string studentID, string newPassword)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Students SET Password = @Password WHERE StudentID = @ID";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@Password", newPassword);
                cmd.Parameters.AddWithValue("@ID", studentID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ============================================
        // Satisfies: Delete - DELETE student
        // ============================================
        public bool DeleteStudent(string studentID)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE StudentID = @ID";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", studentID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ============================================
        // Satisfies: Read (Menu) - Retrieve all meals
        // ============================================
        public List<Meal> GetAllMeals()
        {
            List<Meal> meals = new List<Meal>();

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                string query = "SELECT MealID, MealName, Price, Category, DayOfWeek FROM Meals ORDER BY DayOfWeek, MealID";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                conn.Open();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meals.Add(new Meal
                        {
                            MealID = Convert.ToInt32(reader["MealID"]),
                            MealName = reader["MealName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Category = reader["Category"].ToString(),
                            DayOfWeek = reader["DayOfWeek"].ToString()
                        });
                    }
                }
            }
            return meals;
        }
    }
}
