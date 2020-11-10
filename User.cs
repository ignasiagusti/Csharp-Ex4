using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

public class User
{
    private string username, name, surname, password, date;
    private List<Video> usrVideos;  //Cada usuario tiene su propia lista de videos.

    public User(string username, string name, string surname, string password, string date) //Constructor
    {
        this.username = username;
        this.name = name;
        this.surname = surname;
        this.password = password;
        this.date = date;
        usrVideos = new List<Video>();
    }

    //Getters
    public string getPwd() => this.password;

    public string getUsername() => this.username;

    public string getName() => this.name;

    public List<Video> getVideos()
    {
        bool isEmpty = !usrVideos.Any();
        if (isEmpty)
        {
            Console.WriteLine("\nNo tens cap vídeo!");
            return null;
        }
        else
        {
            return this.usrVideos;
        }
    }

    public void createVideo(Video video) //Método para crear un video
    {
        bool except = false; //variable booleana para comprovar si ha saltado alguna excepción y no añadir vid en ese caso
        foreach (Video vid in usrVideos)
        {
            if (video.getTitle().Equals(vid.getTitle())) //excepción del mismo título que catcheará el programa principal
            {
                except = true;
                ArgumentException titleEx = new ArgumentException("Títol del vídeo ja existent");
                throw titleEx;
            }
            else if (video.getUrl().Equals(vid.getUrl())) //excepción de misma url (podría pasar ya que la generación es aleatoria)
            {
                except = true;
                Exception urlEx = new Exception("Url ja existent");
                throw urlEx;
            }
        }

        if (except == false)
        {
            usrVideos.Add(video);
            Console.WriteLine("S'ha introduit el vídeo '" + video.getTitle() + "' correctament amb la url: " + video.getUrl());
        }
        except = false;
    }

    public void showVideos() //Simple método para mostrar los videos del usuario con un foreach
    {
        if (!usrVideos.Any())
        {
            Console.WriteLine("\nNo tens cap vídeo!");
        }
        else
        {
            Console.WriteLine($"\nL'usuari {this.username} té els següents vídeos: ");
            foreach (Video vid in usrVideos)
            {
                Console.WriteLine(vid.getTitle());
            }
        }
    }


}
