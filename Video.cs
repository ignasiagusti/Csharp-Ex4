using System;
using System.Collections.Generic;

public class Video
{
    private string url, title, actualState;
    private List<String> tags;


    public Video()
    {     //Constructor buit d'un vídeo
        this.url = "";
        this.title = "";
        this.tags = new List<string>();
        this.actualState = "Stop";
    }

    public Video(string title, string url, List<String> tags) //Constructor amb atributs passats per paràmetre
    {
        this.title = title;
        this.url = url;
        this.tags = tags;
        this.actualState = "Stop";
    }

    //Getters dels atributs
    public String getTitle() => this.title;

    public String getUrl() => this.url;

    public List<String> getTags() => this.tags;

    public void addTag(List<String> tag) //Mètode per afegir etiquetes
    {
        foreach (String str in tag)
        {
            if (str.Equals(""))
            {
                ArgumentException tagEx = new ArgumentException("Tag buida!");
                throw tagEx;
            }
            else
            {
                tags.Add(str);
                Console.WriteLine($"Tag {str} afegida correctament");
            }
        }
    }

    public void removeTag(List<String> tag)
    {
        foreach (string str in tag)
        {
            if (tags.Contains(str))
            {
                tags.Remove(str);
                Console.WriteLine($"Tag {str} esborrada correctament");
            }
            else
            {
                ArgumentException tagEx = new ArgumentException("Aquesta tag ja existeix!");
                throw tagEx;
            }
        }
    }

    public void showState() //Retorna l'estat actual del video
    {
        Console.WriteLine("El vídeo es troba en el següent estat: " + this.actualState);
    }

    public string changeState(string state) //Modifica l'estat si aquest ha canviat respecte l'anterior i retorna un misssatge informant del canvi.
    {
        string msg;

        if (this.actualState.Equals(state))
        {
            msg = "El vídeo ja es troba en aquest estat";
        }
        else
        {
            this.actualState = state;
            msg = "Vídeo canviat a " + this.actualState;  //Comprovant de que s'ha canviat correctament.
        }

        return msg;
    }
}
