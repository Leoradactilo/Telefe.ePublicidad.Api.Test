using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestApisEpublicidad
{
    [TestClass]
    public class TarifasApiTest
    {
        //Servidores de prueba 
        private const string BaseAddress = @"https://api.publife.desa.telefe.com.ar";
        private const string Route = @"/api/Tarifas";        

        /*Mapeo de entidades resultantes*/
        public class TarifaTestModel
        {
            public string Senal { get; set; }
            public long IdSenal { get; set; }
            public string Moneda { get; set; }
            public long IdMoneda { get; set; }
            public string TipoPublicidad { get; set; }
            public long IdTipoPublicidad { get; set; }
            public long IdSubTipoTarifa { get; set; }
            public string TipoTarifario { get; set; }
            public int Version { get; set; }
            public DateTime VigenciaHasta { get; set; }
            public DateTime VigenciaDesde { get; set; }
            public string Nombre { get; set; }
            public string Numero { get; set; }
            public long Id { get; set; }
            public long IdTipoTarifario { get; set; }
            public string SubTipoTarifa { get; set; }
        }

        public class TarifaDetalleTestModel
        {
            public string Target { get; set; }
            public long IdTarget { get; set; }
            public string SectorHorario { get; set; }
            public long IdSectorHorario { get; set; }
            public string Variante { get; set; }
            public long IdVariante { get; set; }
            public string Programa { get; set; }
            public long IdPrograma { get; set; }
            public bool EsEnVivo { get; set; }
            public bool SinTarifa { get; set; }
            public bool EscalaHasta { get; set; }
            public bool EscalaDesde { get; set; }
            public long IdTipoAviso { get; set; }
            public TimeSpan? HoraFin { get; set; }
            public bool Domingo { get; set; }
            public bool Sabado { get; set; }
            public bool Viernes { get; set; }
            public bool Jueves { get; set; }
            public bool Miercoles { get; set; }
            public bool Martes { get; set; }
            public bool Lunes { get; set; }
            public decimal Precio { get; set; }
            public long IdPNTDetalle { get; set; }
            public long IdTarifaDetalle { get; set; }
            public long IdTarifa { get; set; }
            public long Id { get; set; }
            public TimeSpan? HoraInicio { get; set; }
            public string TipoAviso { get; set; }
        }

        [TestMethod]
        public void GetTarifasByFechas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string fechas = "fechas";
                string fechaDesde = new DateTime(2019, 01, 01).ToString("yyyyMMdd");
                string fechaHasta = new DateTime(2019, 01, 31).ToString("yyyyMMdd");
                string queryUrl = string.Format("{0}/{1}/{2}/{3}", Route, fechas,fechaDesde,fechaHasta);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

            }
        }

        [TestMethod]
        public void GetTarifasBySenalFechas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //El id de la senal se obtiene del servicio de consulta de Señales => http://tlfepubapi61.tlfextranet.local:8062/odata/Senales
                //Obligatorio
                string senal = "senal";
                long idsenal = 18;
                string fechas = "fechas";
                string fechaDesde = new DateTime(2019, 01, 01).ToString("yyyyMMdd");
                string fechaHasta = new DateTime(2019, 01, 31).ToString("yyyyMMdd");
                string queryUrl = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", Route, senal, idsenal, fechas, fechaDesde, fechaHasta);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

            }
        }

        [TestMethod]
        public void GetTarifasByTipoTarifarioFechas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //El id del tipo de tarifa se obtiene del servicio de consulta de tipos de tarifarios => http://tlfepubapi61.tlfextranet.local:8062/odata/TiposTarifarios
                //Obligatorio
                string tipoTarifa = "tipo";
                string idTipoTarifa = "1";
                string fechas = "fechas";
                string fechaDesde = new DateTime(2019, 01, 01).ToString("yyyyMMdd");
                string fechaHasta = new DateTime(2019, 01, 31).ToString("yyyyMMdd");
                string queryUrl = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", Route, tipoTarifa, idTipoTarifa, fechas, fechaDesde, fechaHasta);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

            }
        }

        [TestMethod]
        public void GetTarifasBySenalTipoTarifarioFechas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //El id de la senal se obtiene del servicio de consulta de Señales => http://tlfepubapi61.tlfextranet.local:8062/odata/Senales
                //El id del tipo de tarifa se obtiene del servicio de consulta de tipos de tarifarios => http://tlfepubapi61.tlfextranet.local:8062/odata/TiposTarifarios
                //Obligatorio
                string senal = "senal";
                long idsenal = 18;
                string tipoTarifa = "tipo";
                string idTipoTarifa = "1";
                string fechas = "fechas";
                string fechaDesde = new DateTime(2019, 01, 01).ToString("yyyyMMdd");
                string fechaHasta = new DateTime(2019, 01, 31).ToString("yyyyMMdd");
                string queryUrl = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", Route, senal, idsenal, tipoTarifa, idTipoTarifa,  fechas, fechaDesde, fechaHasta);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

            }
        }

        [TestMethod]
        public void GetTarifasByIds()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string idsTarifas = "46015,46176,46183";
                string queryUrl = string.Format("{0}/{1}", Route, idsTarifas);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

            }
        }

        [TestMethod]
        public void GetDetallesByIdsTarifas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", GetSamlToken());
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Obligatorio
                string idsTarifas = "45889";
                string detalles = "detalles";
                string queryUrl = string.Format("{0}/{1}/{2}", Route, idsTarifas, detalles);
                HttpResponseMessage response = client.GetAsync(queryUrl).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                List<TarifaDetalleTestModel> tarifas = response.Content.ReadAsAsync<List<TarifaDetalleTestModel>>().Result;
                Assert.IsTrue(tarifas != null && tarifas.Count > 0);

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
