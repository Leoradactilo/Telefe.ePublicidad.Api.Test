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
    public class OrdenApiTest
    {
        //Servidores de prueba 
        private const string BaseAddress = @"https://api.publife.desa.telefe.com.ar";
        private const string Route = @"/api/Ordenes";

        /*Mapeo de entidades resultantes*/
        public class OrdenCreateTestModel
        {
            public string Codigo { get; set; }
            public int Version { get; set; }
            public DateTime VigenciaDesde { get; set; }
            public DateTime VigenciaHasta { get; set; }
            public string NumeroOPA { get; set; }
            public string Pauta { get; set; }
            public string Usuario { get; set; }
            public string UsuarioEmail { get; set; }
            public string NumeroCompra { get; set; }
            public bool SinConvenio { get; set; }
            public bool? Tandem { get; set; }
            public bool? CabeceraTanda { get; set; }
            public bool? FinTanda { get; set; }
            public bool? AvisoIsla { get; set; }
            public bool Rotativos { get; set; }
            public int? Separacion { get; set; }
            public decimal InversionPrevista { get; set; }
            public string Descripcion { get; set; }
            public long IdClienteAgencia { get; set; }
            public long IdClienteAnunciante { get; set; }
            public long IdClienteFacturable { get; set; }
            public string CodigoProducto { get; set; }
            public string CodigoTipoPublicidad { get; set; }
            public string CodigoSenal { get; set; }
            public string CodigoCondicionVenta { get; set; }
            public long? IdTarget { get; set; }
            public long IdMoneda { get; set; }
            public long? IdConvenio { get; set; }

            //En caso de tener avisos rotativos configurarlos 
            public virtual IList<ConfiguracionRotativoTestModel> ConfiguracionesRotativos { get; set; }
            public virtual IList<ConfiguracionEmisionTestModel> DatosEmision { get; set; }
            public virtual IList<string> Materiales { get; set; }

            public OrdenCreateTestModel()
            {
                this.ConfiguracionesRotativos = new List<ConfiguracionRotativoTestModel>();
                this.DatosEmision = new List<ConfiguracionEmisionTestModel>();
                this.Materiales = new List<string>();
            }
        }
        public class OrdenReadTestModel
        {
            public string Codigo { get; set; }
            public bool Activo { get; set; }
            public int Version { get; set; }
            public DateTime VigenciaDesde { get; set; }
            public DateTime VigenciaHasta { get; set; }
            public string NumeroOPA { get; set; }
            public string Pauta { get; set; }
            public string Usuario { get; set; }
            public string UsuarioEmail { get; set; }
            public string NumeroCompra { get; set; }
            public string CodigoRAP { get; set; }
            public bool SinConvenio { get; set; }
            public DateTime? VigenciaDesdeRating { get; set; }
            public DateTime? VigenciaHastaRating { get; set; }
            public string ClienteCertificacion { get; set; }
            public bool? Tandem { get; set; }
            public bool? CabeceraTanda { get; set; }
            public bool? FinTanda { get; set; }
            public bool? AvisoIsla { get; set; }
            public bool Rotativos { get; set; }
            public int? Separacion { get; set; }
            public decimal InversionPrevista { get; set; }
            public string Estado { get; set; }
            public string Derivada { get; set; }
            public string Descripcion { get; set; }
            public long IdClienteAgencia { get; set; }
            public long IdClienteAnunciante { get; set; }
            public long IdClienteFacturable { get; set; }
            public string CodigoProducto { get; set; }
            public long? IdOrdenOriginal { get; set; }
            public long? IdOrdenRelacionada { get; set; }
            public long IdSectorImputacion { get; set; }
            public long? IdProyecto { get; set; }
            public string CodigoTipoPublicidad { get; set; }
            public string CodigoSenal { get; set; }
            public string CodigoCondicionVenta { get; set; }
            public long? IdTarget { get; set; }
            public long IdMoneda { get; set; }
            public long? IdConvenio { get; set; }
            public long? IdEjecutivoSinConvenio { get; set; }
            public string Producto { get; set; }
            public string Senal { get; set; }
            public string Agencia { get; set; }
            public string Anunciante { get; set; }

            public IList<ConfiguracionRotativoTestModel> ConfiguracionesRotativos { get; set; }
            public IList<ConfiguracionEmisionTestModel> DatosEmision { get; set; }
            public IList<OrdenMaterialTestModel> Materiales { get; set; }

            public OrdenReadTestModel()
            {
            }
        }
        public class ConfiguracionRotativoTestModel
        {
            public string Codigo { get; set; }
            public DateTime Fecha { get; set; }
            public string CodigoMaterial { get; set; }
            public int Cantidad { get; set; }
            public virtual TimeSpan HoraDesde { get; set; }
            public virtual TimeSpan HoraHasta { get; set; }
        }
        public class ConfiguracionEmisionTestModel
        {
            public bool Activo { get; set; }
            public long? IdTipoAviso { get; set; }
            public DateTime Fecha { get; set; }
            public long IdVariante { get; set; }
            public long? IdMaterial { get; set; }
            public string CodigoMaterial { get; set; }
            public int Duracion { get; set; }
            public int Cantidad { get; set; }
            public int TandemId { get; set; }
            public int? TandemOrden { get; set; }
            public string Codigo { get; set; }

        }
        public class OrdenMaterialTestModel
        {
            public MaterialTestModel Material { get; set; }
            public long? IdMaterialExterno { get; set; }
        }
        public class MaterialTestModel
        {
            public string Numero { get; set; }
            public string Tema { get; set; }
            public long? IdClienteAgencia { get; set; }
            public long? IdClienteAnunciante { get; set; }
            public int? DuracionReal { get; set; }
            public bool DespuesHorarioProtMenor { get; set; }
            public bool? BebidasAlcoholicas { get; set; }
            public DateTime? VigenciaDesde { get; set; }
            public DateTime? VigenciaHasta { get; set; }
            public long? IdPrograma { get; set; }
            public string CodigoTipoMaterial { get; set; }
            public string PathImage { get; set; }
            public long? IdMaterialComercial { get; set; }
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
            public string Codigo { get; set; }
            public int Version { get; set; }
            public virtual IList<long> Anunciantes { get; set; }
            public ProductoTestModel()
            { }
        }

        /// <summary>
        /// Dar de alta una orden
        /// </summary>
        /// <param name="codigoSenal">Codigo Senal</param>
        /// <param name="idClienteAgencia">Opcional</param>
        /// <param name="idClienteAnunciante">Opcional</param>
        /// <param name="idClienteFacturable">Opcional</param>
        /// <param name="codigoProducto">Opcional</param>
        /// <param name="vigenciaDesde">Opcional</param>
        /// <param name="vigenciaHasta">Opcional</param>
        /// <param name="activo">Por default es true</param>
        /// <returns></returns>
        [TestMethod]
        public void PostOrden()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(12, 0, 0);

                /* OPCION 1 USAR UN MODEL Y SERIALIZARLO CON NEWTONSOFT JSON */
                OrdenCreateTestModel testModel = new OrdenCreateTestModel()
                {
                    VigenciaDesde = new DateTime(2019, 03, 01),
                    VigenciaHasta = new DateTime(2019, 03, 30),
                    NumeroOPA = "Test  Orden OPA 123456",
                    //Opcional pauta
                    Pauta = "Test  Orden Pauta",
                    //Obligatorio
                    Usuario = "Tester",
                    //Obligatorio
                    UsuarioEmail = "Test1234@test.com",
                    //Opcional
                    NumeroCompra = "Test orden Numero de compra",
                    //Opcional
                    Tandem = false,
                    //Opcional
                    CabeceraTanda = true,
                    //Opcional
                    FinTanda = false ,
                    //Opcional
                    AvisoIsla = false,
                    //Opcional
                    Rotativos = false,
                    //Opcional
                    Separacion = null,
                    //Obligatorio
                    InversionPrevista = 1000000,
                    Descripcion = "test havas",
                    //Los id de IdClienteAgencia, IdClienteAnunciante y IdClienteFacturable se obtienen del servicio de consulta de clientes (segurizado solo se si tiene acceso a los que corresponda)
                    //El id de los anunciantes y agencia sale del servicio de clientes => http://tlfepubapi61.tlfextranet.local:8062/odata/Clientes
                    IdClienteAgencia = 26731,
                    IdClienteAnunciante = 32,
                    IdClienteFacturable = 32,
                    //El codigo del producto se obtiene del servicio de epublicidad de creacion de Producto => http://tlfepubapi61.tlfextranet.local/api/Productos/
                    CodigoProducto = "e68c99f1-290c-4b6f-bba6-3940d2830056",
                    //El codigo del tipo de publicidad se obtiene del servicio de consulta de tipo de publicidad => http://tlfepubapi61.tlfextranet.local:8062/odata/TiposPublicidad
                    CodigoTipoPublicidad = "TANDA",
                    //El codigo de la senal se obtiene del servicio de consulta de Señales => http://tlfepubapi61.tlfextranet.local:8062/odata/Senales
                    CodigoSenal = "TLF",
                    //El codigo del tipo de publicidad se obtiene del servicio de consulta de condicion de venta => http://tlfepubapi61.tlfextranet.local:8062/odata/CondicionesVenta
                    CodigoCondicionVenta = "TARIFATRADICIONAL",
                    //Solo obligatorio si compra por raiting, se obtiene del servicio de consulta de target => http://tlfepubapi61.tlfextranet.local:8062/odata/Targets
                    IdTarget = 0,
                    //Obligatorio, se obtiene del servicio de consulta de Monedas => http://tlfepubapi61.tlfextranet.local:8062/odata/Monedas
                    IdMoneda = 1,
                    //Solo pasar si no tiene convenio, deberia de tener un convenio
                    SinConvenio = true,
                    //Poner el Id del convenio informado por el ejecutivo comercial
                    IdConvenio = 0,
                    DatosEmision = new List<ConfiguracionEmisionTestModel>()
                    {
                        new ConfiguracionEmisionTestModel()
                        {
                          Activo = true,
                          Cantidad = 1,
                          //Obtener del tarifario, hace referencia al id de la variantes del programa
                          IdVariante = 2730,
                          Duracion = 23,
                          //El codigo del producto se obtiene del servicio de epublicidad de creacion de Material => http://tlfepubapi61.tlfextranet.local/api/Materiales/
                          CodigoMaterial = "454661cd-768d-41f8-9a77-1a01ef523e00",
                          //Fecha de emision de aviso
                          Fecha = new DateTime(2019,03,03)
                        },
                        new ConfiguracionEmisionTestModel()
                        {
                          Activo = true,
                          Cantidad = 1,
                          //Obtener del tarifario, hace referencia al id de la variantes del programa
                          IdVariante = 2732,
                          Duracion = 23,
                          //El codigo del producto se obtiene del servicio de epublicidad de creacion de Material => http://tlfepubapi61.tlfextranet.local/api/Materiales/
                          CodigoMaterial = "454661cd-768d-41f8-9a77-1a01ef523e00",
                          Fecha = new DateTime(2019,03,04)
                        }
                    },
                    Materiales = new List<string>()
                    {
                        //Listado de todos los codigos de material utilizados en la orden, debe coincidir con los que se utilizan en los DatosEmision(ConfiguracionEmisionTestModel)
                        "454661cd-768d-41f8-9a77-1a01ef523e00"
                    }

                };

                string aux = Newtonsoft.Json.JsonConvert.SerializeObject(testModel);
                /**/

                /* OPCION 2 USAR UN STRING PLANO */
                //string aux1 = "{\"Codigo\":null,\"Version\":0,\"VigenciaDesde\":\"2019-04-01T00:00:00\",\"VigenciaHasta\":\"2019-04-30T00:00:00\",\"NumeroOPA\":\"Test  Orden OPA 1234\",\"Pauta\":\"Test  Orden Pauta\",\"Usuario\":\"Tester\",\"UsuarioEmail\":\"Test1234@test.com\",\"NumeroCompra\":\"Test orden Numero de compra\",\"SinConvenio\":true,\"Tandem\":false,\"CabeceraTanda\":true,\"FinTanda\":false,\"AvisoIsla\":false,\"Rotativos\":false,\"Separacion\":null,\"InversionPrevista\":1000000.0,\"Descripcion\":\"test havas\",\"IdClienteAgencia\":26731,\"IdClienteAnunciante\":32,\"IdClienteFacturable\":32,\"CodigoProducto\":\"08b550d6-6d71-4d82-97d6-22a4a8f2efc6\",\"CodigoTipoPublicidad\":\"TANDA\",\"CodigoSenal\":\"TLF\",\"CodigoCondicionVenta\":\"TARIFATRADICIONAL\",\"IdTarget\":0,\"IdMoneda\":1,\"IdConvenio\":0,\"ConfiguracionesRotativos\":[],\"DatosEmision\":[{\"Activo\":true,\"IdTipoAviso\":null,\"Fecha\":\"2019-04-01T00:00:00\",\"IdVariante\":2730,\"IdMaterial\":null,\"CodigoMaterial\":\"1d40eb13-5a9c-4c0d-afcf-f8e9430ac187\",\"Duracion\":23,\"Cantidad\":1,\"TandemId\":0,\"TandemOrden\":null,\"Codigo\":null},{\"Activo\":true,\"IdTipoAviso\":null,\"Fecha\":\"2019-04-02T00:00:00\",\"IdVariante\":2732,\"IdMaterial\":null,\"CodigoMaterial\":\"1d40eb13-5a9c-4c0d-afcf-f8e9430ac187\",\"Duracion\":23,\"Cantidad\":1,\"TandemId\":0,\"TandemOrden\":null,\"Codigo\":null}],\"Materiales\":[\"1d40eb13-5a9c-4c0d-afcf-f8e9430ac187\"]}";

                HttpContent contentPost = new StringContent(aux, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(Route, contentPost).Result;

                OrdenCreateTestModel ordenResult = response.Content.ReadAsAsync<OrdenCreateTestModel>().Result;

                Assert.IsTrue(ordenResult != null);

                if (ordenResult != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(ordenResult.Codigo));
                }

                
            }

        }

        /// <summary>
        /// Obtiene una orden a partir de su codigo y version
        /// <param name="codigo">Codigo (obligatorio), type => string (guid). Version type=> int</param>
        /// </summary>
        [TestMethod]
        public void GetOrden()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string code = "d32fe56e-4041-4fbf-b6be-40994b71a2a9";
                int version = 1;
                string queryUrl = string.Format("{0}/{1}/versions/{2}", Route, code, version);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                OrdenReadTestModel orden = response.Content.ReadAsAsync<OrdenReadTestModel>().Result;
                Assert.IsTrue(orden != null);

                if (orden != null)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(orden.Codigo));
                }
            }
        }

        /// <summary>
        /// Obtener ordenes
        /// </summary>
        /// <param name="codigoSenal">Codigo Senal, type => string</param>
        /// <param name="idClienteAgencia">Opcional, type => long</param>
        /// <param name="idClienteAnunciante">Opcional, type => long</param>
        /// <param name="codigoProducto">Opcional, type string (guid)</param>
        /// <param name="vigenciaDesde">Opcional, type => DateTime(yyyy,MM,dd)</param>
        /// <param name="vigenciaHasta">Opcional, type => DateTime(yyyy,MM,dd)</param>
        /// <param name="activo">Por default es true, Opcional, type => bool</param>
        /// <returns></returns>
        [TestMethod]
        public void GetOrdenByFilters()
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
                //El codigo de la senal se obtiene del servicio de consulta de Señales => http://tlfepubapi61.tlfextranet.local:8062/odata/Senales
                string codigoSenal = "TLF";
                //El codigo de los clientes se obtiene del servicio de consulta de Clientes => http://tlfepubapi61.tlfextranet.local:8062/odata/Clientes
                string idAgencia = 57.ToString();
                string idAnunciante = 24587.ToString();
                string codigoProducto = "";// new Guid("fd624f14-7cc7-4bce-a322-c7a6a6da63e0").ToString();
                string vigenciaDesde = new DateTime(2017, 10, 01).ToString("yyyy-MM-dd");
                string vigenciaHasta = new DateTime(2017, 10, 31).ToString("yyyy-MM-dd");
                string activo = true.ToString();


                var query = HttpUtility.ParseQueryString(builder.Query);
                /*Obligatorio*/

                query.Add("codigoSenal", codigoSenal);
                query.Add("idClienteAgencia", idAgencia);

                /*Opcionales*/
                if (!string.IsNullOrEmpty(idAnunciante))
                    query.Add("idClienteAnunciante", idAnunciante);
                if (!string.IsNullOrEmpty(codigoProducto))
                    query.Add("codigoProducto", codigoProducto);
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
                List<OrdenReadTestModel> orden = response.Content.ReadAsAsync<List<OrdenReadTestModel>>().Result;
                Assert.IsTrue(orden != null);
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