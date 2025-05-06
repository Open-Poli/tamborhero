using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongIds
{

    public static int GetSongID()
    {
        string songName = NoteGenerator.Instance.GetSongName();




     	if(songName == "ratatata")
   	    {
   	        return 0;
   	    }
   	
   	    if(songName == "gigachad")
   	    {
   	        return 1;
   	    }
   	
   	
   	    if(songName == "medieburger")
   	    {
   	        return 2;
   	    }
   	
        if(songName == "puach")
   	    {
   	        return 3;
   	    }

        if(songName == "girl")
   	    {
   	        return 4;
   	    }

        if(songName == "hero")
   	    {
   	        return 5;
   	    }

        if(songName == "partofme")
   	    {
   	        return 6;
   	    }

        if(songName == "believer")
   	    {
   	        return 7;
   	    }

        if(songName == "rasputin")
   	    {
   	        return 8;
   	    }

        if(songName == "magica")
   	    {
   	        return 9;
   	    }
		if(songName == "HijoDeHernandez")
   	    {
   	        return 10;
   	    }
		if(songName == "takeOnMe")
   	    {
   	        return 11;
   	    }

        if(songName == "despacito")
        {
            return 12;
        }

		if(songName == "haiyorokonde")
        {
            return 13;
        }

		if(songName == "NothingElseMatters")
        {
            return 14;
        }

		if(songName == "toxicity")
        {
            return 15;
        }

		if(songName == "reptilia")
		{
			return 16;
		}
   	
   	    return 0;
    }

}

