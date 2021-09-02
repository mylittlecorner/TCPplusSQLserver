using System;
using System.Data.SqlClient;

class SQLConn{
  private readonly SqlConnection _conn;
  public SQLConn(String SqlConnStr){
    _conn=new SqlConnection(SqlConnStr);
  }

  public void DbOpen(){
    Console.WriteLine("Getting Connection ...");
    try{
        Console.WriteLine("Openning Connection ...");
        _conn.Open();
        Console.WriteLine("Connection successful!");
    }catch (Exception e){
        Console.WriteLine("Error: " + e.Message);
    }
  }

  public void DbClose(){
    _conn.Close();
  }

  public void InsertValues(String VAL){
    DateTime CurrentDate;
    CurrentDate = DateTime.Now;
    try{
       using (SqlCommand command = new SqlCommand(
       "INSERT INTO Text (Val,TS) VALUES (@myval,@myts)",
        _conn)){
              command.Parameters.AddWithValue("@myval",VAL);
              command.Parameters.AddWithValue("@myts", CurrentDate);
              command.ExecuteNonQuery();
      }
    }catch(Exception e){
      Console.WriteLine("Insertion failed. " + e);
    }

  }


  public void TruncateTable(){
    try{
        using (SqlCommand command = new SqlCommand(
        "TRUNCATE TABLE Text",
         _conn)){
          command.ExecuteNonQuery();
        }
     }catch{
       Console.WriteLine("Table not truncated.");
     }
  }


}
