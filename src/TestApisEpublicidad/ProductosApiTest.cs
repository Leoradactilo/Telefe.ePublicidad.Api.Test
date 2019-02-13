using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestApisEpublicidad
{
    [TestClass]
    public class ProductosApiTest
    {
        //Servidores de prueba 
        private const string BaseAddress = @"https://api.publife.desa.telefe.com.ar";
        private const string Route = @"/api/Productos";
        
        /*Mapeo de entidades resultantes*/
        public class ProductoTestModel
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public long IdFamilia { get; set; }
            public string Codigo { get; set; }
            public virtual IList<long> Anunciantes { get; set; }
            public ProductoTestModel()
            { }
        }

        /// <summary>
        /// Alta de un producto
        /// <param name="Codigo">Null</param>
        /// <param name="Nombre">Obligatorio, type => string</param>
        /// <param name="Descripcion">Obligatorio, type => string</param>
        /// <param name="IdFamilia">Obligatorio, type => long</param>
        /// <param name="Anunciantes">Obligatorio, type => Array<long></param>
        /// </summary>
        [TestMethod]
        public void PostProducto()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(12, 0, 0);

                //El id de los anunciantes sale del servicio de clientes => http://tlfepubapi61.tlfextranet.local:8062/odata/Clientes
                //El id de la familia sale del servicio de familias => http://tlfepubapi61.tlfextranet.local:8062/odata/Familias

                //No poner codigo cuando se hace el post y resultado en el caso de ser valido va a tener el codigo del producto

                /* OPCION 1 PASAR UN MODEL Y SERIALIZARLO CON NEWTONSOFT JSON*/
                ProductoTestModel testModel = new ProductoTestModel()
                {
                    Nombre = "Test HAVAS Nombre Producto",
                    Descripcion = "Test HAVAS Description Producto",
                    IdFamilia = 10409,
                    //No pasar un codigo en el alta del producto
                    Codigo = "",
                    Anunciantes = new List<long>() { 2514 }
                };

                string aux = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);
                /* */

                /* OPCION 2 PASAR UN STRING PLANO */
                //string aux1 = "{\"Nombre\":\"Test HAVAS Nombre Producto\",\"Descripcion\":\"Test HAVAS Description Producto\",\"IdFamilia\":10409,\"Codigo\":\"\",\"Anunciantes\":[2514]}";

                HttpContent contentPost = new StringContent(aux, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(Route, contentPost).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                ProductoTestModel productoResult = response.Content.ReadAsAsync<ProductoTestModel>().Result;

                Assert.IsTrue(productoResult != null);

                if (productoResult != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(productoResult.Nombre));
                    Assert.IsTrue(!string.IsNullOrEmpty(productoResult.Codigo));
                    Assert.IsTrue(!string.IsNullOrEmpty(productoResult.Descripcion));
                }
            }
        }

        /// <summary>
        /// Obtiene un producto a partir de su codigo
        /// <param name="codigo">Codigo (obligatorio), type => string (guid)</param>
        /// </summary>
        [TestMethod]
        public void GetProducto()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string code = "e68c99f1-290c-4b6f-bba6-3940d2830056";
                string queryUrl = string.Format("{0}/{1}", Route, code);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                ProductoTestModel producto = response.Content.ReadAsAsync<ProductoTestModel>().Result;
                Assert.IsTrue(producto != null);

                if (producto != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(producto.Nombre));
                    Assert.IsTrue(!string.IsNullOrEmpty(producto.Codigo));
                    Assert.IsTrue(!string.IsNullOrEmpty(producto.Descripcion));

                }
            }
        }

        /// <summary>
        /// Obtiene una lista a partir del Id del anunciante
        /// <param name="IdAnunciante">IdAnunciante (obligatorio) , type => long</param>
        /// <param name="activo">opcional (default true), type => bool</param>
        /// </summary>
        [TestMethod]
        public void GetProductos()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string filter = "getbyfilters";
                long idHavas = 23744;
                bool activo = true;
                string queryUrl = string.Format("{0}/{1}/{2}/{3}", Route, filter, idHavas, activo);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                List<ProductoTestModel> productos = response.Content.ReadAsAsync<List<ProductoTestModel>>().Result;

                Assert.IsTrue(productos != null);
            }
        }

        /// <summary>
        /// Edicion de un producto
        /// <param name="Codigo">Codigo (Obligatorio)</param>
        /// <param name="Nombre">Obligatorio, type => string</param>
        /// <param name="Descripcion">Obligatorio, type => string</param>
        /// <param name="IdFamilia">Obligatorio, type => long</param>
        /// <param name="Anunciantes">Obligatorio, type => IList<long></param>
        /// </summary>
        [TestMethod]
        public void PutProducto()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(12, 0, 0);

                /* OPCION 1 USAR UN MODEL Y SERIALIZARLO CON NEWTONSOFT JSON*/
                ProductoTestModel testModel = new ProductoTestModel()
                {
                    Nombre = "Test HAVAS Nombre Producto",
                    Descripcion = "Test HAVAS Description Producto",
                    IdFamilia = 10409,
                    Codigo = "",
                    Anunciantes = new List<long>() { 2514 }
                };
                
                string aux = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);
                /**/

                /* OPCION 2 USAR UN STRING PLANO */
                string aux1 = "{\"Nombre\":\"Test HAVAS Nombre Producto\",\"Descripcion\":\"Test HAVAS Description Producto\",\"IdFamilia\":10409,\"Codigo\":\"\",\"Anunciantes\":[2514]}";

                HttpContent contentPost = new StringContent(aux, Encoding.UTF8, "application/json");

                string code = "d65947fc-ef3c-4fe6-9cc7-460b0c5e769b";
                string queryUrl = string.Format("{0}/{1}", Route, code);

                HttpResponseMessage response = client.PutAsync(queryUrl, contentPost).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        /// <summary>
        /// El token generado debe de tener las comillas dobles (") escapadas por ejemplo (\")
        /// </summary>
        /// <returns></returns>
        public string GetSamlToken()
        {
            string tokenResult = "";

            return tokenResult;
        }
    }
}
