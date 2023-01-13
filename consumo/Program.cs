namespace consumo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string resposta;
            Console.WriteLine("Olá! Vamos calcular quanto é o consumo do seu veículo. \nVocê sabe informar quantos Km/L o seu veículo faz?");
            resposta = Console.ReadLine();
            if (resposta == "sim") 
            {
                float kml, tanque, media;
                Console.WriteLine("Informe quantos Km/L o seu veículo consome?");
                kml = float.Parse(Console.ReadLine());
                Console.WriteLine("Informe quantos litros de combustível o seu veículo tem?");
                tanque = float.Parse(Console.ReadLine());
                media = kml * tanque;
                Console.WriteLine($"Com o tanque totalmente abastecido o seu veículo percorre a distância de {media} km");
            }
            if (resposta == "nao")
            {
                float tanque, km, litros, kml;
                Console.WriteLine("Informe quantos litros de combustível o seu tanque tem?");
                tanque = float.Parse(Console.ReadLine());
                Console.WriteLine("Com o tanque cheio rode uma certa quilometragem e reabasteça o veículo\nInforme quantos km você dirigiu antes de reabastecer.");
                km = float.Parse(Console.ReadLine());
                Console.WriteLine("Quantos litros você reabasteceu?");
                litros = float.Parse(Console.ReadLine());
                kml = km / litros;
                Console.WriteLine($"O consumo médio de seu veículo é {kml} km, e a distância total percorrida com um tanque de combustível cheio é de {kml*tanque} km");
            }
            
        }
    }
}