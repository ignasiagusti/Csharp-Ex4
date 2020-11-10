using System;
using System.Collections.Generic;

namespace CsharpEx4
{
    class Program
    {
        enum PossibleStates { Play, Pause, Stop };  //Possibles estats que tindrà un vídeo (mirar case 3 del switch)
        static void Main(string[] args)
        {
            List<User> userList = new List<User>(); //Llista on estan guardats els usuaris

            //Usuaris ja registrats guardats a la llista (es podria implementar una funció per registrar també)
            User usr1 = new User("ignasiagusti", "ignasi", "agusti", "abcdef", "12/01/2005");
            User usr2 = new User("pinin", "Juan", "Garcia", "aaabbb", "24/01/2005");
            User usr3 = new User("tonito", "Toni", "Marquez", "12345", "12/03/2005");
            userList.Add(usr1);
            userList.Add(usr2);
            userList.Add(usr3);


            int option = 100; //opció del menú escollida
            string introUser, introPwd; //usuari i constrasenya introduïts per consola
            User usrAuth = null; //variable usrAuth que apunta a l'usuari autenticat un cop comprovades les dades introduïdes

            while (true)
            {
                Console.Write("Benvingut! Per començar, introdueix el teu nom d'usuari i contrasenya. ");
                Console.WriteLine("Si vols sortir, escriu 'exit'");
                Console.Write("Usuari: ");
                introUser = Console.ReadLine();
                if (introUser.Equals("exit")) break;

                Console.Write("Contrasenya: ");
                introPwd = Console.ReadLine();

                foreach (User user in userList) //Búsqueda de les dades introduides amb la llista d'usuaris
                {
                    if (user.getPwd() == introPwd && user.getUsername() == introUser)
                    {
                        usrAuth = user;
                        break;
                    }
                }

                if (usrAuth != null) //Si ha trobat un usuari correcte, executem el menú principal
                {
                    Console.WriteLine($"\nBenvingut {usrAuth.getName()}!\n");
                    while (option != 0)
                    {
                        Console.WriteLine("\nMenú principal:");
                        Console.Write("Opció 1: Crea un nou vídeo\nOpció 2: Veure la llista de vídeos creats\nOpció 3: Modificar tags d'un vídeo" +
                            "\nOpció 4: Reproducció de vídeos\nOpció 0: Desconnectar\nSiusplau, indiqui quina opció vol realitzar:");
                        option = Int32.Parse(Console.ReadLine());
                        switch (option)
                        {
                            case 1: //Crear nou vídeo
                                Console.WriteLine("\nIntrodueix el títol del vídeo a crear");
                                string title = Console.ReadLine();
                                while (title == "") //Millor fer-ho així recusriu que amb una excepció? He utilitzat excepcions més abaix
                                {
                                    Console.WriteLine("Format incorrecte. El codi ha d'incloure algun caràcter.\nTítol del vídeo a crear:");
                                    title = Console.ReadLine();
                                }

                                string url = randomUrl(); //Mètode per generar random urls que es troba al final de l'arxiu

                                string introdTags;
                                do
                                {
                                    Console.WriteLine("\nIntrodueix les tags separades per comes (tag1,tag2,...)");
                                    introdTags = Console.ReadLine();
                                } while (introdTags == "");
                                   
                                string[] tags = introdTags.Split(',');
                                List<String> tagList = new List<String>(); 
                                foreach (String str in tags) //Creem una llista d'strings on cada element es una tag introduïda
                                {
                                    tagList.Add(str);
                                }


                                try //gestió de les excepcions a l'hora de crear un nou vídeo
                                {
                                    usrAuth.createVideo(new Video(title, url, tagList));
                                }
                                catch (ArgumentException aEx) 
                                {
                                    Console.WriteLine(aEx.Message);
                                    Console.WriteLine("Siusplau, introdueix un títol diferent:");
                                    title = Console.ReadLine();
                                    usrAuth.createVideo(new Video(title, url, tagList));
                                }
                                catch (Exception Ex)
                                {
                                    Console.WriteLine(Ex.Message);
                                    Console.WriteLine("Creant nova url...");
                                    url = randomUrl();
                                    usrAuth.createVideo(new Video(title, url, tagList));
                                }
                                break;


                            case 2: //Mostrar llista de videos
                                usrAuth.showVideos();
                                break;


                            case 3: //Modificació de les tags d'un vídeo
                                if (usrAuth.getVideos() == null) break;

                                usrAuth.showVideos(); //Mostrem els titols dels videos per a seleccionar-ne un
                                Console.WriteLine("\nIntrodueix el títol del vídeo per a modificar les seves etiquetes:");
                                title = Console.ReadLine();
                                List<Video> usrVids = usrAuth.getVideos(); //Guardem en una llista els vidos de l'usuari
                                List<String> vidTags = new List<String>(); //variable que guardarà les tags del video a modificar
                                
                                Video selectedVid = new Video();
                                for (int i = 0; i < usrVids.Count; i++) //De la llista de videos obtinguda, busquem el video el títol del qual
                                                                        //coincideix amb l'introduït
                                {
                                    if (usrVids[i].getTitle().Equals(title))
                                    {
                                        selectedVid = usrVids[i];
                                        vidTags = selectedVid.getTags();
                                    }
                                }

                                Console.WriteLine("El vídeo té les següents etiquetes:"); //Mostrem les etiquetes perquè l'usuari decideixi que vol fer
                                foreach (String str in vidTags)
                                {
                                    Console.Write($"{str}, ");
                                }

                                int optMenu = 0;
                                do //Menu de modificacio d'etiquetes
                                {
                                    Console.WriteLine("\n\nIndiqui si vol esborrar o afegir-ne.\nOpció 1: Afegir.\nOpció 2: Esborrar\nOpció 3: Sortir");
                                    optMenu = Int32.Parse(Console.ReadLine());
                                } while (optMenu != 1 && optMenu != 2 && optMenu != 3);

                                if (optMenu == 1) //Afegir
                                {
                                    do
                                    {
                                        Console.WriteLine("Siusplau, introdueix les tags a afegir separades per comes (tag1,tag2,...)");
                                        introdTags = Console.ReadLine();
                                    } while (introdTags == "");

                                    tags = introdTags.Split(',');
                                    tagList = new List<String>();
                                    foreach (String str in tags) //fem el mateix que feiem quan creàvem un vídeo
                                    {
                                        tagList.Add(str);
                                    }
                                    selectedVid.addTag(tagList);
                                }
                                else if (optMenu == 2) //Esborrar
                                {
                                    do
                                    {
                                        Console.WriteLine("Siusplau, introdueix les tags a esborrar separades per comes (tag1,tag2,...)");
                                        introdTags = Console.ReadLine();
                                    } while (introdTags == "");

                                    tags = introdTags.Split(',');
                                    tagList = new List<String>();
                                    foreach (String str in tags)
                                    {
                                        tagList.Add(str);
                                    }
                                    selectedVid.removeTag(tagList);
                                }
                                else
                                {
                                    break;
                                }
                                break;


                            case 4:
                                if (usrAuth.getVideos() == null) break;

                                //Mateix sistema que en la modificacio de tags, però ara per a saber l'estat de reproducció
                                usrAuth.showVideos();
                                Console.WriteLine("\nIntrodueix el títol del vídeo per a mostrar el seu menú de reproducció:");
                                title = Console.ReadLine();
                                usrVids = usrAuth.getVideos();

                                selectedVid = new Video();
                                for (int i = 0; i < usrVids.Count; i++)
                                {
                                    if (usrVids[i].getTitle().Equals(title))
                                    {
                                        selectedVid = usrVids[i]; ;
                                    }
                                }
                                selectedVid.showState(); //Mostrem l'estat del video seleccionat

                                optMenu = 0;
                                do  //Menú de reproducció
                                {
                                    Console.WriteLine("\nIntrodueix l'acció a realizar:\nOpció 1: Reproduir.\nOpció 2: Pausar\nOpció 3: Parar\nOpció 4: Sortir");
                                    optMenu = Int32.Parse(Console.ReadLine());
                                } while (optMenu != 1 && optMenu != 2 && optMenu != 3 && optMenu != 4);

                                if (optMenu == 1) //Reproduir
                                {
                                    Console.WriteLine(selectedVid.changeState(PossibleStates.Play.ToString())); 

                                }
                                else if (optMenu == 2) //Pausar
                                {
                                    Console.WriteLine();
                                    selectedVid.changeState(PossibleStates.Pause.ToString());
                                }
                                else if (optMenu == 3) //Parar
                                {
                                    Console.WriteLine(selectedVid.changeState(PossibleStates.Stop.ToString()));

                                }
                                else //Sortir
                                {
                                    break;
                                }
                                break;

                            case 0: //Cas desconnectar, l'usuari autenticat s'esborra, i ara demanarà per un nou usuari.
                                usrAuth = null;
                                Console.WriteLine("Desconnectant...\n");
                                break;

                            default:
                                Console.WriteLine("Opció introduïda incorrecta. Siusplau, escolliu un nombre vàlid");
                                break;
                        }
                    }
                    option = 100;
                }
                else
                {
                    Console.WriteLine("\nUsuari i/o contrasenya introduïts incorrectes.\n");
                }
            }
            Console.WriteLine("A reveure!!!!");
        }

        //Mètode per generar el codi de url random. Agafa una combinació aleatoria de 8 caràcters del string definit
        public static string randomUrl() 
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string stringChars = "";
            var random = new Random();

            for (int i = 0; i < 8; i++)
            {
                stringChars += chars[random.Next(chars.Length)];
            }
            String defString = "http://www.youtube.com/watch?v=" + stringChars;

            return defString;
        }
    }
}