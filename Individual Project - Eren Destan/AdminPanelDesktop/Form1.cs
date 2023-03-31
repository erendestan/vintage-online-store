using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace AdminPanelDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ViewAllVinyls();
            ViewAllUsers();
            ViewAllVintageProducts();
        }

        public int ProductID;
        public int MemberID;

        public void ViewAllUsers()
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Members", connection);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView3.DataSource = dt;
                dataGridView3.Columns[0].HeaderText = "Id";
                dataGridView3.Columns[1].HeaderText = "Name";
                dataGridView3.Columns[2].HeaderText = "Email";
                dataGridView3.Columns[3].HeaderText = "Password";
                dataGridView3.Columns[4].HeaderText = "DateOfBirth";
                dataGridView3.Columns[5].HeaderText = "Address";

            }
        }

        public void ViewAllVinyls()
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select Products.ID, Products.Name, Vinyls.VinylType, Vinyls.Artist, Genre.genreName, Vinyls.ReleaseDate, Products.PriceInCents, Products.Stock from Products inner join Vinyls on Products.ID = Vinyls.ProductID inner join Genre on Vinyls.genreId = Genre.genreId", connection);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText= "ID";
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "vinylType";
                dataGridView1.Columns[3].HeaderText = "artist";
                dataGridView1.Columns[4].HeaderText = "genreName";
                dataGridView1.Columns[5].HeaderText = "releaseDate";
                dataGridView1.Columns[6].HeaderText = "PriceInCents";
                dataGridView1.Columns[7].HeaderText = "Stock";

            }  
        }
        public void ViewAllVintageProducts()
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Products.ID, Products.Name, ItemType.itemTypeName, VintageShoppingProducts.description, Products.PriceInCents, Products.Stock from Products inner join VintageShoppingProducts on Products.ID = VintageShoppingProducts.ProductID inner join ItemType on VintageShoppingProducts.itemTypeId = ItemType.itemTypeId", connection);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;
                dataGridView2.Columns[0].HeaderText = "ID";
                dataGridView2.Columns[1].HeaderText = "Name";
                dataGridView2.Columns[2].HeaderText = "itemTypeName";
                dataGridView2.Columns[3].HeaderText = "description";
                dataGridView2.Columns[4].HeaderText = "PriceInCents";
                dataGridView2.Columns[5].HeaderText = "Stock";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string name = nameTb.Text;
            string email = emailTb.Text;
            string password = passwordTb.Text;
            DateTime dateTime = dateOfBirthDtp.Value;
            string formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string address = addressTb.Text;

            SqlCommand cmd = new SqlCommand($"INSERT INTO Members (Name, Email, Password, DateOfBirth, Address) VALUES ('{name}', '{email}', '{password}', '{formattedDateTime}' , '{address}')", connection);

            cmd.ExecuteNonQuery();
            connection.Close();
            ViewAllUsers();
            MessageBox.Show("Succesfully Saved!");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            string albumName = albumNameTb.Text;
            int vinylType = Convert.ToInt32(vinylTypeCb.Text);
            string artistName = artistTb.Text;
            int selectedGenre = genreCb.SelectedIndex + 1;
            DateTime releaseDate = releaseDateDtp.Value;
            string formattedReleaseDate = releaseDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int priceInCents = Convert.ToInt32(priceInCentsNud.Value);
            int stock = Convert.ToInt32(stockNud.Value);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command1 = new SqlCommand($"INSERT INTO Products (name, priceInCents, stock) VALUES ('{albumName}', {priceInCents}, {stock})", connection);
                    SqlCommand command2 = new SqlCommand($"INSERT INTO Vinyls (VinylType, Artist, genreId, ReleaseDate, ProductID) VALUES ({vinylType}, '{artistName}', {selectedGenre}, '{formattedReleaseDate}',SCOPE_IDENTITY())",connection);
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    connection.Close();
                    ViewAllVinyls();
                    MessageBox.Show("Vinyl Succesfully Added!");
                    
                }
                catch (Exception ex)
                {
                    connection.Close();
                    //If an error occurs, roll back the transaction
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            string productName = productNameTb.Text;
            int selectedProductType = productTypeCb.SelectedIndex + 1;
            string productDecription = descriptionRtb.Text;
            int productPriceInCents = Convert.ToInt32(priceInCentsProductNud.Value);
            int productStock = Convert.ToInt32(stockProductNud.Value);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command1 = new SqlCommand($"INSERT INTO Products (name, priceInCents, stock) VALUES ('{productName}', {productPriceInCents}, {productStock})", connection);
                    SqlCommand command2 = new SqlCommand($"INSERT INTO VintageShoppingProducts (itemTypeId, description, ProductID) VALUES ({selectedProductType}, '{productDecription}',SCOPE_IDENTITY())", connection);
                    
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    connection.Close();
                    ViewAllVintageProducts();
                    MessageBox.Show("Vintage Product Succesfully Added!");
                }
                catch (Exception ex)
                {
                    connection.Close();
                    //If an error occurs, roll back the transaction
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString(); //Name
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString(); //VinylType
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString(); //Artist
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); //Genre
            dateTimePicker2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString(); //DateTime
            numericUpDown2.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[6].Value); //Price
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[7].Value); //Stock
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            string albumName = textBox5.Text;
            int vinylType = Convert.ToInt32(comboBox1.Text);
            string artistName = textBox6.Text;
            int selectedGenre = comboBox2.SelectedIndex + 1;
            DateTime releaseDate = dateTimePicker2.Value;
            string formattedReleaseDate = releaseDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int priceInCents = Convert.ToInt32(numericUpDown2.Value);
            int stock = Convert.ToInt32(numericUpDown1.Value);

            if (ProductID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"UPDATE Products SET Name = @name, PriceInCents = @priceInCents, Stock = @stock WHERE ID = @ID", connection);
                        SqlCommand command2 = new SqlCommand($"UPDATE Vinyls SET VinylType = @VinylType, Artist = @Artist, genreId = @genreId, ReleaseDate = @ReleaseDate WHERE ProductID = @ID", connection);

                        //SqlCommand command1 = new SqlCommand($"UPDATE Products SET (Name = @name, PriceInCents = @priceInCents, Stock = @stock) VALUES ('{albumName}', {priceInCents}, {stock})", connection);
                        //SqlCommand command2 = new SqlCommand($"UPDATE Vinyls (VinylType = @VinylType, Artist = @Artist, genreId = @genreId, ReleaseDate = @ReleaseDate, ProductId = @ProductID) VALUES ({vinylType}, '{artistName}', {selectedGenre}, '{formattedReleaseDate}',SCOPE_IDENTITY())", connection);
                        command1.Parameters.AddWithValue("@name", albumName);
                        command1.Parameters.AddWithValue("@priceInCents", priceInCents);
                        command1.Parameters.AddWithValue("@stock", stock);
                        command2.Parameters.AddWithValue("@VinylType",vinylType);
                        command2.Parameters.AddWithValue("@Artist",artistName);
                        command2.Parameters.AddWithValue("@genreId",selectedGenre);
                        command2.Parameters.AddWithValue("@ReleaseDate",releaseDate);
                        command1.Parameters.AddWithValue("ID",this.ProductID);
                        command2.Parameters.AddWithValue("ID", this.ProductID);


                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        connection.Close();
                        ViewAllVinyls();
                        MessageBox.Show("Vinyl Succesfully Updated!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            if (ProductID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"DELETE FROM Products WHERE ID = @ID", connection);
                        SqlCommand command2 = new SqlCommand($"DELETE FROM Vinyls WHERE ProductID = @ID", connection);


                        command2.Parameters.AddWithValue("ID", this.ProductID);
                        command1.Parameters.AddWithValue("ID", this.ProductID);

                        command2.ExecuteNonQuery();
                        command1.ExecuteNonQuery();
                        
                        connection.Close();
                        ViewAllVinyls();
                        MessageBox.Show("Vinyl Succesfully Deletedd!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Vinyl Selected!");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
            textBox7.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString(); //Name
            comboBox3.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString(); //ProductType
            richTextBox1.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString(); //Description
            numericUpDown4.Value = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[4].Value); //Price
            numericUpDown3.Value = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[5].Value); //Stock
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemberID = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString(); //Name
            textBox3.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString(); //Email
            textBox2.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString(); //Password
            dateTimePicker1.Text = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();//DateOfBirth
            textBox4.Text = dataGridView3.SelectedRows[0].Cells[5].Value.ToString(); //Address
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            string productName = textBox7.Text;
            int selectedProductType = comboBox3.SelectedIndex + 1;
            string productDecription = richTextBox1.Text;
            int productPriceInCents = Convert.ToInt32(numericUpDown4.Value);
            int productStock = Convert.ToInt32(numericUpDown3.Value);            

            if (ProductID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"UPDATE Products SET Name = @name, PriceInCents = @priceInCents, Stock = @stock WHERE ID = @ID", connection);
                        SqlCommand command2 = new SqlCommand($"UPDATE VintageShoppingProducts SET itemTypeId = @itemTypeId, description = @description WHERE ProductID = @ID", connection);

                        
                        command1.Parameters.AddWithValue("@name", productName);
                        command1.Parameters.AddWithValue("@priceInCents", productPriceInCents);
                        command1.Parameters.AddWithValue("@stock", productStock);
                        command2.Parameters.AddWithValue("@itemTypeId", selectedProductType);
                        command2.Parameters.AddWithValue("@description", productDecription);
                        command1.Parameters.AddWithValue("ID", this.ProductID);
                        command2.Parameters.AddWithValue("ID", this.ProductID);


                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        connection.Close();
                        ViewAllVintageProducts();
                        MessageBox.Show("Vintage Product Succesfully Updated!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            if (ProductID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"DELETE FROM Products WHERE ID = @ID", connection);
                        SqlCommand command2 = new SqlCommand($"DELETE FROM VintageShoppingProducts WHERE ProductID = @ID", connection);


                        command2.Parameters.AddWithValue("ID", this.ProductID);
                        command1.Parameters.AddWithValue("ID", this.ProductID);

                        command2.ExecuteNonQuery();
                        command1.ExecuteNonQuery();

                        connection.Close();
                        ViewAllVintageProducts();
                        MessageBox.Show("Vintage Product Succesfully Deleted!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Vintage Product Selected!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";

            string name = textBox1.Text;
            string email = textBox3.Text;
            string password = textBox2.Text;
            DateTime dateTime = dateTimePicker1.Value;
            string formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string address = textBox4.Text;

            if (MemberID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"UPDATE Members SET Name = @Name, Email = @Email, Password = @Password, DateOfBirth = @DateOfBirth, Address = @Address WHERE Id = @Id", connection);


                        command1.Parameters.AddWithValue("@Name", name);
                        command1.Parameters.AddWithValue("@Email", email);
                        command1.Parameters.AddWithValue("@Password", password);
                        command1.Parameters.AddWithValue("@DateOfBirth", formattedDateTime);
                        command1.Parameters.AddWithValue("@Address", address);
                        command1.Parameters.AddWithValue("Id", this.MemberID);



                        command1.ExecuteNonQuery();
                        connection.Close();
                        ViewAllUsers();
                        MessageBox.Show("User Succesfully Updated!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mssqlstud.fhict.local; TrustServerCertificate=True;Initial Catalog=dbi504303_indproject;User ID=dbi504303_indproject;Password=123456789";
            if (MemberID > 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        SqlCommand command1 = new SqlCommand($"DELETE FROM Members WHERE Id = @Id", connection);


                        command1.Parameters.AddWithValue("Id", this.MemberID);
                        command1.ExecuteNonQuery();

                        connection.Close();
                        ViewAllUsers();
                        MessageBox.Show("User Succesfully Deleted!");

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        //If an error occurs, roll back the transaction
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No User Selected!");
            }
        }
    }
}