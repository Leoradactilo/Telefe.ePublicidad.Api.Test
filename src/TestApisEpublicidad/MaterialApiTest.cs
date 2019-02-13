using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TestApisEpublicidad
{
    [TestClass]
    public class MaterialApiTest
    {
        //Servidores de prueba 
        private const string BaseAddress = @"https://api.publife.desa.telefe.com.ar";
        private const string Route = @"/api/Materiales";
        

        /*Mapeo de entidades resultantes*/
        public class MaterialTestModel
        {
            //Campo solo informativo no editable
            public string Numero { get; set; }
            public string Tema { get; set; }
            public long? IdClienteAgencia { get; set; }
            public long? IdClienteAnunciante { get; set; }
            public int? DuracionReal { get; set; }
            public bool DespuesHorarioProtMenor { get; set; }
            public bool? BebidasAlcoholicas { get; set; }
            public DateTime? VigenciaDesde { get; set; }
            public DateTime? VigenciaHasta { get; set; }
            //Campo solo informativo no editable
            public long? IdPrograma { get; set; }
            //Campo solo informativo no editable
            public string CodigoTipoMaterial { get; set; }
            //Campo solo informativo no editable
            public string PathImage { get; set; }
            //Campo solo informativo no editable
            public long? IdMaterialComercial { get; set; }
            //Campo solo informativo no editable
            public int Version { get; set; }
            public string Observaciones { get; set; }
            public string RegistroAFSCA { get; set; }
            public string Codigo { get; set; }
            public IList<MaterialProductoTestModel> Productos { get; set; }
            public IList<string> TiposPublicidad { get; set; }

            public MaterialTestModel()
            { }
        }
        public class MaterialProductoTestModel
        {
            public ProductoTestModel Producto { get; set; }
            public MaterialProductoTestModel()
            { }
        }
        public class ProductoTestModel
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public long IdFamilia { get; set; }
            //Campo solo informativo no editable
            public string Codigo { get; set; }
            //Campo solo informativo no editable
            public int Version { get; set; }
            public virtual IList<long> Anunciantes { get; set; }
            public ProductoTestModel()
            { }
        }

        /// <summary>
        /// Alta de un material
        /// </summary>
        /// <param name="Tema"> type => string</param>
        /// <param name="IdClienteAgencia">type => long</param>
        /// <param name="IdClienteAnunciante">type => long</param>
        /// <param name="DuracionReal">type => int</param>
        /// <param name="DespuesHorarioProtMenor">type => bool</param>
        /// <param name="BebidasAlcoholicas">type => bool</param>
        /// <param name="VigenciaDesde">type => DateTime(yyyy,MM,dd)</param>
        /// <param name="VigenciaHasta">type => DateTime(yyyy,MM,dd)</param>
        /// <param name="CodigoTipoMaterial">type => string</param>
        /// <param name="Observaciones">type => string</param>
        /// <param name="RegistroAFSCA">type => string</param>
        /// <param name="TiposPublicidad">type => Array (string)</param>
        /// <param name="Productos">type => Array (MaterialProducto)</param>
        /// <param name="activo">type => bool</param>
        /// <param name="MaterialProducto">type => Array(Producto)</param>
        /// <returns></returns>
        [TestMethod]
        public void PostMaterial()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(12, 0, 0);

                /* OPCION 1 USAR UN MODEL Y SERIALIZARLO CON NEWTONSOFT JSON */
                MaterialTestModel testModel = new MaterialTestModel()
                {
                    Tema = "Test Api 3",
                    //Los id de IdClienteAgencia, IdClienteAnunciante y IdClienteFacturable se obtienen del servicio de consulta de clientes (segurizado solo se si tiene acceso a los que corresponda)
                    //El id de los anunciantes y agencia sale del servicio de clientes => http://tlfepubapi61.tlfextranet.local:8062/odata/Clientes
                    IdClienteAgencia = 2514,
                    IdClienteAnunciante = 59,
                    DuracionReal = 10,
                    DespuesHorarioProtMenor = true,
                    BebidasAlcoholicas = true,
                    VigenciaDesde = new DateTime(2019, 03, 01),
                    VigenciaHasta = new DateTime(2019, 03, 31),
                    //Poner COM => COMERCIAL
                    CodigoTipoMaterial = "COM",
                    Observaciones = "Test 3 Observaciones",
                    //obligatorio
                    RegistroAFSCA = "",
                    //El codigo del tipo de publicidad se obtiene del servicio de consulta de tipo  de publicidad => http://tlfepubapi61.tlfextranet.local:8062/odata/TiposPublicidad
                    TiposPublicidad = new List<string>()
                    {
                        "TANDA",
                        "PNT"
                    },
                    Productos = new List<MaterialProductoTestModel>()
                    {
                        new MaterialProductoTestModel()
                        {
                            //El codigo del producto se obtiene del servicio de epublicidad de creacion de Producto => http://tlfepubapi61.tlfextranet.local/api/Productos/
                            //Poner solo el codigo del producto
                            Producto = new ProductoTestModel()
                            {
                                Codigo = "d65947fc-ef3c-4fe6-9cc7-460b0c5e769b"
                            }

                        },
                        new MaterialProductoTestModel()
                        {
                            //El codigo del producto se obtiene del servicio de epublicidad de creacion de Producto => http://tlfepubapi61.tlfextranet.local/api/Productos/
                            //Poner solo el codigo del producto
                            Producto = new ProductoTestModel()
                            {
                                Codigo = "e68c99f1-290c-4b6f-bba6-3940d2830056"
                            }
                        }
                    }
                };

                string aux = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);
                /**/

                /* OPCION 2 USAR UN STRING PLANO */
                //string aux1 = "{\"Numero\":\"4321\",\"Tema\":\"Test Api 3\",\"IdClienteAgencia\":2514,\"IdClienteAnunciante\":59,\"DuracionReal\":10,\"DespuesHorarioProtMenor\":true,\"BebidasAlcoholicas\":true,\"VigenciaDesde\":\"2019-03-01T00:00:00\",\"VigenciaHasta\":\"2019-03-31T00:00:00\",\"IdPrograma\":0,\"CodigoTipoMaterial\":\"COM\",\"PathImage\":\"\",\"IdMaterialComercial\":0,\"Version\":0,\"Observaciones\":\"Test 3 Observaciones\",\"RegistroAFSCA\":\"\",\"Codigo\":null,\"Productos\":[{\"Producto\":{\"Nombre\":null,\"Descripcion\":null,\"IdFamilia\":0,\"Codigo\":\"d65947fc-ef3c-4fe6-9cc7-460b0c5e769b\",\"Version\":0,\"Anunciantes\":null}},{\"Producto\":{\"Nombre\":null,\"Descripcion\":null,\"IdFamilia\":0,\"Codigo\":\"e68c99f1-290c-4b6f-bba6-3940d2830056\",\"Version\":0,\"Anunciantes\":null}}],\"TiposPublicidad\":[\"TANDA\",\"PNT\"]}";

                HttpContent contentPost = new StringContent(aux, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(Route, contentPost).Result;

                MaterialTestModel materialResult = response.Content.ReadAsAsync<MaterialTestModel>().Result;

                Assert.IsTrue(materialResult != null);

                if (materialResult != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(materialResult.Codigo));
                }
            }
        }


        /// <summary>
        /// Obtiene un material a partir de su codigo
        /// <param name="codigo">Codigo (obligatorio), type => string (guid)</param>
        /// </summary>
        [TestMethod]
        public void GetMaterial()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string code = "77715d87-b2b0-47f7-bca7-c5fe51556c66";
                string queryUrl = string.Format("{0}/{1}", Route, code);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                MaterialTestModel material = response.Content.ReadAsAsync<MaterialTestModel>().Result;
                Assert.IsTrue(material != null);

                if (material != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(material.Codigo));
                }
            }
        }

        /// <summary>
        /// Obtener materiales
        /// </summary>
        /// <param name="idClienteAgencia">Requerido, type => long</param>
        /// <param name="idClienteAnunciante">Opcional, type => long</param>
        /// <param name="codigoProducto">Opcional, type => string (guid)</param>
        /// <param name="tema">Opcional, type => string</param>
        /// <param name="vigenciaDesde">Opcional, type => DateTime(yyyy,MM,dd)</param>
        /// <param name="vigenciaHasta">Opcionaltype => DateTime(yyyy,MM,dd)</param>
        /// <param name="activo">Por default es true</param>
        /// <returns></returns>
        [TestMethod]
        public void GetMateriales()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string filter = "getbyfilters";
                string baseUrl = string.Format("{0}/{1}/{2}", BaseAddress, Route, filter);
                var builder = new UriBuilder(baseUrl);

                /*Parametros de busqueda*/
                //Los id de IdClienteAgencia, IdClienteAnunciante y IdClienteFacturable se obtienen del servicio de consulta de clientes (segurizado solo se si tiene acceso a los que corresponda)
                //El id de los anunciantes y agencia sale del servicio de clientes => http://tlfepubapi61.tlfextranet.local:8062/odata/Clientes
                string idAgencia = 2594.ToString();
                string idAnunciante = 59.ToString();
                //El codigo del producto se obtiene del servicio de epublicidad de creacion de Producto => http://tlfepubapi61.tlfextranet.local/api/Productos/
                string codigoProducto = new Guid("B4733765-0E28-4C3F-96E2-F0D835B27869").ToString();
                string tema = "Test Tema";
                string vigenciaDesde = new DateTime(2018, 02, 01).ToString("yyyy-MM-dd");
                string vigenciaHasta = new DateTime(2018, 02, 28).ToString("yyyy-MM-dd");
                string activo = true.ToString();


                var query = HttpUtility.ParseQueryString(builder.Query);
                /*Obligatorio*/
                query.Add("idClienteAgencia", idAgencia);

                /*Opcionales*/
                if (!string.IsNullOrEmpty(idAnunciante))
                    query.Add("idClienteAnunciante", idAnunciante);
                if (!string.IsNullOrEmpty(codigoProducto))
                    query.Add("codigoProducto", codigoProducto);
                if (!string.IsNullOrEmpty(tema))
                    query.Add("tema", tema);
                if (!string.IsNullOrEmpty(vigenciaDesde))
                    query.Add("vigenciaDesde", vigenciaDesde);
                if (!string.IsNullOrEmpty(vigenciaHasta))
                    query.Add("vigenciaHasta", vigenciaHasta);
                if (!string.IsNullOrEmpty(activo))
                    query.Add("activo", "true");

                builder.Query = query.ToString();

                string url = builder.ToString();

                HttpResponseMessage response = client.GetAsync(url).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                List<MaterialTestModel> materiales = response.Content.ReadAsAsync<List<MaterialTestModel>>().Result;
            }
        }


        /// <summary>
        /// Edicion de un Material
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void PutMaterial()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(12, 0, 0);

                /* OPCION 1 USAR UN MODEL Y SERIALIZARLO CON NEWTONSOFT JSON */
                MaterialTestModel testModel = new MaterialTestModel()
                {

                    Tema = "Test Api Edit",
                    IdClienteAgencia = 2514,
                    IdClienteAnunciante = 59,
                    DuracionReal = 10,
                    DespuesHorarioProtMenor = true,
                    BebidasAlcoholicas = true,
                    //La version tiene que ser mayor a cero es recomendable que se le sume 1 a la version del material que se esta modificando
                    VigenciaDesde = new DateTime(2019, 03, 01),
                    VigenciaHasta = new DateTime(2019, 03, 31),
                    CodigoTipoMaterial = "COM",
                    Observaciones = "Test Edit Observaciones",
                    //Obligatorio
                    RegistroAFSCA = "",
                    //El codigo del tipo de publicidad se obtiene del servicio de consulta de tipo de publicidad => http://tlfepubapi61.tlfextranet.local:8062/odata/TiposPublicidad
                    TiposPublicidad = new List<string>()
                    {
                        "TANDA",
                        "PNT"
                    },
                    Productos = new List<MaterialProductoTestModel>()
                    {
                        new MaterialProductoTestModel()
                        {
                            //El codigo del producto se obtiene del servicio de epublicidad de creacion de Producto => http://tlfepubapi61.tlfextranet.local/api/Productos/
                            Producto = new ProductoTestModel()
                            {
                                Codigo = "d65947fc-ef3c-4fe6-9cc7-460b0c5e769b"
                            }

                        }
                    }
                };

                string aux = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);
                /**/

                /* OPCION 2 USAR UN STRING PLANO */
                //string aux1 = "{\"Numero\":\"4321 Edit\",\"Tema\":\"Test Api Edit\",\"IdClienteAgencia\":2514,\"IdClienteAnunciante\":59,\"DuracionReal\":10,\"DespuesHorarioProtMenor\":true,\"BebidasAlcoholicas\":true,\"VigenciaDesde\":\"2019-03-01T00:00:00\",\"VigenciaHasta\":\"2019-03-31T00:00:00\",\"IdPrograma\":0,\"CodigoTipoMaterial\":\"COM\",\"PathImage\":\"\",\"IdMaterialComercial\":0,\"Version\":1,\"Observaciones\":\"Test Edit Observaciones\",\"RegistroAFSCA\":\"\",\"Codigo\":null,\"Productos\":[{\"Producto\":{\"Nombre\":null,\"Descripcion\":null,\"IdFamilia\":0,\"Codigo\":\"d65947fc-ef3c-4fe6-9cc7-460b0c5e769b\",\"Version\":0,\"Anunciantes\":null}}],\"TiposPublicidad\":[\"TANDA\",\"PNT\"]}";

                HttpContent content = new StringContent(aux, Encoding.UTF8, "application/json");

                string code = "454661cd-768d-41f8-9a77-1a01ef523e00";
                string queryUrl = string.Format("{0}/{1}", Route, code);

                HttpResponseMessage response = client.PutAsync(queryUrl, content).Result;

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