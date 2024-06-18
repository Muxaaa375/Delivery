using MySql.Data.MySqlClient;
using System.Windows;
using System;

public class Connection
{
    public static readonly string confing = "server=127.0.0.1;port=3308;uid=root;pwd=;database=Delivery;";
    public static MySqlConnection OpenConnection()
    {
        try
        {
            MySqlConnection connection = new MySqlConnection(confing);
            connection.Open();
            return connection;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show($"Ошибка при открытии соединения: {ex.Message}", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Не удалось открыть соединение с базой данных.", ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Общая ошибка при открытии соединения: {ex.Message}", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Произошла непредвиденная ошибка при открытии соединения.", ex);
        }
    }

    public static MySqlDataReader Query(string SQL, MySqlConnection connection)
    {
        try
        {
            MySqlCommand command = new MySqlCommand(SQL, connection);
            return command.ExecuteReader();
        }
        catch (MySqlException ex)
        {
            MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка запроса", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Ошибка выполнения запроса к базе данных.", ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Общая ошибка выполнения запроса: {ex.Message}", "Ошибка запроса", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Произошла непредвиденная ошибка при выполнении запроса.", ex);
        }
    }

    public static void CloseConnection(MySqlConnection connection)
    {
        try
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                MySqlConnection.ClearPool(connection);
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show($"Ошибка при закрытии соединения: {ex.Message}", "Ошибка закрытия соединения", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Не удалось закрыть соединение с базой данных.", ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Общая ошибка при закрытии соединения: {ex.Message}", "Ошибка закрытия соединения", MessageBoxButton.OK, MessageBoxImage.Error);
            throw new Exception("Произошла непредвиденная ошибка при закрытии соединения.", ex);
        }
    }
}