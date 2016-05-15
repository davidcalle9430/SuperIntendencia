using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebSuperIntendencia.Models;

namespace WebSuperIntendencia.ProxyRS
{
    public class ProxyTransacciones
    {
        public static string REST_PATH = "http://localhost:26186/";
        public static string TRANSACCIONES_PATH = "api/Transaccions/";
        public static string TRANSACCIONES_USUARIO_PATH = TRANSACCIONES_PATH + "Usuario/";
        HttpClient client;
        public ProxyTransacciones()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(REST_PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public List<Transaccion> darTransacciones()
        {
            HttpResponseMessage response = client.GetAsync(TRANSACCIONES_PATH).Result;

            if (response.IsSuccessStatusCode)
            {
                var transacciones = response.Content.ReadAsAsync<List<Transaccion>>().Result;
                return transacciones;
            }
            return null;
        }


        public List<Transaccion> darTransaccionesUsuario( int idUsuario)
        {
            HttpResponseMessage response = client.GetAsync(TRANSACCIONES_USUARIO_PATH + idUsuario).Result;

            if (response.IsSuccessStatusCode)
            {
                var transacciones = response.Content.ReadAsAsync<List<Transaccion>>().Result;
                return transacciones;
            }
            return null;
        }
        /*
        public List<Usuario> darUsuarios(int pagina, int tamPagina)
        {
            HttpResponseMessage response = client.GetAsync(USUARIOS_PATH + $"?pageIndex={pagina}&pageSize={tamPagina}").Result;
            if (response.IsSuccessStatusCode)
            {
                var usuarios = response.Content.ReadAsAsync<List<Usuario>>().Result;
                return usuarios;
            }
            return null;
        }*/

        public Transaccion buscarTransaccion(int id)
        {
            HttpResponseMessage response = client.GetAsync( TRANSACCIONES_PATH + id ).Result;
            if (response.IsSuccessStatusCode)
            {
                var transaccion = response.Content.ReadAsAsync<Transaccion>().Result;
                return transaccion;
            }
            return null;
        }

        public Transaccion crearTransaccion(Transaccion transaccion)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(transaccion), System.Text.Encoding.UTF8, "application/json");
            // la linea anterior convierte el objeto en un JSON
            var response = client.PostAsync( TRANSACCIONES_PATH , content ).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                transaccion = JsonConvert.DeserializeObject<Transaccion>(data);
                return transaccion;
            }
            return null;
        }


        public bool EditarTransaccion(Transaccion transaccion)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(transaccion), System.Text.Encoding.UTF8, "application/json");
            var urll = REST_PATH + TRANSACCIONES_PATH + transaccion.Id;
            var response = client.PutAsync(TRANSACCIONES_PATH + transaccion.Id, content).Result;
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public bool EliminarTransaccion(int usuario)
        {
            var response = client.DeleteAsync( TRANSACCIONES_PATH + usuario ).Result;
            if ( response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent )
            {
                return true;
            }
            return true;
        }

    }
}