using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project4thYear.Models;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;

namespace Project4thYear.Controllers
{
    public class Home3Controller : Controller
    {
        // GET: Home3
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Data3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Data3(HttpPostedFileBase FileUpload)
        {
            // Set up DataTable place holder
            DataTable dt = new DataTable();

            //check we have a file
            if (FileUpload.ContentLength > 0)
            {
                //Workout our file path
                string fileName = Path.GetFileName(FileUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

                //Try and upload
                try
                {
                    FileUpload.SaveAs(path);
                    //Process the CSV file and capture the results to our DataTable place holder
                    dt = ProcessCSV(path);

                    //Process the DataTable and capture the results to our SQL Bulk copy
                    ViewData["Feedback"] = ProcessBulkCopy(dt);
                }
                catch (Exception ex)
                {
                    //Catch errors
                    ViewData["Feedback"] = ex.Message;
                }
            }
            else
            {
                //Catch errors
                ViewData["Feedback"] = "Please select a file";
            }

            //Tidy up
            dt.Dispose();

            return View("Data3", ViewData["Feedback"]);
        }



        private static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            line = sr.ReadLine();
            strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }
        public static String ProcessBulkCopy(DataTable dt)
        {
            string Feedback = string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["FootballContext"].ConnectionString;

            //make our connection and dispose at the end
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //make our command and dispose at the end
                using (var copy = new SqlBulkCopy(conn))
                {

                    //Open our connection
                    conn.Open();

                    ///Set target table and tell the number of rows
                    //copy.DestinationTableName = "BulkImportDetails";

                    //copy.DestinationTableName = "League";
                    //copy.DestinationTableName = "Team";
                    copy.DestinationTableName = "Player";

                    copy.BatchSize = dt.Rows.Count;
                    try
                    {
                        //Send it to the server
                        copy.WriteToServer(dt);
                        Feedback = "Upload complete";
                    }
                    catch (Exception ex)
                    {
                        Feedback = ex.Message;
                    }
                }
            }

            return Feedback;
        }
    }
}