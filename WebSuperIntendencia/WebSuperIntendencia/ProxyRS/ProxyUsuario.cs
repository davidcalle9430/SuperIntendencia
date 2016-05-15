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
    public class ProxyUsuario
    {
        public static string REST_PATH = "http://localhost:26186/";
        public static string USUARIOS_PATH = "api/Usuarios/";
        HttpClient client;
        
        public ProxyUsuario()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(REST_PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Usuario> darUsuarios()
        {
            HttpResponseMessage response =  client.GetAsync(USUARIOS_PATH).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var usuarios =  response.Content.ReadAsAsync<List<Usuario>>().Result;
                return usuarios;
            }
            return null;
        }


        public List<Usuario> darUsuarios( int pagina , int tamPagina )
        {
            HttpResponseMessage response = client.GetAsync( USUARIOS_PATH + $"?pageIndex={pagina}&pageSize={tamPagina}" ).Result;
            if (response.IsSuccessStatusCode)
            {
                var usuarios = response.Content.ReadAsAsync<List<Usuario>>().Result;
                return usuarios;
            }
            return null;
        }

        public  Usuario buscarUsuario( int id)
        {
            HttpResponseMessage response = client.GetAsync(USUARIOS_PATH + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var usuario = response.Content.ReadAsAsync< Usuario >().Result;
                return usuario;
            }
            return null;
        }

        public Usuario crearUsaurio(Usuario usuario)
        {
            HttpContent content = new StringContent( JsonConvert.SerializeObject(usuario) , System.Text.Encoding.UTF8 , "application/json" );
            // la linea anterior convierte el objeto en un JSON
            var response = client.PostAsync( USUARIOS_PATH , content ).Result;
            if ( response.IsSuccessStatusCode )
            {
                string data =  response.Content.ReadAsStringAsync().Result;
                usuario = JsonConvert.DeserializeObject<Usuario>( data );
                return usuario;
            }
            return null;
        }


        public bool EditarUsuario( Usuario usuario)
        {
            HttpContent content = new StringContent( JsonConvert.SerializeObject(usuario) , System.Text.Encoding.UTF8 , "application/json" );
            var urll = REST_PATH + USUARIOS_PATH + usuario.Id;
            var response = client.PutAsync( USUARIOS_PATH + usuario.Id , content ).Result;
            Console.WriteLine( USUARIOS_PATH + usuario.Id );
            if ( response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent )
            {
                return true;
            }
            return false;
        }

        public bool EliminarUsuario( int usuario)
        {
            var response = client.DeleteAsync( USUARIOS_PATH + usuario ).Result;
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return true;
        }
    }
}