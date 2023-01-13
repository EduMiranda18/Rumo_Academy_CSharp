namespace media.notas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int f;
            double media, soma =0.0;
            double[] nota = new double[5];
             
            Console.WriteLine("Esse programa irá mostrar as medias de notas dos alunos");

            for(f=0; f<5; f++) 
            {
                Console.WriteLine($"Informe a {f+1} º nota");
                nota[f]= double.Parse( Console.ReadLine() );
                soma += nota[f];
            }
            media = soma / 5;
            Console.Clear();
            Console.WriteLine($"A média da turma é:{media:F2}");


        }
    }
}