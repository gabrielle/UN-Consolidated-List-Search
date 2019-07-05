using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace UNLookup
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        //define sql connection
        public SqlConnection con;
        public string constr;

        public void Connection()
        {
            constr = ConfigurationManager.ConnectionStrings["UNLookup"].ToString();
            con = new SqlConnection(constr);
            con.Open();

        }

        //hide the text at the bottom of the tables on page load
        protected void Page_Load(object sender, EventArgs e)
        {
            resultstext.Visible = false;
            aliastext.Visible = false;
            lbl_namematch.Visible = false;
            lbl_aliasmatch.Visible = false;
        }

        //method used to bind the results into the grid view
        private void rep_bind(string query, GridView dt)
        {
            Connection();

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt.DataSource = ds;
            dt.DataBind();

        }

        //search button: when clicked populated log table, and searches both alias and name tables
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(namesearch.Text) || dd_type.SelectedValue == "Select Client Type")
            {
              
            }

            else
            {

                //define variables for log table
                String username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                String insertquery = ("INSERT INTO SearchLog (Username, Lookup_Value, Search_Time) values (@username, @lookupvalue, @lookupdate)");
                DateTime time = DateTime.Now;

                Connection();
                //insert the query into log table
                using (SqlCommand com = new SqlCommand(insertquery, con))
                {
                    com.Parameters.AddWithValue("@username", username);
                    com.Parameters.AddWithValue("@lookupvalue", namesearch.Text);
                    com.Parameters.AddWithValue("@lookupdate", time);

                    com.ExecuteNonQuery();

                }

                //build queries to search name and alias tables
                string query1;
                string query2;

                //condition based on whether Individial or Entity was chosen
                //build individual query
                if (dd_type.SelectedValue == "Individual")
                {
                    query1 = "select dataid, ind_full_name, UN_LIST_TYPE, REFERENCE_NUMBER, LISTED_ON, GENDER, LIST_TYPE, DATE_OF_BIRTH, COUNTRY from Individual where ind_Full_Name like '%" + namesearch.Text.Replace("'","''") + "%'";
                    query2 = "select dataid, alias_name, UN_LIST_TYPE, REFERENCE_NUMBER, LISTED_ON, GENDER, LIST_TYPE, DATE_OF_BIRTH, COUNTRY  from IndividualAlias where Alias_Name like'%" + namesearch.Text.Replace("'", "''") + "%'";
                }

                //build entity query
                else
                {
                    query1 = "select *  from Entity where First_Name like '%" + namesearch.Text.Replace("'", "''") + "%'";
                    query2 = "select *  from EntityAlias where Alias_Name like'%" + namesearch.Text.Replace("'", "''") + "%'";
                }

                //call rep_bind to populate gridviews based on queries built above
                using (SqlCommand com = new SqlCommand(query1, con))
                {
                    SqlDataReader dr;
                    dr = com.ExecuteReader();

                    //if name table has results, populate grid
                    if (dr.HasRows)
                    {

                        dr.Read();

                        rep_bind(query1, dt_namematch);
                        dt_namematch.Visible = true;

                        resultstext.Text = "";
                    }

                    //if name table has no results, show results text
                    else
                    {
                        dt_namematch.Visible = false;
                        resultstext.Visible = true;
                        resultstext.Text = "The search term " + namesearch.Text + " is not available in the records";

                    }
                }

                //populate alias gridview
                using (SqlCommand com2 = new SqlCommand(query2, con))
                {
                    SqlDataReader dr2;
                    com2.CommandText = query2;
                    dr2 = com2.ExecuteReader();

                    //if there are results, display alias gridview
                    if (dr2.HasRows)
                    {
                        dr2.Read();

                        rep_bind(query2, dt_aliasmatch);
                        dt_aliasmatch.Visible = true;

                        aliastext.Text = "";
                    }

                    //if there are no results, display the alias results text
                    else
                    {
                        dt_aliasmatch.Visible = false;
                        aliastext.Visible = true;
                        aliastext.Text = "The search term " + namesearch.Text + " is not available in the records"; ;

                    }
                } //end of populate alias gridview

                lbl_aliasmatch.Visible = true;
                lbl_namematch.Visible = true;
            }// end of search not null
        }// end of search button

    }
}

