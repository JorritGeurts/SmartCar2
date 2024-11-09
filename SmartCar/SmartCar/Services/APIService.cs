using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartCar.Models;

namespace SmartCar.Services
{
    public class APIService<T>
    {
        private static readonly string BASE_URL = "http://localhost:5285/api/";
        static HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

        public static async Task<T> GetAsync(string endPoint)
        {
            try
            {
                string url = BASE_URL + endPoint;
                var response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        return JsonConvert.DeserializeObject<T>(jsonData);
                    }
                    else
                    {
                        throw new Exception("Resource Not Found");
                    }
                }
                else
                {
                    throw new Exception("Request failed with status code " + response.StatusCode);
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<int> PostAsyncAndReturnId(string endPoint, T data)
        {
            try
            {
                string url = BASE_URL + endPoint;
                var response = await client.PostAsJsonAsync(url, data);  // Send POST request

                // Check if the endpoint is for "Car"
                if (endPoint == "Car/")
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Resule =" + response);

                        var result = await response.Content.ReadFromJsonAsync<CarSeverityDTO>();  // Deserialize the response

                        var json = JsonConvert.DeserializeObject(result.CarId.ToString());
                        Console.WriteLine(json.ToString());
                        int carId = result.CarId; // Extract CarId from response

                        if (carId > 0)
                        {
                            return carId; // Return the CarId
                        }
                    }

                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Request failed with status code {response.StatusCode}: {responseContent}");
                }

                // Return a default value (0) if it's not a "Car" endpoint
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PostAsync: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<CarSeverityDTO>> GetCarSeveritiesByCarIdAsync(int carId)
        {
            try
            {
                string url = $"{BASE_URL}CarSeverity/ByCarId/{carId}";
                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // If no car severities are found (404), just return an empty list
                    Console.WriteLine("No car severities found.");
                    return new List<CarSeverityDTO>(); // or return null, based on your needs
                }
                else
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonData = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<CarSeverityDTO>>(jsonData);
                    }
                    else
                    {
                        throw new Exception($"Error fetching car severities: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCarSeveritiesByCarIdAsync: {ex.Message}");
                throw;
            }
        }


        public static async Task PostAsync(string endPoint, T data)
        {
            try
            {
                string url = BASE_URL + endPoint;
                var response = await client.PostAsJsonAsync(url, data);

                var json = JsonConvert.SerializeObject(response);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine("Request Content: " + json);


                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Request failed with status code {response.StatusCode}: {responseContent}");
                }

            }
            catch (Exception ex)
            {
                // Log the exception details (using your logging framework)
                Console.WriteLine($"Error in PostAsync: {ex.Message}");
                throw; // Re-throw the exception
            }
        }

        public static async Task PutAsync(string endPoint, T data)
        {
            try
            {
                string url = BASE_URL + endPoint;
                var response = await client.PutAsJsonAsync(url, data);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Request failed with status code " + response.StatusCode);
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string endPoint)
        {
            try
            {
                string url = BASE_URL + endPoint;
                var response = await client.DeleteAsync(url);

                // Check if the response is successful (NoContent or OK)
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent &&
                    response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Request failed with status code {response.StatusCode}: {errorContent}");
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during DELETE request: {ex.Message}");
                throw;
            }
        }
    }
}
