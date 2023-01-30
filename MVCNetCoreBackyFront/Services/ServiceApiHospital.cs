using MVCNetCoreBackyFront.Models;
using System.Net.Http.Headers;

namespace MVCNetCoreBackyFront.Services
{
    public class ServiceApiHospital
    {
        private string UrlApi;

        //TENDREMOS UN OBJETO PARA INDICAR TIPO PETICION
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiHospital()
        {
            // EN LAS URL DE APIS NO SE INCLUYE LA PETICION (/api/hospitales) SOLO EL SITIO WEB
            this.UrlApi = "https://apihospitalesazure2023eve.azurewebsites.net";
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //TAMBIÉN PODEMOS TENER UN CONSTRUCTOR DODNDE INDICAMOS QUE EL CONTAINER PROGRAM NOS INDIQUE LA
        //URL
        public ServiceApiHospital(string urlapi)
        {
            this.UrlApi = urlapi;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        // LOS METODOS DE LECTURA DE CONSUMO DE API SON ASINCRONOS
        public async Task<List<Hospital>> GetHospitallesAync()
        {
            //UTILIZAMOS EL OBJETO HTTPCLIENT PARA LA PETICION
            using (HttpClient client = new HttpClient())
            {
                //LA PETICION
                string request = "/api/hospitales";
                //AÑADIMOS AL CLIENTE LA DIRECCIÓN BASE DE LA URL DEL API
                client.BaseAddress = new Uri(this.UrlApi);
                //LIMPIAMOS LL CABECERA DE OTRAS PETICIONES
                client.DefaultRequestHeaders.Clear();
                //AÑADIMOS AL HEADER EL TIPO DE DATO QUE VAMOS A CONSUMIR
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS PETICION ASYNC CON GET Y NOS DEVUELVE UNA RESPUESTA TIPO
                //HttpResponseMessage
                HttpResponseMessage response = await client.GetAsync(request);
                //COMPROBAMOS SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    //AQUI TENEMOS LOS DATOS EN JSON, CONVERTIR A CLASES MEDIANTE ReadAsAsync()
                    List<Hospital> hospitales = 
                        await response.Content.ReadAsAsync<List<Hospital>>();
                    return hospitales;

                }
                else
                {
                    //ALGO HA FALLADO
                    //SIEMPRE QUE DE FALLO DEVOLVEREMOS NULL DESDE EL SERVICIO
                    return null;
                }

            }
        }

    }
}
