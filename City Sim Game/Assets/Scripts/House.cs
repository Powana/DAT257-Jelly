using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

//This class is for making houses so people can live in 
public class House
{
    string house;
    int space = 0;
    int peopleInPlace = 0;
    private ResourceManager resourceManager = new ResourceManager();


    public House(int space , int peopleInPlace)
    {
        this.space = space;
        this.peopleInPlace = peopleInPlace;
        resourceManager.AddSettlement();
    }
    

    public override string ToString()
	{
		return house + "{space:" + space + "}, {peopleInPlace:" + peopleInPlace + "}";
	}
   

}
