namespace Zoologico.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaEspecies = "api/Especies";
            httpClient.BaseAddress = new Uri("https://localhost:7215/");

            //LECTURA DE DATOS
            var response = httpClient.GetAsync(rutaEspecies).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            var especies = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<List<Modelos.Especie>>>(json);


            //INSERCION DE DATOS
            var nuevaEspecie = new Modelos.Especie()
            {
                Codigo=0,
                    NombreComun = "xyz"
            };

            //Invocar al servicio web para insertar la nueva especie
            var especieJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevaEspecie);
            var content =  new StringContent(especieJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(rutaEspecies, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            //deserializar la respuesta
            var especieCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Especie>>(json);

            //ACTUALIZACION DE DATOS
            especieCreada.Data.NombreComun = "xyz Actualizado";
            especieJson = Newtonsoft.Json.JsonConvert.SerializeObject(especieCreada.Data);
            content = new StringContent(especieJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{rutaEspecies}/{especieCreada.Data.Codigo}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var especieActualizada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Especie>>(json);

            //ELIMINACION DE DATOS
            response = httpClient.DeleteAsync($"{rutaEspecies}/{especieCreada.Data.Codigo}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var especieEliminada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Especie>>(json);


            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
