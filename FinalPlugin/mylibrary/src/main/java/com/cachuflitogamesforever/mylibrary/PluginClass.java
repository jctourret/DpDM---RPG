package com.cachuflitogamesforever.mylibrary;

import android.util.Log;

public class PluginClass {
    private static final PluginClass ourInstance = new PluginClass();
    private static final String LOGTAG = "CachuflitoGames";

    public static PluginClass getInstance(){ return ourInstance;}

    private long startTime;

    private PluginClass(){
        Log.i(LOGTAG,"Created Plugin");
        startTime = System.currentTimeMillis();
    }

    public double GetElapsedTime(){
        return (System.currentTimeMillis()-startTime)/1000.0f;
    }
    public int AddFiveToInteger(int number)
    {
        number += 5;
        return number;
    }
}
