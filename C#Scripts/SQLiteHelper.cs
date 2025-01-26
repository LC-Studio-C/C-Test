using System;
using Mono.Data.Sqlite;
using UnityEngine;
public class SQLiteHelper
{
    private SqliteConnection conn;
    private SqliteCommand cmd;
    public SQLiteHelper(string connectionString)
    {
        try
        {
            conn = new SqliteConnection(connectionString);
            conn.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void CloseConnection()
    {
        if (cmd != null)
        {
            cmd.Cancel();
        }
        conn.Close();
    }

    public SqliteDataReader ExecuteSql(string sql)
    {
        cmd = new SqliteCommand(sql, conn);
        return cmd.ExecuteReader();
    }

    public SqliteDataReader ReadFullTable(string tableName)
    {
        string sql = "SELECT * FROM " + tableName;
        return ExecuteSql(sql);
    }

    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        int fieldCount = ReadFullTable(tableName).FieldCount;
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length != fieldCount");
        }
        string sql = "INSERT INTO " + tableName + " VALUES(" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            sql += "," + values[i];
        }
        sql += ")";
        return ExecuteSql(sql);
    }

    public SqliteDataReader DeleteValuesOR(string tableName,string[] colNames,string[] operations, string[] colValues)
    {
        if (colValues.Length != colNames.Length || colValues.Length != operations.Length || colNames.Length != operations.Length)
        {
            throw new SqliteException("colValues.Length != colNames.Length || colValues.Length != operations.Length || colNames.Length != operations.Length");
        }
        string sql = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            sql += " OR " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteSql(sql);
    }

    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        if (colValues.Length != colNames.Length || colValues.Length != operations.Length || colNames.Length != operations.Length)
        {
            throw new SqliteException("colValues.Length != colNames.Length || colValues.Length != operations.Length || colNames.Length != operations.Length");
        }
        string sql = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            sql += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteSql(sql);
    }

    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
    {
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length != colValues.Length");
        }
        string sql = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            sql += "," + colNames[i] + "=" + colValues[i];
        }
        sql += " WHERE " + key + " " + operation + " " + value;
        return ExecuteSql(sql);
    }

    public void CreateTable(string tableName, string[] fieldName, string[] fieldType)
    {
        if (fieldName.Length != fieldType.Length)
        {
            throw new SqliteException("fieldName.Length != fieldType.Length!");
        }
        string sql = "CREATE TABLE " + tableName + "(" + fieldName[0] + " " + fieldType[0];
        for (int i = 1; i < fieldName.Length; i++)
        {
            sql += "," + fieldName[i] + " " + fieldType[i];
        }
        sql += ")";
        ExecuteSql(sql);
    }
}
