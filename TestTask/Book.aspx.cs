using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTask
{
    public partial class Book : System.Web.UI.Page
    {
        string connectionString = @"Data Source=91.201.72.213;Initial Catalog=GubkinPhoneBook;User ID=study;Password=study;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            DataTable dt = new DataTable();
            string persons = "SELECT * FROM PersonTest ORDER BY Id";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(persons, connection);
                sqlData.Fill(dt);
            }

            PhoneBook.DataSource = dt;
            PhoneBook.DataBind();
        }

        bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        protected void PhoneBook_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void PhoneBook_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            successMesage.Text = "";
            errorMessage.Text = "";
            if ((PhoneBook.FooterRow.FindControl("txtNameFooter") as TextBox).Text.Trim() == "" ||
                (PhoneBook.FooterRow.FindControl("txtSurnameFooter") as TextBox).Text.Trim() == "" ||
                (PhoneBook.FooterRow.FindControl("txtPhoneFooter") as TextBox).Text.Trim() == "" ||
                (PhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim() == "")
            {
                errorMessage.Text = "Заполните все поля";
            }
            else if (!isValid((PhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim()))
            {
                errorMessage.Text = "Невалидное значение email!";
            }
            else
            {
                try
                {
                    if (e.CommandName.Equals("AddNew"))
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            sqlConnection.Open();
                            string command = "INSERT INTO PersonTest (Name,Surname,Phone,Email) VALUES (@Name, @Surname, @Phone, @Email)";
                            SqlCommand add = new SqlCommand(command, sqlConnection);
                            add.Parameters.AddWithValue("@Name", (PhoneBook.FooterRow.FindControl("txtNameFooter") as TextBox).Text.Trim());
                            add.Parameters.AddWithValue("@Surname", (PhoneBook.FooterRow.FindControl("txtSurnameFooter") as TextBox).Text.Trim());
                            add.Parameters.AddWithValue("@Phone", (PhoneBook.FooterRow.FindControl("txtPhoneFooter") as TextBox).Text.Trim());
                            add.Parameters.AddWithValue("@Email", (PhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
                            add.ExecuteNonQuery();
                            LoadData();
                            successMesage.Text = "Добавлено!";
                            errorMessage.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    successMesage.Text = "";
                    errorMessage.Text = "Ошибка!";
                }
            }
        }

        protected void PhoneBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            successMesage.Text = "";
            errorMessage.Text = "";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string command = "DELETE FROM PersonTest WHERE Id = @Id";
                    SqlCommand add = new SqlCommand(command, sqlConnection);
                    add.Parameters.AddWithValue("@Id", Convert.ToInt32(PhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                    add.ExecuteNonQuery();
                    LoadData();
                    successMesage.Text = "Удалено!";
                    errorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                successMesage.Text = "";
                errorMessage.Text = "Ошибка!";
            }
        }
    }
}