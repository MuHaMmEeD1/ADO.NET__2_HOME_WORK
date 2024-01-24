using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace ADO.NET__2_HOME_WORK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DataTable DataTable { get; set; } 

        public MainWindow()
        {

            InitializeComponent();

            SqlConnection sql = new SqlConnection("Data Source=DESKTOP-RV3QOTA\\SQLEXPRESS;Initial Catalog=\"Library \";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Authors", sql);
            DataTable = new DataTable();
            sqlDataAdapter.Fill(DataTable);

            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DataTable.Clear();

            SqlConnection sql = new SqlConnection("Data Source=DESKTOP-RV3QOTA\\SQLEXPRESS;Initial Catalog=\"Library \";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Authors AS A WHERE A.FirstName = @name", sql);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Authors AS A WHERE A.FirstName Like @name", sql);

            string name = "%"+ComboBox_1.Text + "%";
            sqlCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = name });

            sqlCommand.Parameters["@name"].SourceColumn = "FirstName";
            sqlCommand.Parameters["@name"].SourceVersion = DataRowVersion.Current;
   

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            sqlDataAdapter.Fill(DataTable);




        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sql = new SqlConnection("Data Source=DESKTOP-RV3QOTA\\SQLEXPRESS;Initial Catalog=\"Library \";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False")) { 



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Authors", sql);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.Update(DataTable);
        }

     
        }
    }
}


