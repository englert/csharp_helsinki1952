// helsinki.txt  http://www.infojegyzet.hu/erettsegi/informatika-ismeretek/kozep-prog-2017maj/
//- Az elért helyezés. Például: „3”
//− A helyezést elérő sportoló vagy csapat esetén sportolók száma. Például: „4”
//− A sportág neve. Például: „atletika”
//− A versenyszám neve. Például: „4x100m_valtofutas”

//1 1 atletika kalapacsvetes	
//1 1 uszas 400m_gyorsuszas

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class Olimpia
{
    public string eredetisor    { get; set; }
    public int helyezes         { get; set; }
    public int sportolok_szama  { get; set; }
    public string sportag       { get; set; }
    public string versenyszam   { get; set; }
    
    public Olimpia(string sor)
    {
        eredetisor      = sor;
        var s = sor.Split();

        helyezes        = int.Parse(s[0]);
        sportolok_szama = int.Parse(s[1]);
        sportag         = s[2];
        versenyszam     = s[3];     
    }
}

class Program 
{
    public static void Main (string[] args) 
    {
        //2. feladat: Adatok beolvasása, tárolása
        var lista = new List<Olimpia>();
        var fr    = new StreamReader("helsinki.txt");
        while(!fr.EndOfStream)
        {   
            var sor = fr.ReadLine().Trim();
            lista.Add( new Olimpia(sor) ); 
        }
        fr.Close();

        //3. feladat: Pontszerző helyezések száma:
        Console.WriteLine($"3. feladat:");
        Console.WriteLine($"Pontszerző helyezések száma: {lista.Count()}");
        
        //4. feladat: Statisztika a megszerzett érmek számáról.    
        Console.WriteLine($"4. feladat:");
        var stat =
        (
            from sor in lista
            where sor.helyezes == 1 || sor.helyezes == 2 || sor.helyezes == 3
            group sor by sor.helyezes
        );
        int osszesen = 0;
        var hely_erem = new Dictionary<int, string>(){ {1, "Arany"}, {2, "Ezüst"}, {3, "Bronz"} };
        foreach(var sor in stat)
        {
            osszesen += sor.Count();
            Console.WriteLine($"{hely_erem[sor.Key]}: {sor.Count()}");
        }
        Console.WriteLine($"Összesen: {osszesen}");

        //5. feladat: Olimpiai pontok száma:
        Console.WriteLine($"5. feladat:");
        var hely_pont = new Dictionary<int, int>()
        {  {1, 7}, {2, 5}, {3, 4}, {4, 3}, {5, 2},{6, 1} };
        var pontok_szama = 
            (
                from sor in lista
                let pont = hely_pont[ sor.helyezes ]
                select pont
            ).Sum();
        Console.WriteLine($"Olimpiai pontok száma:{pontok_szama}");

        //6. feladat: úszás vagy a torna?
        var uszas = 
            (
                from sor in lista
                where sor.helyezes == 1 || sor.helyezes == 2 || sor.helyezes == 3
                where sor.sportag == "uszas"
                select sor
            ).Count();

        var torna = 
            (
                from sor in lista
                where sor.helyezes == 1 || sor.helyezes == 2 || sor.helyezes == 3
                where sor.sportag == "torna"
                select sor
            ).Count();

        Console.WriteLine($"6. feladat:");
        if (uszas == torna) Console.WriteLine("Egyenlő volt az érmek száma");
        if (uszas > torna)  Console.WriteLine("Úszás sportágban szereztek több érmet");
        if (uszas < torna)  Console.WriteLine("Torna sportágban szereztek több érmet");

        //7. feladat:helsinki2.txt 
        var fw = new StreamWriter("helsinki2.txt");
        foreach(var sor in lista)
        {
            var s = sor.eredetisor.Replace("kajakkenu","kajak-kenu");
            fw.WriteLine(s);
        }
        fw.Close();
    
    //8. feladat:
    var maxi =
        (
            from sor in lista
            orderby sor.sportolok_szama
            select sor
        ).Last();
    
    Console.WriteLine($"8. feladat:");
    Console.WriteLine($"Helyezés: {maxi.helyezes}");
    Console.WriteLine($"Sportág: {maxi.sportag}");
    Console.WriteLine($"Versenyszám: {maxi.versenyszam}");
    Console.WriteLine($"Sportolók száma: {maxi.sportolok_szama}");



    }// ----------- Main vége -----------------------------
}     